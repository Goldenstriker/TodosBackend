using Microsoft.EntityFrameworkCore;
using TodoBackend.Context.Managers.Records.Incoming;
using TodoBackend.Context.Models;

namespace TodoBackend.Context.Managers
{
    internal class TodoManager : ITodoManager
    {
        private readonly DbContextOptions<TodoContext> contextOptions;

        public TodoManager(DbContextOptions<TodoContext> contextOptions)
        {
            this.contextOptions = contextOptions;
        }

        public async Task CreateTodoAsync(TodoRecord todoRecord)
        {
            using (TodoContext context = new TodoContext(contextOptions))
            {
                context.CreateOrUpdateTodo(todoRecord);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateTodoAsync(Guid todoId, TodoRecord todoRecord)
        {
            using (TodoContext context = new TodoContext(contextOptions))
            {
                Todo todo = await context.Todos.SingleOrDefaultAsync(x => x.Id == todoId);
                context.CreateOrUpdateTodo(todoRecord, todo);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteTodoAsync(Guid todoId)
        {
            using (TodoContext context = new TodoContext(contextOptions))
            {
                Todo todo = await context.Todos.SingleOrDefaultAsync(x => x.Id == todoId);
                context.Todos.Remove(todo);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteTodoItemAsync(Guid todoItemId)
        {
            using (TodoContext context = new TodoContext(contextOptions))
            {
                TodoItem todoItem = await context.TodoItems.SingleOrDefaultAsync(x => x.Id == todoItemId);
                context.TodoItems.Remove(todoItem);
                await context.SaveChangesAsync();
            }
        }

    }
}
