using Business.Abstract;
using Entities.DTOs.UserDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.V2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        [MapToApiVersion("2.0")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            var result = await _userService.Register(registerDTO);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
        [MapToApiVersion("2.0")]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDTO loginDTO)
        {
            var result = _userService.Login(loginDTO);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [MapToApiVersion("2.0")]
        [HttpPost("verifypassword")]
        public IActionResult VerifyEmail(string email, string token)
        {
            var result = _userService.VerifyEmail(email, token);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
