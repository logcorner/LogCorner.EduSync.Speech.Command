using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using LogCorner.EduSync.Speech.Command.SharedKernel.Events;
using LogCorner.EduSync.Speech.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace LogCorner.EduSync.Speech.Infrastructure.UnitTests.Specs
{
    public class EventStoreRepositorySpecs
    {
        [Fact]
        public void InstanciatingEventStoreRepositoryWithNullContexShouldRaiseArgumentNullException()
        {
            //Arrange
            //Act
            //Assert
            Assert.Throws<ArgumentNullException>(() => new EventStoreRepository<StubAggregate>(It.IsAny<DataBaseContext>(), It.IsAny<IInvoker<StubAggregate>>(), It.IsAny<IDomainEventRebuilder>()));
        }

        [Fact(DisplayName = "AppendAsync should append an event on eventstore")]
        public async Task AppendAsyncShouldAppendAnEventOnEventStore()
        {
            //Arrange
            var optionsBuilder = new DbContextOptionsBuilder<DataBaseContext>();
            optionsBuilder.UseInMemoryDatabase("FakeInMemoryDataEventStore");
            var moqContext = new DataBaseContext(optionsBuilder.Options);
            moqContext.Database.EnsureCreated();

            var evt = new EventStore(Guid.NewGuid(),
                1, "2@735f8407-16be-44b5-be96-2bab582b5298",
                "LogCorner.EduSync.Speech.Domain.Events.Speech.SpeechCreatedEvent",
                DateTime.Now, "{}");

            var sut = new EventStoreRepository<StubAggregate>(moqContext, It.IsAny<IInvoker<StubAggregate>>(), It.IsAny<IDomainEventRebuilder>());

            //Act
            await sut.AppendAsync(evt);
            await moqContext.SaveChangesAsync();
            var result = await moqContext.EventStore.SingleOrDefaultAsync();
            moqContext.Dispose();
            Console.WriteLine(evt.Id);
            Console.WriteLine(result.Id);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(evt.Id, result.Id);
            Assert.Equal(evt.AggregateId, result.AggregateId);
            Assert.Equal(evt.Name, result.Name);
            Assert.Equal(evt.TypeName, result.TypeName);
            Assert.Equal(evt.OccurredOn, result.OccurredOn);
            Assert.Equal(evt.PayLoad, result.PayLoad);
            Assert.Equal(evt.Version, result.Version);
        }

        [Fact]
        public async Task GetByIdAsyncWithBadAggregateIdShouldRaiseBadAggregateIdException()
        {
            //Arrange
            var moqDb = new Mock<DataBaseContext>();
            var sut = new EventStoreRepository<StubAggregate>(moqDb.Object, It.IsAny<IInvoker<StubAggregate>>(), It.IsAny<IDomainEventRebuilder>());
            var aggregateId = Guid.Empty;

            //Act
            //Assert
            await Assert.ThrowsAsync<BadAggregateIdException>(() =>
                sut.GetByIdAsync<StubAggregate>(aggregateId));
        }

        [Fact]
        public async Task GetByIdAsyncWithNullInstanceOfAggregateShouldRaiseNullInstanceOfAggregateIdException()
        {
            //Arrange
            var moqDb = new Mock<DataBaseContext>();
            var moqInvoker = new Mock<IInvoker<StubAggregate>>();
            moqInvoker.Setup(i => i.CreateInstanceOfAggregateRoot<StubAggregate>()).Returns((StubAggregate)null);
            var sut = new EventStoreRepository<StubAggregate>(moqDb.Object, moqInvoker.Object, It.IsAny<IDomainEventRebuilder>());
            var aggregateId = Guid.NewGuid();

            //Act
            //Assert
            await Assert.ThrowsAsync<NullInstanceOfAggregateException>(() =>
                sut.GetByIdAsync<StubAggregate>(aggregateId));
        }

        [Fact]
        public async Task GetByIdAsyncWithoutEventsShouldReturnEmptyList()
        {
            //Arrange
            var optionsBuilder = new DbContextOptionsBuilder<DataBaseContext>();
            optionsBuilder.UseInMemoryDatabase("FakeInMemoryData");
            var moqContext = new DataBaseContext(optionsBuilder.Options);
            moqContext.Database.EnsureCreated();

            var aggregate = (StubAggregate)typeof(StubAggregate)
                .GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,
                    null,
                    new Type[0],
                    new ParameterModifier[0])
                ?.Invoke(new object[0]);

            var moqInvoker = new Mock<IInvoker<StubAggregate>>();
            moqInvoker.Setup(i => i.CreateInstanceOfAggregateRoot<StubAggregate>()).Returns(aggregate);

            var sut = new EventStoreRepository<StubAggregate>(moqContext, moqInvoker.Object, It.IsAny<IDomainEventRebuilder>());
            var aggregateId = Guid.NewGuid();

            //Act
            var result = await sut.GetByIdAsync<StubAggregate>(aggregateId);

            //Assert
            Assert.Null(result);
        }

        [Fact(DisplayName = "GetByIdAsync with events should return the current state of the aggregate")]
        public async Task GetByIdAsyncWithEventsShouldReturnTheCurrentStateOfTheAggregate()
        {
            //Arrange

            #region mock of DataBaseContext , mock of IDomainEventRebuilder and mock of moqInvoker

            var aggregateId = Guid.NewGuid();
            var optionsBuilder = new DbContextOptionsBuilder<DataBaseContext>();
            optionsBuilder.UseInMemoryDatabase("FakeInMemoryData");
            var moqContext = new DataBaseContext(optionsBuilder.Options);
            moqContext.Database.EnsureCreated();

            var obj = new EventOject(aggregateId, "Dupont", "45 av charles degaulle paris, france");
            var type = obj.GetType().AssemblyQualifiedName;
            var json = @"
                {
                    'FullName':'Dupont',
                    'Adresse':'45 av charles degaulle paris, france'
                }";

            moqContext.EventStore.Add(new EventStore(aggregateId,
                1,
                "2@735f8407-16be-44b5-be96-2bab582b5298",
                type,
                DateTime.Now, json));
            moqContext.SaveChanges();

            Mock<IDomainEventRebuilder> moqDomainEventRebuilder = new Mock<IDomainEventRebuilder>();
            moqDomainEventRebuilder.Setup(m => m.RebuildDomainEvents(It.IsAny<IEnumerable<EventStore>>()))
                .Returns(new List<Event>
                {
                    obj
                });

            var aggregate = (StubAggregate)typeof(StubAggregate)
                .GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,
                    null,
                    new Type[0],
                    new ParameterModifier[0])
                ?.Invoke(new object[0]);

            var moqInvoker = new Mock<IInvoker<StubAggregate>>();
            moqInvoker.Setup(i => i.CreateInstanceOfAggregateRoot<StubAggregate>()).Returns(aggregate);

            #endregion mock of DataBaseContext , mock of IDomainEventRebuilder and mock of moqInvoker

            var sut = new EventStoreRepository<StubAggregate>(moqContext, moqInvoker.Object, moqDomainEventRebuilder.Object);

            //Act
            var result = await sut.GetByIdAsync<StubAggregate>(aggregateId);
            moqContext.Dispose();

            //Assert
            Assert.NotNull(aggregate);
            Assert.Equal(aggregate, result);
            Assert.Equal(aggregateId, aggregate.Id);
            Assert.Equal(obj.FullName, aggregate.FullName);
            Assert.Equal(obj.Adresse, aggregate.Adresse);
        }
    }
}