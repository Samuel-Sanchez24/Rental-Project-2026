using Microsoft.EntityFrameworkCore;
using Rental_Project_2026.Domain.Entities;
using Rental_Project_2026.Domain.Entities.Branches;

namespace Rental_Project_2026.Persistence.Seeding
{
    internal class BranchesSeeder : ISeedable
    {
        private readonly DataContext _context;

        public BranchesSeeder(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            List<Branch> branchesToSeed = new List<Branch>
            {
                new Branch("AutoRent Laureles", "Medellin", "Circular 74 #39-20", "3002345678"),
                new Branch("AutoRent Bogota Norte", "Bogota", "Calle 123 #45-67", "3009876543"),
                new Branch("Autorent Centro", "Medellin", "Carrera 46 #42-47", "3246712588"),
                new Branch("AutoRent Barranquilla Centro", "Barranquilla", "Calle 72 #45-60", "3204561238"),
                new Branch("AutoRentCo El Poblado", "Medellin", "Calle 43A #6 Sur-15", "3001234567"),
                new Branch("Auto Rent Las Vegas", "Envigado", "Cra. 43a #25B SUR-176, Zona 2", "3105640015"),
                new Branch("AutoRent Cali Sur", "Cali", "Av Pasoancho #80-50", "3201239875"),
                new Branch("AutoRent Hollywood", "Cartagena", "Calle 3 #3-2a 3-62", "3241299755"),
                new Branch("AutoRent Aeropuerto JMC", "Rionegro", "Aeropuerto JMC Local 12", "3019246578"),
                new Branch("AutoRent Sabaneta", "Sabaneta", "Calle 70 Sur #45-25", "3005678901")
            };

            foreach (Branch branch in branchesToSeed)
            {
                bool exists = await _context.Branches.AnyAsync(b => b.Name == branch.Name);
                if (!exists)
                {
                    await _context.Branches.AddAsync(branch);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
