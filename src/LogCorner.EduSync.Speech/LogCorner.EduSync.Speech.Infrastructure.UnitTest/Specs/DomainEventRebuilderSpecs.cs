using LogCorner.EduSync.Speech.Domain.Events;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LogCorner.EduSync.Speech.Infrastructure.UnitTest.Specs
{
    public class DomainEventRebuilderSpecs
    {
        [Fact]
        public void RebuildDomainEvents()
        {
            //Arrange
            Guid aggregateId = Guid.NewGuid();
            var json = @"
                {
                    'FullName':'Dupont',
                    'Adresse':'45 av charles degaulle paris, france'
                }";
            var events = new List<Event>
            {
                new EventOject(aggregateId, "Georges Dupont", "45 av charles degaulle paris, france")
            };

            var eventStoreItems = new List<EventStore>
            {
                new EventStore(aggregateId,It.IsAny<long>(),It.IsAny<string>(),It.IsAny<string>(),It.IsAny<DateTime>(),json)
            };
            Mock<IEventSerializer> moqEventSerializer = new Mock<IEventSerializer>();
            moqEventSerializer.Setup(m => m.Deserialize<Event>(It.IsAny<string>(), It.IsAny<string>())).Returns(events.FirstOrDefault());
            var sut = new DomainEventRebuilder(moqEventSerializer.Object);
            var result = sut.RebuildDomainEvents(eventStoreItems);

            Assert.Equal(events, result);
        }
    }
}