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
    public class AssetAssignmentController : ControllerBase
    {
        private readonly ILogger<AssetAssignmentController> _logger;
        private readonly AssetManagementContext _context;
        private readonly IMapper _mapper;

        public AssetAssignmentController(AssetManagementContext context, IMapper mapper, ILogger<AssetAssignmentController> logger)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }
                
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssignmentResponseDto>>> Get([FromQuery] AssignmentResourceParameters parameters)
        {
            try
            {
                IQueryable<AssetAssignment> query = _context.AssetAssignment
                   .Include(a => a.Asset)
                   .Include(e => e.Employee);
              
                if (!string.IsNullOrEmpty(parameters.Manufacturer))
                {
                    query = query
                      .Where(r => (r.Asset.Manufacturer == parameters.Manufacturer));
                }
                if (!string.IsNullOrEmpty(parameters.Model))
                {
                    query = query
                        .Where(r => (r.Asset.Model == parameters.Model));
                }
                if (!string.IsNullOrEmpty(parameters.AssetTag))
                {
                    query = query
                        .Where(r => (r.Asset.AssetTag == parameters.AssetTag));
                }
                if (!string.IsNullOrEmpty(parameters.Color))
                {
                    query = query
                        .Where(r => (r.Asset.Color == parameters.Color));
                }
                if (!string.IsNullOrEmpty(parameters.SerialNumber))
                {
                    query = query
                        .Where(r => (r.Asset.SerialNumber == parameters.SerialNumber));
                }
                if (!string.IsNullOrEmpty(parameters.PurchasedFrom))
                {
                    query = query
                        .Where(r => (r.Asset.PurchasedFrom == parameters.PurchasedFrom));
                }
                if (parameters.YearManufactured != null)
                {
                    query = query
                        .Where(r => (r.Asset.ManufacturedYear == parameters.YearManufactured));
                }
                if (parameters.AssetId != null)
                {
                    query = query
                        .Where(r => (r.AssetId == parameters.AssetId));
                }
                // Employee
                if (!string.IsNullOrEmpty(parameters.FirstName))
                {
                    query = query
                      .Where(r => (r.Employee.FirstName == parameters.FirstName));
                }
                if (!string.IsNullOrEmpty(parameters.LastName))
                {
                    query = query
                        .Where(r => (r.Employee.LastName == parameters.LastName));
                }
                if (!string.IsNullOrEmpty(parameters.City))
                {
                    query = query
                        .Where(r => (r.Employee.City == parameters.City));
                }
                if (!string.IsNullOrEmpty(parameters.State))
                {
                    query = query
                        .Where(r => (r.Employee.State == parameters.State));
                }
                if (!string.IsNullOrEmpty(parameters.Email))
                {
                    query = query
                        .Where(r => (r.Employee.State == parameters.Email));
                }
                if (parameters.EmployeeId != null)
                {
                    query = query
                        .Where(r => (r.EmployeeId == parameters.EmployeeId));
                }

                var results = await query.OrderBy(r => r.AssetId).ToListAsync();

                if (results == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<List<AssignmentResponseDto>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get assets: {ex}");
                return BadRequest("Failed to get assets");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AssignmentResponseDto>> Get(int id)
        {
            try
            {
                var result = await _context.AssetAssignment
                    .Include(a => a.Asset)
                    .Include(e => e.Employee)
                    .Where(e => e.AssetAssignmentsId == id)
                    .FirstOrDefaultAsync();

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<AssignmentResponseDto>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get asset assignment: {ex}");
                return BadRequest("Failed to get asset assignment.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(AssignmentRequestDto assetAssignment)
        {
            try
            {
                if (assetAssignment == null)
                {
                    return BadRequest();
                }

                var result = _mapper.Map<AssignmentRequestDto, AssetAssignment>(assetAssignment);

                if (assetAssignment.EmployeeId == 0)
                {
                    _context.Add(result);
                }
                else
                {
                    _context.Update(result);
                }

                _context.Add(result);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to add asset assignment: {ex}");
                return BadRequest("Failed to add asset assignment.");
            }
        }
    }
}