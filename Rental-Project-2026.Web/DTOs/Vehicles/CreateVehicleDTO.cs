using Rental_Project_2026.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Rental_Project_2026.Web.DTOs.Vehicles
{
    public class CreateVehicleDTO
    {
        [Required(ErrorMessage = "La placa es obligatoria") ]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "La placa debe ser valida")]
        [Display(Name = "Placa")]
        public string Plate { get; set; } = string.Empty;

        [Required(ErrorMessage = "La marca es obligatoria")]
        [StringLength(32, MinimumLength = 3, ErrorMessage = "La marca es obligatoria")]
        [Display(Name = "Marca")]
        public  string Brand { get; set; } = string.Empty;

        [Required(ErrorMessage = "El modelo es obligatorio")]
        [StringLength(32, MinimumLength = 2, ErrorMessage = "El modelo es obligatorio")]
        [Display(Name = "Modelo")]
        public  string Model { get; set; } = string.Empty;

        [Required(ErrorMessage = "El color es obligatorio")]
        [StringLength(16, MinimumLength = 3)]
        [Display(Name = "Color")]
        public  string Color { get; set; } = string.Empty;

        [Required(ErrorMessage = "El año es obligatorio")]
        [Range(1990, 2030, ErrorMessage = "El año debe estar entre el 1990 y 2030")]
        [Display(Name = "Año")]
        public  int Year { get; set; } = default(int);

        [Required(ErrorMessage = "El precio diario es obligatorio")]
        [Range(0, 999999999, ErrorMessage = "El precio diario no es valido")]
        [Display(Name = "Precio Diario")]
        public decimal DailyPrice { get; set; } = default(decimal);

        [Required(ErrorMessage = "El estado es obligatorio")]
        [EnumDataType(typeof(VehicleStatus), ErrorMessage = "El estado es invalido")]
        [Display(Name = "Estado")]
        public  VehicleStatus Status { get; set; } 

        [Display(Name = "Imagen del vehículo")]
        public IFormFile? ImageFile { get; set; }

        [Required(ErrorMessage = "La sucursal es obligatoria")]
        [Display(Name = "Sucursal")]
        public Guid BranchId { get; set; }
        public List<SelectListItem> Branches { get; set; } = new();
    }
}
