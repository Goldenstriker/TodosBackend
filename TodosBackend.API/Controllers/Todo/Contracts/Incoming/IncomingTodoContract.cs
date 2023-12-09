using TodoBackend.Context.Managers.Records.Incoming;

namespace TodosBackend.API.Controllers.Todo.Contracts.Incoming
{
    public class IncomingTodoContract
    {
        public Guid? Id { get; set; }

        public string Title { get; set; }

        public string Desciption { get; set; }

        public List<IncomingTodoItemContract> TodoItems { get; set; }
    }
}
