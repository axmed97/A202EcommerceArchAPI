using Business.Abstract;
using Entities.DTOs.UserDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("[action]")]
        public IActionResult Register([FromBody]RegisterDTO registerDTO)
        {
            var result = _userService.Register(registerDTO);
            if(result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody]LoginDTO loginDTO)
        {
            var result = _userService.Login(loginDTO);
            if(result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("verifypassword")]
        public IActionResult VerifyPassword(string email, string token)
        {
            var result = _userService.VerifyEmail(email, token);
            if(result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
