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
            var mockAdd = new Mock<IDataMutatorHandler>();
            var mockIdentity = new Mock<IIdentity>();
            var newState = new object();
            var oldState = new object();

            mockAdd.Setup(x => x.CanHandle(It.IsAny<IDataMutatorContext>())).Returns(true);

            await new DataMutator(new[] { mockAdd.Object }).BeforeAdd(newState, mockIdentity.Object);

            mockAdd.Verify(x => x.BeforeAdd(newState, It.IsAny<IDataMutatorContext>()), Times.Once);
            mockAdd.Verify(x => x.BeforeModify(oldState, newState, It.IsAny<IDataMutatorContext>()), Times.Never);
            mockAdd.Verify(x => x.BeforeRemove(oldState, It.IsAny<IDataMutatorContext>()), Times.Never);
        }

        [Fact]
        public async Task ItCallsModifyHandlers()
        {
            var mockAdd = new Mock<IDataMutatorHandler>();
            var mockIdentity = new Mock<IIdentity>();
            var newState = new object();
            var oldState = new object();

            mockAdd.Setup(x => x.CanHandle(It.IsAny<IDataMutatorContext>())).Returns(true);

            await new DataMutator(new[] { mockAdd.Object }).BeforeModify(oldState, newState, mockIdentity.Object);

            mockAdd.Verify(x => x.BeforeAdd(newState, It.IsAny<IDataMutatorContext>()), Times.Never);
            mockAdd.Verify(x => x.BeforeModify(oldState, newState, It.IsAny<IDataMutatorContext>()), Times.Once);
            mockAdd.Verify(x => x.BeforeRemove(oldState, It.IsAny<IDataMutatorContext>()), Times.Never);
        }

        [Fact]
        public async Task ItCallsRemoveHandlers()
        {
            var mockAdd = new Mock<IDataMutatorHandler>();
            var mockIdentity = new Mock<IIdentity>();
            var newState = new object();
            var oldState = new object();

            mockAdd.Setup(x => x.CanHandle(It.IsAny<IDataMutatorContext>())).Returns(true);

            await new DataMutator(new[] { mockAdd.Object }).BeforeRemove(oldState, mockIdentity.Object);

            mockAdd.Verify(x => x.BeforeAdd(newState, It.IsAny<IDataMutatorContext>()), Times.Never);
            mockAdd.Verify(x => x.BeforeModify(oldState, newState, It.IsAny<IDataMutatorContext>()), Times.Never);
            mockAdd.Verify(x => x.BeforeRemove(oldState, It.IsAny<IDataMutatorContext>()), Times.Once);
        }


    }

}
