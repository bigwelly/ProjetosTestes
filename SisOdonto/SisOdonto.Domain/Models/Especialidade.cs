namespace SisOdonto.Domain.Models
{
    public partial class Especialidade
    {
        public Especialidade()
        {
            ProcedimentoConvenios = new HashSet<ProcedimentoConvenio>();
            Procedimentos = new HashSet<Procedimento>();
        }

        public int Id { get; set; }
        public string Nome { get; set; } = null!;

        public virtual ICollection<ProcedimentoConvenio> ProcedimentoConvenios { get; set; }
        public virtual ICollection<Procedimento> Procedimentos { get; set; }
    }
}
