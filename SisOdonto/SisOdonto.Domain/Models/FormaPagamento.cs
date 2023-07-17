namespace SisOdonto.Domain.Models
{
    public partial class FormaPagamento
    {
        public FormaPagamento()
        {
            Tratamentos = new HashSet<Tratamento>();
        }

        public int Id { get; set; }
        public string? Nome { get; set; }

        public virtual ICollection<Tratamento> Tratamentos { get; set; }
    }
}
