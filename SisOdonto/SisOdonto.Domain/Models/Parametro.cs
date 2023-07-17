namespace SisOdonto.Domain.Models
{
    public partial class Parametro
    {
        public int Id { get; set; }
        public TimeSpan HoraInicialAtendimento { get; set; }
        public TimeSpan HoraFinalAtendimento { get; set; }
        public bool Atende2a { get; set; }
        public bool Atende3a { get; set; }
        public bool Atende4a { get; set; }
        public bool Atende5a { get; set; }
        public bool Atende6a { get; set; }
        public bool AtendeSab { get; set; }
        public bool AtendeDom { get; set; }
        public bool AceitaConvenio { get; set; }
    }
}
