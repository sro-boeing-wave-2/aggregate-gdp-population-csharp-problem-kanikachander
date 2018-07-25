using System;
using Xunit;
using AggregateGDPPopulation;
using System.IO;
using System.Threading.Tasks;

namespace AggregateGDPPopulation.Tests
{
    public class AggregateGDPPopulationTest
    {
        [Fact]
        public async void VerifyExpectedAndActualOutput()
        {
            Task<string> expectedOutputTask = AggregateGDPPopulation.ReadFileAsync(@"../../../../AggregateGDPPopulation.Tests/expected-output.json");
            Task<string> actualOutputTask = AggregateGDPPopulation.ReadFileAsync(@"../../../../AggregateGDPPopulation/output/output.json");
            string expectedOutput = await expectedOutputTask;
            string actualOutput = await expectedOutputTask;
            Assert.Equal(expectedOutput, actualOutput);
        }
    }
}
