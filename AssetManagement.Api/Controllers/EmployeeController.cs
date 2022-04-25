using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Api.Attributes;
using AssetManagement.Api.Data.Entities;
using AssetManagement.Api.Models;
using AssetManagement.Api.ResourceParemeters;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AssetManagement.Api.Controllers
{
    [ApiKey]
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly AssetManagementContext _context;
        private readonly IMapper _mapper;

        public EmployeeController(AssetManagementContext context, IMapper mapper, ILogger<EmployeeController> logger)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;    
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> Get([FromQuery] EmployeeResourceParameters parameters)
        {
            try
            {
                IQueryable<Employee> query = _context.Employee;
                if (!string.IsNullOrEmpty(parameters.FirstName))
                {
                    query = query
                      .Where(r => (r.FirstName == parameters.FirstName));
                }
                if (!string.IsNullOrEmpty(parameters.LastName))
                {
                    query = query
                        .Where(r => (r.LastName == parameters.LastName));
                }
                if (!string.IsNullOrEmpty(parameters.City))
                {
                    query = query
                        .Where(r => (r.City == parameters.City));
                }
                if (!string.IsNullOrEmpty(parameters.State))
                {
                    query = query
                        .Where(r => (r.State == parameters.State));
                }
                if (!string.IsNullOrEmpty(parameters.Email))
                {
                    query = query
                        .Where(r => (r.Email == parameters.Email));
                }

                var results = await query.OrderBy(r=>r.EmployeeId).ToListAsync();

                if (results == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<List<EmployeeDto>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get employees: {ex}");
                return BadRequest("Failed to get employee");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> Get(int id)
        {
            try
            {
                var result = await _context.Employee.FindAsync(id);  

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<EmployeeDto>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get employee: {ex}");
                return BadRequest("Failed to get employee.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(long id, EmployeeDto employeeDto)
        {
            try
            {
                if (id == employeeDto.EmployeeId)
                {
                    return BadRequest();
                }

                var employee = await _context.Employee.FindAsync(id); 
                if(employee == null)
                {
                    return NotFound();
                }

                var result = _mapper.Map<EmployeeDto, Employee>(employeeDto);
                _context.Update(result);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to add employee: {ex}");
                return BadRequest("Failed to add employee.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(EmployeeDto employeeDto)
        {
            try
            {
                if(employeeDto == null)
                {
                    return BadRequest();
                }

                var result = _mapper.Map<EmployeeDto, Employee>(employeeDto);

                if (employeeDto.EmployeeId == 0)
                {
                    _context.Add(result);
                }
                else
                {
                    _context.Update(result);
                }

                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to add employee: {ex}");
                return BadRequest("Failed to add employee.");
            }
        }
    }
}
