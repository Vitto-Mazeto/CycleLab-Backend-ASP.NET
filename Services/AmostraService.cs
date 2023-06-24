using AutoMapper;
using DTOs.Responses;
using Domain.Entities;
using Repository.Interfaces;
using Services.Interfaces;

namespace Services
{
    public class AmostraService : IAmostraService
    {
        private readonly IAmostraRepository _repository;
        private readonly IMapper _mapper;

        public AmostraService(IAmostraRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AmostraDto>> GetAmostrasAsync()
        {
            IEnumerable<Amostra> amostras = await _repository.GetAmostrasAsync();
            return _mapper.Map<IEnumerable<AmostraDto>>(amostras);
        }

        public async Task<AmostraDto> GetAmostraByIdAsync(int id)
        {
            Amostra amostra = await _repository.GetAmostraByIdAsync(id);
            return _mapper.Map<AmostraDto>(amostra);
        }

        public async Task AddAmostraAsync(Amostra amostra)
        {
            _repository.Add(amostra);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAmostraAsync(Amostra amostra)
        {
            _repository.Update(amostra);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAmostraAsync(int id)
        {
            Amostra amostra = await _repository.GetAmostraByIdAsync(id);
            _repository.Delete(amostra);
            await _repository.SaveChangesAsync();
        }
    }
}
