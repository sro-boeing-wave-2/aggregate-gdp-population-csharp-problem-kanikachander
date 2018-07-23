using System;
using Xunit;
using AggregateGDPPopulation;
using System.IO;

namespace AggregateGDPPopulation.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //string expectedOutput = Class1.Aggregate();
            StreamReader streamReader1 = new StreamReader(@"../../../../AggregateGDPPopulation.Tests/expected-output.json");
            StreamReader streamReader2 = new StreamReader(@"../../../../AggregateGDPPopulation/output/output.json");
            string actualOutput = streamReader1.ReadToEnd();
            string expectedOutput = streamReader2.ReadToEnd();
            Assert.Equal(expectedOutput, actualOutput);
        }
    }
}
