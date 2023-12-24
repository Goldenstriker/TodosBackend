using Microsoft.AspNetCore.Mvc;
using TodoBackend.Context.Managers;
using TodosBackend.API.Controllers.Todo.Contracts.Incoming;
using TodosBackend.API.Controllers.Todo.Contracts.Outgoing;

namespace TodosBackend.API.Controllers.Todo
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListsController : ControllerBase
    {
        private readonly ITodoManager todoManager;

        public TodoListsController(ITodoManager todoManager)
        {
            this.todoManager = todoManager;
        }

        [HttpGet]
        public async Task<ActionResult<Contracts.Outgoing.TodosListContract>> GetAllTodoListsAsync([FromQuery] int? pageNumber = null, [FromQuery] int? pageSize = 10)
        {
            List<TodoBackend.Context.Managers.Records.Outgoing.TodoRecord> todoRecords = new List<TodoBackend.Context.Managers.Records.Outgoing.TodoRecord>();
            int todosCount = 0;

            await Task.WhenAll(
                 Task.Run(async () => { todoRecords = await todoManager.GetAllTodosAsync(pageNumber, pageSize); }),
                 Task.Run(async () => { todosCount = await todoManager.CountAllTodoAsync(); })
             );

            Contracts.Outgoing.TodosListContract todosListContract = new TodosListContract()
            {
                CurrentPage = pageNumber ?? 1,
                Count = todosCount,
                Todos = todoRecords.Select(x => new Contracts.Outgoing.TodoContract()
                {
                    Id = x.Id,
                    Description = x.Description,
                    Title = x.Title,
                    TodoItems = x.TodoItems?.Select(y => new TodoItemContract()
                    {
                        Id = y.Id,
                        Description = y.Description,
                        Title = y.Title,
                        DueDate = y.DueDate,
                    }).ToList()
                }).ToList()
            };

            return Ok(todosListContract);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTodoListAsync([FromBody] CreateTodoContract todoContract)
        {
            TodoBackend.Context.Managers.Records.Incoming.TodoRecord todoRecord = new TodoBackend.Context.Managers.Records.Incoming.TodoRecord()
            {
                Description = todoContract.Description,
                Title = todoContract.Title,
                TodoItems = todoContract.TodoItems?.Select(x => new TodoBackend.Context.Managers.Records.Incoming.TodoItemRecord()
                {
                    Description = x.Description,
                    Title = x.Title,
                    DueDate = x.DueDate,
                }).ToList()
            };

            await todoManager.CreateTodoAsync(todoRecord);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTodoListAsync(Guid id, [FromBody] UpdateTodoContract todoContract)
        {
            TodoBackend.Context.Managers.Records.Incoming.TodoRecord todoRecord = new TodoBackend.Context.Managers.Records.Incoming.TodoRecord()
            {
                Description = todoContract.Description,
                Title = todoContract.Title
            };

            await todoManager.UpdateTodoAsync(id, todoRecord);

            return Ok();
        }
    }
}

