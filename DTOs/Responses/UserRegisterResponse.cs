using System.Collections.Generic;

namespace DTOs.Responses
{
    public class UserRegisterResponse
    {
        public bool Sucesso { get; private set; }
        public List<string> Erros { get; private set; }

        public UserRegisterResponse(bool sucesso = true) // Preciso dar uma olhada nisso
        {
            Sucesso = sucesso;
            Erros = new List<string>();
        }

        public void AdicionarErros(IEnumerable<string> erros)
        {
            Erros.AddRange(erros);
        }
    }
}
