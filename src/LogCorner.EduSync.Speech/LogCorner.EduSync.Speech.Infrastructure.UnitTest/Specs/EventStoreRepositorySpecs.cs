using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace LogCorner.EduSync.Speech.Infrastructure.UnitTest.Specs
{
    public class EventStoreRepositorySpecs
    {
        [Fact(DisplayName = "AppendAsync should append an event on eventstore")]
        public async Task AppendAsyncShouldAppendAnEventOnEventStore()
        {
            //Arrange
            var optionsBuilder = new DbContextOptionsBuilder<DataBaseContext>();
            optionsBuilder.UseInMemoryDatabase("FakeInMemoryData");
            var moqContext = new DataBaseContext(optionsBuilder.Options);
            moqContext.Database.EnsureCreated();

            var evt = new EventStore(Guid.NewGuid(),
                1, "2@735f8407-16be-44b5-be96-2bab582b5298",
                "LogCorner.EduSync.Speech.Domain.Events.Speech.SpeechCreatedEvent",
                DateTime.Now, "{}");

            IEventStoreRepository sut = new EventStoreRepository(moqContext);

            //Act
            await sut.AppendAsync(evt);
            await moqContext.SaveChangesAsync();
            var result = await moqContext.EventStore.SingleOrDefaultAsync();
            moqContext.Dispose();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(evt.Id, result.Id);
            Assert.Equal(evt.AggregateId, result.AggregateId);
            Assert.Equal(evt.Name, result.Name);
            Assert.Equal(evt.TypeName, result.TypeName);
            Assert.Equal(evt.OccurredOn, result.OccurredOn);
            Assert.Equal(evt.SerializedBody, result.SerializedBody);
            Assert.Equal(evt.IsSync, result.IsSync);
        }
    }
}