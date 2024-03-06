using FluentValidation;
using SubscriptionQuery;

namespace Todo.Query.Validators
{
    public class PendingInvitationsSentRequestValidator : AbstractValidator<PendingInvitationsSentRequest>
    {
        public PendingInvitationsSentRequestValidator()
        {
            RuleFor(r => r.UserId)
                .Must(id => Guid.TryParse(id, out _));
            RuleFor(r => r.AccountId)
                .Must(id => Guid.TryParse(id, out _));
            RuleFor(r => r.SubscriptionId)
                .Must(id => Guid.TryParse(id, out _));
        }
    }
 
}
