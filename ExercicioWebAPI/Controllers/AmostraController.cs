using ExercicioWebAPI.DTOs.ViewModels;
using ExercicioWebAPI.Models.Entities;
using ExercicioWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExercicioWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AmostraController : ControllerBase
    {
        private readonly IAmostraService _service;

        public AmostraController(IAmostraService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var amostras = await _service.GetAmostrasAsync();
            var amostrasRetorno = amostras.ToList();

            return amostrasRetorno.Any()
                ? Ok(amostrasRetorno)
                : BadRequest("Não há amostras");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var amostra = await _service.GetAmostraByIdAsync(id);

            return amostra != null
                ? Ok(amostra)
                : BadRequest("Amostra não encontrada");
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> Post(AmostraAddViewModel amostra)
        {
            if (amostra == null) return BadRequest("Dados Inválidos");

            await _service.AddAmostraAsync(amostra);

            return Ok("Amostra adicionada com sucesso!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, AmostraUpdateViewModel amostra)
        {
            if (id <= 0) return BadRequest("Amostra não informada");

            await _service.UpdateAmostraAsync(id, amostra);

            return Ok("Amostra atualizada com sucesso!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest("Amostra inválida");

            await _service.DeleteAmostraAsync(id);

            return Ok("Amostra deletada com sucesso!");
        }
    }
}
