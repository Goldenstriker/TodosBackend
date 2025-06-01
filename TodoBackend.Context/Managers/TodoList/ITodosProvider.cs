namespace TodoBackend.Context.Managers.TodosList
{
    public interface ITodosProvider
    {
        Task<List<Records.Outgoing.TodoRecord>> GetAllTodosAsync(int? pageNumber, int? pageSize);

        Task<int> CountAllTodoAsync();
    }
}