using System;

namespace ToDo.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool Checked { get; set; }
    }
}