using Rental_Project_2026.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Rental_Project_2026.Web.DTOs.Reservations
{
    public class EditReservationDTO
    {
        [Required]
        public Guid Id { get; set; }

        [Display(Name = "Vehículo")]
        public Guid VehicleId { get; set; }

        [Display(Name = "Placa")]
        public string VehiclePlate { get; set; } = string.Empty;

        [Display(Name = "Marca")]
        public string VehicleBrand { get; set; } = string.Empty;

        [Display(Name = "Modelo")]
        public string VehicleModel { get; set; } = string.Empty;

        [Display(Name = "Sucursal")]
        public string BranchName { get; set; } = string.Empty;

        [Display(Name = "Cliente")]
        public string UserFullName { get; set; } = string.Empty;

        [Display(Name = "Correo")]
        public string UserEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de renta es requerida.")]
        [Display(Name = "Fecha de renta")]
        public DateTime RentDate { get; set; }

        [Required(ErrorMessage = "La fecha de devolución es requerida.")]
        [Display(Name = "Fecha de devolución")]
        public DateTime ReturnDate { get; set; }

        [Display(Name = "Días")]
        public int Days { get; set; }

        [Display(Name = "Precio diario")]
        public decimal DailyPriceAtBooking { get; set; }

        [Display(Name = "Total")]
        public decimal TotalPrice { get; set; }

        [Required(ErrorMessage = "El estado de la reserva es requerido.")]
        [Display(Name = "Estado")]
        public ReservationStatus Status { get; set; }
    }
}
