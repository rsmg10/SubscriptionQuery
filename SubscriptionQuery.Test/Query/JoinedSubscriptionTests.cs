using Xunit.Abstractions;
using Microsoft.AspNetCore.Mvc.Testing;
using SubscriptionQuery.Test.Helpers;
using SubscriptionQuery.Domain.Enums;
using SubscriptionQuery.Infrastructure.Presistance.Entities;
using Microsoft.Azure.Amqp.Transport;
namespace SubscriptionQuery.Test;

public class JoinedSubscriptionTests : TestBase, IClassFixture<WebApplicationFactory<Program>>
{
    public JoinedSubscriptionTests(WebApplicationFactory<Program> factory, ITestOutputHelper testOutput) : base(factory, testOutput)
    {
    }


    [Fact]
    public async Task GetJoinedSubscriptions()
    {
        var aggregateId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var invitationId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        var memberId = Guid.NewGuid();
        var rand = new Random();
        List<UserSubscription> users = new List<UserSubscription>();
        for (int i = 0; i < 10; i++)
        {
            var subscription = Guid.NewGuid();

            UserSubscription userSubscription = GetUserSubscription(Guid.NewGuid(), userId, subscription, Guid.NewGuid(),
                                                                    accountId, memberId, rand.Next(0, 9), (Permissions)rand.Next(0, 3),
                                                                    InvitationStatus.Accepted, true);

            UserSubscription userSubscriptionOther = GetUserSubscription(Guid.NewGuid(), userId, subscription, Guid.NewGuid(),
                                                               accountId, Guid.NewGuid(), rand.Next(0, 9), (Permissions)rand.Next(0, 3),
                                                               InvitationStatus.Accepted, true);
            users.Add(userSubscription);
            users.Add(userSubscriptionOther);

        }
        await Database.Subscriptions.AddRangeAsync(users);
        await Database.SaveChangesAsync();


        var client = new SubscriptionQuery.SubscriptionQueryClient(CreateGrpcChannel(Factory));
        var joinedSubscriptions = await client.GetMyJoinedSubscriptionAsync(new JoinedSubscriptionRequest
        {
            UserId = memberId.ToString(),
        });

        Assert.NotNull(joinedSubscriptions);
        Assert.Equal(10, joinedSubscriptions.Subscriptions.Count);
    }
}