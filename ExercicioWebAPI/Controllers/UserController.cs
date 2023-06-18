using ExercicioWebAPI.Models.DTOs;
using ExercicioWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExercicioWebAPI.Controllers
{
    public class UserController : ControllerBase
    {
        private IIdentityService _identityService;

        public UserController(IIdentityService identityService) =>
            _identityService = identityService;

        [HttpPost("cadastro")]
        public async Task<ActionResult<UserRegisterResponse>> Cadastrar(UserRegisterRequest usuarioCadastro)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var role = usuarioCadastro.IsAdmin ? "ADMIN" : "USER";

            var resultado = await _identityService.RegisterUser(usuarioCadastro);
            if (resultado.Sucesso)
                return Ok(resultado);
            else if (resultado.Erros.Count > 0)
                return BadRequest(resultado);

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserRegisterResponse>> Login(UserLoginRequest usuarioLogin)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var resultado = await _identityService.Login(usuarioLogin);
            if (resultado.Sucesso)
                return Ok(resultado);

            return Unauthorized(resultado);
        }

        [HttpGet("usuarios")]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetUsersWithRoles()
        {
            var usersWithRoles = await _identityService.GetUsersWithRolesAsync();

            return Ok(usersWithRoles);
        }
    }
}
