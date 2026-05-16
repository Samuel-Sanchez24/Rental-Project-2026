using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rental_Project_2026.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Rental_Project_2026.Web.DTOs.Vehicles
{
    public class EditVehicleDTO
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 3)]
        [Display(Name = "Placa")]
        public string Plate { get; set; } = string.Empty;

        [Required]
        [StringLength(64, MinimumLength = 3)]
        [Display(Name = "Marca")]
        public string Brand { get; set; } = string.Empty;

        [Required]
        [StringLength(64, MinimumLength = 2)]
        [Display(Name = "Modelo")]
        public string Model { get; set; } = string.Empty;

        [Required]
        [StringLength(64, MinimumLength = 3)]
        [Display(Name = "Color")]
        public string Color { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Año")]
        public int Year { get; set; }

        [Required]
        [Display(Name = "Precio Diario")]
        public decimal DailyPrice { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public VehicleStatus Status { get; set; }

        [Required]
        [Display(Name = "Sucursal")]
        public Guid BranchId { get; set; }

        public string? CurrentImageUrl { get; set; }

        [Display(Name = "Nueva imagen del vehículo")]
        public IFormFile? ImageFile { get; set; }

        public List<SelectListItem> Branches { get; set; } = new();
    }
}