using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteTecnicoCep.Data;
using TesteTecnicoCep.DTOs;
using TesteTecnicoCep.Models;
using TesteTecnicoCep.Services;

namespace TesteTecnicoCep.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly TesteTecnicoCepDbContext _context;
        private readonly IMapper _mapper;

        public ClientesController(TesteTecnicoCepDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteCadastroDTO>>> GetClientes()
        {
            var clientes = await _context.cliente
                .Include(c => c.Endereco)
                .Include(c => c.Contatos)
                .ToListAsync();

            return _mapper.Map<List<ClienteCadastroDTO>>(clientes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteCadastroDTO>> GetCliente(int id)
        {
            var cliente = await _context.cliente
         .Include(c => c.Endereco)  
         .Include(c => c.Contatos)  
         .FirstOrDefaultAsync(c => c.id == id);

            if (cliente == null)
            {
                return NotFound();
            }

            return _mapper.Map<ClienteCadastroDTO>(cliente);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, ClienteCadastroDTO clienteUpdate, [FromServices] CepService cepService)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            var clienteExistente = await _context.cliente
                .Include(c => c.Contatos)
                .Include(c => c.Endereco)
                .FirstOrDefaultAsync(c => c.id == id);

            clienteExistente.nome = clienteUpdate.Nome;

           
            var contatoExistente = clienteExistente?.Contatos?.ToString();
            if (!string.IsNullOrWhiteSpace(contatoExistente))
            {
                var contato = clienteExistente.Contatos;
                contato.tipo = clienteUpdate.TipoContato;
                contato.texto = clienteUpdate.TextoContato;
            }

            
            if (clienteUpdate.Cep != clienteExistente.Endereco?.cep)
            {
                var (logradouro, cidade, complemento) = await cepService.BuscarEnderecoPorCep(clienteUpdate.Cep);
                clienteExistente.Endereco.cep = clienteUpdate.Cep;
                clienteExistente.Endereco.logradouro = logradouro;
                clienteExistente.Endereco.cidade = cidade;
                clienteExistente.Endereco.complemento = complemento;
            }
            clienteExistente.Endereco.numero = clienteUpdate.Numero;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(ClienteCadastroDTO clientecadastro, [FromServices] CepService cepService)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var (logradouro, cidade,complemento) = await cepService.BuscarEnderecoPorCep(clientecadastro.Cep);
            
            var cliente = new Cliente
            {
                nome = clientecadastro.Nome,
                data_cadastro = DateTime.Now,
                Contatos = new Contato
        {
            
                tipo = clientecadastro.TipoContato,
                texto = clientecadastro.TextoContato
           
        },
                Endereco = new Endereco
                {
                    cep = clientecadastro.Cep,
                    numero = clientecadastro.Numero,
                    logradouro = logradouro,
                    cidade = cidade,
                    complemento = complemento

                    
                  
                }
            };

            _context.cliente.Add(cliente);
            await _context.SaveChangesAsync();


            var responseDto = new ClienteCadastroDTO
            {
                Nome = cliente.nome,
                TipoContato = cliente.Contatos?.tipo,
                TextoContato = cliente.Contatos?.texto,
                Cep = cliente.Endereco.cep,
                Numero = cliente.Endereco.numero,
                Logradouro = cliente.Endereco.logradouro,
                Cidade = cliente.Endereco.cidade,
                Complemento = cliente.Endereco.complemento
            };

            return CreatedAtAction("GetCliente", new { id = cliente.id }, responseDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.cliente.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(int id)
        {
            return _context.cliente.Any(e => e.id == id);
        }
    }
}
