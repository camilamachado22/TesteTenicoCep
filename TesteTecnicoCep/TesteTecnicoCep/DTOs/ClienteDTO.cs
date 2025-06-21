using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TesteTecnicoCep.DTOs
{
    public class ClienteDTO
    {
        [Required]
        public int id { get; set; }
        [Required]
        public  string? nome { get; set; }
        [Required]
        public DateTime data_cadastro { get; set; }
        

    }
}
