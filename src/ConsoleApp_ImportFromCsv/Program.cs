using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection.Metadata;
using System;
using System.Linq;
using System.Formats.Asn1;
using System.Globalization;
using CsvHelper;
using System.Text;
using Newtonsoft.Json;

class Program
{
    static async Task Main(string[] args)
    {
        const string filePath = @"C:\Users\nlosa\source\repos\nicolathebag\aspnetcore-server-generated\ff3f3add-7b1d-4f08-ba27-7a9c24fbcd34.csv";
        const int batchSize = 1000;

        //SETUP
        await GetDataFromCsvAsync(filePath, batchSize);



    }
    

    private static async Task GetDataFromCsvAsync(string filePath, int batchSize)
    {
        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<Measure>().OrderBy(m => m.mm).Take(batchSize).ToList();

            var url = "http://localhost:5000/api/v3/store"; // Replace with your actual URL
            var json = JsonConvert.SerializeObject(records); // Your JSON data

            using var client = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode(); // Check if the request was successful                
                Console.WriteLine($"Response from server: {response.StatusCode}");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }


    }
}
    


internal class Measure
{
    public long mm { get; set; }
    public double p1 { get; set; }
    public double p2 { get; set; }
    public double p3 { get; set; }
    public double p4 { get; set; }
}
