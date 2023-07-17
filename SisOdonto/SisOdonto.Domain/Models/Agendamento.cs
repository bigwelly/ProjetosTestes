namespace SisOdonto.Domain.Models
{
    public partial class Agendamento
    {
        public DateTime DataAgendamento { get; set; }
        public TimeSpan HoraAgendamento { get; set; }
        public int? IdCliente { get; set; }
        public string? Nome { get; set; }
        public string? Dddtelefone { get; set; }
        public string? NumTelefone { get; set; }

        public virtual Cliente? IdClienteNavigation { get; set; }
    }
}
