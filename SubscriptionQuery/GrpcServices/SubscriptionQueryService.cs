using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using SubscriptionQuery.Extensions;

namespace SubscriptionQuery.GrpcServices
{
    public class SubscriptionQueryService : SubscriptionQuery.SubscriptionQueryBase
    {
        private readonly IMediator _mediator;

        public SubscriptionQueryService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<MembersInSubscriptionResponse> GetMembersInSubscription(MembersInSubscriptionRequest request, ServerCallContext context)
        {
            return await _mediator.Send(request.ToQuery());
        }
        public override async Task<JoinedSubscriptionResponse> GetMyJoinedSubscription(JoinedSubscriptionRequest request, ServerCallContext context)
        {
            return await _mediator.Send(request.ToQuery());
        }

        public override Task<PendingInvitationsReceivedResponse> PendingInvitationsReceived(PendingInvitationsReceivedRequest request, ServerCallContext context)
        {
            return base.PendingInvitationsReceived(request, context);
        }
        public override Task<PendingInvitationsSentResponse> PendingInvitationsSent(PendingInvitationsSentRequest request, ServerCallContext context)
        {
            return base.PendingInvitationsSent(request, context);
        }

        public override Task Notifications(Empty request, IServerStreamWriter<NotificationResponse> responseStream, ServerCallContext context)
        {
            return base.Notifications(request, responseStream, context);

        }
    }
}
