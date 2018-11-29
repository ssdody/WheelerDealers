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
    //arrange
    //var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
    //    .UseInMemoryDatabase(databaseName:
    //    "EditModelCorrectly_WhenValidParametersArePassed").Options;

    [TestClass]
    public class EditModel_Should
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenNullArgumentIsPassed()
        {
            //arrange
            var unitOfWorkStub = new Mock<IUnitOfWork>();
            var carServiceStub = new Mock<ICarService>();

            var editCarService = new Services.EditCarService(unitOfWorkStub.Object, carServiceStub.Object);
            //act&assert
            Assert.ThrowsException<ArgumentNullException>(() => editCarService.EditModel(null));
        }
        [TestMethod]
        public void ThrowArgumentNullException_WhenNoArgumentArePassed()
        {
            //arrange
            var unitOfWorkStub = new Mock<IUnitOfWork>();
            var carServiceStub = new Mock<ICarService>();

            var emptyArray = new string[3];

            var editCarService = new Services.EditCarService(unitOfWorkStub.Object, carServiceStub.Object);
            //act&assert
            Assert.ThrowsException<ArgumentException>(() => editCarService.EditModel(emptyArray));
        }

        [TestMethod]
        public void EditModelCorrectly_WhenValidParametersArePassed()
        {
            var testCar = new Car()
            {
                Brand = new Brand() { Name = "test" },
                Model = "test",

            };

            var validParameters = new string[2] { "1", "330xi" };
            string result;

            var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
               .UseInMemoryDatabase(databaseName:
               "EditModelCorrectly_WhenValidParametersArePassed").Options;

            DealershipContext context;
            using (context = new DealershipContext(contextOptions))
            {

                var unitOfWork = new UnitOfWork(context);

                context.Cars.Add(testCar).Context.SaveChanges();

                var carService = new Mock<Services.CarService>();
                carService.Setup(x => x.GetCar(1)).Returns(testCar);

                var sut = new Services.EditCarService(unitOfWork, carService.Object);

                result = sut.EditModel(validParameters);
            }
            //assert    
            Assert.IsTrue(result.Contains("edited"));
            Assert.IsTrue(testCar.Model == validParameters[1]);
        }
    }
}
