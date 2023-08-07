using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Data
{
    public class SeedData
    {
        public async static Task Initialize(IServiceProvider serviceProvider)
        {
            StoreContext db = new StoreContext(serviceProvider.GetRequiredService<DbContextOptions<StoreContext>>());

            db.Database.EnsureDeleted();
            db.Database.Migrate();
        }
    }
}
