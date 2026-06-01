using Rental_Project_2026.Application;
using Rental_Project_2026.Persistence;
using Rental_Project_2026.Persistence.Seeding;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices();

WebApplication app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    SeedDb seedDb = scope.ServiceProvider.GetRequiredService<SeedDb>();
    await seedDb.SeedAsync();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
