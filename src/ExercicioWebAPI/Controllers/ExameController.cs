using AutoMapper;
using Domain.Entities;
using DTOs.Responses;
using DTOs.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExercicioWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ExameController : ControllerBase
    {
        private readonly IExameService _service;
        private const string AdminRole = "ADMIN";
        private readonly IMapper _mapper;

        public ExameController(IExameService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<ExameDto> exames = await _service.GetExamesAsync();
            List<ExameDto> examesRetorno = exames.ToList();

            return Ok(examesRetorno);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            ExameDto exame = await _service.GetExameByIdAsync(id);

            return exame != null
                ? Ok(exame)
                : NotFound("Exame não encontrado");
        }

        [Authorize(Roles = AdminRole)]
        [HttpPost]
        public async Task<IActionResult> Post(ExameAddViewModel exame)
        {
            if (exame == null)
            {
                return BadRequest("Dados Inválidos");
            };

            Exame exameEntity = _mapper.Map<Exame>(exame);
            await _service.AddExameAsync(exameEntity);

            return Ok(new { message = "Exame adicionado com sucesso!" });
        }

        [Authorize(Roles = AdminRole)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ExameUpdateViewModel exame)
        {
            if (id <= 0)
            {
                return BadRequest("Exame não informado");
            };

            ExameDto exameExistente = await _service.GetExameByIdAsync(id);

            if (exameExistente == null)
            {
                return NotFound("Exame não encontrado");
            }

            Exame exameEntity = _mapper.Map<Exame>(exame);
            exameEntity.Id = id;
            await _service.UpdateExameAsync(exameEntity);

            return Ok("Exame atualizado com sucesso!");
        }

        [Authorize(Roles = AdminRole)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Exame inválido");
            };

            ExameDto exameExistente = await _service.GetExameByIdAsync(id);

            if (exameExistente == null)
            {
                return NotFound("Exame não encontrado");
            };

            await _service.DeleteExameAsync(id);

            return Ok(new { message = "Exame deletado com sucesso!" });
        }
    }
}
