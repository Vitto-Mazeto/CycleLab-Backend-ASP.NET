using DTOs.Responses;
using DTOs.ViewModels;
using Authentication.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExercicioWebAPI.Controllers
{
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IIdentityService _identityService;
        private const string AdminRole = "ADMIN";
        private const string UserRole = "USER";

        public UserController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("cadastro")]
        public async Task<ActionResult<UserRegisterResponse>> Cadastrar(UserRegisterViewModel usuarioCadastro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var resultado = await _identityService.RegisterUser(usuarioCadastro);
            if (resultado.Sucesso)
            {
                return Ok(resultado);
            }
            else if (resultado.Erros.Count > 0)
            {
                return BadRequest(resultado);
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserRegisterResponse>> Login(UserLoginViewModel usuarioLogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var resultado = await _identityService.Login(usuarioLogin);
            if (resultado.Sucesso)
            {
                return Ok(resultado);
            }

            return Unauthorized(resultado);
        }

        [Authorize(Roles = AdminRole + ", " + UserRole)]
        [HttpGet("usuarios")]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetUsersWithRoles()
        {
            var usersWithRoles = await _identityService.GetUsersWithRolesAsync();

            return Ok(usersWithRoles);
        }

        [Authorize(Roles = AdminRole)]
        [HttpPut("usuarios/{login}/alterar-permissao")]
        public async Task<IActionResult> AlterarPermissaoUsuario(string login)
        {
            try
            {
                await _identityService.ChangeUserPermissionAsync(login);
                return Ok("Sucesso");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = AdminRole)]
        [HttpDelete("usuarios/{login}")]
        public async Task<IActionResult> RemoverUsuario(string login)
        {
            try
            {
                await _identityService.RemoveUserAsync(login);
                return Ok("Sucesso");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
