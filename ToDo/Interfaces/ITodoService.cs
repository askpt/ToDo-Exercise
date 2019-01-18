using System.Collections.Generic;
using ToDo.Models;

namespace ToDo.Interfaces
{
    public interface ITodoService
    {
        IEnumerable<Todo> GetAllTodo();

        IEnumerable<Todo> GetAllTodoByUser(int userId);
    }
}