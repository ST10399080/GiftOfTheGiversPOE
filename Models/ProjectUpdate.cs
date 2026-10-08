using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGiversPOE.Models
{
    public class ProjectUpdate
    {
        public int ProjectUpdateID { get; set; }

        [Required]
        public int ReliefProjectID { get; set; }

        [Required]
        [StringLength(2000)]
        public string UpdateText { get; set; } = "";

        public DateTime UpdateDate { get; set; }

        public ReliefProject? ReliefProject { get; set; }
    }
}