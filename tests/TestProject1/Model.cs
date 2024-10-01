using Microsoft.EntityFrameworkCore;
using Domain.Models;
namespace Tests.Models
{
    public class MeasuresContext : DbContext
    {       

        public DbSet<Measure> Measures { get; set; }
        public DbSet<Defect> Defects { get; set; }



        public string DbPath { get; }

        public MeasuresContext(string connString)
        {
            DbPath = connString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer($"{DbPath}");
    }

   

}