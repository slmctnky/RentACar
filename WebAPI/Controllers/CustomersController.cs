using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
       
    
        ICustomerService _entityService;

        public CustomersController(ICustomerService entityService)
        {
            _entityService = entityService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _entityService.GetAll();
            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result);
        }

        [HttpGet("getallwithdetail")]
        public IActionResult GetAllWithDetail()
        {
            var result = _entityService.GetCustomerDetails();
            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _entityService.GetById(id);
            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Customer entity)
        {
            var result = _entityService.Add(entity);
            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Customer entity)
        {
            var result = _entityService.Update(entity);
            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Customer entity)
        {
            var result = _entityService.Delete(entity);
            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result);
        }

    }
}


