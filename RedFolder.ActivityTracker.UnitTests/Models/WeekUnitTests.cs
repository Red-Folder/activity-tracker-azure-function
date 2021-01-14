using RedFolder.ActivityTracker.Models;
using System;
using Xunit;

namespace RedFolder.ActivityTracker.UnitTests.Models
{
    public class WeekUnitTests
    {
        [Theory]
        [InlineData("2021-01-11", "2021-01-04")]
        public void FromDate_CorrectCalculatesStartDate(string seedDate, string expected)
        {
            var sut = Week.FromDate(DateTime.Parse(seedDate).AddDays(-7));
            Assert.Equal(DateTime.Parse(expected), sut.Start);
        }
    }
}
