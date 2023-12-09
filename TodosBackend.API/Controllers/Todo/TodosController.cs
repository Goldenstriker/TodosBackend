using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using TodoBackend.Context.Managers;
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
        public async Task<ActionResult<List<Contracts.Outgoing.TodoContract>>> GetAllTodos()
        {
            List<TodoBackend.Context.Managers.Records.Outgoing.TodoRecord> todoRecords = await todoManager.GetAllTodosAsync(0);

            List<Contracts.Outgoing.TodoContract> todos = todoRecords.Select(x => new Contracts.Outgoing.TodoContract()
            {
                Id = x.Id,
                Desciption = x.Desciption,
                Title = x.Title,
                TodoItems = x.TodoItems != null ? x.TodoItems.Select(y => new Contracts.Outgoing.TodoItemContract()
                {
                    Id = y.Id,
                    Desciption = y.Desciption,
                    Title = y.Title,
                    DueDate = y.DueDate,
                }).ToList() : null
            }).ToList();

            return Ok(todos);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTodo([FromBody] Contracts.Incoming.IncomingTodoContract todoContract)
        {
            TodoBackend.Context.Managers.Records.Incoming.TodoRecord todoRecord = new TodoBackend.Context.Managers.Records.Incoming.TodoRecord()
            {
                Id = todoContract.Id,
                Desciption = todoContract.Desciption,
                Title = todoContract.Title,
                TodoItems = todoContract.TodoItems != null ? todoContract.TodoItems.Select(x => new TodoBackend.Context.Managers.Records.Incoming.TodoItemRecord()
                {
                    Id = x.Id,
                    Desciption = x.Desciption,
                    Title = x.Title,
                    DueDate = x.DueDate,
                }).ToList() : null
            };

            await todoManager.CreateTodoAsync(todoRecord);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> CreateTodo(Guid id, [FromBody] Contracts.Incoming.IncomingTodoContract todoContract)
        {
            TodoBackend.Context.Managers.Records.Incoming.TodoRecord todoRecord = new TodoBackend.Context.Managers.Records.Incoming.TodoRecord()
            {
                Id = todoContract.Id,
                Desciption = todoContract.Desciption,
                Title = todoContract.Title,
                TodoItems = todoContract.TodoItems != null ? todoContract.TodoItems.Select(x => new TodoBackend.Context.Managers.Records.Incoming.TodoItemRecord()
                {
                    Id = x.Id,
                    Desciption = x.Desciption,
                    Title = x.Title,
                    DueDate = x.DueDate,
                }).ToList() : null
            };

            await todoManager.UpdateTodoAsync(id, todoRecord);

            return Ok();
        }
    }
}

