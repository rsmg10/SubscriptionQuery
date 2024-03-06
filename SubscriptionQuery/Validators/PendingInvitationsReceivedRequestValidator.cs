using FluentValidation;
using SubscriptionQuery;

namespace Todo.Query.Validators
{
    public class PendingInvitationsReceivedRequestValidator : AbstractValidator<PendingInvitationsReceivedRequest>
    {
        public PendingInvitationsReceivedRequestValidator()
        {
            RuleFor(r => r.UserId)
                .Must(id => Guid.TryParse(id, out _)); 
        }
    }
 
}
