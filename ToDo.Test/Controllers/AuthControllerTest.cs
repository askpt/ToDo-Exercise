using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ToDo.Controllers;
using ToDo.Interfaces;
using ToDo.ViewModels;

namespace ToDo.Test.Controllers
{
    [TestClass]
    public class AuthControllerTest
    {
        [TestMethod]
        public void AuthenticateTest()
        {
            var username = "test";
            var password = "pwd123";

            var userVM = new UserViewModel
            {
                Username = username,
                Password = password
            };

            var expectedToken = "dummy_token";

            // Arrange
            var mockServ = new Mock<IUserService>();
            mockServ.Setup(s => s.Authenticate(username, password)).Returns(expectedToken);
            var controller = new AuthController(mockServ.Object);

            // Act
            var actual = controller.Authenticate(userVM) as OkObjectResult;

            // Assert
            Assert.AreEqual(actual.Value, expectedToken);
        }

        [TestMethod]
        public void AuthenticateFailTest()
        {
            var username = "test";
            var password = "pwd123";

            var userVM = new UserViewModel
            {
                Username = username,
                Password = password
            };

            var expectedString = "Username or password is incorrect";

            // Arrange
            var mockServ = new Mock<IUserService>();
            mockServ.Setup(s => s.Authenticate(username, password));
            var controller = new AuthController(mockServ.Object);

            // Act
            var actual = controller.Authenticate(userVM) as BadRequestObjectResult;

            // Assert
            Assert.AreEqual(actual.Value, expectedString);
        }
    }
}