using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using SalesLeadManagement.DTO;
using SalesLeadManagement.Services.Interface;

namespace SalesLeadManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaleLeadController : Controller
    {

        private readonly ILeadService _leadService;
        public SaleLeadController(ILeadService leadService) { 
         _leadService = leadService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLead([FromQuery] string? name,[FromQuery] string? email,CancellationToken cancellationToken) {

            var lead = await _leadService.GetAll(name, email, cancellationToken);
            return Ok(lead);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetLeadById(int id,CancellationToken cancellationToken)
        {
            var lead = await _leadService.GetById(id, cancellationToken);
            if (lead == null)
            {
                return NotFound(new { message = "Lead not found." });
            }
            return Ok(lead);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLead([FromBody] CreateLeadRequest request,CancellationToken cancellationToken)
        {
            var lead = await _leadService.Create(request,cancellationToken);
            return CreatedAtAction(nameof(GetLeadById), new { id = lead.Id }, lead);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id,[FromBody] UpdateLeadRequest request,CancellationToken cancellationToken)
        {
            var lead = await _leadService.Update(id, request, cancellationToken);

            if (lead == null)
                return NotFound(new { message = "Lead not found." });

            return Ok(lead);
        }

        [HttpPost("{id:int}/activities")]
        public async Task<IActionResult> AddActivity(int id, CreateLeadActivity req, CancellationToken cancellationToken)
        {
            var activity = await _leadService.AddLeadActivity( id, req, cancellationToken);
            if (activity == null)
            {
                return NotFound(new { message = "Lead not found." });
            }
            return StatusCode(201, activity);

        }
    }
}
