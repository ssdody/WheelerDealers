using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Data.UnitOfWork;
using Dealership.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Dealership.Tests.Service.Tests.EditCarService
{
    [TestClass]
    public class EditBodyType_Should
    {
        [TestMethod]
        public void EditBodyTypeCorrectly_WhenValidParametersArePassed()
        {
            //arrange
            var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
                .UseInMemoryDatabase(databaseName:
                "EditModelCorrectly_WhenValidParametersArePassed").Options;

            string result;
            Car testCar;

            string[] validParameters = new string[2] { "1", "Sedan" };
            string expectedBodyTypeName = validParameters[1];

            using (var dealerShipContext = new DealershipContext(contextOptions))
            {
                var testBody = new BodyType() { Name = "Sedan" };

                var unitOfWork = new UnitOfWork(dealerShipContext);

                testCar = new Car() {Brand = new Brand() { Name = "test"}
                                                         , Model = "test"
                                                         , BodyType = new BodyType() {Name = "Coupe" } };

                dealerShipContext.Cars.Add(testCar);
                dealerShipContext.Chassis.Add(testBody).Context.SaveChanges();

                var carServiceStub = new Mock<ICarService>();

                carServiceStub.Setup(cs => cs.CreateCar(It.IsAny<string>(), It.IsAny<string>()
                    , It.IsAny<short>(), It.IsAny<short>(), It.IsAny<DateTime>()
                    , It.IsAny<decimal>(), It.IsAny<string>(), It.IsAny<string>()
                    , It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()
                    , It.IsAny<int>())).Returns(testCar);

                carServiceStub.Setup(cs => cs.GetCar(1)).Returns(testCar);

                var editCarService = new Services.EditCarService(unitOfWork, carServiceStub.Object);

                result = editCarService.EditBodyType(validParameters);
            }

            Assert.IsTrue(result.Contains("edited"));
            Assert.IsTrue(testCar.BodyType.Name == expectedBodyTypeName);
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenNullValueIsPassed()
        {
            var unitOfWorkStub = new Mock<IUnitOfWork>();
            var carServiceStub = new Mock<ICarService>();

            string[] invalidParameters = null;

            var sut = new Services.EditCarService(unitOfWorkStub.Object, carServiceStub.Object);

            Assert.ThrowsException<ArgumentNullException>(() => sut.EditBodyType(invalidParameters));
        }

        [TestMethod]
        public void ThowArgumentException_WhenInvalidIDIsPassed()
        {

            var unitOfWorkStub = new Mock<IUnitOfWork>();
            var carServiceStub = new Mock<ICarService>();

            string[] validParameters = { "invalidID", "test" };

            var sut = new Services.EditCarService(unitOfWorkStub.Object, carServiceStub.Object);

            Assert.ThrowsException<ArgumentException>(() => sut.EditBodyType(validParameters));
        }
    }
}
