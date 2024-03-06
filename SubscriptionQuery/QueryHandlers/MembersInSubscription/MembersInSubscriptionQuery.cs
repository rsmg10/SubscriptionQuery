using MediatR;
using SubscriptionQuery.QueryHandlers.MembersInSubscription;

namespace SubscriptionQuery.Extensions
{ 
        public record MembersInSubscriptionQuery(Guid AccountId,
                                                 Guid SubscriptionId,
                                                 Guid UserId): IRequest<MembersInSubscriptionResponse>;
     
  
}
