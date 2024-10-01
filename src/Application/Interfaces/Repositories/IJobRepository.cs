
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces.Repositories
{
    public interface IJobRepository
    {
        public int StoreJobAsync(string guid, string state);
        public string GetJobState(string guid);

    }
}