namespace ExercicioWebAPI.DTOs.Responses
{
    public class TokenResponseDto
    {
        public string Token { get; set; }
        public DateTime DataExpiracao { get; set; }

        public TokenResponseDto(string token, DateTime dataExpiracao)
        {
            Token = token;
            DataExpiracao = dataExpiracao;
        }
    }
}
