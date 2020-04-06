using Acme.Tests;
using Moq;
using System;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Xunit;

namespace Acme.Muators.Tests
{
    public class DataMutatorTests
    {
        [Fact]
        public void ItCanBeConstructed()
        {
            new DataMutator(Enumerable.Empty<IDataMutatorHandler>()).ShouldNotBeNull();
        }

        [Fact]
        public async Task ItCallsAddHandlers()
        {
            var mockhandler = new Mock<IDataMutatorHandler>();
            var mockIdentity = new Mock<IIdentity>();
            var newState = new object();
            var oldState = new object();

            mockhandler.Setup(x => x.CanHandle(It.IsAny<IDataMutatorContext>())).Returns(true);

            await new DataMutator(new[] { mockhandler.Object }).BeforeAdd(newState, mockIdentity.Object);

            mockhandler.Verify(x => x.BeforeAdd(newState, It.IsAny<IDataMutatorContext>()), Times.Once);
            mockhandler.Verify(x => x.BeforeModify(oldState, newState, It.IsAny<IDataMutatorContext>()), Times.Never);
            mockhandler.Verify(x => x.BeforeRemove(oldState, It.IsAny<IDataMutatorContext>()), Times.Never);
        }

        [Fact]
        public async Task ItCallsModifyHandlers()
        {
            var mockhandler = new Mock<IDataMutatorHandler>();
            var mockIdentity = new Mock<IIdentity>();
            var newState = new object();
            var oldState = new object();

            mockhandler.Setup(x => x.CanHandle(It.IsAny<IDataMutatorContext>())).Returns(true);

            await new DataMutator(new[] { mockhandler.Object }).BeforeModify(oldState, newState, mockIdentity.Object);

            mockhandler.Verify(x => x.BeforeAdd(newState, It.IsAny<IDataMutatorContext>()), Times.Never);
            mockhandler.Verify(x => x.BeforeModify(oldState, newState, It.IsAny<IDataMutatorContext>()), Times.Once);
            mockhandler.Verify(x => x.BeforeRemove(oldState, It.IsAny<IDataMutatorContext>()), Times.Never);
        }

        [Fact]
        public async Task ItCallsRemoveHandlers()
        {
            var mockhandler = new Mock<IDataMutatorHandler>();
            var mockIdentity = new Mock<IIdentity>();
            var newState = new object();
            var oldState = new object();

            mockhandler.Setup(x => x.CanHandle(It.IsAny<IDataMutatorContext>())).Returns(true);

            await new DataMutator(new[] { mockhandler.Object }).BeforeRemove(oldState, mockIdentity.Object);

            mockhandler.Verify(x => x.BeforeAdd(newState, It.IsAny<IDataMutatorContext>()), Times.Never);
            mockhandler.Verify(x => x.BeforeModify(oldState, newState, It.IsAny<IDataMutatorContext>()), Times.Never);
            mockhandler.Verify(x => x.BeforeRemove(oldState, It.IsAny<IDataMutatorContext>()), Times.Once);
        }


    }

}
