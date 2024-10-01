using Infrastructure.Persistance.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Reflection.PortableExecutable;
using Microsoft.Data.SqlClient;
using Domain.Models;
using Infrastructure.Persistance.Contexts;
using Tests.Models;
using System;
using Newtonsoft.Json;

namespace TestProject1
{
    [TestFixture]
    public class Defects_IsDeltaCorrect
    {
        private List<Defect> defectsList;
        private List<Measure> measures;


        [SetUp]
        public void SetUp()
        {
            //measures
            var context = new MeasuresContext();
            measures = context.Measures.AsEnumerable().ToList();

            string defectsJson;
            using StreamReader reader = new StreamReader(Path.Combine(@"C:\Users\nlosa\source\repos\nicolathebag\aspnetcore-server-generated\src\IO.Swagger\temp", $"a20b8335-4c22-4efe-a6a4-9e8456a0913c.txt"));

            // Read the stream as a string.
            defectsJson = reader.ReadToEnd();

            defectsList = defectsJson != null
                        ? JsonConvert.DeserializeObject<List<Defect>>(defectsJson)
                        : default(List<Defect>);

        }

        [Test]
        public void AreAllDefectsDeltaCorrect()
        {
            foreach (var item in defectsList)
            {
                Measure measure = measures.Where(m => m.mm == item.Mm).ToList().FirstOrDefault();
                if (item.Param == 1 && item.Level.Equals("Low"))
                {
                    Assert.That(measure.P1 == 5 + item.Delta, Is.True, "P1 is threshold + delta");
                }
            }

            
        }
    }
}