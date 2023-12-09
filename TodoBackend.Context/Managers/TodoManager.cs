using Microsoft.EntityFrameworkCore;
using TodoBackend.Context.Managers.Records.Incoming;
using TodoBackend.Context.Models;

namespace TodoBackend.Context.Managers
{
    public class TodoManager : ITodoManager
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

                if (todo == null)
                {
                    return;
                }

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

        public async Task<List<Records.Outgoing.TodoRecord>> GetAllTodosAsync(int pageSize)
        {
            using TodoContext context = new TodoContext(contextOptions);

            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            IQueryable<Records.Outgoing.TodoRecord> todosQuery = from todo in context.Todos
                                                                 select new Records.Outgoing.TodoRecord()
                                                                 {
                                                                     Id = todo.Id,
                                                                     Title = todo.Title,
                                                                     Desciption = todo.Description,
                                                                     TodoItems = todo.TodoItems != null ? todo.TodoItems.Select(todoItem => new Records.Outgoing.TodoItemRecord()
                                                                     {
                                                                         Id = todoItem.Id,
                                                                         Title = todoItem.Title,
                                                                         Desciption = todoItem.Description,
                                                                         DueDate = todoItem.DueDate,
                                                                     }).ToList() : new List<Records.Outgoing.TodoItemRecord>()
                                                                 };
            return await todosQuery.ToListAsync();
        }
    }
}
