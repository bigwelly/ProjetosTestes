using SisOdonto.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisOdonto.Domain.DTO
{
    public partial class TratamentoDTO
    {
        public long Id { get; set; }
        public int IdCliente { get; set; }
        public DateTime DataInicio { get; set; }
        public decimal ValorEntrada { get; set; }
        public decimal ValorTotal { get; set; }
        public string? Observacao { get; set; }
        public int IdTipoPagamento { get; set; }
        public int IdFormaPagamento { get; set; }

        public virtual ClienteDTO? Cliente { get; set; } = null!;
    }
}
