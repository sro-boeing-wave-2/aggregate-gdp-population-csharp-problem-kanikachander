using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AggregateGDPPopulation
{
    public class AggregateGDPPopulation
    {
        public static async Task<string> ReadFileAsync(string filepath)
        {
            string data = "";
            using (StreamReader streamReader = new StreamReader(filepath))
            {
                data = await streamReader.ReadToEndAsync();
            }
            return data;
        }

        public static async void WriteFileAsync(string filepath, string data)
        {
            using (StreamWriter streamwriter = new StreamWriter(filepath))
            {
                await streamwriter.WriteLineAsync(data);
            }
        }
 
        public static async Task Aggregate() {
            string datafilePath = "../../../../AggregateGDPPopulation/data/datafile.csv";
            string mapfilePath = "../../../../AggregateGDPPopulation/data/country-continent-mapper.json";
            string outputPath = "../../../../AggregateGDPPopulation/output/output.json";
            Task<string> datatask = ReadFileAsync(datafilePath);
            Task<string> mapfiletask = ReadFileAsync(mapfilePath);
            string datafile = await datatask;
            string mapfile = await mapfiletask;

            Regex reg = new Regex("[*'\"_&#^@]");
            datafile = reg.Replace(datafile, string.Empty);
            var mapfileDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(mapfile);
            string[] datafileArr = datafile.Split('\n');
            string[] header = datafileArr[0].Split(',');
            int indexOfCountryName = Array.IndexOf(header, "Country Name");
            int indexofGDP2012 = Array.IndexOf(header, "GDP Billions (USD) 2012");
            int indexofPopulation2012 = Array.IndexOf(header, "Population (Millions) 2012");
            Dictionary<string, GDPPopulation> result = new Dictionary<string, GDPPopulation>();
            
            for(int i=1; i<(datafileArr.Length - 1); i++)
            {
                try
                {
                    string[] rows = datafileArr[i].Split(',');
                    var continent = mapfileDict[rows[indexOfCountryName]];
                    try
                    {
                        result[continent].GDP_2012 += float.Parse(rows[indexofGDP2012]);
                        result[continent].POPULATION_2012 += float.Parse(rows[indexofPopulation2012]);
                    }
                    catch
                    {
                        GDPPopulation gDPPopulation = new GDPPopulation()
                        {
                            GDP_2012 = float.Parse(rows[indexofGDP2012]),
                            POPULATION_2012 = float.Parse(rows[indexofPopulation2012])
                        };
                        result.Add(continent, gDPPopulation);
                    }
                }

                catch(Exception e) { }
            }
            string json = JsonConvert.SerializeObject(result, Formatting.Indented);
            WriteFileAsync(outputPath, json);
        }
    }
    public class GDPPopulation
    {
        public float GDP_2012 { get; set; }
        public float POPULATION_2012 { get; set; }
    }
}
