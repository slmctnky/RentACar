using Business.Abstract;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        
    
        IUserService _entityService;
        IAuthService _authService;
        public UsersController(IUserService entityService)
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
        public IActionResult Add(User entity)
        {
            var result = _entityService.Add(entity);
            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(User entity)
        {
            var result = _entityService.Update(entity);
            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(User entity)
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


