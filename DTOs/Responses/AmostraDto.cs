using Domain.Entities;

namespace DTOs.Responses
{
    public class AmostraDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int NumeroDeExames { get; set; }
        public ICollection<Exame> Exames { get; set; }
    }
}
