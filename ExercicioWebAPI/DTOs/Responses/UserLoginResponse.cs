using System.Text.Json.Serialization;

namespace ExercicioWebAPI.DTOs.Responses
{
    public class UserLoginResponse
    {
        public bool Sucesso { get; private set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] //Caso não consiga logar não retorna pro usuario
        public string Token { get; private set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? DataExpiracao { get; private set; }
        public List<string> Erros { get; private set; }

        public UserLoginResponse() => Erros = new List<string>();

        public UserLoginResponse(bool sucesso = true) : this() =>
            Sucesso = sucesso;

        public UserLoginResponse(bool sucesso, string token, DateTime? dataExpiracao) : this(sucesso)
        {
            Token = token;
            DataExpiracao = dataExpiracao;
        }

        public void AdicionarErro(string erro) =>
            Erros.Add(erro);

        public void AdicionarErros(IEnumerable<string> erros) =>
            Erros.AddRange(erros);
    }
}
