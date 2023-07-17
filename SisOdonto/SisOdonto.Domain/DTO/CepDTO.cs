namespace SisOdonto.Domain.DTO
{
    public class CepDTO
    {
        public CepDTO()
        {
            Clientes = new HashSet<ClienteDTO>();
        }

        public int Codigo { get; set; }
        public string? Uf { get; set; }
        public string? Municipio { get; set; }
        public string? Bairro { get; set; }
        public string? Logradouro { get; set; }
        public string? NomeEdificio { get; set; }

        public virtual ICollection<ClienteDTO> Clientes { get; set; }
    }
}
