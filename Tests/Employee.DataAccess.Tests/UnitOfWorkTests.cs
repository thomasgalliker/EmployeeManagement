
using Employee.DataAccess.Abstractions;
using Employee.DataAccess.Core;

using FluentAssertions;

using Moq;

using Xunit;

namespace Employee.DataAccess.Tests
{
    public class UnitOfWorkTests
    {
        [Fact]
        public void ShouldCommitNoChangesWhenNothingNeedsToBeDone()
        {
            // Arrange
            using (IUnitOfWork unitOfWork = new UnitOfWork())
            {
                // Act
                var numberOfChanges = unitOfWork.Commit();

                // Assert
                numberOfChanges.Should().Be(0);
            }
        }

        [Fact]
        public void ShouldCommitChangesOfContext()
        {
            // Arrange
            IUnitOfWork unitOfWork = new UnitOfWork();
            var contextMock = new Mock<IContext>();
            contextMock.Setup(c => c.SaveChanges()).Returns(99);
            unitOfWork.RegisterContext(contextMock.Object);

            // Act
            var numberOfChanges = unitOfWork.Commit();

            // Assert
            numberOfChanges.Should().Be(99);
        }

        [Fact]
        public void ShouldHandleMultipleInstances()
        {
            // Arrange
            using (IUnitOfWork outerUnitOfWork = new UnitOfWork())
            {
                using (IUnitOfWork innerUnitofWork = new UnitOfWork())
                {
                    // Act
                    var numberOfChanges = innerUnitofWork.Commit();

                    // Assert
                    numberOfChanges.Should().Be(0);
                }
                outerUnitOfWork.Commit();
            }
        }
    }
}
