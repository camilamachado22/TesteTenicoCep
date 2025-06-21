using System.ComponentModel.DataAnnotations;

namespace TesteTecnicoCep.Models
{
    public class Cliente
    {
        public Cliente()
        {
            Contatos = new HashSet<Contato>(); 
        }
        [Required]
        public int id { get; set; }
        [Required]
        public string nome { get; set; }
        [Required]
        
        public DateTime data_cadastro { get; set; }

        public virtual Endereco Endereco { get; set; }

        public virtual ICollection<Contato> Contatos { get; set; }

    }
}

