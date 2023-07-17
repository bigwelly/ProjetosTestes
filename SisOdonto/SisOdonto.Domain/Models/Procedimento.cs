namespace SisOdonto.Domain.Models
{
    public partial class Procedimento
    {
        public Procedimento()
        {
            TratamentoItens = new HashSet<TratamentoIten>();
        }

        public long Id { get; set; }
        public int IdEspecialidade { get; set; }
        public string NomeProcedimento { get; set; } = null!;
        public decimal Valor { get; set; }
        public short Ano { get; set; }
        public decimal PercDesc { get; set; }

        public virtual Especialidade IdEspecialidadeNavigation { get; set; } = null!;
        public virtual ICollection<TratamentoIten> TratamentoItens { get; set; }
    }
}
