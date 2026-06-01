using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Moq;
using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Application.UseCases.Vehicles.Commands.CreateVehicle;
using Rental_Project_2026.Application.UseCases.Vehicles.Commands.DeleteVehicle;
using Rental_Project_2026.Application.UseCases.Vehicles.Commands.UpdateVehicle;
using Rental_Project_2026.Domain.Entities;
using Rental_Project_2026.Domain.Enums;
using Rental_Project_2026.Domain.Exceptions;
using Rental_Project_2026.Persistence;
using Rental_Project_2026.Persistence.Repositories;

namespace Rental_Project_2026.Test.Domain.Entities
{
    [TestClass]
    public class VehicleTests
    {
                //DOMAIN

        [TestMethod]
        public void Constructor_WithValidData_CreatesVehicle()
        {
            // Arrange
            string plate = "ABC123";
            string model = "Corolla";
            string brand = "Toyota";
            string color = "Blanco";
            int year = 2022;
            decimal dailyPrice = 150000m;
            VehicleStatus status = VehicleStatus.Available;
            Guid branchId = Guid.NewGuid();

            // Act
            Vehicle vehicle = new Vehicle(plate, model, brand, color, year, dailyPrice, status, branchId);

            // Assert
            Assert.AreNotEqual(Guid.Empty, vehicle.Id);
            Assert.AreEqual(plate, vehicle.Plate);
            Assert.AreEqual(model, vehicle.Model);
            Assert.AreEqual(brand, vehicle.Brand);
            Assert.AreEqual(color, vehicle.Color);
            Assert.AreEqual(year, vehicle.Year);
            Assert.AreEqual(dailyPrice, vehicle.DailyPrice);
            Assert.AreEqual(status, vehicle.Status);
            Assert.AreEqual(branchId, vehicle.BranchId);
        }

        [TestMethod]
        public void Constructor_WithZeroDailyPrice_ThrowsBusinessRulesException()
        {
            // Arrange
            decimal dailyPrice = 0m;

            // Act & Assert
            Assert.ThrowsExactly<BusinessRulesException>(() =>
                new Vehicle("ABC123", "Corolla", "Toyota", "Blanco", 2022, dailyPrice, VehicleStatus.Available, Guid.NewGuid()));
        }

        [TestMethod]
        public void Constructor_WithEmptyBranchId_ThrowsBusinessRulesException()
        {
            // Arrange
            Guid branchId = Guid.Empty;

            // Act & Assert
            Assert.ThrowsExactly<BusinessRulesException>(() =>
                new Vehicle("ABC123", "Corolla", "Toyota", "Blanco", 2022, 150000m, VehicleStatus.Available, branchId));
        }

        [TestMethod]
        public void MarkAsEnable_AfterMarkAsInactive_SetsStatusToAvailable()
        {
            // Arrange
            Vehicle vehicle = new Vehicle("ABC123", "Corolla", "Toyota", "Blanco", 2022, 150000m, VehicleStatus.Available, Guid.NewGuid());
            vehicle.MarkAsInactive();

            // Act
            vehicle.MarkAsEnable();

            // Assert
            Assert.AreEqual(VehicleStatus.Available, vehicle.Status);
        }

                //APPLICATION

        [TestMethod]
        public void CreateVehicleCommandValidator_WithValidCommand_PassesValidation()
        {
            // Arrange
            CreateVehicleCommandValidator validator = new CreateVehicleCommandValidator();
            CreateVehicleCommand command = new CreateVehicleCommand
            {
                Plate = "ABC123",
                Model = "Corolla",
                Brand = "Toyota",
                Color = "Blanco",
                Year = 2022,
                DailyPrice = 150000m,
                Status = VehicleStatus.Available,
                BranchId = Guid.NewGuid()
            };

            // Act
            FluentValidation.Results.ValidationResult result = validator.Validate(command);

            // Assert
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void CreateVehicleCommandValidator_WithEmptyPlate_FailsValidation()
        {
            // Arrange
            CreateVehicleCommandValidator validator = new CreateVehicleCommandValidator();
            CreateVehicleCommand command = new CreateVehicleCommand
            {
                Plate = "",
                Model = "Corolla",
                Brand = "Toyota",
                Color = "Blanco",
                Year = 2022,
                DailyPrice = 150000m,
                Status = VehicleStatus.Available,
                BranchId = Guid.NewGuid()
            };

            // Act
            FluentValidation.Results.ValidationResult result = validator.Validate(command);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Any(e => e.PropertyName == nameof(command.Plate)));
        }

        [TestMethod]
        public async Task DeleteVehicleUseCase_WithNonExistentId_ThrowsBusinessRulesException()
        {
            // Arrange
            Mock<IVehiclesRepository> repositoryMock = new Mock<IVehiclesRepository>();
            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();

            repositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Vehicle?)null);

            DeleteVehicleUseCase useCase = new DeleteVehicleUseCase(repositoryMock.Object, unitOfWorkMock.Object);
            DeleteVehicleCommand command = new DeleteVehicleCommand { Id = Guid.NewGuid() };

            // Act & Assert
            await Assert.ThrowsExactlyAsync<BusinessRulesException>(() => useCase.Handler(command));
        }

                //PERSISTENCE

        [TestMethod]
        public async Task GetByPlateAsync_WithExistingPlate_ReturnsVehicle()
        {
            // Arrange
            DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            Guid branchId = Guid.NewGuid();
            Vehicle vehicle = new Vehicle("ABC123", "Corolla", "Toyota", "Blanco", 2022, 150000m, VehicleStatus.Available, branchId);

            using DataContext context = new DataContext(options);
            await context.Vehicles.AddAsync(vehicle);
            await context.SaveChangesAsync();

            VehiclesRepository repository = new VehiclesRepository(context);

            // Act
            Vehicle? result = await repository.GetByPlateAsync("ABC123");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("ABC123", result.Plate);
        }

        [TestMethod]
        public async Task GetByPlateAsync_WithNonExistentPlate_ReturnsNull()
        {
            // Arrange
            DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using DataContext context = new DataContext(options);
            VehiclesRepository repository = new VehiclesRepository(context);

            // Act
            Vehicle? result = await repository.GetByPlateAsync("NOEXISTE");

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetByBranchIdAsync_WithExistingBranchId_ReturnsVehicles()
        {
            // Arrange
            DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            Guid branchId = Guid.NewGuid();
            Vehicle vehicle1 = new Vehicle("AAA111", "Corolla", "Toyota", "Blanco", 2022, 150000m, VehicleStatus.Available, branchId);
            Vehicle vehicle2 = new Vehicle("BBB222", "Civic", "Honda", "Negro", 2021, 130000m, VehicleStatus.Available, branchId);
            Vehicle vehicleOther = new Vehicle("CCC333", "Spark", "Chevrolet", "Rojo", 2020, 100000m, VehicleStatus.Available, Guid.NewGuid());

            using DataContext context = new DataContext(options);
            await context.Vehicles.AddRangeAsync(vehicle1, vehicle2, vehicleOther);
            await context.SaveChangesAsync();

            VehiclesRepository repository = new VehiclesRepository(context);

            // Act
            List<Vehicle> result = await repository.GetByBranchIdAsync(branchId);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.All(v => v.BranchId == branchId));
        }
    }
}