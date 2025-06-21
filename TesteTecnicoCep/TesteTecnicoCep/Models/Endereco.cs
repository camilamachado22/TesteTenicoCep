using System.ComponentModel.DataAnnotations;

namespace TesteTecnicoCep.Models
{
    public class Endereco
    {
        [Required]
        public string ?cep { get; set; }
        
        public string?logradouro { get; set; }

        public string? cidade { get; set; }
        public string? numero { get; set; }
        public string? complemento { get; set; }
        [Required]
        public int id_cliente { get; set; }
        
        

    }
}
