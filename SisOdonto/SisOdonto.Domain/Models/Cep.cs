using System;
using System.Collections.Generic;

namespace SisOdonto.Domain.Models
{
    public partial class Cep
    {
        public Cep()
        {
            Clientes = new HashSet<Cliente>();
        }

        public int Codigo { get; set; }
        public string? Uf { get; set; }
        public string? Municipio { get; set; }
        public string? Bairro { get; set; }
        public string? Logradouro { get; set; }
        public string? NomeEdificio { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
