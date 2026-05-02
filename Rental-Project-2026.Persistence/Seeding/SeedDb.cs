namespace Rental_Project_2026.Persistence.Seeding
{
    public class SeedDb
    {
        private readonly IEnumerable<ISeedable> _seeders;

        public SeedDb(DataContext context)
        {
            _seeders = new List<ISeedable>
            {
                new UsersSeeder(context),
                new BranchesSeeder(context)
            };
        }

        public async Task SeedAsync()
        {
            foreach (ISeedable seeder in _seeders)
            {
                await seeder.SeedAsync();
            }
        }
    }
}
