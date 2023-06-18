using AutoMapper;
using ExercicioWebAPI.Models.DTOs;
using ExercicioWebAPI.Models.Entities;
using ExercicioWebAPI.Models.ViewModels;
using ExercicioWebAPI.Repository.Interfaces;
using ExercicioWebAPI.Services.Interfaces;


namespace ExercicioWebAPI.Services
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
            var usuario = await _repository.GetAmostraByIdAsync(id);
            return _mapper.Map<AmostraDto>(usuario);
        }

        public async Task AddAmostraAsync(AmostraAddViewModel usuario)
        {
            var amostraEntity = _mapper.Map<Amostra>(usuario);
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
