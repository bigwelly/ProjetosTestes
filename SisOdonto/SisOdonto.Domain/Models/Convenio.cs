namespace SisOdonto.Domain.Models
{
    public partial class Convenio
    {
        public Convenio()
        {
            ConvenioPlanos = new HashSet<ConvenioPlano>();
            ProcedimentoConvenios = new HashSet<ProcedimentoConvenio>();
            TratamentoPagtoConvenios = new HashSet<TratamentoPagtoConvenio>();
        }

        public int Id { get; set; }
        public string NomeFantasia { get; set; } = null!;
        public string RazaoSocial { get; set; } = null!;
        public string Cnpj { get; set; } = null!;
        public DateTime DataInclusao { get; set; }
        public bool? Ativo { get; set; }
        public string DddTelefone1 { get; set; } = null!;
        public string NumTelefone1 { get; set; } = null!;
        public string? DddTelefone2 { get; set; }
        public string? NumTelefone2 { get; set; }
        public string? Email { get; set; }
        public int NumAns { get; set; }

        public virtual ICollection<ConvenioPlano> ConvenioPlanos { get; set; }
        public virtual ICollection<ProcedimentoConvenio> ProcedimentoConvenios { get; set; }
        public virtual ICollection<TratamentoPagtoConvenio> TratamentoPagtoConvenios { get; set; }
    }
}
