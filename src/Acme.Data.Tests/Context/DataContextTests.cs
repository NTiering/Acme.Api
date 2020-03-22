using Acme.ChangeHandlers;
using Acme.Data.Context;
using Acme.Data.DataModels.Contracts;
using Acme.Muators;
using FluentAssert;
using Moq;
using System;
using System.Security.Principal;
using System.Threading.Tasks;
using Xunit;

namespace Acme.Data.Tests.Context
{
    public class DataContextTests
    {
        private readonly Mock<IChangeTracker> _changeTracker;
        private readonly Mock<ICrudDataTools> _crudDataTools;
        private readonly DataContext _dataContext;
        private readonly Mock<IDataMutator> _dataMutator;
        private readonly Mock<IIdentity> _identity;

        public DataContextTests()
        {
            _changeTracker = new Mock<IChangeTracker>();
            _dataMutator = new Mock<IDataMutator>();
            _crudDataTools = new Mock<ICrudDataTools>();
            _identity = new Mock<IIdentity>();
            _dataContext = new DataContext(_changeTracker.Object, _dataMutator.Object, _crudDataTools.Object);
        }

        [Fact]
        public async Task AddChangeIsBroadcast()
        {
            // arrange
            var model = new MockDataModel();
            _changeTracker.Setup(x => x.BroadcastAdd(model, _identity.Object));

            // act
            await _dataContext.Add(model, _identity.Object);

            // assert
            _changeTracker.Verify(x => x.BroadcastAdd(model, _identity.Object), Times.Once);
        }

        [Fact]
        public async Task AddDataMutatorIsCalled()
        {
            // arrange
            var model = new MockDataModel();
            _dataMutator.Setup(x => x.BeforeAdd(model, _identity.Object));

            // act
            await _dataContext.Add(model, _identity.Object);

            // assert
            _dataMutator.Verify(x => x.BeforeAdd(model, _identity.Object), Times.Once);
        }

        [Fact]
        public async Task AddRecordIsSaved()
        {
            // arrange
            var model = new MockDataModel();
            _crudDataTools.Setup(x => x.AddModel(model));

            // act
            await _dataContext.Add(model, _identity.Object);

            // assert
            _crudDataTools.Verify(x => x.AddModel(model), Times.Once);
        }

        [Fact]
        public async Task AddThrowExpectionOnNullIdentity()
        {
            Exception ex = null;
            try
            {
                await _dataContext.Add(new MockDataModel(), null);
            }
            catch (Exception e)
            {
                ex = e;
            }
            ex.ShouldNotBeNull();
        }

        [Fact]
        public async Task AddThrowExpectionOnNullModel()
        {
            Exception ex = null;
            try
            {
                await _dataContext.Add<MockDataModel>(null, _identity.Object);
            }
            catch (Exception e)
            {
                ex = e;
            }
            ex.ShouldNotBeNull();
        }

        [Fact]
        public async Task ModifyChangeIsBroadcast()
        {
            // arrange
            var oldState = new MockDataModel();
            var newState = new MockDataModel();

            _crudDataTools.Setup(x => x.GetModel<MockDataModel>(It.IsAny<Guid>())).Returns(Task.FromResult(oldState));
            ;
            _changeTracker.Setup(x => x.BroadcastModify(oldState, newState, _identity.Object));

            // act
            await _dataContext.Modify(newState, _identity.Object);

            // assert
            _changeTracker.Verify(x => x.BroadcastModify(oldState, newState, _identity.Object), Times.Once);
        }

        [Fact]
        public async Task ModifyDataMutatorIsCalled()
        {
            // arrange
            var oldState = new MockDataModel();
            var newState = new MockDataModel();

            _crudDataTools.Setup(x => x.GetModel<MockDataModel>(It.IsAny<Guid>())).Returns(Task.FromResult(oldState));
            _dataMutator.Setup(x => x.BeforeModify(oldState, newState, _identity.Object));

            // act
            await _dataContext.Modify(newState, _identity.Object);

            // assert
            _dataMutator.Verify(x => x.BeforeModify(oldState, newState, _identity.Object), Times.Once);
        }

        [Fact]
        public async Task ModifyRecordIsSaved()
        {
            // arrange
            var oldState = new MockDataModel();
            var newState = new MockDataModel();

            _crudDataTools.Setup(x => x.GetModel<MockDataModel>(It.IsAny<Guid>())).Returns(Task.FromResult(oldState));
            _crudDataTools.Setup(x => x.ModifyModel(oldState, newState));

            // act
            await _dataContext.Modify(newState, _identity.Object);

            // assert
            _crudDataTools.Verify(x => x.ModifyModel(oldState, newState), Times.Once);
        }

        [Fact]
        public async Task ModifyThrowExpectionOnNullIdentity()
        {
            Exception ex = null;
            try
            {
                await _dataContext.Modify(new MockDataModel(), null);
            }
            catch (Exception e)
            {
                ex = e;
            }
            ex.ShouldNotBeNull();
        }

        [Fact]
        public async Task ModifyThrowExpectionOnNullModel()
        {
            Exception ex = null;
            try
            {
                await _dataContext.Modify<MockDataModel>(null, _identity.Object);
            }
            catch (Exception e)
            {
                ex = e;
            }
            ex.ShouldNotBeNull();
        }

        [Fact]
        public async Task RemoveChangeIsBroadcast()
        {
            // arrange
            var oldState = new MockDataModel();
            var id = Guid.NewGuid();

            _crudDataTools.Setup(x => x.GetModel<MockDataModel>(id)).Returns(Task.FromResult(oldState));

            _changeTracker.Setup(x => x.BroadcastDelete(oldState, _identity.Object));

            // act
            await _dataContext.Remove<MockDataModel>(id, _identity.Object);

            // assert
            _changeTracker.Verify(x => x.BroadcastDelete(oldState, _identity.Object), Times.Once);
        }

        [Fact]
        public async Task RemoveDataMutatorIsCalled()
        {
            // arrange
            var oldState = new MockDataModel();
            var id = Guid.NewGuid();
            _crudDataTools.Setup(x => x.GetModel<MockDataModel>(id)).Returns(Task.FromResult(oldState));
            _dataMutator.Setup(x => x.BeforeRemove(oldState, _identity.Object));

            // act
            await _dataContext.Remove<MockDataModel>(id, _identity.Object);

            // assert
            _dataMutator.Verify(x => x.BeforeRemove(oldState, _identity.Object), Times.Once);
        }

        [Fact]
        public async Task RemoveRecordIsSaved()
        {
            // arrange
            var oldState = new MockDataModel();
            var id = Guid.NewGuid();
            _crudDataTools.Setup(x => x.GetModel<MockDataModel>(id)).Returns(Task.FromResult(oldState));
            _crudDataTools.Setup(x => x.RemoveModel<MockDataModel>(id));

            // act
            await _dataContext.Remove<MockDataModel>(id, _identity.Object);

            // assert
            _crudDataTools.Verify(x => x.GetModel<MockDataModel>(id), Times.Once);
        }

        [Fact]
        public async Task RemoveThrowExpectionOnNullIdentity()
        {
            Exception ex = null;
            try
            {
                await _dataContext.Remove<MockDataModel>(Guid.Empty, null);
            }
            catch (Exception e)
            {
                ex = e;
            }
            ex.ShouldNotBeNull();
        }

        public class MockDataModel : IDataModel
        {
            public DateTime? DeletedOn { get; set; }
            public Guid Id { get; set; }
        }
    }
}