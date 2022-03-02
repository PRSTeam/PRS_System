using cer2.Models;
using Microsoft.EntityFrameworkCore;
using PRS_System.Models.Data;

namespace cer2.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDataModel>().HasKey();
            
        }

        public DbSet<UserDataModel> userdata { get; set; }

        
    }
}
