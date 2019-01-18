using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Interfaces;

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
            int userId;
            try
            {
                userId = int.Parse(User.Identity.Name);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            var todo = _todoService.GetAllTodoByUser(userId);
            return Ok(todo);
        }
    }
}