using TodoBackend.Context.Managers.Records.Incoming;

namespace TodoBackend.Context.Managers
{
    public interface ITodoManager
    {
        Task<List<Records.Outgoing.TodoRecord>> GetAllTodosAsync(int pageSize);

        Task CreateTodoAsync(TodoRecord todoRecord);

        Task UpdateTodoAsync(Guid todoId, TodoRecord todoRecord);

        Task DeleteTodoAsync(Guid todoId);

        Task DeleteTodoItemAsync(Guid todoId);
    }
}
