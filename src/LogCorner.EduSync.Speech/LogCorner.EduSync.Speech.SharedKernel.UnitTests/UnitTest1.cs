using LogCorner.EduSync.Speech.SharedKernel.Serialyser;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Xunit;

namespace LogCorner.EduSync.Speech.SharedKernel.UnitTest
{
    public class SharedKernelUnitTest
    {
        [Fact]
        public void ShouldAddSharedKernel()
        {
            //Arrange
            IServiceCollection services = new ServiceCollection();

            //Act
            services.AddSharedKernel();
            var contains = services.ToList();
            var jsonSerializer = contains.SingleOrDefault(c => c.ServiceType.Name == nameof(IJsonSerializer) && c.Lifetime == ServiceLifetime.Singleton);
            var eventSerializer = contains.SingleOrDefault(c => c.ServiceType.Name == nameof(IEventSerializer) && c.Lifetime == ServiceLifetime.Singleton);
            var jsonProvider = contains.SingleOrDefault(c => c.ServiceType.Name == nameof(IJsonProvider) && c.Lifetime == ServiceLifetime.Singleton);

            //Assert
            Assert.Equal(jsonSerializer?.ImplementationType?.FullName, typeof(JsonSerializer).FullName);
            Assert.Equal(eventSerializer?.ImplementationType?.FullName, typeof(JsonEventSerializer).FullName);
            Assert.Equal(jsonProvider?.ImplementationType?.FullName, typeof(JsonProvider).FullName);
        }
    }
}