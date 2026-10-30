using Microsoft.EntityFrameworkCore;
using SalesLeadManagement.Data;
using SalesLeadManagement.DTO;
using SalesLeadManagement.Models;
using SalesLeadManagement.Services.Interface;

namespace SalesLeadManagement.Services
{
    public class LeadService:ILeadService
    {
        private readonly AppDbContext _db;
        public LeadService(AppDbContext db) {
            _db = db;

        }

        public async Task<LeadActivityResponse> AddLeadActivity(int LeadId, CreateLeadActivity DtoRequest, CancellationToken cancellationToken = default)
        {
            var leads = await _db.leads.AnyAsync(l => l.Id == LeadId,cancellationToken);
            if (!leads)
            {
                return null;
            }
            var leadActivity = new LeadActivity
            {
                LeadId = LeadId,
                ActivityType = DtoRequest.ActivityType,
                Description = DtoRequest.Description,
            };
            _db.LeadActivities.Add(leadActivity);
            await _db.SaveChangesAsync(cancellationToken);

            return mapLeadActivityRespoonse(leadActivity);
        }

        public async Task<LeadResponse> Create(CreateLeadRequest DtoRequest, CancellationToken cancellationToken = default)
        {

            var lead = new Lead
            {
                CustomerName = DtoRequest.CustomerName,
                Email = DtoRequest.Email,
                PhoneNumber = DtoRequest.PhoneNumber,
                Status = LeadStatus.New,
                Notes = DtoRequest.Notes,
            };
            await _db.leads.AddAsync(lead);
            await _db.SaveChangesAsync(cancellationToken);
            return mapLeadResponse(lead);
  
        }

        public async Task<List<LeadResponse>> GetAll(string? CustomerName, string Email, CancellationToken cancellationToken = default)
        {
            var query = _db.leads
                .AsNoTracking()
                .Include(x => x.leadActivities)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(CustomerName))
            {
                query = query.Where(x =>
                    x.CustomerName.Contains(CustomerName));
            }

            if (!string.IsNullOrWhiteSpace(Email))
            {
                query = query.Where(x =>
                    x.Email.Contains(Email));
            }

            var leads = await query
                .OrderBy(x => x.CreatedAt)
                .ToListAsync(cancellationToken);

            return leads.Select(mapLeadResponse).ToList();

        }

        public async Task<LeadResponse?> GetById(int Id, CancellationToken cancellationToken = default)
        {
            var lead = await _db.leads.AsNoTracking()
                .Include(x => x.leadActivities)
                .FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);

            return lead == null ? null : mapLeadResponse(lead);
        }

        public async Task<LeadResponse?> Update(int Id, UpdateLeadRequest DtoRequest, CancellationToken cancellationToken = default)
        {
            var lead = await _db.leads
                .Include(x => x.leadActivities)
                .FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
            if (lead == null)
                return null;

            lead.CustomerName = DtoRequest.CustomerName;
            lead.Email = DtoRequest.Email;
            lead.PhoneNumber = DtoRequest.PhoneNumber;
            lead.Status = DtoRequest.Status;
            lead.Notes = DtoRequest.Notes;

            await _db.SaveChangesAsync(cancellationToken);
            return lead == null ? null : mapLeadResponse(lead);
        }

        private static LeadResponse mapLeadResponse(Lead response) {
           return new LeadResponse
            {
                Id = response.Id,
                CreatedAt = response.CreatedAt,
                CustomerName = response.CustomerName,
                Email = response.Email,
                PhoneNumber = response.PhoneNumber,
                Notes = response.Notes,
                Status = response.Status,
                leadActivities = response.leadActivities.OrderBy(l=>l.ActivityDate).ThenBy(l=>l.Id).Select(mapLeadActivityRespoonse).ToList(),
            };
        }

        private static LeadActivityResponse mapLeadActivityRespoonse(LeadActivity response)
        {
            return new LeadActivityResponse
            {
                Id = response.Id,
                LeadId = response.LeadId,
                ActivityType = response.ActivityType,
                Description = response.Description,
                ActivityDate = response.ActivityDate,

            };
        } 
    }
}
