
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces.Repositories
{
    public interface IMeasureRepository
    {
        public Task<int> StoreMeasures(List<Measure> measures);
        public List<Measure> GetMeasures(double initialMM, double endMM, int page, int pageSize);
        public Task<List<Defect>> FilterMeasuresAsync(int par, double initialMM, double endMM, double low, double medium , double high);

    }
}