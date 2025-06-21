using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TesteTecnicoCep.Models;

namespace TesteTecnicoCep.DTOs
{
    public class ClienteDTO
    {
        public ClienteDTO()
        {
            Contatos = new HashSet<ContatoDTO>();
        }
        [Required]
        public int id { get; set; }
        [Required]
        public  string? nome { get; set; }
        [Required]
        public DateTime data_cadastro { get; set; }

        public virtual EnderecoDTO Endereco { get; set; }

        public virtual ICollection<ContatoDTO> Contatos { get; set; }

    }
}
