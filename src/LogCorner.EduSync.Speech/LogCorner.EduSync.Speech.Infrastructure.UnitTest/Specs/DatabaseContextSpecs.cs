using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LogCorner.EduSync.Speech.Infrastructure.UnitTest.Specs
{
    public class DatabaseContextSpecs
    {
        [Fact(DisplayName = "Verify that DatabaseContext Shoud Be  instanciable and disposable and DbSet is not null")]
        public void Verify_that_DatabaseContext_Shoud_Be_instanciable_and_disposable_and_DbSet_is_not_null()
        {
            //Arrange
            DbContextOptionsBuilder<DataBaseContext> optionsBuilder
                = new DbContextOptionsBuilder<DataBaseContext>();
            optionsBuilder.UseInMemoryDatabase("FakeInMemoryData");
            var context = new DataBaseContext(optionsBuilder.Options);

            //Act
            context.Database.EnsureCreated();
            context.Dispose();

            //Assert
            Assert.NotNull(context.Speech);
            Assert.NotNull(context.MediaFile);
        }
    }
}