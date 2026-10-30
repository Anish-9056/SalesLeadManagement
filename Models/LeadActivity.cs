namespace SalesLeadManagement.Models
{
    public class LeadActivity
    {
        public int Id { get; set; }
        public int LeadId { get; set; }
        public LeadActivityType ActivityType {get; set; }
        public string? Description { get; set; }
        public DateTime ActivityDate { get; set; } = DateTime.UtcNow;
        public Lead Lead { get; set; } = null;
    }
}

public enum LeadActivityType
{
    Call,
    Email,
    TestDrive,
}