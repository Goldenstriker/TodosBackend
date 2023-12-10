namespace TodosBackend.API.Controllers.Todo.Contracts.Incoming
{
    public class UpdateTodoItemContract
    {
        public string Title { get; set; }

        public string Desciption { get; set; }

        public DateTime? DueDate { get; set; }
    }
}
