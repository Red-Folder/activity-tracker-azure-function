using RedFolder.ActivityTracker.BeyondPod.Converters;
using RedFolder.ActivityTracker.Models.BeyondPod;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RedFolder.ActivityTracker.BeyondPod.UnitTests.Converters
{
    public class PodCastConverterUnitTests
    {
        private readonly PodCastConverter _sut;

        public PodCastConverterUnitTests()
        {
            _sut = new PodCastConverter();
        }

        [Fact]
        public void TemporaryTestUntilWehaveDependacyInjection()
        {
            var source = new PodCastTableEntity
            {
                FeedName = "The Azure Podcast"
            };

            var result = _sut.Convert(source);

            Assert.NotNull(result);
            Assert.Equal("The Azure Podcast", result.FeedName);
            Assert.Equal("Azure & AWS", result.Category);
        }
    }
}
