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
        public async Task<IActionResult> Post(UsuarioAddDto usuario)
        {
            if (usuario == null) return BadRequest("Dados Inválidos");

            var usuarioAdd = _mapper.Map<Usuario>(usuario);

            _repository.Add(usuarioAdd);

            return await _repository.SaveChangesAsync()
                ? Ok("Usuário Adicionado com sucesso!") : BadRequest("Erro ao adicionar o usuário");
        }
    }
}
