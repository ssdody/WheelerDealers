using Dealership.Data.Context;
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
    public class Constructor_Should
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenNullContextIsPassed()
        {
            //arrange
            var carServiceStub = new Mock<ICarService>();
            //act&assert
            Assert.ThrowsException<ArgumentNullException>(() => new Services.EditCarService(null, carServiceStub.Object));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenNullCarServiceIsPassed()
        {
            //arrange
            var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
               .UseInMemoryDatabase(databaseName:
               "EditModelCorrectly_WhenValidParametersArePassed").Options;

            var context = new DealershipContext(contextOptions);
            //act&assert
            Assert.ThrowsException<ArgumentNullException>(() => new Services.EditCarService(context, null));
        }

        [TestMethod]
        public void InitializeEditCarsServiceCorrectly_WhenValidArgumentsArePassed()
        {
            var contextOptions = new DbContextOptionsBuilder<DealershipContext>()
              .UseInMemoryDatabase(databaseName:
              "EditModelCorrectly_WhenValidParametersArePassed").Options;

            var context = new DealershipContext(contextOptions);
            var carServiceStub = new Mock<ICarService>();

            var sut = new Services.EditCarService(context, carServiceStub.Object);

            Assert.IsInstanceOfType(sut, typeof(IEditCarService));
        }

    }
}
