using System.Collections.Generic;
using ToDo.Models;

namespace ToDo.Interfaces
{
    public interface ITodoService
    {
        IEnumerable<Todo> GetAllTodo();

        IEnumerable<Todo> GetAllTodoByUser(int userId);

        int CreateTodo(string description, int userId);

        bool DeleteTodo(int todoId, int userId);

        Todo GetTodo(int id, int userId);

        Todo UpdateTodo(int id, string description, bool check, int userId);
    }
}