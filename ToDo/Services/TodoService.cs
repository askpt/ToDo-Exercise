using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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

        public Todo GetTodo(int id, int userId)
        {
            return GetAllTodo()?.FirstOrDefault(t => t.UserId == userId && t.Id == id);
        }

        public int CreateTodo(string description, int userId)
        {
            var todo = new Todo
            {
                Description = description,
                UserId = userId,
                LastUpdated = DateTime.UtcNow,
                Checked = false
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

        public Todo UpdateTodo(int id, string description, bool check, int userId)
        {
            var todo = GetTodo(id, userId);
            if (todo == null)
            {
                return null;
            }

            todo.Description = description;
            todo.Checked = check;
            todo.LastUpdated = DateTime.UtcNow;

            _context.Entry(todo).State = EntityState.Modified;
            _context.SaveChanges();

            return todo;
        }
    }
}