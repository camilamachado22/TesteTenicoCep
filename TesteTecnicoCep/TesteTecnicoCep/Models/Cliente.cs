using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace TesteTecnicoCep.Models
{
    public class Cliente
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string? nome { get; set; }
        [Required]

        public DateTime data_cadastro { get; set; }
        public virtual Endereco? Endereco { get; set; }


        public virtual Contato? Contatos { get; set; }



    }
}
