namespace TodosBackend.API.Controllers.Todo.Contracts.Outgoing
{
    public class TodosListContract
    {
        public List<TodoContract> Todos { get; set; }

        public int Count { get; set; }

        public int CurrentPage { get; set; }
    }
}
