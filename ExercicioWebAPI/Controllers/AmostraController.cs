using AutoMapper;
using Domain.Entities;
using DTOs.Responses;
using DTOs.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
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
        private readonly IMapper _mapper;

        public AmostraController(IAmostraService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
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

            var amostraEntity = _mapper.Map<Amostra>(amostra);
            await _service.AddAmostraAsync(amostraEntity);

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

            var amostraEntity = _mapper.Map<Amostra>(amostra);
            amostraEntity.Id = id;
            await _service.UpdateAmostraAsync(amostraEntity);

            return Ok("Amostra atualizada com sucesso!");
        }

        [Authorize(Roles = AdminRole)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Amostra inválida");
            }

            AmostraDto amostraExistente = await _service.GetAmostraByIdAsync(id);

            if (amostraExistente == null)
            {
                return NotFound("Amostra não encontrada");
            }

            await _service.DeleteAmostraAsync(id);

            return Ok(new { message = "Amostra deletada com sucesso!" });
        }

        [HttpGet("{id}/exames")]
        public async Task<ActionResult<IEnumerable<ExameDto>>> GetExamesByAmostraId(int id)
        {
            var amostraDto = await _service.GetAmostraByIdAsync(id);

            if (amostraDto == null)
            {
                return NotFound();
            }

            var amostra = _mapper.Map<Amostra>(amostraDto);

            var exames = amostra.Exames;

            var examesDto = _mapper.Map<IEnumerable<ExameDto>>(exames);

            return Ok(examesDto);
        }

    }
}
