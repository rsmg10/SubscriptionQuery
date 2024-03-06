using MediatR;
using SubscriptionQuery.QueryHandlers.PendingInvitationsReceived;

namespace SubscriptionQuery.Extensions
{
 
    public record PendingInvitationsSentQuery(
        Guid AccountId,
        Guid SubscriptionId,
        Guid UserId): IRequest<List<SentInvitation>>;

     
  
}
