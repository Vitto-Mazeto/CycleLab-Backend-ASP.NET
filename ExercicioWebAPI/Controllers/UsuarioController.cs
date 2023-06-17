using AutoMapper;
using ExercicioWebAPI.Models.DTOs;
using ExercicioWebAPI.Models.Entities;
using ExercicioWebAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ExercicioWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _repository;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var usuarios = await _repository.GetUsuariosAsync();
            var usuariosRetorno = new List<UsuarioDto>();

            foreach(Usuario usuario in usuarios)
            {
                usuariosRetorno.Add(_mapper.Map<UsuarioDto>(usuario));
            };

            return usuariosRetorno.Any()
                ? Ok(usuariosRetorno) : BadRequest("Não há usuarios");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = await _repository.GetUsuarioByIdAsync(id);
            var usuarioRetorno = _mapper.Map<UsuarioDto>(usuario);
            return usuarioRetorno != null 
                ? Ok(usuarioRetorno) : BadRequest("Usuario Não Encontrado");
        }
        [HttpPost]
        public async Task<IActionResult> Post(UsuarioAddViewModel usuario)
        {
            if (usuario == null) return BadRequest("Dados Inválidos");

            var usuarioAdd = _mapper.Map<Usuario>(usuario);

            _repository.Add(usuarioAdd);

            return await _repository.SaveChangesAsync()
                ? Ok("Usuário Adicionado com sucesso!") : BadRequest("Erro ao adicionar o usuário");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UsuarioUpdateViewModel usuario)
        {
            if (id <= 0) return BadRequest("Usuário não informado");

            var usuarioBanco = await _repository.GetUsuarioByIdAsync(id);

            // Mapeia o objeto "usuario" recebido como parâmetro para o objeto "usuarioBanco" obtido do banco de dados
            var usuarioUpdate = _mapper.Map(usuario, usuarioBanco);

            _repository.Update(usuarioUpdate);

            return await _repository.SaveChangesAsync()
                ? Ok("Usuário atualizado com sucesso!") : BadRequest("Erro ao atualizar o usuário");

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest("Usuário inválido");

            var usuarioDelete = await _repository.GetUsuarioByIdAsync(id);

            if (usuarioDelete == null) return NotFound("Usuário não encontrado");

            _repository.Delete(usuarioDelete);

            return await _repository.SaveChangesAsync()
                ? Ok("Usuário deletado com sucesso!") : BadRequest("Erro ao deletar o usuário");
        }

    }
}
