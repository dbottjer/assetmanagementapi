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
    public class AssetController : ControllerBase
    {

        private readonly ILogger<AssetController> _logger;
        private readonly AssetManagementContext _context;
        private readonly IMapper _mapper;

        public AssetController(AssetManagementContext context, IMapper mapper, ILogger<AssetController> logger)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssetDto>>> Get([FromQuery] AssetResourceParameters parameters)
        {
            try
            {
                IQueryable<Asset> query = _context.Asset;
                if (!string.IsNullOrEmpty(parameters.Manufacturer))
                {
                    query = query
                      .Where(r => (r.Manufacturer == parameters.Manufacturer));
                }
                if (!string.IsNullOrEmpty(parameters.Model))
                {
                    query = query
                        .Where(r => (r.Model == parameters.Model));
                }
                if (!string.IsNullOrEmpty(parameters.AssetTag))
                {
                    query = query
                        .Where(r => (r.AssetTag == parameters.AssetTag));
                }
                if (!string.IsNullOrEmpty(parameters.Color))
                {
                    query = query
                        .Where(r => (r.Color == parameters.Color));
                }
                if (!string.IsNullOrEmpty(parameters.SerialNumber))
                {
                    query = query
                        .Where(r => (r.SerialNumber == parameters.SerialNumber));
                }
                if (!string.IsNullOrEmpty(parameters.PurchasedFrom))
                {
                    query = query
                        .Where(r => (r.PurchasedFrom == parameters.PurchasedFrom));
                }
                if (parameters.YearManufactured != null)
                {
                    query = query
                        .Where(r => (r.ManufacturedYear == parameters.YearManufactured));
                }

                var results = await query.OrderBy(r => r.AssetId).ToListAsync();

                if (results == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<List<AssetDto>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get assets: {ex}");
                return BadRequest("Failed to get assets");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AssetDto>> Get(int id)
        {
            try
            {
                var result = await _context.Asset.FindAsync(id);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<AssetDto>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get asset: {ex}");
                return BadRequest("Failed to get asset.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(AssetDto assetDto)
        {
            try
            {
                if (assetDto == null)
                {
                    return BadRequest();
                }

                var result = _mapper.Map<AssetDto, Asset>(assetDto);

                if (assetDto.AssetId == 0)
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
                _logger.LogError($"Failed to add asset: {ex}");
                return BadRequest("Failed to add asset.");
            }
        }
    }
}