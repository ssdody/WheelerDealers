using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Web.Tests.EditCarService
{
    [TestClass]
    public class EditEngineCapacity_Should
    {
        [TestMethod]
        public void ThrowArgumentException_WhenEmptyParametersArePassed()
        {
            var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
                .UseInMemoryDatabase(databaseName:
                "EditModelCorrectly_WhenValidParametersArePassed").Options;
            IEditCarService sut;

            using (var dealershipContext = new DealershipContext(contextOptions))
            {
                var carServiceStub = new Mock<ICarService>();
                sut = new Dealership.Services.EditCarService(dealershipContext, carServiceStub.Object);

            }
            var invalidParameters = new string[0];


            Assert.ThrowsException<ArgumentNullException>(() => sut.EditEngineCapacity(invalidParameters));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenNullValueIsPassed()
        {
            var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
                .UseInMemoryDatabase(databaseName:
                "EditModelCorrectly_WhenValidParametersArePassed").Options;
            IEditCarService sut;

            string[] invalidParameters = null;
            using (var dealershipContext = new DealershipContext(contextOptions))
            {
                var carServiceStub = new Mock<ICarService>();

                sut = new Services.EditCarService(dealershipContext, carServiceStub.Object);
            }
            Assert.ThrowsException<ArgumentNullException>(() => sut.EditEngineCapacity(invalidParameters));
        }

        [TestMethod]
        public void ThowArgumentException_WhenInvalidIDIsPassed()
        {
            var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
                            .UseInMemoryDatabase(databaseName:
                            "EditModelCorrectly_WhenValidParametersArePassed").Options;

            IEditCarService sut;
            string[] validParameters = { "invalidID", "test" };

            using (var dealershipContext = new DealershipContext(contextOptions))
            {
                var carServiceStub = new Mock<ICarService>();

                sut = new Services.EditCarService(dealershipContext, carServiceStub.Object);

            }

            Assert.ThrowsException<ArgumentException>(() => sut.EditEngineCapacity(validParameters));
        }
        //[TestMethod]
        //public void EditEngineCapacityValueCorrectly_WhenValidParametersArePassed()
        //{
        //    var testCar = new Car()
        //    {
        //        Brand = new Brand() { Name = "test" },
        //        CarModel = new CarModel() { Name = "test" },
        //        EngineCapacity = 1000
        //    };

        //    var validParameters = new string[2] { "1", "4444" };
        //    var expectedValue = int.Parse(validParameters[1]);

        //    string result;

        //    var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
        //        .UseInMemoryDatabase(databaseName:
        //        "EditModelCorrectly_WhenValidParametersArePassed").Options;

        //    IEditCarService sut;
        //    using (var dealershipContext = new DealershipContext(contextOptions))
        //    {
        //        var carService = new Mock<ICarService>();
        //        carService.Setup(x => x.GetCarAsync(1).Result).Returns(testCar);

        //        dealershipContext.Cars.Add(testCar).Context.SaveChanges();

        //        sut = new Services.EditCarService(dealershipContext, carService.Object);

        //        result = sut.EditEngineCapacity(validParameters);
        //    }
        //    //assert    
        //    Assert.IsTrue(result.Contains("edited"));
        //    Assert.IsTrue(testCar.EngineCapacity == expectedValue);
        //}
    }
}
