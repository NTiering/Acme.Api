using Acme.ChangeHandlers;
using Acme.Tests;
using Moq;
using System;
using System.Security.Principal;
using System.Threading.Tasks;
using Xunit;

namespace Acme.Data.Tests.ChangeTrackerTests
{
    public class ChangeTrackerTests
    {
        private IChangeTracker _changeTracker;
        private MockIChangeEventHandler mockIChangeEventHandlerOne;
        private MockIChangeEventHandler mockIChangeEventHandlerTwo;

        public ChangeTrackerTests()
        {
            mockIChangeEventHandlerOne = new MockIChangeEventHandler();
            mockIChangeEventHandlerTwo = new MockIChangeEventHandler();
            _changeTracker = new ChangeTracker(new[] { mockIChangeEventHandlerOne, mockIChangeEventHandlerTwo });
        }

        [Fact]
        public void ImplementsIChangeTracker()
        {
            _changeTracker.ShouldNotBeNull();
        }

        [Fact]
        public async Task IsCallHandlersThatReturnTrueForAdd()
        {
            mockIChangeEventHandlerOne.CanHandleReturn = true;
            mockIChangeEventHandlerTwo.CanHandleReturn = false;

            await _changeTracker.BroadcastAdd(new object(), new Mock<IIdentity>().Object);

            mockIChangeEventHandlerOne.OnChangeCalled.ShouldBeTrue();
            mockIChangeEventHandlerTwo.OnChangeCalled.ShouldBeFalse();
        }

        [Fact]
        public async Task IsCallHandlersThatReturnTrueForDelete()
        {
            mockIChangeEventHandlerOne.CanHandleReturn = true;
            mockIChangeEventHandlerTwo.CanHandleReturn = false;

            var identity = new Mock<IIdentity>().Object;

            await _changeTracker.BroadcastDelete(new object(), identity);

            mockIChangeEventHandlerOne.OnChangeCalled.ShouldBeTrue();
            mockIChangeEventHandlerTwo.OnChangeCalled.ShouldBeFalse();
        }

        [Fact]
        public async Task IsCallHandlersThatReturnTrueForModify()
        {
            mockIChangeEventHandlerOne.CanHandleReturn = true;
            mockIChangeEventHandlerTwo.CanHandleReturn = false;

            await _changeTracker.BroadcastModify(new object(), new object(), new Mock<IIdentity>().Object);

            mockIChangeEventHandlerOne.OnChangeCalled.ShouldBeTrue();
            mockIChangeEventHandlerTwo.OnChangeCalled.ShouldBeFalse();
        }

        [Fact]
        public async Task PassesCorrectParamsForAdd()
        {
            mockIChangeEventHandlerOne.CanHandleReturn = true;

            var newState = new object();
            var identity = new Mock<IIdentity>().Object;

            await _changeTracker.BroadcastAdd(newState, identity);

            mockIChangeEventHandlerOne.OnChangeCalled.ShouldBeTrue();
            mockIChangeEventHandlerOne.OnChangeOldStateInput.ShouldBeNull();
            mockIChangeEventHandlerOne.OnChangeNewStateInput.ShouldBeEqualTo(newState);
            mockIChangeEventHandlerOne.OnChangeIIdentityInput.ShouldBeEqualTo(identity);
        }

        [Fact]
        public async Task PassesCorrectParamsForDelete()
        {
            mockIChangeEventHandlerOne.CanHandleReturn = true;

            var oldState = new object();
            var identity = new Mock<IIdentity>().Object;

            await _changeTracker.BroadcastDelete(oldState, identity);

            mockIChangeEventHandlerOne.OnChangeCalled.ShouldBeTrue();
            mockIChangeEventHandlerOne.OnChangeOldStateInput.ShouldBeEqualTo(oldState);
            mockIChangeEventHandlerOne.OnChangeNewStateInput.ShouldBeNull();
            mockIChangeEventHandlerOne.OnChangeIIdentityInput.ShouldBeEqualTo(identity);
        }

        [Fact]
        public async Task PassesCorrectParamsForModify()
        {
            mockIChangeEventHandlerOne.CanHandleReturn = true;

            var newState = new object();
            var oldState = new object();
            var identity = new Mock<IIdentity>().Object;

            await _changeTracker.BroadcastModify(oldState, newState, identity);

            mockIChangeEventHandlerOne.OnChangeCalled.ShouldBeTrue();
            mockIChangeEventHandlerOne.OnChangeOldStateInput.ShouldBeEqualTo(oldState);
            mockIChangeEventHandlerOne.OnChangeNewStateInput.ShouldBeEqualTo(newState);
            mockIChangeEventHandlerOne.OnChangeIIdentityInput.ShouldBeEqualTo(identity);
        }
    }

    public class MockIChangeEventHandler : IChangeEventHandler
    {
        public Type CanHandleInput { get; private set; }
        public bool CanHandleReturn { get; set; }
        public bool OnChangeCalled { get; private set; }
        public IIdentity OnChangeIIdentityInput { get; private set; }
        public object OnChangeNewStateInput { get; private set; }
        public object OnChangeOldStateInput { get; private set; }

        public bool CanHandle(Type type)
        {
            CanHandleInput = type;
            return CanHandleReturn;
        }

        public void OnChange(object oldState, object newState, IIdentity identity)
        {
            OnChangeCalled = true;
            OnChangeOldStateInput = oldState;
            OnChangeNewStateInput = newState;
            OnChangeIIdentityInput = identity;
        }
    }
}