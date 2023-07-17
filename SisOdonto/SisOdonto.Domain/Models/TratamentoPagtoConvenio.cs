namespace SisOdonto.Domain.Models
{
    public partial class TratamentoPagtoConvenio
    {
        public TratamentoPagtoConvenio()
        {
            AutorizacaoPagtoConvenios = new HashSet<AutorizacaoPagtoConvenio>();
        }

        public long Id { get; set; }
        public long IdTratamentoIntens { get; set; }
        public int IdConvenio { get; set; }
        public decimal ValorPagoConvenio { get; set; }
        public DateTime? DataPagamento { get; set; }

        public virtual Convenio IdConvenioNavigation { get; set; } = null!;
        public virtual TratamentoIten IdTratamentoIntensNavigation { get; set; } = null!;
        public virtual ICollection<AutorizacaoPagtoConvenio> AutorizacaoPagtoConvenios { get; set; }
    }
}
