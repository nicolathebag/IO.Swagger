
using Application.Interfaces.Repositories;
using AutoMapper;
using AutoMapper.Features;
using Domain.Models;
using Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Persistance.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IMapper mapper;

        public JobRepository(ApplicationDBContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this.mapper = mapper;
        }
        

        public int StoreJobAsync(string guid, string state)
        {
            var result = _dbContext.Jobs.SingleOrDefault(b => b.Id == guid);
            if (result != null)
            {
                result.State = state;
                
            }
            else
            {
                _dbContext.Jobs.Add(new Job(guid, state));
                
            }
            return _dbContext.SaveChanges();

        }

        public string GetJobState(string guid)
        {
            return _dbContext.Jobs.Where(m => m.Id.Equals(guid, StringComparison.Ordinal)).ToList().FirstOrDefault().State;
            throw new NotImplementedException();
        }



        //private static async Task<List<Defect>> CalculateDefectsOnP(List<double> thresholds, int p, DbSet<Measure> list,string Level, double initialMM = 0, double finalMM = double.MaxValue)
        //{
        //    List<Defect> AllDefects = new List<Defect>();

        //    if(Level.Equals("Low"))
        //    {
        //        //prendo tutte le misure maggiori della soglia bassa
        //        var defectsLow = list
        //                        .AsEnumerable()
        //                        .Where(m => m.p1 > thresholds[0] && m.p1 < thresholds[1] && m.MeasureId > initialMM && m.MeasureId < finalMM)
        //                        .OrderBy(m => m.MeasureId)
        //                        .ToList();                

        //    }
        //    else if(Level.Equals("Medium"))
        //    {
        //        var defectsMedium = list
        //                        .AsEnumerable()
        //                        .Where(m => m.p1 > thresholds[1] && m.p1 < thresholds[2] && m.MeasureId > initialMM && m.MeasureId < finalMM)
        //                        .OrderBy(m => m.MeasureId)
        //                        .ToList();
        //        List<Defect> DefectsMedium = new List<Defect>();

        //        foreach (var item in defectsMedium)
        //        {
        //            GetDefects(thresholds[1], p, DefectsMedium, item.p1, item.MeasureId, "Medium");
        //        }
        //        CompressDefects(DefectsMedium);
        //        AllDefects.AddRange(DefectsMedium);


        //    }
        //    else
        //    {
        //        var defectsHigh = list
        //                    .AsEnumerable()
        //                    .Where(m => m.p1 > thresholds[2] && m.MeasureId > initialMM && m.MeasureId < finalMM)
        //                    .OrderBy(m => m.MeasureId)
        //                    .ToList();
        //        List<Defect> DefectsHigh = new List<Defect>();
        //        foreach (var item in defectsHigh)
        //        {
        //            GetDefects(thresholds[2], p, DefectsHigh, item.p1, item.MeasureId, "High");
        //        }
        //        CompressDefects(DefectsHigh);
        //        AllDefects.AddRange(DefectsHigh);

        //    }
        //    return AllDefects;

        //}

        //private static void GetDefects(double threshold, int p, List<Defect> Defects, double parameter, int MeasureId, string Level)
        //{
        //    Defect temp = new Defect();
        //    temp.MeasureId = MeasureId;
        //    temp.Level = Level;
        //    temp.Delta = parameter - threshold;
        //    temp.p = p;
        //    Defects.Add(temp);
        //}

        //private static void CompressDefects(List<Defect> DefectsOnp)
        //{
        //    List<Defect> defectsToRemove = new List<Defect>();

        //    int i = 0;

        //    int length;
        //    while (i < DefectsOnp.Count - 2)
        //    {
        //        length = 0;
        //        while (DefectsOnp[i].MeasureId == DefectsOnp[i + 1].MeasureId - 1)
        //        {
        //            length++;
        //            defectsToRemove.Add(DefectsOnp[i + 1]);
        //            DefectsOnp[i].DefectLength = length;
        //            i++;
        //        }
        //        i = i + length;
        //        i++;
        //    }

        //    foreach (var item in defectsToRemove)
        //    {
        //        DefectsOnp.Remove(item);
        //    }
        //}

    }
}