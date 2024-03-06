using MediatR;

namespace SubscriptionQuery.QueryHandlers.PendingInvitationsReceived
{

    public record PendingInvitationsReceivedQuery(Guid UserId): IRequest<List<SentInvitation>>;


}
