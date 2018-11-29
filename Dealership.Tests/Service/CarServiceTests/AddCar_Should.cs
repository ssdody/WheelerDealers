using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Data.Models.Contracts;
using Dealership.Data.Repository;
using Dealership.Data.UnitOfWork;
using Dealership.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dealership.Tests.CarServiceTests
{
    [TestClass]
    public class AddCar_Should
    {
        [TestMethod]
        public void ThrowServiceExcpetion_WhenNullArgumentIsPassed()
        {
            //arrange
            var fakeUnitOfWork = new Mock<IUnitOfWork>();
            var sut = new Services.CarService(fakeUnitOfWork.Object);
            //act
            //assert
            Assert.ThrowsException<ServiceException>(() => sut.AddCar(null));
        }

        [TestMethod]
        public void AddCarToDatabase_WhenValidParametersArePassed()
        {
            var contexOptions = new DbContextOptionsBuilder<DealershipContext>()
                .UseInMemoryDatabase(databaseName: "AddCarToDatabase_WhenValidParametersArePassed").Options;

            var testCar = new Mock<Car>();

            DealershipContext dealershipContext;

            using (dealershipContext = new DealershipContext(contexOptions))
            {
                var unitofWork = new UnitOfWork(dealershipContext);
                var carService = new Services.CarService(unitofWork);


                carService.AddCar(testCar.Object);
            }
            using (dealershipContext = new DealershipContext(contexOptions))
            {
                Assert.IsTrue(dealershipContext.Cars.Count() == 1);

            }
        }
    }
}
