using LogCorner.EduSync.Speech.Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace LogCorner.EduSync.Speech.Infrastructure.UnitTests.Specs
{
    public class RepositorySpecs
    {
        [Fact(DisplayName = "Verify that CreateAsync can be called on Repository and should trigger dbset.AddAsync")]
        public async Task Verify_that_CreateAsync_can_be_called_on_Repository_and_trigger_dbsetAddAsyncTest()
        {
            //Arrange
            var dbSet = GetQueryableMockDbSet(new List<EntityAsAggregateRoot>
            {
                new EntityAsAggregateRoot()
            });

            var context = new Mock<DataBaseContext>();
            context.Setup(c => c.Set<EntityAsAggregateRoot>()).Returns(dbSet.Object);

            //Act
            IRepository<EntityAsAggregateRoot, Guid> repository = new Repository<EntityAsAggregateRoot, Guid>(context.Object);
            await repository.CreateAsync(new EntityAsAggregateRoot());

            //Assert
            dbSet.Verify(m => m.AddAsync(It.IsAny<EntityAsAggregateRoot>(), It.IsAny<CancellationToken>()),
                Times.Once, "Commit must be called only once");
        }

        private static EntityEntry<EntityAsAggregateRoot> AddDbSet(DbSet<EntityAsAggregateRoot> m)
        {
            return m.AddAsync(It.IsAny<EntityAsAggregateRoot>(), It.IsAny<CancellationToken>()).Result;
        }

        private static Mock<DbSet<T>> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

            dbSet
                .Setup(_ => _.AddAsync(It.IsAny<T>(), It.IsAny<CancellationToken>()))
                .Callback((T model, CancellationToken _) => { sourceList.Add(model); })
                .Returns(async (T model, CancellationToken token) => await Task.FromResult((EntityEntry<T>)null)).Verifiable();

            return dbSet;
        }

        [Fact(DisplayName = "Verify that UpdateAsync can be called on Repository and should trigger dbset.AddAsync")]
        public async Task Verify_that_UpdateAsync_can_be_called_on_Repository_and_trigger_dbsetAddAsyncTest()
        {
            //Arrange
            var dbSet = GetQueryableMockDbSet(new List<EntityAsAggregateRoot>
            {
                new EntityAsAggregateRoot()
            });

            var context = new Mock<DataBaseContext>();
            context.Setup(c => c.Set<EntityAsAggregateRoot>()).Returns(dbSet.Object);

            //Act
            IRepository<EntityAsAggregateRoot, Guid> repository = new Repository<EntityAsAggregateRoot, Guid>(context.Object);
            await repository.UpdateAsync(new EntityAsAggregateRoot());

            //Assert
            dbSet.Verify(m => m.Update(It.IsAny<EntityAsAggregateRoot>()), Times.Once, "Commit must be called only once");
        }
    }
}