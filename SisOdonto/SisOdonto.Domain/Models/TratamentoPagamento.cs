using System;
using System.Collections.Generic;

namespace SisOdonto.Domain.Models
{
    public partial class TratamentoPagamento
    {
        public long Id { get; set; }
        public long IdTratamento { get; set; }
        public int NumeroParcela { get; set; }
        public decimal ValorParcela { get; set; }
        public decimal ValorPago { get; set; }
        public DateTime? DataPagamento { get; set; }

        public virtual Tratamento IdTratamentoNavigation { get; set; } = null!;
    }
}
