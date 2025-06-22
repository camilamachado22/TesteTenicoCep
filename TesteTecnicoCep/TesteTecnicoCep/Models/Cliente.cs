using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace TesteTecnicoCep.Models
{
    public class Cliente
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
<<<<<<< Updated upstream
        public Cliente()
        {
            Contatos = new HashSet<Contato>(); 
        }
        [Required]
        public int id { get; set; }
        [Required]
        public string nome { get; set; }
        [Required]
=======

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        
        public string ? nome { get; set; }
        [Required]
        
        public DateTime data_cadastro { get; set; }
        public virtual Endereco? Endereco { get; set; }
        public virtual ICollection<Contato>? Contato { get; set; }

<<<<<<< Updated upstream
        public virtual Endereco Endereco { get; set; }

        public virtual ICollection<Contato> Contatos { get; set; }

=======
>>>>>>> Stashed changes
    }
}

