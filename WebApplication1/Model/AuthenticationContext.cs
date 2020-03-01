
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;


namespace WebApplication1.Model
{
    public class AuthenticationContext: IdentityDbContext
    {
        public AuthenticationContext(DbContextOptions options) : base(options)
        {

        } 

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Deposita> Deposit { get; set; }
    }
}
