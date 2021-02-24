using System;
using Api.Enums;
using Api.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Api.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

      





    }
}
