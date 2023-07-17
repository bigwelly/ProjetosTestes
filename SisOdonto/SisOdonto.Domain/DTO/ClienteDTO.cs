using SisOdonto.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisOdonto.Domain.DTO
{
    public partial class ClienteDTO
    {

        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string DataNascimento { get; set; }
        public string IdcSexo { get; set; } = null!;
        public string? Cpf { get; set; }
        public string? Rg { get; set; }
        public int? CodigoCep { get; set; }
        public string? Numero { get; set; }
        public string? Complemento { get; set; }
        public string? DddTelefone { get; set; }
        public string? NumTelefone { get; set; }
        public string? DddCelular { get; set; }
        public string? NumCelular { get; set; }
        public string? Email { get; set; }

        public virtual CepDTO? CodigoCepNavigation { get; set; }
        public virtual ICollection<AgendamentoDTO?> Agendamentos { get; set; }
        public virtual ICollection<OrcamentoDTO?> Orcamentos { get; set; }
        public virtual ICollection<TratamentoDTO?> Tratamentos { get; set; }
    }
}
