using Microsoft.Extensions.Logging;
using Moq;
using RedFolder.ActivityTracker.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RedFolder.ActivityTracker.IntegrationTests
{
    public class PluralsightProxyUnitTests
    {
        [Fact(Skip = "Provide the PluralsightID to run")]
        public async Task GetsData()
        {
            var logger = new Mock<ILogger>();
            var sut = new PluralsightProxy(logger.Object);

            await sut.PopulateAsync("POPULATE ME", DateTime.Now.AddDays(-7), DateTime.Now);

            var activity = sut.GetPluralsightActivity();

            Assert.NotEmpty(activity.Courses);
            var first = activity.Courses.First();

            Assert.NotEmpty(first.CourseId);
            Assert.NotEmpty(first.CourseImageUrl);
            Assert.NotEmpty(first.Description);
            Assert.Equal(100, first.PercentageComplete);
            Assert.NotEmpty(first.ShortDescription);
            Assert.NotEmpty(first.Title);
            Assert.NotEmpty(first.Url);
        }
    }
}
