namespace TodosBackend.API.Controllers.Todo.Contracts.Outgoing
{
    public class TodoItemContract
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? DueDate { get; set; }
    }
}
