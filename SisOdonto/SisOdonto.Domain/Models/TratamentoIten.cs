using System;
using System.Collections.Generic;

namespace SisOdonto.Domain.Models
{
    public partial class TratamentoIten
    {
        public TratamentoIten()
        {
            TratamentoPagtoConvenios = new HashSet<TratamentoPagtoConvenio>();
        }

        public long Id { get; set; }
        public long IdTratamento { get; set; }
        public int IdDenticao { get; set; }
        public long IdProcedimento { get; set; }
        public string Observacao { get; set; } = null!;

        public virtual Denticao IdDenticaoNavigation { get; set; } = null!;
        public virtual Procedimento IdProcedimentoNavigation { get; set; } = null!;
        public virtual Tratamento IdTratamentoNavigation { get; set; } = null!;
        public virtual ICollection<TratamentoPagtoConvenio> TratamentoPagtoConvenios { get; set; }
    }
}
