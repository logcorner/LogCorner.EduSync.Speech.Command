using LogCorner.EduSync.Speech.Domain.IRepository;
using Moq;
using Xunit;

namespace LogCorner.EduSync.Speech.Infrastructure.UnitTests.Specs
{
    public class UnitOfWorkSpecs
    {
        [Fact(DisplayName = "When saving IUnitOfWork.Commit Should Save Aggregate Root and DbContext.SaveChanges called only once")]
        public void Commit()
        {
            //Arrange
            var context = new Mock<DataBaseContext>();
            context.Setup(c => c.SaveChanges()).Returns(1).Verifiable();

            //Act
            IUnitOfWork unitOfWork = new UnitOfWork(context.Object);
            unitOfWork.Commit();

            //Assert
            context.Verify(m => m.SaveChanges(), Times.Once, "SaveChanges should be called only once");
        }

        [Fact(DisplayName = "When disposing unitOfWork.Dispose should be called only once")]
        public void Dispose()
        {
            //Arrange
            var context = new Mock<DataBaseContext>();
            context.Setup(c => c.Dispose()).Verifiable();

            //Act
            IUnitOfWork unitOfWork = new UnitOfWork(context.Object);
            unitOfWork.Dispose();

            //Assert
            context.Verify(m => m.Dispose(), Times.Once, "Dispose must be called only once");
        }
    }
}