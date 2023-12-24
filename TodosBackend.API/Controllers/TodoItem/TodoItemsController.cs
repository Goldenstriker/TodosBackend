using Microsoft.AspNetCore.Mvc;
using TodoBackend.Context.Managers;
using TodosBackend.API.Controllers.Todo.Contracts.Incoming;

namespace TodosBackend.API.Controllers.TodoItem
{
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoManager todoManager;

        public TodoItemsController(ITodoManager todoManager)
        {
            this.todoManager = todoManager;
        }

        [HttpPut("api/todolists/{todoListId}/todoItem/{todoItemId}")]
        public async Task<ActionResult> CreateTodo(Guid todoListId, Guid todoItemId, [FromBody] UpdateTodoItemContract todoItemContract)
        {
            TodoBackend.Context.Managers.Records.Incoming.TodoItemRecord todoRecord = new TodoBackend.Context.Managers.Records.Incoming.TodoItemRecord()
            {
                Description = todoItemContract.Description,
                Title = todoItemContract.Title,
                DueDate = todoItemContract.DueDate
            };

            await todoManager.UpdateTodoItemAsync(todoListId, todoItemId, todoRecord);

            return Ok();
        }
    }
}
