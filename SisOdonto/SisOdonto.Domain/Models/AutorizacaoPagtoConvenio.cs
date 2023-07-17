using System;
using System.Collections.Generic;

namespace SisOdonto.Domain.Models
{
    public partial class AutorizacaoPagtoConvenio
    {
        public int Id { get; set; }
        public long IdTratamentoPagtoConvenio { get; set; }
        public DateTime DataAutorizacao { get; set; }
        public decimal ValorAutorizado { get; set; }

        public virtual TratamentoPagtoConvenio IdTratamentoPagtoConvenioNavigation { get; set; } = null!;
    }
}
