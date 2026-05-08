using Rental_Project_2026.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Rental_Project_2026.Web.DTOs.Vehicles
{
    public class EditVehicleDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "La placa es obligatoria")]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "La placa debe ser valida")]
        [Display(Name = "Placa")]
        public required string Plate { get; set; }

        [Required(ErrorMessage = "La marca es obligatoria")]
        [StringLength(32, MinimumLength = 3, ErrorMessage = "La marca es obligatoria")]
        [Display(Name = "Marca")]
        public required string Brand { get; set; }

        [Required(ErrorMessage = "El modelo es obligatorio")]
        [StringLength(32, MinimumLength = 3, ErrorMessage = "El modelo es obligatorio")]
        [Display(Name = "Modelo")]
        public required string Model { get; set; }

        [Required(ErrorMessage = "El color es obligatorio")]
        [StringLength(16, MinimumLength = 3)]
        [Display(Name = "Color")]
        public required string Color { get; set; }

        [Required(ErrorMessage = "El año es obligatorio")]
        [Range(1990, 2030, ErrorMessage = "El año debe estar entre el 1990 y 2030")]
        [Display(Name = "Año")]
        public required int Year { get; set; }

        [Required(ErrorMessage = "El precio diario es obligatorio")]
        [Range(0, 999999999, ErrorMessage = "El precio diario no es valido")]
        [Display(Name = "Precio Diario")]
        public required decimal DailyPrice { get; set; } = default(decimal);

        [Required(ErrorMessage = "El estado es obligatorio")]
        [EnumDataType(typeof(VehicleStatus), ErrorMessage = "El estado es invalido")]
        [Display(Name = "Estado")]
        public required VehicleStatus Status { get; set; }

        [Required(ErrorMessage = "La sucursal es obligatoria")]
        [Display(Name = "Sucursal")]
        public Guid BranchId { get; set; }
    }
}
