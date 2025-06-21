using System.ComponentModel.DataAnnotations;

namespace TesteTecnicoCep.Models
{
    public class Contato
    {
        [Required]
        public int id { get; set; }
        
        [Required]
        public int id_cliente { get; set; }
        [Required]
        public string? tipo { get; set; }
        public string? texto { get; set; }


    }
}
