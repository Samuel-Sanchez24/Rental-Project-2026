using Rental_Project_2026.Domain.Entities.Branches;
using Rental_Project_2026.Domain.Enums;
using Rental_Project_2026.Domain.Exceptions;

namespace Rental_Project_2026.Domain.Entities
{
    public class Vehicle
    {
        public Guid Id { get; set; }
        public string Plate { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string Color { get; set; } = null!;
        public int Year { get; set; }
        public decimal DailyPrice { get; set; }
        public VehicleStatus Status { get; set; }

        // Foreign key to Branch
        public Guid BranchId { get; set; }
        public Branch branch { get; set; } = null!;

        public Vehicle(string plate, string model, string brand,string color, int year, decimal dailyPrice, VehicleStatus status, Guid branchId)
        {
            ApplyBusinessRules(plate, model, brand, color, year,dailyPrice, branchId);

            Id = Guid.CreateVersion7();
            Plate = plate;
            Model = model;
            Brand = brand;
            Color = color;
            Year = year;
            DailyPrice = dailyPrice;
            Status = VehicleStatus.Available;
            BranchId = branchId;
        }

        public void UpdateVehicle(string plate, string model, string brand, int year, string color, decimal dailyPrice,VehicleStatus status, Guid branchId)
        {   
            ApplyBusinessRules(plate, model, brand, color, year, dailyPrice, branchId);
            Plate = plate;
            Model = model;
            Brand = brand;
            Color = color;
            Year = year;
            DailyPrice = dailyPrice;
            BranchId = branchId;
        }

        public void ChangeStatus(VehicleStatus status)
        {
            Status = status;
        }

        public void MarkAsEnable() => Status = VehicleStatus.Available;
        public void MarkAsInactive() => Status = VehicleStatus.Inactive;
        public void MarkAsRented() => Status = VehicleStatus.Rented;
        public void MarkAsMaintenance() => Status = VehicleStatus.Maintenance;

        public void ApplyBusinessRules(string plate, string model, string brand, string color, int year, decimal dailyPrice, Guid branchId)
        {
            if (string.IsNullOrWhiteSpace(plate) || plate.Length < 5 || plate.Length > 10)
                throw new BusinessRulesException($"La {nameof(plate)} es requerida (5-10 caracteres).");
            if (string.IsNullOrWhiteSpace(model) || model.Length < 2 || model.Length > 50)
                throw new BusinessRulesException($"El {nameof(model)} es requerido (2-50 caracteres).");
            if (string.IsNullOrWhiteSpace(brand) || brand.Length < 2 || brand.Length > 50)
                throw new BusinessRulesException($"La {nameof(brand)} es requerida (2-50 caracteres).");
            if (string.IsNullOrWhiteSpace(color) || color.Length < 2 || color.Length > 30)
                throw new BusinessRulesException($"El {nameof(color)} es requerido (2-30 caracteres).");    
            if (year < 1900 || year > DateTime.Now.Year + 1)
                throw new BusinessRulesException($"El {nameof(year)} debe ser entre 1900 y el próximo año.");
            if (dailyPrice <= 0)
                throw new BusinessRulesException($"El {nameof(dailyPrice)} debe ser mayor que cero.");
            if (branchId == Guid.Empty)
                throw new BusinessRulesException($"El {nameof(branchId)} es requerido.");
        }
    }
}
