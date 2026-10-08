using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGiversPOE.Models
{
    public class Donation
    {
        public int DonationID { get; set; }

        [Required]
        [StringLength(200)]
        public string DonorName { get; set; } = "";

        [EmailAddress]
        [StringLength(200)]
        public string? DonorEmail { get; set; }

        [Required]
        [Range(1, 100000000)]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(3)]
        public string Currency { get; set; } = "ZAR";

        [Required]
        [StringLength(20)]
        public string DonationType { get; set; } = "One-Time";

        [Required]
        [StringLength(30)]
        public string DonorType { get; set; } = "Registered Donor";

        [StringLength(50)]
        public string FundOption { get; set; } = "General Relief";

        public int? ReliefProjectID { get; set; }

        public DateTime DonationDate { get; set; }

        [StringLength(100)]
        public string? TaxCertificateNumber { get; set; }

        public ReliefProject? ReliefProject { get; set; }
    }
}