using ExercicioWebAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExercicioWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioController(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var usuarios = _repository.GetUsuarios();
            return usuarios.Any() 
                ? Ok(usuarios)
                : BadRequest("Não há usuarios");
        }
    }
}
