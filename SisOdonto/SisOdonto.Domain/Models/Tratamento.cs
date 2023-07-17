using System;
using System.Collections.Generic;

namespace SisOdonto.Domain.Models
{
    public partial class Tratamento
    {
        public Tratamento()
        {
            TratamentoItens = new HashSet<TratamentoIten>();
            TratamentoPagamentos = new HashSet<TratamentoPagamento>();
        }

        public long Id { get; set; }
        public int IdCliente { get; set; }
        public DateTime DataInicio { get; set; }
        public decimal ValorEntrada { get; set; }
        public decimal ValorTotal { get; set; }
        public string? Observacao { get; set; }
        public int IdTipoPagamento { get; set; }
        public int IdFormaPagamento { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; } = null!;
        public virtual FormaPagamento IdFormaPagamentoNavigation { get; set; } = null!;
        public virtual TipoPagamento IdTipoPagamentoNavigation { get; set; } = null!;
        public virtual ICollection<TratamentoIten> TratamentoItens { get; set; }
        public virtual ICollection<TratamentoPagamento> TratamentoPagamentos { get; set; }
    }
}
