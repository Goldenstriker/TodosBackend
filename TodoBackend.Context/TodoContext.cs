using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TodoBackend.Context.Models;
using Utility;

namespace TodoBackend.Context
{
    public class TodoContext : IdeaWareHouseContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }

        internal DbSet<Todo> Todos { get; set; }

        internal DbSet<TodoItem> TodoItems { get; set; }
    }
}