using Dealership.Data.UnitOfWork;
using Dealership.Services.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Dealership.Tests.Service.Tests.EditCarService
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenNullArgumentIsPassedAsUnitOfWork()
        {
            //arrange
            var carServiceStub = new Mock<ICarService>();
            //act&assert
            Assert.ThrowsException<ArgumentNullException>(() => new Services.EditCarService(null, carServiceStub.Object));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenNullArgumentIsPassedAsCarService()
        {
            //arrange
            var unitOfWorkStub = new Mock<IUnitOfWork>();
            //act&assert
            Assert.ThrowsException<ArgumentNullException>(() => new Services.EditCarService(unitOfWorkStub.Object, null));
        }

        [TestMethod]
        public void InitializeUnitOfWorkCorrectly_WhenValidArgumentIsPassed()
        {
            var unitOfWorkStub = new Mock<IUnitOfWork>();
            var carServiceStub = new Mock<ICarService>();

            var sut = new Services.EditCarService(unitOfWorkStub.Object,carServiceStub.Object);

            Assert.IsInstanceOfType(sut.UnitOfWork, typeof(IUnitOfWork));
        }

        [TestMethod]
        public void InitializeCarServiceCorrectly_WhenValidArgumentIsPassed()
        {
            var unitOfWorkStub = new Mock<IUnitOfWork>();
            var carServiceStub = new Mock<ICarService>();

            var sut = new Services.EditCarService(unitOfWorkStub.Object, carServiceStub.Object);

            Assert.IsInstanceOfType(sut.CarService, typeof(ICarService));
        }
    }
}
