namespace DTOs.Responses
{
    public class UserLoginResponse
    {
        public bool Sucesso { get; private set; }

        public string Token { get; private set; }

        public DateTime? DataExpiracao { get; private set; }
        public List<string> Erros { get; private set; }

        public UserLoginResponse(bool sucesso = false, string token = null, DateTime? dataExpiracao = null)
        {
            Sucesso = sucesso;
            Token = token;
            DataExpiracao = dataExpiracao;
            Erros = new List<string>();
        }

        public void AddError(string erro)
        {
            Erros.Add(erro);
        }
    }
}
