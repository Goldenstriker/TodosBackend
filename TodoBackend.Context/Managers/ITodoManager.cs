﻿using TodoBackend.Context.Managers.Records.Incoming;

namespace TodoBackend.Context.Managers
{
    public interface ITodoManager
    {
        Task<List<Records.Outgoing.TodoRecord>> GetAllTodosAsync(int? pageNumber, int? pageSize);

        Task CreateTodoAsync(TodoRecord todoRecord);

        Task UpdateTodoAsync(Guid todoListId, TodoRecord todoRecord);

        Task UpdateTodoItemAsync(Guid todoListId, Guid todoListItemId, TodoItemRecord todoItemRecord);

        Task DeleteTodoAsync(Guid todoListId);

        Task DeleteTodoItemAsync(Guid todoListItemId);

        Task<int> CountAllTodoAsync();
    }
}
