using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Data.UnitOfWork;
using Dealership.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Tests.Service.Tests.EditCarService
{
    [TestClass]
    public class EditEngineCapacity_Should
    {
        [TestMethod]
        public void ThrowArgumentException_WhenEmptyParametersArePassed()
        {
            var unitOfWorkStub = new Mock<IUnitOfWork>();
            var carServiceStub = new Mock<ICarService>();

            var invalidParameters = new string[0];

            var sut = new Dealership.Services.EditCarService(unitOfWorkStub.Object, carServiceStub.Object);

            Assert.ThrowsException<ArgumentNullException>(() => sut.EditEngineCapacity(invalidParameters));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenNullValueIsPassed()
        {
            var unitOfWorkStub = new Mock<IUnitOfWork>();
            var carServiceStub = new Mock<ICarService>();

            string[] invalidParameters = null;

            var sut = new Services.EditCarService(unitOfWorkStub.Object, carServiceStub.Object);

            Assert.ThrowsException<ArgumentNullException>(() => sut.EditEngineCapacity(invalidParameters));
        }

        [TestMethod]
        public void ThowArgumentException_WhenInvalidIDIsPassed()
        {

            var unitOfWorkStub = new Mock<IUnitOfWork>();
            var carServiceStub = new Mock<ICarService>();

            string[] validParameters = { "invalidID", "test" };

            var sut = new Services.EditCarService(unitOfWorkStub.Object, carServiceStub.Object);

            Assert.ThrowsException<ArgumentException>(() => sut.EditEngineCapacity(validParameters));
        }

        [TestMethod]
        public void EditEngineCapacityValueCorrectly_WhenValidParametersArePassed()
        {
            var testCar = new Car()
            {
                Brand = new Brand() { Name = "test" },
                Model = "test",
                EngineCapacity = 1000
            };

            var validParameters = new string[2] { "1", "4444" };
            var expectedValue = int.Parse(validParameters[1]);

            var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
                .UseInMemoryDatabase(databaseName:
                "EditModelCorrectly_WhenValidParametersArePassed").Options;

            DealershipContext context;
            string result;

            using (context = new DealershipContext(contextOptions))
            {

                var unitOfWork = new UnitOfWork(context);
                var carService = new Mock<Services.CarService>();
                carService.Setup(x => x.GetCar(1)).Returns(testCar);

                context.Cars.Add(testCar).Context.SaveChanges();

                var sut = new Services.EditCarService(unitOfWork, carService.Object);

                result = sut.EditEngineCapacity(validParameters);
            }
            //assert    
            Assert.IsTrue(result.Contains("edited"));
            Assert.IsTrue(testCar.EngineCapacity == expectedValue);
        }
    }
}
