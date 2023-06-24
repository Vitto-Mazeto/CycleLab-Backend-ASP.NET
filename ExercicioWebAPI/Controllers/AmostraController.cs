using DTOs.Responses;
using DTOs.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace ExercicioWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AmostraController : ControllerBase
    {
        private readonly IAmostraService _service;
        private const string AdminRole = "ADMIN";

        public AmostraController(IAmostraService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<AmostraDto> amostras = await _service.GetAmostrasAsync();
            List<AmostraDto> amostrasRetorno = amostras.ToList();

            return Ok(amostrasRetorno);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            AmostraDto amostra = await _service.GetAmostraByIdAsync(id);

            return amostra != null
                ? Ok(amostra)
                : NotFound("Amostra não encontrada");
        }

        [Authorize(Roles = AdminRole)]
        [HttpPost]
        public async Task<IActionResult> Post(AmostraAddViewModel amostra)
        {
            if (amostra == null)
            {
                return BadRequest("Dados Inválidos");
            };

            await _service.AddAmostraAsync(amostra);

            return Ok("Amostra adicionada com sucesso!");
        }

        [Authorize(Roles = AdminRole)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, AmostraUpdateViewModel amostra)
        {
            if (id <= 0)
            {
                return BadRequest("Amostra não informada");
            };

            AmostraDto amostraExistente = await _service.GetAmostraByIdAsync(id);

            if (amostraExistente == null)
            {
                return NotFound("Amostra não encontrada");
            }

            await _service.UpdateAmostraAsync(id, amostra);

            return Ok("Amostra atualizada com sucesso!");
        }

        [Authorize(Roles = AdminRole)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Amostra inválida");
            };

            AmostraDto amostraExistente = await _service.GetAmostraByIdAsync(id);

            if (amostraExistente == null)
            {
                return NotFound("Amostra não encontrada");
            };

            await _service.DeleteAmostraAsync(id);

            return Ok("Amostra deletada com sucesso!");
        }
    }
}
