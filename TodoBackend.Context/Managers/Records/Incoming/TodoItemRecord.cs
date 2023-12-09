namespace TodoBackend.Context.Managers.Records.Incoming
{
    public class TodoItemRecord
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Desciption { get; set; }

        public DateTime? DueDate { get; set; }
    }
}
