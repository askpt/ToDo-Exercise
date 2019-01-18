using System.Collections.Generic;
using System.Linq;
using ToDo.Interfaces;
using ToDo.Models;

namespace ToDo.Services
{
    public class TodoService : ITodoService
    {
        private readonly TodoContext _context;

        public TodoService(TodoContext context)
        {
            _context = context;
        }

        public IEnumerable<Todo> GetAllTodo()
        {
            return _context.Todos;
        }

        public IEnumerable<Todo> GetAllTodoByUser(int userId)
        {
            return GetAllTodo()?.Where(t => t.UserId == userId);
        }

        public int CreateTodo(string description, int userId)
        {
            var todo = new Todo
            {
                Description = description,
                UserId = userId
            };

            _context.Todos.Add(todo);
            _context.SaveChanges();
            return todo.Id;
        }

        public bool DeleteTodo(int todoId, int userId)
        {
            var todo = _context.Todos.FirstOrDefault(t => t.Id == todoId && t.UserId == userId);
            if (todo == null)
            {
                return false;
            }

            _context.Todos.Remove(todo);
            _context.SaveChanges();

            return true;
        }
    }
}