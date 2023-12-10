namespace TodosBackend.API.Controllers.Todo.Contracts.Outgoing
{
    public class TodoContract
    {
        public TodoContract()
        {
            TodoItems = new List<TodoItemContract>();
        }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public List<TodoItemContract> TodoItems { get; set; }
    }
}
