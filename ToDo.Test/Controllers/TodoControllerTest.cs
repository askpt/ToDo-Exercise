using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ToDo.Controllers;
using ToDo.Interfaces;
using ToDo.Models;
using ToDo.ViewModels;

namespace ToDo.Test.Controllers
{
    [TestClass]
    public class TodoControllerTest
    {
        private ControllerContext FakeContext
        {
            get
            {
                var userFake = new User() { Username = "1", Id = 1 };

                var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userFake.Username),
                new Claim(ClaimTypes.NameIdentifier, userFake.Id.ToString()),
            };
                var identity = new ClaimsIdentity(claims);
                var claimsPrincipal = new ClaimsPrincipal(identity);

                return new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = claimsPrincipal
                    }
                };
            }
        }

        private IList<Todo> GetAllTodo
        {
            get
            {
                return new List<Todo>
                {
                    new Todo
                    {
                        Id = 1,
                        Description = "AAA",
                        UserId = 1,
                        Checked = true,
                        LastUpdated = new System.DateTime(2019, 1, 1, 12, 22, 33)
                    },
                    new Todo
                    {
                        Id = 1,
                        Description = "AAA",
                        UserId = 1,
                        Checked = true,
                        LastUpdated = new System.DateTime(2019, 1, 1, 12, 22, 33)
                    }
                };
            }
        }

        [TestMethod]
        public void GetAllTest()
        {
            var userId = 1;

            // Arrange
            var mockServ = new Mock<ITodoService>();
            mockServ.Setup(s => s.GetAllTodoByUser(userId)).Returns(GetAllTodo);
            var controller = new TodoController(mockServ.Object);
            controller.ControllerContext = FakeContext;

            // Act
            var actualResp = controller.Get(null) as OkObjectResult;
            var actual = actualResp.Value as List<Todo>;

            // Assert
            Assert.AreEqual(actual[0].Id, GetAllTodo[0].Id);
            Assert.AreEqual(actual[1].Id, GetAllTodo[1].Id);
        }

        [TestMethod]
        public void GetSingleTest()
        {
            var userId = 1;
            var todoId = 1;
            var todo = GetAllTodo.First(t => t.Id == todoId && t.UserId == userId);

            // Arrange
            var mockServ = new Mock<ITodoService>();
            mockServ.Setup(s => s.GetTodo(todoId, userId)).Returns(todo);
            var controller = new TodoController(mockServ.Object);
            controller.ControllerContext = FakeContext;

            // Act
            var actualResp = controller.Get(1) as OkObjectResult;
            var actual = actualResp.Value as Todo;

            // Assert
            Assert.AreEqual(actual.Id, todo.Id);
        }

        [TestMethod]
        public void UserFailTest()
        {
            // Arrange
            var mockServ = new Mock<ITodoService>();
            var controller = new TodoController(mockServ.Object);

            // Act
            var actualGetResp = controller.Get(null) as BadRequestResult;
            var actualPostResp = controller.Post(null) as BadRequestResult;
            var actualPutResp = controller.Put(0, null) as BadRequestResult;
            var actualDeleteResp = controller.Delete(0) as BadRequestResult;

            // Assert
            Assert.IsNotNull(actualGetResp);
            Assert.IsNotNull(actualPostResp);
            Assert.IsNotNull(actualPutResp);
            Assert.IsNotNull(actualDeleteResp);
        }

        [TestMethod]
        public void PostTest()
        {
            var userId = 1;
            var todoId = 1;
            var description = "AAA";

            var todoVM = new TodoViewModel
            {
                Id = 1,
                Description = "AAA",
                Check = false
            };

            // Arrange
            var mockServ = new Mock<ITodoService>();
            mockServ.Setup(s => s.CreateTodo(description, userId)).Returns(todoId);
            var controller = new TodoController(mockServ.Object);
            controller.ControllerContext = FakeContext;

            // Act
            var actualResp = controller.Post(todoVM) as OkObjectResult;
            var actual = int.Parse(actualResp.Value.ToString());

            // Assert
            Assert.AreEqual(actual, todoId);
        }

        [TestMethod]
        public void DeleteTest()
        {
            var userId = 1;
            var taskId = 1;

            // Arrange
            var mockServ = new Mock<ITodoService>();
            mockServ.Setup(s => s.DeleteTodo(taskId, userId)).Returns(true);
            var controller = new TodoController(mockServ.Object);
            controller.ControllerContext = FakeContext;

            // Act
            var actualResp = controller.Delete(taskId) as NoContentResult;

            // Assert
            Assert.IsNotNull(actualResp);
        }

        [TestMethod]
        public void DeleteFailTest()
        {
            var userId = 1;
            var todoId = 1;

            // Arrange
            var mockServ = new Mock<ITodoService>();
            mockServ.Setup(s => s.DeleteTodo(todoId, userId)).Returns(false);
            var controller = new TodoController(mockServ.Object);
            controller.ControllerContext = FakeContext;

            // Act
            var actualResp = controller.Delete(todoId) as BadRequestResult;

            // Assert
            Assert.IsNotNull(actualResp);
        }

        [TestMethod]
        public void PutTest()
        {
            var userId = 1;
            var taskId = 1;
            var description = "AAA";
            var check = false;

            var todo = GetAllTodo.First(t => t.Id == taskId && t.UserId == userId);

            var todoVM = new TodoViewModel {
                Id = 1,
                Description = "AAA",
                Check = false
            };

            // Arrange
            var mockServ = new Mock<ITodoService>();
            mockServ.Setup(s => s.UpdateTodo(taskId, description, check, userId)).Returns(todo);
            var controller = new TodoController(mockServ.Object);
            controller.ControllerContext = FakeContext;

            // Act
            var actualResp = controller.Put(taskId, todoVM) as OkObjectResult;
            var actual = actualResp.Value as Todo;

            // Assert
            Assert.AreEqual(actual.Id, todo.Id);
            Assert.AreEqual(actual.Checked, todo.Checked);
            Assert.AreEqual(actual.UserId, todo.UserId);
            Assert.AreEqual(actual.Description, todo.Description);
        }

        [TestMethod]
        public void PutFailTest()
        {
            var taskId = 1;

            var todoVM = new TodoViewModel {
                Id = 1,
                Description = "AAA",
                Check = false
            };

            // Arrange
            var mockServ = new Mock<ITodoService>();
            var controller = new TodoController(mockServ.Object);
            controller.ControllerContext = FakeContext;

            // Act
            var actualResp = controller.Put(taskId, todoVM) as BadRequestResult;

            // Assert
            Assert.IsNotNull(actualResp);
        }
    }
}