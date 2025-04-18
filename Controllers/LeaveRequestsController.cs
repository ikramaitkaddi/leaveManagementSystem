using AutoMapper;
using LeaveManagementSystem.DTOs;
using LeaveManagementSystem.Interfaces;
using LeaveManagementSystem.Models;
using LeaveManagementSystem.Repositories;
using Microsoft.EntityFrameworkCore;
using LeaveManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagementSystem.Controllers
{
    namespace WebAPI.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class LeaveRequestsController : ControllerBase
        {
            private readonly ILeaveRequestService _leaveRequestService;
            private readonly ILeaveRequestRepository _leaveRequestRepository;
            private readonly IMapper _mapper;

            public LeaveRequestsController(ILeaveRequestService leaveRequestService, ILeaveRequestRepository repository, IMapper mapper)
            {
                _leaveRequestService = leaveRequestService;
                _leaveRequestRepository = repository;
                _mapper = mapper;
            }

            [HttpGet]
            public async Task<IActionResult> Get()
            {
                var leaveRequests = await _leaveRequestRepository.GetAllAsync();
                var result = _mapper.Map<IEnumerable<LeaveRequestDto>>(leaveRequests);
                return Ok(result);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> Get(int id)
            {
                var leaveRequest = await _leaveRequestRepository.GetByIdAsync(id);
                if (leaveRequest == null) return NotFound();
                return Ok(_mapper.Map<LeaveRequestDto>(leaveRequest));
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var request = await _leaveRequestRepository.GetByIdAsync(id);
                if (request == null) return NotFound();
                await _leaveRequestRepository.DeleteAsync(request);
                await _leaveRequestRepository.SaveChangesAsync();
                return NoContent();
            }
            [HttpGet("filter")]
            public async Task<ActionResult<IEnumerable<LeaveRequest>>> GetLeaveRequests(
               [FromQuery] int? employeeId,
               [FromQuery] LeaveType? leaveType,
               [FromQuery] LeaveStatus? status,
               [FromQuery] DateTime? startDate,
               [FromQuery] DateTime? endDate,
               [FromQuery] string? keyword,
               [FromQuery] int page = 1,
               [FromQuery] int pageSize = 10,
               [FromQuery] string sortBy = "StartDate",
               [FromQuery] string sortOrder = "asc")
            {
                var leaveRequests = await _leaveRequestRepository.FilterLeaveRequests(employeeId, leaveType, status, startDate, endDate, keyword, page, pageSize, sortBy, sortOrder);
                return Ok(leaveRequests);
            }

            [HttpPost]
            public async Task<ActionResult<LeaveRequest>> CreateLeaveRequest(LeaveRequest leaveRequest)
            {
                // Business logic validation
                if (!await _leaveRequestService.IsLeaveValid(leaveRequest))
                {
                    return BadRequest("Leave request is not valid according to the business rules.");
                }

                await _leaveRequestRepository.AddAsync(leaveRequest);
                return CreatedAtAction(nameof(GetLeaveRequests), new { id = leaveRequest.Id }, leaveRequest);
            }

            [HttpGet("report")]
            public async Task<IActionResult> GetLeaveSummaryReport(int year, string? department, DateTime? from, DateTime? to, string format = "json")
            {
                try
                {
                    var content = await _leaveRequestService.GenerateLeaveReport(year, department, from, to, format);
                    var mimeType = format.ToLower() == "csv" ? "text/csv" : "application/json";
                    return Content(content, mimeType);
                }
                catch (NotSupportedException ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            [HttpPost("{id}/approve")]
            public async Task<IActionResult> ApproveLeave(int id)
            {
                var result = await _leaveRequestService.ApproveLeaveRequestAsync(id);

                if (!result.Success)
                    return result.Leave == null ? NotFound(result.Message) : BadRequest(result.Message);

                return Ok(result.Leave);
            }
        }
    }
}


