namespace SisOdonto.Domain.Models
{
    public partial class OrcamentoIten
    {
        public long Id { get; set; }
        public int IdOrcamento { get; set; }
        public int IdDenticao { get; set; }
        public long IdProcedimento { get; set; }
    }
}
