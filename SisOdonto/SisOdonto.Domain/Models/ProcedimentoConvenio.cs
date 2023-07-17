namespace SisOdonto.Domain.Models
{
    public partial class ProcedimentoConvenio
    {
        public long Id { get; set; }
        public int IdEspecialidade { get; set; }
        public int IdConvenio { get; set; }
        public string Procedimento { get; set; } = null!;
        public decimal Valor { get; set; }
        public short Ano { get; set; }
        public long CodigoPlano { get; set; }

        public virtual ConvenioPlano CodigoPlanoNavigation { get; set; } = null!;
        public virtual Convenio IdConvenioNavigation { get; set; } = null!;
        public virtual Especialidade IdEspecialidadeNavigation { get; set; } = null!;
    }
}
