using TodoBackend.Context.Managers.Records.Incoming;

namespace TodoBackend.Context.Managers
{
    internal interface ITodoManager
    {
        Task CreateTodoAsync(TodoRecord todoRecord);

        Task UpdateTodoAsync(Guid todoId, TodoRecord todoRecord);

        Task DeleteTodoAsync(Guid todoId);

        Task DeleteTodoItemAsync(Guid todoId);
    }
}
