using TodoBackend.Context.Managers.Records.Incoming;

namespace TodosBackend.API.Controllers.Todo.Contracts.Incoming
{
    public class UpdateTodoContract
    {
        public string Title { get; set; }

        public string Description { get; set; }
    }
}
