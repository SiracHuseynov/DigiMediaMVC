using DigiMedia.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiMedia.Data.DAL
{
    public class AppDbContext : IdentityDbContext
    {
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public AppDbContext(DbContextOptions option) : base(option)
        {
            
        }
    }
}
