namespace TesteTecnicoCep.DTOs
{
    public class EnderecoDTO
    {
        public string cep { get; set; }
        public string? logradouro { get; set; }
        public string? cidade { get; set; }
        public string? numero { get; set; }
        public string? complemento { get; set; }
        public int id_cliente { get; set; }
    }
}
