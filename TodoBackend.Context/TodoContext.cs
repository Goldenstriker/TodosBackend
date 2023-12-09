using Microsoft.EntityFrameworkCore;
using TodoBackend.Context.Models;

namespace TodoBackend.Context
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }

        internal DbSet<Todo> Todos { get; set; }
    }
}