using System;
using Xunit;
using AggregateGDPPopulation;
using System.IO;
using System.Threading.Tasks;

namespace AggregateGDPPopulation.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async void Test1()
        {
            //StreamReader streamReader1 = new StreamReader(@"../../../../AggregateGDPPopulation.Tests/expected-output.json");
            //StreamReader streamReader2 = new StreamReader(@"../../../../AggregateGDPPopulation/output/output.json");
            //string actualOutput = streamReader1.ReadToEnd();
            //string expectedOutput = streamReader2.ReadToEnd();
            //Assert.Equal(expectedOutput, actualOutput);

            Task<string> expectedOutputTask = Class1.ReadFileAsync(@"../../../../AggregateGDPPopulation.Tests/expected-output.json");
            Task<string> actualOutputTask = Class1.ReadFileAsync(@"../../../../AggregateGDPPopulation/output/output.json");
            string expectedOutput = await expectedOutputTask;
            string actualOutput = await expectedOutputTask;
            Assert.Equal(expectedOutput, actualOutput);
        }
    }
}
