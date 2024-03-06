using MediatR;
using Microsoft.EntityFrameworkCore;
using SubscriptionQuery.Infrastructure.Presistance;
using SubscriptionQuery.QueryHandlers.MembersInSubscription;

namespace SubscriptionQuery.QueryHandlers.PendingInvitationsReceived
{
    public class PendingInvitationsReceivedQueryHandler : IRequestHandler<PendingInvitationsReceivedQuery, List<SentInvitation>>
    {
        private readonly ApplicationDatabase _db;

        public PendingInvitationsReceivedQueryHandler(ApplicationDatabase db)
        {
            _db = db;
        }

        public async Task<List<SentInvitation>> Handle(PendingInvitationsReceivedQuery request, CancellationToken cancellationToken)
        {

            var userSubscriptions = await _db.Subscriptions
              .Include(sub => sub.Invitations)
              .Where(sub => sub.MemberId == request.UserId
                  && !sub.IsJoined 
                  && sub.Invitations.MaxBy(x => x.DateCreated) == null ? false : (sub.Invitations.MaxBy(x => x.DateCreated)!.Status == Domain.Enums.InvitationStatus.Pending))
               .Select(sub => sub.Invitations.MaxBy(x => x.DateCreated))
              .ToListAsync(cancellationToken: cancellationToken);

            return userSubscriptions.Select(inv=> new SentInvitation("subscriptionName",
                                                                     inv.Id,
                                                                     inv.UserSubscription.OwnerId,
                                                                     inv.UserSubscription.OwnerId,
                                                                     inv.DateCreated))
                .ToList();
        }
    }
}
