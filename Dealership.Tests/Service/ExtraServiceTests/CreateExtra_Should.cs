using Dealership.Data.Models;
using Dealership.Data.UnitOfWork;
using Dealership.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dealership.Tests.Service.Tests.ExtraServiceTests
{
    [TestClass]
    public class CreateExtra_Should
    {
        [TestMethod]
        public void AddExtra_WhenValidParametersArePassed()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            string extraName = "testExtra";

            unitOfWorkMock.Setup(u => u.GetRepository<Extra>().All()).Returns(new List<Extra>().AsQueryable());

            var sut = new ExtraService(unitOfWorkMock.Object);

            // Act
            sut.CreateExtra(extraName);

            // Assert
            unitOfWorkMock.Verify(u => u.GetRepository<Extra>().Add(It.IsAny<Extra>()), Times.Once);

            unitOfWorkMock.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void ThrowArgumentException_WhenExtraAlreadyExists()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            string extraName = "testExtra";
            var existingExtra = new Extra() { Name = "testExtra" };
            var listOfExtras = new List<Extra>() { existingExtra };

            unitOfWorkMock.Setup(u => u.GetRepository<Extra>().All()).Returns(listOfExtras.AsQueryable());

            var sut = new ExtraService(unitOfWorkMock.Object);
            //Act && Assert
            Assert.ThrowsException<ArgumentException>(() => sut.CreateExtra(extraName));

        }
    }
}
