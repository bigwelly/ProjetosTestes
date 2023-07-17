using System;
using System.Collections.Generic;

namespace SisOdonto.Domain.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Agendamentos = new HashSet<Agendamento>();
            Orcamentos = new HashSet<Orcamento>();
            Tratamentos = new HashSet<Tratamento>();
        }

        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public DateTime DataNascimento { get; set; }
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

        public virtual Cep? CodigoCepNavigation { get; set; }
        public virtual ICollection<Agendamento> Agendamentos { get; set; }
        public virtual ICollection<Orcamento> Orcamentos { get; set; }
        public virtual ICollection<Tratamento> Tratamentos { get; set; }
    }
}
