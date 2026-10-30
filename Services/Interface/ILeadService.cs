using SalesLeadManagement.DTO;

namespace SalesLeadManagement.Services.Interface
{
    public interface ILeadService
    {
       Task<List<LeadResponse>> GetAll(string? CustomerName,string? Email, CancellationToken cancellationToken = default);
       Task<LeadResponse>? GetById(int Id, CancellationToken cancellationToken = default);
       Task<LeadResponse> Create(CreateLeadRequest DtoRequest, CancellationToken cancellationToken = default);
       Task<LeadResponse>? Update(int Id, UpdateLeadRequest DtoRequest, CancellationToken cancellationToken = default);
       Task<LeadActivityResponse> AddLeadActivity(int LeadId, CreateLeadActivity DtoRequest, CancellationToken cancellationToken = default);

    }
}
