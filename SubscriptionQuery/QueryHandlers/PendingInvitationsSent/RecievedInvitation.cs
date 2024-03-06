namespace SubscriptionQuery.QueryHandlers.PendingInvitationsReceived
{
    public record SentInvitation(
        string SubscriptionName,
        Guid Id,
        Guid SentBy,
        Guid OwnerId,
        DateTime CreatedAt);
 
}
