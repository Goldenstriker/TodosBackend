namespace TodoBackend.Context.Managers
{
    internal interface ITodoManager
    {
        Task CreateOrUpdateTodoAsync();
    }
}
