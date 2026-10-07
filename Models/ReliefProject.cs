using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGiversPOE.Models
{
    public class ReliefProject
    {
        public int ReliefProjectID { get; set; }

        [Required]
        [StringLength(200)]
        public string ProjectName { get; set; } = "";

        [Required]
        [StringLength(100)]
        public string Location { get; set; } = "";

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Active";

        [StringLength(1000)]
        public string Description { get; set; } = "";

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}