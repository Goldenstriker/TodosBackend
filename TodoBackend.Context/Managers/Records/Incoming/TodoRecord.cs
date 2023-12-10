namespace TodoBackend.Context.Managers.Records.Incoming
{
    public class TodoRecord
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public List<TodoItemRecord> TodoItems { get; set; }
    }
}
