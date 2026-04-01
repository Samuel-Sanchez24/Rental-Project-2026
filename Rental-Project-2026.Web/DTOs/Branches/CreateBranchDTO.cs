using System.ComponentModel.DataAnnotations;

namespace Rental_Project_2026.Web.DTOs.Branches
{
    public class CreateBranchDTO
    {
        [Required]
        [StringLength(64, MinimumLength = 3)]
        [Display(Name = "Sucursal")]
        public required string Name { get; set; }

        [Required]
        [StringLength (64, MinimumLength = 2)]
        [Display(Name = "Ciudad")]
        public required string City { get; set; }

        [Required]
        [StringLength(128)]
        [Display(Name = "Direccion")]
        public required string Address { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Telefono")]
        public required string Phone { get; set; }

        [EnumDataType(typeof(BranchStatus))]
        [Display(Name = "Estado")]
        public required BranchStatus Status { get; set; }
    }
}
