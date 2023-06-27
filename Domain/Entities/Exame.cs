namespace Domain.Entities
{
    public class Exame : Base
    {
        public string Nome { get; set; }
        public string Resultado { get; set; }
        public int AmostraId { get; set; }
        public Amostra Amostra { get; set; }
    }
}
