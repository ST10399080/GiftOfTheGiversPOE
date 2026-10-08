using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGiversPOE.Models
{
    public class Volunteer
    {
        public int VolunteerID { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = "";

        [Required]
        [StringLength(100)]
        public string Surname { get; set; } = "";

        [Required]
        [EmailAddress]
        [StringLength(200)]
        public string Email { get; set; } = "";

        [Required]
        [Phone]
        [StringLength(30)]
        public string PhoneNumber { get; set; } = "";

        [Required]
        [StringLength(500)]
        public string Address { get; set; } = "";

        [Required]
        [StringLength(50)]
        public string Discipline { get; set; } = "";

        [StringLength(1000)]
        public string? Skills { get; set; }

        [StringLength(100)]
        public string? ProfessionalRegistrationNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string NextOfKinName { get; set; } = "";

        [Required]
        [Phone]
        [StringLength(30)]
        public string NextOfKinPhone { get; set; } = "";

        [Required]
        [StringLength(100)]
        public string Availability { get; set; } = "";

        [Required]
        [StringLength(100)]
        public string GeographicFlexibility { get; set; } = "";

        [StringLength(2000)]
        public string? AdditionalInformation { get; set; }

        public DateTime ApplicationDate { get; set; }
    }
}