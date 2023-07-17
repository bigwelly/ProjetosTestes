using System;
using System.Collections.Generic;

namespace SisOdonto.Domain.Models
{
    public partial class ConvenioPlano
    {
        public ConvenioPlano()
        {
            ProcedimentoConvenios = new HashSet<ProcedimentoConvenio>();
        }

        public long Codigo { get; set; }
        public int IdConvenio { get; set; }
        public short TipoPlano { get; set; }
        public string NomePlano { get; set; } = null!;

        public virtual Convenio IdConvenioNavigation { get; set; } = null!;
        public virtual ICollection<ProcedimentoConvenio> ProcedimentoConvenios { get; set; }
    }
}
