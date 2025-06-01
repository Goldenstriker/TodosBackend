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

        public async Task UpdateTodoItemAsync(Guid todoListId, Guid todoItemId, TodoItemRecord todoItemRecord)
        {
            using (TodoContext context = new TodoContext(contextOptions))
            {
                TodoItem? todoItem = await context.TodoItems.SingleOrDefaultAsync(x => x.Id == todoItemId && x.TodoId == todoListId);

                if (todoItem == null)
                {
                    return;
                }

                context.CreateOrUpdateTodoItem(todoItemRecord, todoListId, todoItem);
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
    }
}
