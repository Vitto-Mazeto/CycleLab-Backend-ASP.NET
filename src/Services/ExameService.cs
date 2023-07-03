using AutoMapper;
using DTOs.Responses;
using Domain.Entities;
using Repository.Interfaces;
using Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class ExameService : IExameService
    {
        private readonly IExameRepository _repository;
        private readonly IMapper _mapper;

        public ExameService(IExameRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ExameDto>> GetExamesAsync()
        {
            IEnumerable<Exame> exames = await _repository.GetExamesAsync();
            return _mapper.Map<IEnumerable<ExameDto>>(exames);
        }

        public async Task<ExameDto> GetExameByIdAsync(int id)
        {
            Exame exame = await _repository.GetExameByIdAsync(id);
            return _mapper.Map<ExameDto>(exame);
        }

        public async Task AddExameAsync(Exame exame)
        {
            _repository.Add(exame);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateExameAsync(Exame exame)
        {
            _repository.Update(exame);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteExameAsync(int id)
        {
            Exame exame = await _repository.GetExameByIdAsync(id);
            _repository.Delete(exame);
            await _repository.SaveChangesAsync();
        }
    }
}
