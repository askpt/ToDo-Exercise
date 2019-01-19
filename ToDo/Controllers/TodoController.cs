using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Interfaces;
using ToDo.ViewModels;

namespace ToDo.Controllers
{
    [Authorize]
    [Route("/api/[controller]")]
    public class TodoController : Controller
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return BadRequest();
            }

            var todo = _todoService.GetAllTodoByUser(userId.Value);
            return Ok(todo);
        }

        [HttpPost]
        public IActionResult Post([FromBody]TodoViewModel todoViewModel)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return BadRequest();
            }

            var id = _todoService.CreateTodo(todoViewModel.Description, userId.Value);

            return Ok(id);
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery]int id)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return BadRequest();
            }

            var isDeleted = _todoService.DeleteTodo(id, userId.Value);

            if (isDeleted)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }

        private int? GetUserId()
        {
            int? userId;
            try
            {
                userId = int.Parse(User.Identity.Name);
            }
            catch (Exception)
            {
                userId = null;
            }
            return userId;
        }
    }
}