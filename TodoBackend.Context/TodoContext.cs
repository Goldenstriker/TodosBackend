using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TodoBackend.Context.Models;

namespace TodoBackend.Context
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }

        internal DbSet<Todo> Todos { get; set; }

        internal DbSet<TodoItem> TodoItems { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            IEnumerable<EntityEntry<IBaseModel>> entityEntries = ChangeTracker.Entries<IBaseModel>();

            foreach (EntityEntry<IBaseModel> entityEntry in entityEntries)
            {
                switch (entityEntry.State)
                {
                    case EntityState.Added:
                        entityEntry.Entity.DateCreated = DateTime.UtcNow;
                        entityEntry.Entity.DateModified = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entityEntry.Entity.DateModified = DateTime.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}