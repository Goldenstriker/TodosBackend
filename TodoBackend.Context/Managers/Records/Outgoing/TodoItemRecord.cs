namespace TodoBackend.Context.Managers.Records.Outgoing
{
    public class TodoItemRecord
    {
        public Guid Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime? DueDate { get; set; }
    }
}
