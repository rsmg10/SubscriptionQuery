using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace SubscriptionQuery.Infrastructure.Presistance
{
    public static class DatabaseExtension
    {
        public static void DetachAllEntities(this DbContext context)
        {
            var entries = context.ChangeTracker.Entries()
                //.Where(e => e.State != EntityState.Detached)
                .ToList();

            foreach (var entry in entries)
            {
                if (entry.Entity != null)
                {
                    entry.State = EntityState.Detached;
                }
            }
        }

     

    }
}
