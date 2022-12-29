using BurgerWebAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace BurgerWebAPI.Context
{
    public class BurgerDbContext : DbContext
    {
        public BurgerDbContext(DbContextOptions<BurgerDbContext> dbContext) : base(dbContext)
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
                Console.WriteLine(ex.ToString());
            }
        }
        public DbSet<Burger> Burgers { get; set; }
    }
}
