using AspWebApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspWebApi.Database
{
    // db context dontroller 
    public class AppDbcontext : IdentityDbContext<ApplicationUser>

    {
        // db context
        public AppDbcontext(DbContextOptions<AppDbcontext> options) : base(options)
        {

        }

    }
}
