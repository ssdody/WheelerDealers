using Dealership.Data.Models;
using Dealership.Data.UnitOfWork;
using Dealership.Services;
using Dealership.Services.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Dealership.Tests.Service.Tests.UserServiceTests
{
    [TestClass]
    public class DeleteUser_Should
    {
        [TestMethod]
        public void DeleteUser_WhenValidParametersArePassed()
        {
            // Arranges
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
            sut.DeleteUser(username, password);

            // Assert
            unitOfWorkMock.Verify(u => u.GetRepository<User>().Delete(It.IsAny<User>()), Times.Once);

            unitOfWorkMock.Verify(c => c.SaveChanges(), Times.Once);
        }
    }
}
