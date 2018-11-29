using Dealership.Data.Models;
using Dealership.Data.UnitOfWork;
using Dealership.Services;
using Dealership.Services.Abstract;
using Dealership.Services.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dealership.Tests.Service.Tests.UserServiceTests
{
    [TestClass]
    public class RegisterUser_Should
    {
        [TestMethod]
        public void AddUser_WhenValidParametersArePassed()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var carServiceMock = new Mock<ICarService>();

            string username = "username";
            string password = "password";
            string email = "email@email.com";

            unitOfWorkMock.Setup(u => u.GetRepository<User>().All()).Returns(new List<User>().AsQueryable());

            var sut = new UserService(unitOfWorkMock.Object, carServiceMock.Object);

            // Act
            sut.RegisterUser(username, password, password, email);

            // Assert
            unitOfWorkMock.Verify(u => u.GetRepository<User>().Add(It.IsAny<User>()), Times.Once);

            unitOfWorkMock.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void ThrowServiceException_WhenUsernameAlreadyExists()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var carServiceMock = new Mock<ICarService>();

            string username = "username";
            string password = "password";
            string email = "email@email.com";

            var user = new User() { Username = username };

            var users = new List<User>() { user };

            unitOfWorkMock.Setup(u => u.GetRepository<User>().All()).Returns(users.AsQueryable());

            var sut = new UserService(unitOfWorkMock.Object, carServiceMock.Object);

            // Act && Assert           
            Assert.ThrowsException<ServiceException>(() => sut.RegisterUser(username, password, password, email));
        }

        [TestMethod]
        public void ThrowServiceException_WhenEmailAlreadyExists()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var carServiceMock = new Mock<ICarService>();

            string username = "username";
            string password = "password";
            string email = "email@email.com";

            var user = new User() { Email = email };

            var users = new List<User>() { user };

            unitOfWorkMock.Setup(u => u.GetRepository<User>().All()).Returns(users.AsQueryable());

            var sut = new UserService(unitOfWorkMock.Object, carServiceMock.Object);

            // Act && Assert           
            Assert.ThrowsException<ServiceException>(() => sut.RegisterUser(username, password, password, email));
        }

        [TestMethod]
        public void ThrowServiceException_WhenPasswordDoesntMatch()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var carServiceMock = new Mock<ICarService>();

            string username = "username";
            string password = "password1";
            string confirmPassword = "password2";
            string email = "email@email.com";

            unitOfWorkMock.Setup(u => u.GetRepository<User>().All()).Returns(new List<User>().AsQueryable());

            var sut = new UserService(unitOfWorkMock.Object, carServiceMock.Object);

            // Act && Assert           
            Assert.ThrowsException<ServiceException>(() => sut.RegisterUser(username, password, confirmPassword, email));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenInvalidUsernameIsPassed()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var carServiceMock = new Mock<ICarService>();

            string username = "i";
            string password = "password";
            string email = "email@email.com";

            unitOfWorkMock.Setup(u => u.GetRepository<User>().All()).Returns(new List<User>().AsQueryable());

            var sut = new UserService(unitOfWorkMock.Object, carServiceMock.Object);

            // Act && Assert           
            Assert.ThrowsException<ArgumentException>(() => sut.RegisterUser(username, password, password, email));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenInvalidPasswordIsPassed()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var carServiceMock = new Mock<ICarService>();

            string username = "username";
            string password = "i";
            string email = "email@email.com";

            unitOfWorkMock.Setup(u => u.GetRepository<User>().All()).Returns(new List<User>().AsQueryable());

            var sut = new UserService(unitOfWorkMock.Object, carServiceMock.Object);

            // Act && Assert           
            Assert.ThrowsException<ArgumentException>(() => sut.RegisterUser(username, password, password, email));
        }

        [TestMethod]
        public void ThrowArgumentException_WhenInvalidEmailIsPassed()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var carServiceMock = new Mock<ICarService>();

            string username = "username";
            string password = "password";
            string email = "invalidEmail";

            unitOfWorkMock.Setup(u => u.GetRepository<User>().All()).Returns(new List<User>().AsQueryable());

            var sut = new UserService(unitOfWorkMock.Object, carServiceMock.Object);

            // Act && Assert           
            Assert.ThrowsException<ArgumentException>(() => sut.RegisterUser(username, password, password, email));
        }
    }
}

