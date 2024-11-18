using Application;
using Infrastructure.Contexts;

namespace RestAPI
{
    public static class DatabaseSeeder
    {
        public static async Task Seed(IServiceScopeFactory serviceScopeFactory)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();
                var dbContext = scope.ServiceProvider.GetService<WriteDbContext>();

                try
                {
                    await unitOfWork.BeginTransaction().ConfigureAwait(false);
                    // TODO: added initialize value to save in database
                    await unitOfWork.CommitTransaction().ConfigureAwait(false);
                }
                catch
                {
                    await unitOfWork.RollbackTransaction().ConfigureAwait(false);
                }
            }
        }
    }
}
