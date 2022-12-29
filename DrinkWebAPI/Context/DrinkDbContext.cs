using DrinkWebAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace DrinkWebAPI.Context
{
    public class DrinkDbContext : DbContext
    {
        public DrinkDbContext(DbContextOptions<DrinkDbContext> dbContextOptions) : base(dbContextOptions)
        {
            try
            {
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databaseCreator != null)
                {
                    if (!databaseCreator.CanConnect())
                        databaseCreator.Create();
                    if (!databaseCreator.HasTables())
                        databaseCreator.CreateTables();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public DbSet<Drink> Drinks { get; set; }
    }
}
