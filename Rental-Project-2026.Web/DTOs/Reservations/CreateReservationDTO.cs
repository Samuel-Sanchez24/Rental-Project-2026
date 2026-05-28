using System.ComponentModel.DataAnnotations;

namespace Rental_Project_2026.Web.DTOs.Reservations
{
    public class CreateReservationDTO
    {
        [Required]
        public Guid VehicleId { get; set; }

        [Display(Name = "Placa")]
        public string VehiclePlate { get; set; } = string.Empty;

        [Display(Name = "Marca")]
        public string VehicleBrand { get; set; } = string.Empty;

        [Display(Name = "Modelo")]
        public string VehicleModel { get; set; } = string.Empty;

        [Display(Name = "Imagen")]
        public string? VehicleImageUrl { get; set; }

        [Display(Name = "Precio diario")]
        public decimal DailyPrice { get; set; }

        [Display(Name = "Sucursal")]
        public string BranchName { get; set; } = string.Empty;

        [Display(Name = "Ciudad")]
        public string BranchCity { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre completo es requerido.")]
        [StringLength(120, MinimumLength = 3)]
        [Display(Name = "Nombre completo")]
        public string CustomerFullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El número de identificación es requerido.")]
        [StringLength(30, MinimumLength = 5)]
        [Display(Name = "Identificación")]
        public string DocumentNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es requerido.")]
        [StringLength(20, MinimumLength = 7)]
        [Display(Name = "Teléfono")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo electrónico es requerido.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        [StringLength(120)]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de nacimiento es requerida.")]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime BirthDate { get; set; } = DateTime.Now.Date.AddYears(-18);

        [Required(ErrorMessage = "Debe seleccionar al menos una categoría de licencia.")]
        [Display(Name = "Categorías de licencia")]
        public List<string> DriverLicenseCategories { get; set; } = new();

        public List<string> AvailableLicenseCategories { get; set; } =
            new() { "A1", "A2", "B1", "B2", "B3", "C1", "C2", "C3" };

        [Required(ErrorMessage = "La fecha de vencimiento de la licencia es requerida.")]
        [Display(Name = "Vencimiento de licencia")]
        public DateTime DriverLicenseExpirationDate { get; set; } = DateTime.Now.Date.AddYears(1);

        [Display(Name = "¿Requiere asistencia especial?")]
        public bool RequiresSpecialAssistance { get; set; }

        [StringLength(300)]
        [Display(Name = "Observaciones de asistencia")]
        public string? AssistanceNotes { get; set; }

        [Required(ErrorMessage = "La fecha de renta es requerida.")]
        [Display(Name = "Fecha de renta")]
        public DateTime RentDate { get; set; } = DateTime.Now.Date;

        [Required(ErrorMessage = "La fecha de devolución es requerida.")]
        [Display(Name = "Fecha de devolución")]
        public DateTime ReturnDate { get; set; } = DateTime.Now.Date.AddDays(1);
    }
}