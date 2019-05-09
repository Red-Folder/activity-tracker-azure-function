using Moq;
using RedFolder.ActivityTracker.BeyondPod.Converters.Handlers;
using RedFolder.ActivityTracker.Models.BeyondPod;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RedFolder.ActivityTracker.BeyondPod.UnitTests.Converters.Handlers
{
    public class DefaultCategoryHandlerUnitTests
    {
        private readonly DefaultCategoryHandler _sut;
        private readonly Mock<IHandler> _handler;

        public DefaultCategoryHandlerUnitTests()
        {
            _handler = new Mock<IHandler>();

            _sut = new DefaultCategoryHandler();
            _sut.AddInner(_handler.Object);
        }

        [Fact]
        public void WillSetDefault_IfCategoryNull()
        {
            _handler
                .Setup(x => x.Convert(It.IsAny<PodCastTableEntity>()))
                .Returns(new Models.PodCast { Category = null });

            var result = _sut.Convert(new PodCastTableEntity());

            Assert.Equal("Other", result.Category);
        }

        [Fact]
        public void WillSetDefault_IfCategoryEmpty()
        {
            _handler
                .Setup(x => x.Convert(It.IsAny<PodCastTableEntity>()))
                .Returns(new Models.PodCast { Category = "" });

            var result = _sut.Convert(new PodCastTableEntity());

            Assert.Equal("Other", result.Category);
        }

        [Fact]
        public void WillNotChange_IfCategorySet()
        {
            _handler
                .Setup(x => x.Convert(It.IsAny<PodCastTableEntity>()))
                .Returns(new Models.PodCast { Category = "Test" });

            var result = _sut.Convert(new PodCastTableEntity());

            Assert.Equal("Test", result.Category);
        }
    }
}
