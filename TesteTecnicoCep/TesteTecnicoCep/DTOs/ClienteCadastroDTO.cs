using System.ComponentModel.DataAnnotations;

namespace TesteTecnicoCep.DTOs
{
    public class ClienteCadastroDTO
    {
    
            [Required(ErrorMessage = "O nome do cliente é obrigatório")]
            public string? Nome { get; set; }

            // Contato (email, telefone, etc.)
            [Required(ErrorMessage = "O tipo de contato é obrigatório")]
            public string ? TipoContato { get; set; }

            [Required(ErrorMessage = "O texto do contato é obrigatório")]
            public string ? TextoContato { get; set; }

            // Endereço
            [Required(ErrorMessage = "O CEP é obrigatório")]
            public string ? Cep { get; set; }

            [Required(ErrorMessage = "O número é obrigatório")]
            public string ? Numero { get; set; }
            
            public string ? Logradouro { get; set; }

            public string ? Cidade { get; set; }

            public string ? Complemento { get; set; }

           
        
    }
}

