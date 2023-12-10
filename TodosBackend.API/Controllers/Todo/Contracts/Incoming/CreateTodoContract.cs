using TodoBackend.Context.Managers.Records.Incoming;

namespace TodosBackend.API.Controllers.Todo.Contracts.Incoming
{
    public class CreateTodoContract
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public List<UpdateTodoItemContract> TodoItems { get; set; }
    }
}
