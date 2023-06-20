using DTOs.Responses;
using DTOs.ViewModels;
using Authentication.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExercicioWebAPI.Controllers
{
    public class UserController : ControllerBase
    {
        private IIdentityService _identityService;

        public UserController(IIdentityService identityService) =>
            _identityService = identityService;

        [HttpPost("cadastro")]
        public async Task<ActionResult<UserRegisterResponse>> Cadastrar(UserRegisterViewModel usuarioCadastro)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var resultado = await _identityService.RegisterUser(usuarioCadastro);
            if (resultado.Sucesso)
                return Ok(resultado);
            else if (resultado.Erros.Count > 0)
                return BadRequest(resultado);

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserRegisterResponse>> Login(UserLoginViewModel usuarioLogin)
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

        [Authorize(Roles = "ADMIN")]
        [HttpPut("usuarios/{login}/alterar-permissao")]
        public async Task<IActionResult> AlterarPermissaoUsuario(string login)
        {
            try
            {
                await _identityService.AlterarPermissaoUsuarioAsync(login);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("usuarios/{login}")]
        public async Task<IActionResult> RemoverUsuario(string login)
        {
            try
            {
                await _identityService.RemoverUsuarioAsync(login);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
