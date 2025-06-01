using Microsoft.EntityFrameworkCore;

namespace TodoBackend.Context.Managers.TodosList
{
    public class TodosProvider : ITodosProvider
    {
        private readonly DbContextOptions<TodoContext> options;

        public TodosProvider(DbContextOptions<TodoContext> options)
        {
            this.options = options;
        }

        public async Task<List<Records.Outgoing.TodoRecord>> GetAllTodosAsync(int? pageNumber, int? pageSize)
        {
            using TodoContext context = new TodoContext(options);

            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            IQueryable<Records.Outgoing.TodoRecord> todosQuery = from todo in context.Todos
                                                                 orderby todo.DateCreated descending
                                                                 select new Records.Outgoing.TodoRecord()
                                                                 {
                                                                     Id = todo.Id,
                                                                     Title = todo.Title,
                                                                     Description = todo.Description
                                                                 };


            if (pageSize.HasValue)
            {
                int fetchPageNumber = pageNumber ?? 1;

                todosQuery = todosQuery
                               .Skip(pageSize.Value * (fetchPageNumber - 1))
                               .Take(pageSize.Value);
            }

            List<Records.Outgoing.TodoRecord> todoRecords = await todosQuery.ToListAsync();

            List<Guid> todoIds = todoRecords.Select(record => record.Id).ToList();

            Dictionary<Guid, List<Records.Internal.TodoItemRecord>> todoItemRecordsTodoIdLkp = (await (from todoItem in context.TodoItems
                                                                                                       where todoIds.Contains(todoItem.TodoId)
                                                                                                       select new Records.Internal.TodoItemRecord()
                                                                                                       {
                                                                                                           Id = todoItem.Id,
                                                                                                           TodoId = todoItem.TodoId,
                                                                                                           Title = todoItem.Title,
                                                                                                           Description = todoItem.Description,
                                                                                                           DueDate = todoItem.DueDate,
                                                                                                       }).ToListAsync())
                                                                                                .GroupBy(x => x.TodoId)
                                                                                                .ToDictionary(x => x.Key, y => y.ToList());

            foreach (Records.Outgoing.TodoRecord item in todoRecords)
            {
                List<Records.Internal.TodoItemRecord> todoItemRecords = todoItemRecordsTodoIdLkp.GetValueOrDefault(item.Id, new List<Records.Internal.TodoItemRecord>());
                if (todoItemRecords.Any())
                {
                    item.TodoItems = todoItemRecords.Select(todoItem => new Records.Outgoing.TodoItemRecord()
                    {
                        Id = todoItem.Id,
                        Title = todoItem.Title,
                        Description = todoItem.Description,
                        DueDate = todoItem.DueDate,
                    }).ToList();
                }
            }

            return todoRecords;
        }

        public async Task<int> CountAllTodoAsync()
        {
            using TodoContext context = new TodoContext(options);
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return await context.Todos.CountAsync();
        }
    }
}