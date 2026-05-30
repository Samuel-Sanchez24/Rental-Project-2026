using Rental_Project_2026.Domain.Account;
using Rental_Project_2026.Domain.Entities.Branches;
using Rental_Project_2026.Domain.Enums;
using Rental_Project_2026.Domain.Exceptions;

namespace Rental_Project_2026.Domain.Entities
{
    public class Reservation
    {
        private static readonly string[] AllowedDriverLicenseCategories =
        {
            "A1", "A2", "B1", "B2", "B3", "C1", "C2", "C3"
        };

        public Guid Id { get; set; }

        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public int Days { get; set; }

        public decimal DailyPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public ReservationStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        // Customer information
        public string CustomerFullName { get; set; } = null!;
        public string DocumentNumber { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime BirthDate { get; set; }

        // Driver license information
        public string DriverLicenseCategories { get; set; } = null!;
        public DateTime DriverLicenseExpirationDate { get; set; }

        // Special assistance
        public bool RequiresSpecialAssistance { get; set; }
        public string? AssistanceNotes { get; set; }

        // Foreign key to Vehicle
        public Guid VehicleId { get; set; }
        public Vehicle Vehicle { get; set; } = null!;

        // Foreign key to Branch
        public Guid BranchId { get; set; }
        public Branch Branch { get; set; } = null!;

        // Foreign key to User
        public string UserId { get; set; } = null!;

        private Reservation()
        {
        }

        public Reservation(
            Guid vehicleId,
            Guid branchId,
            string userId,
            DateTime rentDate,
            DateTime returnDate,
            decimal dailyPriceAtBooking,
            string customerFullName,
            string documentNumber,
            string phoneNumber,
            string email,
            DateTime birthDate,
            IEnumerable<string> driverLicenseCategories,
            DateTime driverLicenseExpirationDate,
            bool requiresSpecialAssistance,
            string? assistanceNotes)
        {
            ApplyBusinessRules(
                vehicleId,
                branchId,
                userId,
                rentDate,
                returnDate,
                dailyPriceAtBooking,
                customerFullName,
                documentNumber,
                phoneNumber,
                email,
                birthDate,
                driverLicenseCategories,
                driverLicenseExpirationDate,
                requiresSpecialAssistance,
                assistanceNotes);

            Id = Guid.CreateVersion7();

            VehicleId = vehicleId;
            BranchId = branchId;
            UserId = userId;

            RentDate = rentDate.Date;
            ReturnDate = returnDate.Date;

            Days = CalculateDays(RentDate, ReturnDate);

            DailyPrice = dailyPriceAtBooking;
            TotalPrice = CalculateTotalPrice(Days, DailyPrice);

            CustomerFullName = customerFullName.Trim();
            DocumentNumber = documentNumber.Trim();
            PhoneNumber = phoneNumber.Trim();
            Email = email.Trim();
            BirthDate = birthDate.Date;

            DriverLicenseCategories = BuildDriverLicenseCategories(driverLicenseCategories);
            DriverLicenseExpirationDate = driverLicenseExpirationDate.Date;

            RequiresSpecialAssistance = requiresSpecialAssistance;
            AssistanceNotes = string.IsNullOrWhiteSpace(assistanceNotes)
                ? null
                : assistanceNotes.Trim();

            Status = ReservationStatus.Pending;
            CreatedAt = DateTime.Now;
        }

        public void UpdateDates(DateTime rentDate, DateTime returnDate)
        {
            ApplyReservationRules(
                VehicleId,
                BranchId,
                UserId,
                rentDate,
                returnDate,
                DailyPrice);

            RentDate = rentDate.Date;
            ReturnDate = returnDate.Date;

            Days = CalculateDays(RentDate, ReturnDate);
            TotalPrice = CalculateTotalPrice(Days, DailyPrice);
        }

        public void UpdateCustomerInformation(
            string customerFullName,
            string documentNumber,
            string phoneNumber,
            string email,
            DateTime birthDate,
            IEnumerable<string> driverLicenseCategories,
            DateTime driverLicenseExpirationDate,
            bool requiresSpecialAssistance,
            string? assistanceNotes)
        {
            ApplyCustomerRules(
                customerFullName,
                documentNumber,
                phoneNumber,
                email,
                birthDate);

            ApplyDriverLicenseRules(
                driverLicenseCategories,
                driverLicenseExpirationDate);

            ApplyAssistanceRules(
                requiresSpecialAssistance,
                assistanceNotes);

            CustomerFullName = customerFullName.Trim();
            DocumentNumber = documentNumber.Trim();
            PhoneNumber = phoneNumber.Trim();
            Email = email.Trim();
            BirthDate = birthDate.Date;
            DriverLicenseCategories = BuildDriverLicenseCategories(driverLicenseCategories);
            DriverLicenseExpirationDate = driverLicenseExpirationDate.Date;
            RequiresSpecialAssistance = requiresSpecialAssistance;
            AssistanceNotes = string.IsNullOrWhiteSpace(assistanceNotes)
                ? null
                : assistanceNotes.Trim();
        }

        public void ChangeStatus(ReservationStatus status)
        {
            Status = status;
        }

        public void MarkAsPendingPayment() => Status = ReservationStatus.Pending;
        public void MarkAsConfirmed() => Status = ReservationStatus.Confirmed;
        public void MarkAsCancelled() => Status = ReservationStatus.Cancelled;
        public void MarkAsFinished() => Status = ReservationStatus.Finished;

        public void Cancel()
        {
            if (Status == ReservationStatus.Finished)
                throw new BusinessRulesException("No se puede cancelar una reserva finalizada.");

            if (Status == ReservationStatus.Cancelled)
                throw new BusinessRulesException("La reserva ya se encuentra cancelada.");

            Status = ReservationStatus.Cancelled;
        }

        public void Confirm()
        {
            if (Status == ReservationStatus.Cancelled)
                throw new BusinessRulesException("No se puede confirmar una reserva cancelada.");

            if (Status == ReservationStatus.Finished)
                throw new BusinessRulesException("No se puede confirmar una reserva finalizada.");

            Status = ReservationStatus.Confirmed;
        }

        public void Finish()
        {
            if (Status == ReservationStatus.Cancelled)
                throw new BusinessRulesException("No se puede finalizar una reserva cancelada.");

            Status = ReservationStatus.Finished;
        }

        public void ApplyBusinessRules(
            Guid vehicleId,
            Guid branchId,
            string userId,
            DateTime rentDate,
            DateTime returnDate,
            decimal dailyPriceAtBooking,
            string customerFullName,
            string documentNumber,
            string phoneNumber,
            string email,
            DateTime birthDate,
            IEnumerable<string> driverLicenseCategories,
            DateTime driverLicenseExpirationDate,
            bool requiresSpecialAssistance,
            string? assistanceNotes)
        {
            ApplyReservationRules(
                vehicleId,
                branchId,
                userId,
                rentDate,
                returnDate,
                dailyPriceAtBooking);

            ApplyCustomerRules(
                customerFullName,
                documentNumber,
                phoneNumber,
                email,
                birthDate);

            ApplyDriverLicenseRules(
                driverLicenseCategories,
                driverLicenseExpirationDate);

            ApplyAssistanceRules(
                requiresSpecialAssistance,
                assistanceNotes);
        }

        private void ApplyReservationRules(
            Guid vehicleId,
            Guid branchId,
            string userId,
            DateTime rentDate,
            DateTime returnDate,
            decimal dailyPriceAtBooking)
        {
            if (vehicleId == Guid.Empty)
                throw new BusinessRulesException($"El {nameof(vehicleId)} es requerido.");

            if (branchId == Guid.Empty)
                throw new BusinessRulesException($"El {nameof(branchId)} es requerido.");

            if (string.IsNullOrWhiteSpace(userId))
                throw new BusinessRulesException($"El {nameof(userId)} es requerido.");

            if (rentDate.Date < DateTime.Now.Date)
                throw new BusinessRulesException("La fecha de renta no puede ser menor a la fecha actual.");

            if (returnDate.Date <= rentDate.Date)
                throw new BusinessRulesException("La fecha de devolución debe ser mayor a la fecha de renta.");

            if (dailyPriceAtBooking <= 0)
                throw new BusinessRulesException("El precio diario de la reserva debe ser mayor que cero.");
        }

        private void ApplyCustomerRules(
            string customerFullName,
            string documentNumber,
            string phoneNumber,
            string email,
            DateTime birthDate)
        {
            if (string.IsNullOrWhiteSpace(customerFullName) ||
                customerFullName.Length < 3 ||
                customerFullName.Length > 120)
                throw new BusinessRulesException("El nombre completo es requerido y debe tener entre 3 y 120 caracteres.");

            if (string.IsNullOrWhiteSpace(documentNumber) ||
                documentNumber.Length < 5 ||
                documentNumber.Length > 30)
                throw new BusinessRulesException("El número de identificación es requerido y debe tener entre 5 y 30 caracteres.");

            if (string.IsNullOrWhiteSpace(phoneNumber) ||
                phoneNumber.Length < 7 ||
                phoneNumber.Length > 20)
                throw new BusinessRulesException("El teléfono es requerido y debe tener entre 7 y 20 caracteres.");

            if (string.IsNullOrWhiteSpace(email) || email.Length > 120)
                throw new BusinessRulesException("El correo electrónico es requerido.");

            int age = DateTime.Now.Year - birthDate.Year;

            if (birthDate.Date > DateTime.Now.AddYears(-age))
                age--;

            if (age < 18)
                throw new BusinessRulesException("El cliente debe ser mayor de edad para realizar una reserva.");
        }

        private void ApplyDriverLicenseRules(
            IEnumerable<string> driverLicenseCategories,
            DateTime driverLicenseExpirationDate)
        {
            List<string> normalizedCategories = NormalizeDriverLicenseCategories(driverLicenseCategories);

            if (!normalizedCategories.Any())
                throw new BusinessRulesException("Debe seleccionar al menos una categoría de licencia.");

            if (normalizedCategories.Any(category => !AllowedDriverLicenseCategories.Contains(category)))
                throw new BusinessRulesException("Una o más categorías de licencia no son válidas.");

            if (driverLicenseExpirationDate.Date <= DateTime.Now.Date)
                throw new BusinessRulesException("La licencia de conducción debe estar vigente.");
        }

        private void ApplyAssistanceRules(
            bool requiresSpecialAssistance,
            string? assistanceNotes)
        {
            if (requiresSpecialAssistance &&
                string.IsNullOrWhiteSpace(assistanceNotes))
                throw new BusinessRulesException("Debe indicar las observaciones de asistencia especial.");

            if (!string.IsNullOrWhiteSpace(assistanceNotes) &&
                assistanceNotes.Length > 300)
                throw new BusinessRulesException("Las observaciones de asistencia no pueden superar los 300 caracteres.");
        }

        private string BuildDriverLicenseCategories(IEnumerable<string> driverLicenseCategories)
        {
            List<string> normalizedCategories = NormalizeDriverLicenseCategories(driverLicenseCategories);

            return string.Join(",", normalizedCategories);
        }

        private List<string> NormalizeDriverLicenseCategories(IEnumerable<string> driverLicenseCategories)
        {
            if (driverLicenseCategories == null)
                return new List<string>();

            return driverLicenseCategories
                .Where(category => !string.IsNullOrWhiteSpace(category))
                .Select(category => category.Trim().ToUpperInvariant())
                .Distinct()
                .ToList();
        }

        private int CalculateDays(DateTime rentDate, DateTime returnDate)
        {
            return (returnDate.Date - rentDate.Date).Days;
        }

        private decimal CalculateTotalPrice(int days, decimal dailyPriceAtBooking)
        {
            return days * dailyPriceAtBooking;
        }
    }
}
