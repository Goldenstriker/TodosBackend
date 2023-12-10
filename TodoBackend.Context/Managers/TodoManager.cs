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
                Todo todo = context.CreateOrUpdateTodo(todoRecord);

                if (todoRecord.TodoItems != null && todoRecord.TodoItems.Any())
                {
                    foreach (TodoItemRecord item in todoRecord.TodoItems)
                    {
                        context.CreateOrUpdateTodoItem(item, todo.Id);
                    }
                }

                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateTodoAsync(Guid todoId, TodoRecord todoRecord)
        {
            using (TodoContext context = new TodoContext(contextOptions))
            {
                Todo? todo = await context.Todos.SingleOrDefaultAsync(x => x.Id == todoId);

                if (todo == null)
                {
                    return;
                }

                context.CreateOrUpdateTodo(todoRecord, todo);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateTodoItemAsync(Guid todoId, Guid todoItemId, TodoItemRecord todoItemRecord)
        {
            using (TodoContext context = new TodoContext(contextOptions))
            {
                TodoItem? todoItem = await context.TodoItems.SingleOrDefaultAsync(x => x.TodoId == todoId && x.Id == todoItemId);

                if (todoItem == null)
                {
                    return;
                }

                context.CreateOrUpdateTodoItem(todoItemRecord, todoId, todoItem);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteTodoAsync(Guid todoId)
        {
            using (TodoContext context = new TodoContext(contextOptions))
            {
                Todo? todo = await context.Todos.SingleOrDefaultAsync(x => x.Id == todoId);

                if (todo != null)
                {
                    context.Todos.Remove(todo);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteTodoItemAsync(Guid todoItemId)
        {
            using (TodoContext context = new TodoContext(contextOptions))
            {
                TodoItem? todoItem = await context.TodoItems.SingleOrDefaultAsync(x => x.Id == todoItemId);
                if (todoItem != null)
                {
                    context.TodoItems.Remove(todoItem);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<List<Records.Outgoing.TodoRecord>> GetAllTodosAsync(int? pageNumber, int? pageSize)
        {
            using TodoContext context = new TodoContext(contextOptions);

            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            IQueryable<Records.Outgoing.TodoRecord> todosQuery = from todo in context.Todos
                                                                 orderby todo.DateCreated descending
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


            if (pageNumber.HasValue && pageSize.HasValue)
            {
                todosQuery = todosQuery
                               .Skip(pageSize.Value * pageNumber.Value)
                               .Take(pageSize.Value);
            }

            return await todosQuery.ToListAsync();
        }
    }
}
