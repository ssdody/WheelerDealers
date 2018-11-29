using Dealership.Data.Models;
using Dealership.Data.UnitOfWork;
using Dealership.Services;
using Dealership.Services.Abstract;
using Dealership.Services.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Dealership.Tests.Service.Tests.UserServiceTests
{
    [TestClass]
    public class GetUserByCredentials_Should
    {
        [TestMethod]
        public void ReturnCorrectUser_WhenValidParametersArePassed()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var carServiceMock = new Mock<ICarService>();

            string username = "username";
            string password = "password";

            var user = new User()
            {
                Username = username,
                Password = password,
            };

            var users = new List<User>() { user };
            unitOfWorkMock.Setup(u => u.GetRepository<User>().All()).Returns(users.AsQueryable());

            var sut = new UserService(unitOfWorkMock.Object, carServiceMock.Object);

            // Act
            var result = sut.GetUserByCredentials(username, password);

            // Assert
            Assert.AreEqual(user, result);
        }

        [TestMethod]
        public void ThrowServiceException_WhenUsernameNotExist()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var carServiceMock = new Mock<ICarService>();

            string username = "username";
            string password = "password";

            var user = new User() { Username = "otherUsername" };

            var users = new List<User>() { user };

            unitOfWorkMock.Setup(u => u.GetRepository<User>().All()).Returns(users.AsQueryable());

            var sut = new UserService(unitOfWorkMock.Object, carServiceMock.Object);

            // Act && Assert           
            Assert.ThrowsException<ServiceException>(() => sut.GetUserByCredentials(username, password));
        }

        [TestMethod]
        public void ThrowServiceException_WhenPasswordNotMatch()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var carServiceMock = new Mock<ICarService>();

            string username = "username";
            string password = "password";

            var user = new User()
            {
                Username = username,
                Password = "otherPassword"
            };

            var users = new List<User>() { user };

            unitOfWorkMock.Setup(u => u.GetRepository<User>().All()).Returns(users.AsQueryable());

            var sut = new UserService(unitOfWorkMock.Object, carServiceMock.Object);

            // Act && Assert           
            Assert.ThrowsException<ServiceException>(() => sut.GetUserByCredentials(username, password));
        }
    }
}
