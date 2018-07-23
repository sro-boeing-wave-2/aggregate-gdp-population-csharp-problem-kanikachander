using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace AggregateGDPPopulation
{
    public class Class1
    {
        public static string Aggregate() {
            string datafile = "";
            string mapfile = "";
            using (StreamReader streamReader = new StreamReader(@"../../../../AggregateGDPPopulation/data/datafile.csv"))
            {
                while (!streamReader.EndOfStream)
                {
                    datafile = streamReader.ReadToEnd();
                }
            }
            using (StreamReader streamReader = new StreamReader(@"../../../../AggregateGDPPopulation/data/country-continent-mapper.json"))
            {
                while (!streamReader.EndOfStream)
                {
                    mapfile = streamReader.ReadToEnd();
                }
            }
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
                            //GDP_2012 = result[continent].GDP_2012,
                            //Population_2012 = result[continent].Population_2012
                            GDP_2012 = float.Parse(rows[indexofGDP2012]),
                            POPULATION_2012 = float.Parse(rows[indexofPopulation2012])
                        };
                        result.Add(continent, gDPPopulation);
                    }
                }
                catch(Exception e) { }
            }
            string json = JsonConvert.SerializeObject(result, Formatting.Indented);
            File.WriteAllText(@"../../../../AggregateGDPPopulation/output/output.json", json);
            return json;
        }
    }
    public class GDPPopulation
    {
        public float GDP_2012 { get; set; }
        public float POPULATION_2012 { get; set; }
    }
}
