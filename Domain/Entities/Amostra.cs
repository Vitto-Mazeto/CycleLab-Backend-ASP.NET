namespace Domain.Entities
{
    public class Amostra : Base
    {
        public string Nome { get; set; }
        public int NumeroDeRegistro { get; set; }
        public ICollection<Exame> Exames { get; set; }
    }
}
