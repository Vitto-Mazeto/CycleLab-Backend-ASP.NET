using Microsoft.IdentityModel.Tokens;

namespace ExercicioWebAPI.Configurations
{
    //Classe usada junto com o IOptions para configurar essas opções no token
    public class JwtOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public SigningCredentials SigningCredentials { get; set; }
        public int Expiration { get; set; }
    }
}
