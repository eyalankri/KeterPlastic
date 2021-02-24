using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using Api.Dtos;
using Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {


        private readonly IMapper _mapper;
        private string _connStr;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ProductController(IConfiguration configuration, IMapper mapper, IHostingEnvironment hostingEnvironment)
        {
            _connStr = configuration.GetConnectionString("DefaultConnection");
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }




        [HttpPost]
        [AllowAnonymous]
        [EnableCors("MyPolicy")]
        [Route("Insert")]
        public IActionResult Insert([FromForm] ProductDto productDto)
        {
            if (!ModelState.IsValid) return BadRequest();


            var product = _mapper.Map<Product>(productDto);

            var fileExt = Path.GetExtension(productDto.ProductPhoto.FileName);
            var newFileName = $"{Guid.NewGuid()}{fileExt}";
            var root = _hostingEnvironment.ContentRootPath;
            var dir = Path.Combine($"{root}\\Uploads");
            var fullPath = Path.Combine(dir, newFileName);

            try
            {
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    productDto.ProductPhoto.CopyTo(fileStream);
                }
            }
            catch (Exception)
            {
                return BadRequest("הקובץ אינו נשמר!");
            }


            using (var conn = new SqlConnection(_connStr))
            {
                using (var cmd = new SqlCommand("Product_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                    cmd.Parameters.AddWithValue("@ProductDescription", product.ProductDescription);
                    cmd.Parameters.AddWithValue("@ProductPriceUsd", product.ProductPriceUsd);
                    cmd.Parameters.AddWithValue("@ProductFileName", newFileName);
                    conn.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    conn.Close();
                }
            }
            return Ok();
        }



        [HttpPost]
        [AllowAnonymous]
        [EnableCors("MyPolicy")]
        [Route("Update")]
        public IActionResult Update([FromForm] ProductDto productDto)
        {
            if (!ModelState.IsValid) return BadRequest();


            var product = _mapper.Map<Product>(productDto);

            var newFileName = "";
            if (productDto.ProductPhoto != null)
            {
                var fileExt = Path.GetExtension(productDto.ProductPhoto.FileName);
                newFileName = $"{Guid.NewGuid()}{fileExt}";
                var root = _hostingEnvironment.ContentRootPath;
                var dir = Path.Combine($"{root}\\Uploads");
                var fullPath = Path.Combine(dir, newFileName);


                try
                {
                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        productDto.ProductPhoto.CopyTo(fileStream);
                    }
                }
                catch (Exception)
                {
                    return BadRequest("הקובץ אינו נשמר!");
                }
            }

            using (var conn = new SqlConnection(_connStr))
            {
                using (var cmd = new SqlCommand("Product_Update_ProductId", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
                    cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                    cmd.Parameters.AddWithValue("@ProductDescription", product.ProductDescription);
                    cmd.Parameters.AddWithValue("@ProductPriceUsd", product.ProductPriceUsd);

                    if (productDto.ProductPhoto != null)
                    {
                        cmd.Parameters.AddWithValue("@ProductFileName", newFileName);
                    }
                    conn.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }

                    conn.Close();
                }
            }
            return Ok();
        }



        [HttpGet]
        [Route("Delete")]
        [AllowAnonymous]
        [EnableCors("MyPolicy")]
        public IActionResult Delete(int productId)
        {

            using (var conn = new SqlConnection(_connStr))
            {
                using (var cmd = new SqlCommand("Product_Delete_ProductId", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ProductId", productId);

                    conn.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }

                    conn.Close();
                }
            }
            return Ok();

        }


        [HttpGet]
        [Route("List")]
        [AllowAnonymous]
        [EnableCors("MyPolicy")]
        public List<Product> List()
        {
            var list = new List<Product>();
            using (var conn = new SqlConnection(_connStr))
            {
                using (var cmd = new SqlCommand("Product_Select", conn))
                {
                    conn.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {


                            try
                            {
                                list.Add(new Product()
                                {
                                    ProductId = (int)reader["ProductId"],
                                    ProductName = (string)reader["ProductName"],
                                    ProductDescription = (string)reader["ProductDescription"],
                                    ProductPriceUsd = (decimal)reader["ProductPriceUsd"],
                                    ProductFileName = (string)reader["ProductFileName"],
                                    DateCreated = DateTime.Parse(reader["DateCreated"].ToString()),
                                    LastUpdated = DateTime.Parse(reader["LastUpdated"].ToString()),
                                });
                            }
                            catch (Exception ex)
                            {
                                var a = ex;

                                throw;
                            }


                        }
                    }
                }
            }
            return list;

        }

 


    }
}

