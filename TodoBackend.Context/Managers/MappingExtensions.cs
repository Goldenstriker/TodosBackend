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
            todo.Description = todoRecord.Desciption;

            if (todoRecord.TodoItems != null && todoRecord.TodoItems.Any())
            {
                foreach (TodoItemRecord item in todoRecord.TodoItems)
                {
                    TodoItem todoItem = todo.TodoItems?.SingleOrDefault(x => x.Id == item.Id && x.TodoId == todo.Id);
                    context.CreateOrUpdateTodoItem(item, todo.Id, todoItem);
                }
            }

            return todo;
        }

        private static TodoItem CreateOrUpdateTodoItem(this TodoContext context, TodoItemRecord todoItemRecord, Guid todoId, TodoItem todoItem)
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
            todoItem.Description = todoItemRecord.Desciption;
            todoItem.DueDate = todoItemRecord.DueDate;
            return todoItem;
        }
    }
}
