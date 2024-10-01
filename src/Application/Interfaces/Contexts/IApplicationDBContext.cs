using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces.Contexts
{
    public interface IApplicationDBContext
    {
        DbSet<Measure> Measures { get; set; }
        DbSet<Defect> Defects { get; set; }
        DbSet<Job> Jobs { get; set; }
        Task<int> SaveChangesAsync();
    }
}