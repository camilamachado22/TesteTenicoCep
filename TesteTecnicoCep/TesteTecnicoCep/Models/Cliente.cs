using System.ComponentModel.DataAnnotations;

namespace TesteTecnicoCep.Models
{
    public class Cliente
    {
        
        [Required]
        public int id { get; set; }
        [Required]
        public string ? nome { get; set; }
        [Required]
        
        public DateTime data_cadastro { get; set; }

        
    }
}

