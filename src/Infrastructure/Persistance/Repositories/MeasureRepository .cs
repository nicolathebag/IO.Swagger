
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Models;
using Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Persistance.Repositories
{
    public class MeasureRepository : IMeasureRepository
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IMapper mapper;

        public MeasureRepository(ApplicationDBContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<int> StoreMeasures(List<Measure> measures)
        {   
            _dbContext.Measures.AddRange(measures);
            int res = 0;
            try
            {
                 res = await _dbContext.SaveChangesAsync();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            return res;
            

        }
        public List<Measure> GetMeasures(double initialMM,double endMM, int page = 1, int pageSize = 1000)
        {
            List<Measure> result = null;
            try
            {
                result = _dbContext.Measures.AsEnumerable()
                        .Where(m => m.mm > initialMM && m.mm < endMM)
                        .OrderBy(m => m.mm)
                        .ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return result;

        }

        public async Task<List<Defect>> FilterMeasuresAsync(int par, double initialMM, double endMM, double low, double medium, double high)
        {
            var measureList = _dbContext.Measures.AsEnumerable().Where(m => m.mm > initialMM && m.mm < endMM).ToList();
            
            // Get only input parameter data
            List<double> thresholds = new List<double>() { low,medium, high};            

            return await CalculateDefectsOnP(thresholds, measureList);

            throw new NotImplementedException();
        }



        private static async Task<List<Defect>> CalculateDefectsOnP(List<double> thresholds, List<Measure> list, double initialMM = 0, double finalMM = double.MaxValue)
        {
            List<Defect> AllDefects = new List<Defect>();
            
            List<Measure> Low = new List<Measure>();
            List<Measure> Medium = new List<Measure>();
            List<Measure> High = new List<Measure>();

            Low.AddRange(list
                        .AsEnumerable()
                        .Where(m => m.P1 > thresholds[0] && m.P1 < thresholds[1])
                        .OrderBy(m => m.mm)
                        .ToList());
            Low.AddRange(list
                        .AsEnumerable()
                        .Where(m => m.P2 > thresholds[0] && m.P2 < thresholds[1])
                        .OrderBy(m => m.mm)
                        .ToList());
            Low.AddRange(list
                        .AsEnumerable()
                        .Where(m => m.P3 > thresholds[0] && m.P3 < thresholds[1])
                        .OrderBy(m => m.mm)
                        .ToList());
            Low.AddRange(list
                        .AsEnumerable()
                        .Where(m => m.P4 > thresholds[0] && m.P4 < thresholds[1])
                        .OrderBy(m => m.mm)
                        .ToList());

            List<Defect> DefectsLow = new List<Defect>();

            foreach (var item in Low)
            {
                if (item.P1 > thresholds[0])
                {
                    GetDefects(thresholds[0], 1, DefectsLow, (double)item.P1, (long)item.mm, "Low");
                }
                if (item.P2 > thresholds[0])
                {
                    GetDefects(thresholds[0], 2, DefectsLow, (double)item.P2, (long)item.mm, "Low");
                }
                if (item.P3 > thresholds[0])
                {
                    GetDefects(thresholds[0], 3, DefectsLow, (double)item.P3, (long)item.mm, "Low");
                }
                if (item.P4 > thresholds[0])
                {
                    GetDefects(thresholds[0], 4, DefectsLow, (double)item.P4, (long)item.mm, "Low");
                }
            }
            CompressDefects(DefectsLow);
            AllDefects.AddRange(DefectsLow);


            Medium.AddRange(list
                     .AsEnumerable()
                     .Where(m => m.P1 > thresholds[1] && m.P1 < thresholds[2])
                     .OrderBy(m => m.mm)
                     .ToList());
            Medium.AddRange(list
                     .AsEnumerable()
                     .Where(m => m.P2 > thresholds[1] && m.P2 < thresholds[2])
                     .OrderBy(m => m.mm)
                     .ToList());
            Medium.AddRange(list
                     .AsEnumerable()
                     .Where(m => m.P3 > thresholds[1] && m.P3 < thresholds[2])
                     .OrderBy(m => m.mm)
                     .ToList());
            Medium.AddRange(list
                        .AsEnumerable()
                        .Where(m => m.P4 > thresholds[1] && m.P4 < thresholds[2])
                        .OrderBy(m => m.mm)
                        .ToList());
            List<Defect> DefectsMedium = new List<Defect>();

            foreach (var item in Medium)
            {
                if (item.P1 > thresholds[1])
                {
                    GetDefects(thresholds[1], 1, DefectsMedium, (double)item.P1, (long)item.mm, "Medium");
                }
                if (item.P2 > thresholds[1])
                {
                    GetDefects(thresholds[1], 2, DefectsMedium, (double)item.P2, (long)item.mm, "Medium");
                }
                if (item.P3 > thresholds[1])
                {
                    GetDefects(thresholds[1], 3, DefectsMedium, (double)item.P3, (long)item.mm, "Medium");
                }
                if (item.P4 > thresholds[1])
                {
                    GetDefects(thresholds[1], 4, DefectsMedium, (double)item.P4, (long)item.mm, "Medium");
                }
            }
            CompressDefects(DefectsMedium);
            AllDefects.AddRange(DefectsMedium);

            High.AddRange(list
                    .AsEnumerable()
                    .Where(m => m.P1 > thresholds[2])
                    .OrderBy(m => m.mm)
                    .ToList());
            High.AddRange(list
                    .AsEnumerable()
                    .Where(m => m.P2 > thresholds[2])
                    .OrderBy(m => m.mm)
                    .ToList());
            High.AddRange(list
                    .AsEnumerable()
                    .Where(m => m.P3 > thresholds[2])
                    .OrderBy(m => m.mm)
                    .ToList());
            High.AddRange(list
                    .AsEnumerable()
                    .Where(m => m.P4 > thresholds[2])
                    .OrderBy(m => m.mm)
                    .ToList());
            List<Defect> DefectsHigh = new List<Defect>();
            foreach (var item in High)
            {
                if (item.P1 > thresholds[2])
                {
                    GetDefects(thresholds[2], 1, DefectsHigh, (double)item.P1, (long)item.mm, "High");
                }
                if (item.P2 > thresholds[2])
                {
                    GetDefects(thresholds[2], 2, DefectsHigh, (double)item.P2, (long)item.mm, "High");
                }
                if (item.P3 > thresholds[2])
                {
                    GetDefects(thresholds[2], 3, DefectsHigh, (double)item.P3, (long)item.mm, "High");
                }
                if (item.P4 > thresholds[2])
                {
                    GetDefects(thresholds[2], 4, DefectsHigh, (double)item.P4, (long)item.mm, "High");
                }
            }
            CompressDefects(DefectsHigh);
            AllDefects.AddRange(DefectsHigh);
            
            return AllDefects;

        }

        private static void GetDefects(double threshold, int p, List<Defect> Defects, double parameter, long MeasureId, string Level)
        {
            Defect temp = new Defect();
            temp.Mm = MeasureId;
            temp.Level = Level;
            temp.Delta = parameter - threshold;
            temp.Param = p;
            Defects.Add(temp);
        }

        private static void CompressDefects(List<Defect> DefectsOnp)
        {
            List<Defect> defectsToRemove = new List<Defect>();

            int i = 0;

            int length;
            while (i < DefectsOnp.Count - 2)
            {
                length = 0;
                while (i < DefectsOnp.Count - 2 && DefectsOnp[i].Mm == DefectsOnp[i + 1].Mm - 1)
                {
                    length++;
                    defectsToRemove.Add(DefectsOnp[i + 1]);                    
                    i++;
                }
                DefectsOnp[i-length].Length = length;
                i++;
            }

            foreach (var item in defectsToRemove)
            {
                DefectsOnp.Remove(item);
            }
        }

    }
}