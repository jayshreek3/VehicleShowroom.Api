using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleShowroom.Api.Interfaces;
using VehicleShowroom.Api.Models;

namespace VehicleShowroom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowroomsController : ControllerBase
    {
        private readonly IShowroomRepository _showroomRepository;

        public ShowroomsController(IShowroomRepository showroomRepository)
        {
            _showroomRepository = showroomRepository;
        }

        [HttpGet("GetAllShowrooms")]
        public async Task<IActionResult> GetAllShowrooms()
        {
            try
            {
                var showroom = await _showroomRepository.GetAllAsync();
                return Ok(showroom);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server error: {ex.Message}");
            }
        }

        [HttpGet("GetShowroomById/{id}")]
        public async Task<IActionResult> GetShowroomById(int id)
        {
            try
            {
                var showroom = await _showroomRepository.GetByIdAsync(id);
                if (showroom == null)
                {
                    return NotFound();
                }
                return Ok(showroom);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("AddNewShowroom")]
        public async Task<IActionResult> Create([FromBody] Showroom showroom)
        {
            try
            {
                await _showroomRepository.CreateAsync(showroom);
                return Ok(showroom);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("UpdateShowroom/{id}")]
        public async Task<IActionResult> UpdateShowroom(int? id, [FromBody] Showroom showroom)
        {
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await _showroomRepository.UpdateAsync(id.Value, showroom);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("DeleteShowroom/{id}")]
        public async Task<IActionResult> DeleteShowroom(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await _showroomRepository.DeleteAsync(id.Value);
                if (!result)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}