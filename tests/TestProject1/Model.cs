using Microsoft.EntityFrameworkCore;
using Domain.Models;
namespace Tests.Models
{
    public class MeasuresContext : DbContext
    {
        public DbSet<Measure> Measures { get; set; }
        public DbSet<Defect> Defects { get; set; }



        public string DbPath { get; }

        public MeasuresContext()
        {
            DbPath = "Server=localhost;Database=CLEAN_ARCH_TEST;Trusted_Connection=true;TrustServerCertificate=True";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer($"{DbPath}");
    }

   

}