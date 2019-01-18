using System.Collections.Generic;
using System.Linq;
using ToDo.Interfaces;
using ToDo.Models;

namespace ToDo.Services
{
    public class TodoService : ITodoService
    {
        private IList<Todo> TodosRepository
        {
            get
            {
                return new List<Todo>
                {
                    new Todo { Id = 1, Description = "AAA", UserId = 1 }
                };
            }
        }

        public IEnumerable<Todo> GetAllTodo()
        {
            return TodosRepository;
        }

        public IEnumerable<Todo> GetAllTodoByUser(int userId)
        {
            return GetAllTodo()?.Where(t => t.UserId == userId);
        }
    }
}