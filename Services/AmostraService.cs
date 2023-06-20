using AutoMapper;
using DTOs.Responses;
using DTOs.ViewModels;
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
            var amostras = await _repository.GetAmostrasAsync();
            return _mapper.Map<IEnumerable<AmostraDto>>(amostras);
        }

        public async Task<AmostraDto> GetAmostraByIdAsync(int id)
        {
            var amostra = await _repository.GetAmostraByIdAsync(id);
            return _mapper.Map<AmostraDto>(amostra);
        }

        public async Task AddAmostraAsync(AmostraAddViewModel amostra)
        {
            var amostraEntity = _mapper.Map<Amostra>(amostra);
            _repository.Add(amostraEntity);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAmostraAsync(int id, AmostraUpdateViewModel amostra)
        {
            var amostraBanco = await _repository.GetAmostraByIdAsync(id);
            var amostraEntity = _mapper.Map(amostra, amostraBanco);
            _repository.Update(amostraEntity);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAmostraAsync(int id)
        {
            var amostra = await _repository.GetAmostraByIdAsync(id);
            _repository.Delete(amostra);
            await _repository.SaveChangesAsync();
        }
    }
}
