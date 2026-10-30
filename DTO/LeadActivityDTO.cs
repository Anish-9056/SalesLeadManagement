using System.ComponentModel.DataAnnotations;
using SalesLeadManagement.Models;

namespace SalesLeadManagement.DTO
{
    public class CreateLeadActivity
    {
        [Required]
        [RegularExpression("^(Call|Email|TestDrive)$")]
        public LeadActivityType ActivityType { get; set; }

        [Required]
        [StringLength(2000, MinimumLength = 1)]
        public string? Description { get; set; }
    }

    public class LeadActivityResponse
    {
        public int Id { get; set; }
        public int LeadId { get; set; }
        public LeadActivityType ActivityType { get; set; }
        public string? Description { get; set; }
        public DateTime ActivityDate { get; set; }
    }
}
