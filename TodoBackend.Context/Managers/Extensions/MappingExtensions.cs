using TodoBackend.Context.Managers.Records.Incoming;
using TodoBackend.Context.Models;

namespace TodoBackend.Context.Managers
{
    internal static class MappingExtensions
    {
        internal static Todo CreateOrUpdateTodo(this TodoContext context, TodoRecord todoRecord, Todo todo = null)
        {
            if (todo == null)
            {
                todo = new Todo()
                {
                    Id = Guid.NewGuid(),
                };

                context.Todos.Add(todo);
            }

            todo.Title = todoRecord.Title;
            todo.Description = todoRecord.Description;
            return todo;
        }

        internal static TodoItem CreateOrUpdateTodoItem(this TodoContext context, TodoItemRecord todoItemRecord, Guid todoId, TodoItem todoItem = null)
        {
            if (todoItem == null)
            {
                todoItem = new TodoItem()
                {
                    Id = Guid.NewGuid(),
                    TodoId = todoId
                };

                context.TodoItems.Add(todoItem);
            }

            todoItem.Title = todoItemRecord.Title;
            todoItem.Description = todoItemRecord.Description;
            todoItem.DueDate = todoItemRecord.DueDate;
            return todoItem;
        }
    }
}
