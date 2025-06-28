using ApplicationLayer.Authorize.Commands.Register;
using ApplicationLayer.Authorize.DTOs;
using ApplicationLayer.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            var command = new RegisterCommand(userRegisterDto.UserName, userRegisterDto.UserEmail, userRegisterDto.Password);
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            var query = new LoginQuery(userLoginDto.UserName, userLoginDto.Password);
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }


        
        [HttpPost("Sign out")]
        public ActionResult LogOff()
        {  
            return RedirectToAction("Index", "Home");
        }


        private readonly IAuthRepository _authRepository;
        private readonly IJwtGenerator _jwtGenerator;

        //public LogoffQueryHandler(IAuthRepository authRepository, IJwtGenerator jwtGenerator)
        //{
        //    _authRepository = authRepository;
        //    _jwtGenerator = jwtGenerator;
        //}


    }
}
