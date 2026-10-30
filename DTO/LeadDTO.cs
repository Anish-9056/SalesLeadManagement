using System.ComponentModel.DataAnnotations;
using SalesLeadManagement.Models;

namespace SalesLeadManagement.DTO
{
    public class CreateLeadRequest
    {
        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string CustomerName { get; set; } = string.Empty;

        [EmailAddress]
        [StringLength(150, MinimumLength = 10)]
        public string Email { get; set; }

        [Required]
        [StringLength(30)]
        public string PhoneNumber { get; set; }

        [StringLength(500)]
        public string Notes { get; set; } = string.Empty;
    }

    public class UpdateLeadRequest
    {
        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string CustomerName { get; set; } = string.Empty;

        [EmailAddress]
        [StringLength(250)]
        public string? Email { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 5)]
        public string PhoneNumber { get; set; } = string.Empty;

        [EnumDataType(typeof(LeadStatus))]
        public LeadStatus Status { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }
    }

    public class LeadResponse
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string? Email { get; set; }
        public string PhoneNumber { get; set; }
        public LeadStatus Status { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<LeadActivityResponse> leadActivities { get; set; } = new();
    }


}
