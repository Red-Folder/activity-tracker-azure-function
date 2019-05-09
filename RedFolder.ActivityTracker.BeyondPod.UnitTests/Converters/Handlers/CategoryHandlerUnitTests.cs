using Moq;
using RedFolder.ActivityTracker.BeyondPod.Converters.Handlers;
using RedFolder.ActivityTracker.Models.BeyondPod;
using System;
using Xunit;

namespace RedFolder.ActivityTracker.BeyondPod.UnitTests.Converters.Handlers
{
    public class CategoryHandlerUnitTests
    {
        private const string TEST_CATEGORY = "Test Category";
        private const string TEST_FEED_NAME = "Test Feed Name";

        private readonly CategoryHandler _sut;
        private readonly Mock<IHandler> _handler;

        public CategoryHandlerUnitTests()
        {
            _handler = new Mock<IHandler>();

            _sut = new CategoryHandler(TEST_CATEGORY, TEST_FEED_NAME);
            _sut.AddInner(_handler.Object);
        }

        [Fact]
        public void ThrowsException_IfCategoryNull()
        {
            CategoryHandler sut = null;
            Assert.Throws<NullReferenceException>(() => sut = new CategoryHandler(null, TEST_FEED_NAME));
        }

        [Fact]
        public void ThrowsException_IfCategoryEmpty()
        {
            CategoryHandler sut = null;
            Assert.Throws<NullReferenceException>(() => sut = new CategoryHandler("", TEST_FEED_NAME));
        }

        [Fact]
        public void ThrowsException_IfFeednameNull()
        {
            CategoryHandler sut = null;
            Assert.Throws<NullReferenceException>(() => sut = new CategoryHandler(TEST_CATEGORY, null));
        }

        [Fact]
        public void ThrowsException_IfFeenameEmpty()
        {
            CategoryHandler sut = null;
            Assert.Throws<NullReferenceException>(() => sut = new CategoryHandler(TEST_CATEGORY, ""));
        }

        [Fact]
        public void SetsCategory_IfMatchesFeedname()
        {
            _handler
                .Setup(x => x.Convert(It.IsAny<PodCastTableEntity>()))
                .Returns(new Models.PodCast { FeedName = TEST_FEED_NAME, Category = "Unknown" });

            var result = _sut.Convert(new PodCastTableEntity());

            Assert.Equal(TEST_CATEGORY, result.Category);
        }

        [Fact]
        public void DoesNotChangeCategory_IfDoesNotMatchesFeedname()
        {
            _handler
                .Setup(x => x.Convert(It.IsAny<PodCastTableEntity>()))
                .Returns(new Models.PodCast { FeedName = "No match", Category = "Unknown" });

            var result = _sut.Convert(new PodCastTableEntity());

            Assert.Equal("Unknown", result.Category);
        }
    }
}
