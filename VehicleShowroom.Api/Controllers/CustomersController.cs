using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleShowroom.Api.Interfaces;
using VehicleShowroom.Api.Models;

namespace VehicleShowroom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet("GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var customer = await _customerRepository.GetAllAsync();
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        

        [HttpGet("GetCustomersById/{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            try
            {
                var vehicle = await _customerRepository.GetByIdAsync(id);
                if (vehicle == null)
                {
                    return NotFound();
                }
                return Ok(vehicle);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }
           
        }

        [HttpPost("AddNewCustomer")]
        public async Task<IActionResult> Create([FromBody] Customer customer)
        {
            try
            {

                //throw new Exception("Unable to add new Customer");
                //await _customerRepository.Add(customer);
               // if(_customerRepository.Customer.Where(u => u ))
                await _customerRepository.CreateAsync(customer);

                return Ok(customer);
                
                return Ok("Success from create method");
            }
            catch(Exception ex)
            {

              return StatusCode(500, ex.Message);
            }
            //return Ok("Success from create method");
            
        }

        [HttpPut("UpdateCustomer/{id}")]
        public async Task<IActionResult> UpdateCustomer(int? id, [FromBody] Customer customer)
        {
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await _customerRepository.UpdateAsync(id.Value, customer);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpDelete("DeleteCustomer/{id}")]
        public async Task<IActionResult> DeleteCustomer(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await _customerRepository.DeleteAsync(id.Value);
                if (!result)
                {
                    return NotFound();
                }
                return Ok();

            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }
    }
}