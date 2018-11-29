using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Data.Repository;
using Dealership.Data.UnitOfWork;
using Dealership.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Dealership.Tests.Service.Tests.EditCarService
{
    [TestClass]
    public class EditBrand_Should
    {
        [TestMethod]
        public void ThrowArgumentException_WhenEmptyParametersArePassed()
        {
            var unitOfWorkStub = new Mock<IUnitOfWork>();
            var carServiceStub = new Mock<ICarService>();

            var invalidParameters = new string[0];

            var sut = new Services.EditCarService(unitOfWorkStub.Object, carServiceStub.Object);

            Assert.ThrowsException<ArgumentException>(() => sut.EditBrand(invalidParameters));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenNullValueIsPassed()
        {
            var unitOfWorkStub = new Mock<IUnitOfWork>();
            var carServiceStub = new Mock<ICarService>();

            string[] invalidParameters = null;

            var sut = new Services.EditCarService(unitOfWorkStub.Object, carServiceStub.Object);

            Assert.ThrowsException<ArgumentNullException>(() => sut.EditBrand(invalidParameters));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenInvalidIdInParametersIsPassed()
        {
            //arrange
            var unitOfWorkStub = new Mock<IUnitOfWork>();
            var carServiceStub = new Mock<ICarService>();

            string[] invalidParameters = new string[2] { "invalid", "test" };

            var editCarService = new Services.EditCarService(unitOfWorkStub.Object, carServiceStub.Object);

            //Act&assert
            Assert.ThrowsException<ArgumentException>(() => editCarService.EditBrand(invalidParameters));
        }

        [TestMethod]
        public void EditBrandCorrectly_WhenValidParametersArePassed()
        {
            //arrange
            var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
                .UseInMemoryDatabase(databaseName:
                "EditModelCorrectly_WhenValidParametersArePassed").Options;

            string result;
            Car testCar;

            string[] validParameters = new string[2] { "1", "newBrand" };
            string expectedBrandName = validParameters[1];

            using (var dealerShipContext = new DealershipContext(contextOptions))
            {
                var testBrand = new Brand() { Name = "testBrand" };
                var testNewBrand = new Brand() { Name = "newBrand" };

                dealerShipContext.Brands.Add(testNewBrand).Context.SaveChanges();

                var unitOfWork = new UnitOfWork(dealerShipContext);

                testCar = new Car() { Brand = testBrand };
                dealerShipContext.Cars.Add(testCar).Context.SaveChanges();
                var carServiceStub = new Mock<ICarService>();

                carServiceStub.Setup(cs => cs.GetCar(1)).Returns(testCar);

                var editCarService = new Services.EditCarService(unitOfWork, carServiceStub.Object);

                result = editCarService.EditBrand(validParameters);
            }

            Assert.IsTrue(result.Contains("edited"));
            Assert.IsTrue(testCar.Brand.Name == expectedBrandName);
        }

        [TestMethod]
        public void CreateNewBrand_IfInputBrandNotExistsInDatabase()
        {
            //arrange
            var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
                .UseInMemoryDatabase(databaseName:
                "EditModelCorrectly_WhenValidParametersArePassed").Options;

            string result;
            Car testCar;

            string[] validParameters = new string[2] { "1", "unexistingBrand" };
            string expectedBrandName = validParameters[1];

            //using (var dealerShipContext = new DealershipContext(contextOptions))
            //{
            //    var testBrand = new Brand() { Name = "testBrand" };

            //    var unitOfWork = new UnitOfWork(dealerShipContext);

            //    testCar = new Car() { Brand = testBrand };

            //    var carServiceStub = new Mock<ICarService>();
            //    carServiceStub.Setup(cs => cs.GetCar(1)).Returns(testCar);

            //    var editCarService = new Services.EditCarService(unitOfWork, carServiceStub.Object);

            //    result = editCarService.EditBrand(validParameters);
            //}

            using (var arrangeContext = new DealershipContext(contextOptions))
            {


                var unitOfWork = new UnitOfWork(arrangeContext);
                var testBrand = new Brand() { Name = "testBrand" };


                testCar = new Car() { Brand = testBrand, Model = "test" };
                arrangeContext.Cars.Add(testCar).Context.SaveChanges();

                var carServiceStub = new Mock<ICarService>();
                carServiceStub.Setup(cs => cs.GetCar(1)).Returns(testCar);

                var editCarService = new Services.EditCarService(unitOfWork, carServiceStub.Object);

                result = editCarService.EditBrand(validParameters);
            }


            Assert.IsTrue(testCar.Brand.Name == expectedBrandName);


        }
    }
}
