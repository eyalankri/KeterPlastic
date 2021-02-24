using System.Linq;
using System.Net;
using Api.Dtos;
using Api.Enums;
using Api.Models;
using Api.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
 
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly ApplicationDbContext _db;

        public AccountController(ApplicationDbContext db)
        {
            _db = db;
        }

         
    }
}