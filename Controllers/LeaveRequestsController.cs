using AutoMapper;
using LeaveManagementSystem.DTOs;
using LeaveManagementSystem.Interfaces;
using LeaveManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagementSystem.Controllers
{
    namespace WebAPI.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class LeaveRequestsController : ControllerBase
        {
            private readonly ILeaveRequestRepository _repository;
            private readonly IMapper _mapper;

            public LeaveRequestsController(ILeaveRequestRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            [HttpGet]
            public async Task<IActionResult> Get()
            {
                var leaveRequests = await _repository.GetAllAsync();
                var result = _mapper.Map<IEnumerable<LeaveRequestDto>>(leaveRequests);
                return Ok(result);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> Get(int id)
            {
                var leaveRequest = await _repository.GetByIdAsync(id);
                if (leaveRequest == null) return NotFound();
                return Ok(_mapper.Map<LeaveRequestDto>(leaveRequest));
            }

            [HttpPost]
            public async Task<IActionResult> Post([FromBody] CreateLeaveRequestDto dto)
            {
                var request = _mapper.Map<LeaveRequest>(dto);
                await _repository.AddAsync(request);
                await _repository.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { id = request.Id }, _mapper.Map<LeaveRequestDto>(request));
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var request = await _repository.GetByIdAsync(id);
                if (request == null) return NotFound();
                await _repository.DeleteAsync(request);
                await _repository.SaveChangesAsync();
                return NoContent();
            }
        }
    }

}
