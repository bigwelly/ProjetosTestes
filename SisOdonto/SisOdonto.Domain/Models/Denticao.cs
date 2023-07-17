namespace SisOdonto.Domain.Models
{
    public partial class Denticao
    {
        public Denticao()
        {
            TratamentoItens = new HashSet<TratamentoIten>();
        }

        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Altura { get; set; } = null!;
        public string Lado { get; set; } = null!;

        public virtual ICollection<TratamentoIten> TratamentoItens { get; set; }
    }
}
