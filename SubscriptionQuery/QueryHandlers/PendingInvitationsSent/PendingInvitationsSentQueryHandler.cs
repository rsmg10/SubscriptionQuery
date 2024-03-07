using MediatR;
using Microsoft.EntityFrameworkCore;
using SubscriptionQuery.Extensions;
using SubscriptionQuery.Infrastructure.Presistance;
using SubscriptionQuery.QueryHandlers.PendingInvitationsReceived;
using SubscriptionQuery.QueryHandlers.PendingInvitationsSent;

namespace SubscriptionQuery.QueryHandlers.PendingInvitationsSent
{
    public class PendingInvitationsSentQueryHandler : IRequestHandler<PendingInvitationsSentQuery, List<SentInvitation>>
    {
        private readonly ApplicationDatabase _db;

        public PendingInvitationsSentQueryHandler(ApplicationDatabase db)
        {
            _db = db;
        }
        public async Task<List<SentInvitation>> Handle(PendingInvitationsSentQuery request, CancellationToken cancellationToken)
        {
            var userSubscriptions = await _db.Subscriptions
          .Include(sub => sub.Invitations)
          .Where(sub => sub.OwnerId == request.UserId
              && !sub.IsJoined && sub.Invitations.MaxBy(x => x.DateCreated) == null ? false : (sub.Invitations.MaxBy(x => x.DateCreated).Status == Domain.Enums.InvitationStatus.Pending))
          .Select(sub => sub.Invitations.MaxBy(x => x.DateCreated))
          .ToListAsync(cancellationToken: cancellationToken);

            return userSubscriptions.Select(inv => new SentInvitation("subscriptionName",
                                                                     inv.Id,
                                                                     inv.UserSubscription.OwnerId,
                                                                     inv.UserSubscription.OwnerId,
                                                                     inv.DateCreated))
                .ToList();
        }
    }
}
