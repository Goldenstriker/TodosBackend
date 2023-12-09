namespace TodoBackend.Context.Managers.Records.Incoming
{
    public class TodoRecord
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Desciption { get; set; }

        public List<TodoItemRecord> TodoItems { get; set; }
    }
}
