using System;
using System.Collections.Generic;

namespace SisOdonto.Domain.Models
{
    public partial class TipoPagamento
    {
        public TipoPagamento()
        {
            Tratamentos = new HashSet<Tratamento>();
        }

        public int Id { get; set; }
        public string Nome { get; set; } = null!;

        public virtual ICollection<Tratamento> Tratamentos { get; set; }
    }
}
