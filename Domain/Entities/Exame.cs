namespace Domain.Entities
{
    public class Exame : Base
    {
        public int ExameId { get; set; }
        public string Nome { get; set; }
        public string Resultado { get; set; }
        public Amostra Amostra { get; set; }
    }
}
