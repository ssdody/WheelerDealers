using Dealership.Data.UnitOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Dealership.Tests.CarServiceTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenNullArgumentIsPassed()
        {
            //arrange
            IUnitOfWork invalidUnitOfWork = null;
            //act&assert
            Assert.ThrowsException<ArgumentNullException>(() => new Dealership.Services.CarService(invalidUnitOfWork));
        }
        [TestMethod]
        public void NotThrowException_WhenValidUnitOfWorkIsPassed()
        {
            //arrange
            var validUnitOfWork = new Mock<IUnitOfWork>();
            //act
            var carService = new Services.CarService(validUnitOfWork.Object);
        }
    }
}
