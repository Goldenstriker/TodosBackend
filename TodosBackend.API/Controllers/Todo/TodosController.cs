using Microsoft.AspNetCore.Mvc;
using TodoBackend.Context.Managers;
using TodosBackend.API.Controllers.Todo.Contracts.Incoming;
using TodosBackend.API.Controllers.Todo.Contracts.Outgoing;

namespace TodosBackend.API.Controllers.Todo
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoManager todoManager;

        public TodosController(ITodoManager todoManager)
        {
            this.todoManager = todoManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<Contracts.Outgoing.TodoContract>>> GetAllTodos([FromQuery] int? pageNumber = null, [FromQuery] int? pageSize = 10)
        {
            List<TodoBackend.Context.Managers.Records.Outgoing.TodoRecord> todoRecords = await todoManager.GetAllTodosAsync(pageNumber, pageSize);

            List<Contracts.Outgoing.TodoContract> todos = todoRecords.Select(x => new Contracts.Outgoing.TodoContract()
            {
                Id = x.Id,
                Desciption = x.Desciption,
                Title = x.Title,
                TodoItems = x.TodoItems?.Select(y => new TodoItemContract()
                {
                    Id = y.Id,
                    Desciption = y.Desciption,
                    Title = y.Title,
                    DueDate = y.DueDate,
                }).ToList()
            }).ToList();

            return Ok(todos);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTodo([FromBody] CreateTodoContract todoContract)
        {
            TodoBackend.Context.Managers.Records.Incoming.TodoRecord todoRecord = new TodoBackend.Context.Managers.Records.Incoming.TodoRecord()
            {
                Desciption = todoContract.Desciption,
                Title = todoContract.Title,
                TodoItems = todoContract.TodoItems?.Select(x => new TodoBackend.Context.Managers.Records.Incoming.TodoItemRecord()
                {
                    Desciption = x.Desciption,
                    Title = x.Title,
                    DueDate = x.DueDate,
                }).ToList()
            };

            await todoManager.CreateTodoAsync(todoRecord);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> CreateTodo(Guid id, [FromBody] UpdateTodoContract todoContract)
        {
            TodoBackend.Context.Managers.Records.Incoming.TodoRecord todoRecord = new TodoBackend.Context.Managers.Records.Incoming.TodoRecord()
            {
                Desciption = todoContract.Desciption,
                Title = todoContract.Title
            };

            await todoManager.UpdateTodoAsync(id, todoRecord);

            return Ok();
        }

        [HttpPut("{todoId}/todoItem/{todoItemId}")]
        public async Task<ActionResult> CreateTodo(Guid todoId, Guid todoItemId, [FromBody] UpdateTodoItemContract todoItemContract)
        {
            TodoBackend.Context.Managers.Records.Incoming.TodoItemRecord todoRecord = new TodoBackend.Context.Managers.Records.Incoming.TodoItemRecord()
            {
                Desciption = todoItemContract.Desciption,
                Title = todoItemContract.Title,
                DueDate = todoItemContract.DueDate
            };

            await todoManager.UpdateTodoItemAsync(todoId, todoItemId, todoRecord);

            return Ok();
        }
    }
}

