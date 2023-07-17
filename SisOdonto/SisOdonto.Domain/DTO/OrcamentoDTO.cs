using SisOdonto.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisOdonto.Domain.DTO
{
    public partial class OrcamentoDTO
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public bool IdcTratamento { get; set; }
        public int IdCliente { get; set; }

        public virtual ClienteDTO? Cliente { get; set; } = null!;
    }
}
