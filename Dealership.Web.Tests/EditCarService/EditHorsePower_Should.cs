using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dealership.Web.Tests.EditCarService
{
    [TestClass]
    public class EditHorsePower_Should
    {
        [TestMethod]
        public void ThrowArgumentException_WhenEmptyParametersArePassed()
        {
            var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
                            .UseInMemoryDatabase(databaseName:
                            "EditModelCorrectly_WhenValidParametersArePassed").Options;

            var invalidParameters = new string[0];
            IEditCarService sut;

            using (var dealershipContext = new DealershipContext(contextOptions))
            {
                var carServiceStub = new Mock<ICarService>();
                sut = new Dealership.Services.EditCarService(dealershipContext, carServiceStub.Object);
            }

            Assert.ThrowsException<ArgumentNullException>(() => sut.EditHorsePower(invalidParameters));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenNullValueIsPassed()
        {
            var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
                             .UseInMemoryDatabase(databaseName:
                             "EditModelCorrectly_WhenValidParametersArePassed").Options;

            string[] invalidParameters = null;
            IEditCarService sut;

            using (var dealershipContext = new DealershipContext(contextOptions))
            {
                var carServiceStub = new Mock<ICarService>();
                sut = new Dealership.Services.EditCarService(dealershipContext, carServiceStub.Object);
            }

            Assert.ThrowsException<ArgumentNullException>(() => sut.EditHorsePower(invalidParameters));
        }

        [TestMethod]
        public void ThowArgumentException_WhenInvalidIDIsPassed()
        {

            var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
                             .UseInMemoryDatabase(databaseName:
                             "EditModelCorrectly_WhenValidParametersArePassed").Options;

            string[] validParameters = { "invalidID", "test" };
            IEditCarService sut;

            using (var dealershipContext = new DealershipContext(contextOptions))
            {
                var carServiceStub = new Mock<ICarService>();

                sut = new Services.EditCarService(dealershipContext, carServiceStub.Object);
            }
            Assert.ThrowsException<ArgumentException>(() => sut.EditHorsePower(validParameters));
        }

        //[TestMethod]
        //public void EditHorsePowerValueCorrectly_WhenValidParametersArePassed()
        //{


        //    var testCar = new Car()
        //    {
        //        Brand = new Brand() { Name = "test" },
        //        CarModel = new CarModel() { Name = "test" },
        //        HorsePower = 100
        //    };

        //    var validParameters = new string[2] { "1", "333" };
        //    var expectedValue = int.Parse(validParameters[1]);

        //    var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
        //       .UseInMemoryDatabase(databaseName:
        //       "EditModelCorrectly_WhenValidParametersArePassed").Options;

        //    string result;

        //    using (var dealershipContext = new DealershipContext(contextOptions))
        //    {
        //        dealershipContext.Cars.Add(testCar).Context.SaveChanges();

        //        var carService = new Mock<ICarService>();
        //        carService.Setup(x => x.GetCarAsync(1).Result).Returns(testCar);

        //        var sut = new Services.EditCarService(dealershipContext, carService.Object);

        //        result = sut.EditHorsePower(validParameters);
        //    }
        //    //assert    
        //    Assert.IsTrue(result.Contains("edited"));
        //    Assert.IsTrue(testCar.HorsePower == expectedValue);
        //}
    }
}
