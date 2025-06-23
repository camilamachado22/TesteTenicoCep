using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        public ClientesController(TesteTecnicoCepDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            return await _context.cliente.ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _context.cliente
         .Include(c => c.Endereco)  // Carrega o endereço
         .Include(c => c.Contatos)   // Carrega os contatos
         .FirstOrDefaultAsync(c => c.id == id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, ClienteCadastroDTO clienteUpdate, [FromServices] CepService cepService)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Busca o cliente existente com relacionamentos
            var clienteExistente = await _context.cliente
                .Include(c => c.Contatos)
                .Include(c => c.Endereco)
                .FirstOrDefaultAsync(c => c.id == id);

            clienteExistente.nome = clienteUpdate.Nome;

            // Atualiza contato (assumindo 1 contato por cliente)
            var contatoExistente = clienteExistente?.Contatos?.ToString();
            if (!string.IsNullOrWhiteSpace(contatoExistente))
            {
                var contato = clienteExistente?.Contatos;
                contato.tipo = clienteUpdate.TipoContato;
                contato.texto = clienteUpdate.TextoContato;
            }

            // Atualiza endereço
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

            return CreatedAtAction("GetCliente", new { id = cliente.id }, cliente);
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
