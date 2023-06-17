using ExercicioWebAPI.Models.DTOs;
using ExercicioWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExercicioWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var usuarios = await _service.GetUsuariosAsync();
            var usuariosRetorno = usuarios.ToList();

            return usuariosRetorno.Any()
                ? Ok(usuariosRetorno)
                : BadRequest("Não há usuários");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = await _service.GetUsuarioByIdAsync(id);

            return usuario != null
                ? Ok(usuario)
                : BadRequest("Usuário não encontrado");
        }

        [HttpPost]
        public async Task<IActionResult> Post(UsuarioAddViewModel usuario)
        {
            if (usuario == null) return BadRequest("Dados Inválidos");

            await _service.AddUsuarioAsync(usuario);

            return Ok("Usuário adicionado com sucesso!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UsuarioUpdateViewModel usuario)
        {
            if (id <= 0) return BadRequest("Usuário não informado");

            await _service.UpdateUsuarioAsync(id, usuario);

            return Ok("Usuário atualizado com sucesso!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest("Usuário inválido");

            await _service.DeleteUsuarioAsync(id);

            return Ok("Usuário deletado com sucesso!");
        }
    }
}
