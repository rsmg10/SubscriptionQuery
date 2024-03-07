using Bogus;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using SubscriptionQuery.Infrastructure.Presistance;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace SubscriptionQuery.Test.Helpers
{
    public class TestBase
    {
        WebApplicationFactory<Program> Factory;
        public ApplicationDatabase Database;
        public IMediator Mediator;

        public TestBase(WebApplicationFactory<Program> factory, ITestOutputHelper testOutput)
        {
            Factory = factory.WithDefaultConfigurations(testOutput, services =>
            {
                var descriptor = services.Single(d => d.ServiceType == typeof(DbContextOptions<ApplicationDatabase>));
                services.Remove(descriptor);
                var dbName = Guid.NewGuid().ToString();
                services.AddDbContext<ApplicationDatabase>(options => options.UseInMemoryDatabase(dbName));
            });

            var scope = Factory.Services.CreateScope();
            Database = scope.ServiceProvider.GetRequiredService<ApplicationDatabase>();
            Mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        }
    }
}