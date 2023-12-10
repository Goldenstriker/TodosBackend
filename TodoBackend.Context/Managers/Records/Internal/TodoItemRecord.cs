namespace TodoBackend.Context.Managers.Records.Internal
{
    internal class TodoItemRecord
    {
        public Guid Id { get; set; }

        public Guid TodoId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? DueDate { get; set; }
    }
}
