namespace TodoBackend.Context.Managers.Records.Outgoing
{
    public class TodoRecord
    {
        public TodoRecord()
        {
            TodoItems = new List<TodoItemRecord>();
        }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public List<TodoItemRecord> TodoItems { get; set; }
    }
}
