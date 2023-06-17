using AutoMapper;
using ExercicioWebAPI.Models.DTOs;
using ExercicioWebAPI.Models.Entities;
using ExercicioWebAPI.Repository.Interfaces;
using ExercicioWebAPI.Services.Interfaces;


namespace ExercicioWebAPI.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UsuarioDto>> GetUsuariosAsync()
        {
            var usuarios = await _repository.GetUsuariosAsync();
            return _mapper.Map<IEnumerable<UsuarioDto>>(usuarios);
        }

        public async Task<UsuarioDto> GetUsuarioByIdAsync(int id)
        {
            var usuario = await _repository.GetUsuarioByIdAsync(id);
            return _mapper.Map<UsuarioDto>(usuario);
        }

        public async Task AddUsuarioAsync(UsuarioAddViewModel usuario)
        {
            var usuarioEntity = _mapper.Map<Usuario>(usuario);
            _repository.Add(usuarioEntity);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateUsuarioAsync(int id, UsuarioUpdateViewModel usuario)
        {
            var usuarioBanco = await _repository.GetUsuarioByIdAsync(id);
            var usuarioEntity = _mapper.Map(usuario, usuarioBanco);
            _repository.Update(usuarioEntity);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteUsuarioAsync(int id)
        {
            var usuario = await _repository.GetUsuarioByIdAsync(id);
            _repository.Delete(usuario);
            await _repository.SaveChangesAsync();
        }
    }
}
