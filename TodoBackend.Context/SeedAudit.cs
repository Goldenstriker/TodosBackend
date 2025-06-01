using TodoBackend.Context.Models;
using Utility.Models;

namespace TodoBackend.Context;

public class SeedAudit
{
    public static string TodoSubscriptionName { get { return "TodoSubscription"; } }

    public static void SeedDatabase(TodoContext todoContext)
    {
        if (todoContext.Database.CanConnect())
        {
            using var transaction = todoContext.Database.BeginTransaction();
            SeedSubscribers(todoContext);
            SeedSubscriptions(todoContext);

            transaction.Commit();
        }
    }

    protected static void SeedSubscribers(TodoContext context)
    {
        List<string> subscribers = new List<string>();
        subscribers.Add(TodoSubscriptionName);
        context.AddSubcribers(subscribers);
    }

    protected static void SeedSubscriptions(TodoContext context)
    {
        List<Tuple<string, string, Guid>> subscriptions = new List<Tuple<string, string, Guid>>();
        {
            AuditSubscriber subscriber = context.GetSubscriber(TodoSubscriptionName);

            if (subscriber != null)
            {
                subscriptions.Add(new Tuple<string, string, Guid>(nameof(Todo), nameof(Todo.Title), subscriber.Id));
                subscriptions.Add(new Tuple<string, string, Guid>(nameof(Todo), nameof(Todo.Description), subscriber.Id));
                subscriptions.Add(new Tuple<string, string, Guid>(nameof(TodoItem), nameof(TodoItem.TodoId), subscriber.Id));
                subscriptions.Add(new Tuple<string, string, Guid>(nameof(TodoItem), nameof(TodoItem.Title), subscriber.Id));
                subscriptions.Add(new Tuple<string, string, Guid>(nameof(TodoItem), nameof(TodoItem.Description), subscriber.Id));
                subscriptions.Add(new Tuple<string, string, Guid>(nameof(TodoItem), nameof(TodoItem.DueDate), subscriber.Id));
            }

        }

        context.AddSubscriptions(subscriptions);
        context.SaveChanges();
    }

}
