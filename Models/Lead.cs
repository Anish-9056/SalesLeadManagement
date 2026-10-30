namespace SalesLeadManagement.Models
{
    public class Lead
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string? Email { get; set; }
        public string PhoneNumber { get; set; }
        public LeadStatus Status { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.Now;
        public ICollection<LeadActivity> leadActivities { get; set; } = new List<LeadActivity>();
    }
}

public enum LeadStatus
{
    New,
    Contacted,
    Qualified,
}