﻿namespace Domain.Entities
{
    public class Amostra : Base
    {
        public string Nome { get; set; }
        public int NumeroDeExames { get; set; }
        public ICollection<Exame> Exames { get; set; }
    }
}
