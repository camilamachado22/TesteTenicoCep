using System;
using TesteTecnicoCep.Data;

namespace TesteTecnicoCep.Repositories
{
    public class EnderecoRepository
    {
           private readonly TesteTecnicoCepDbContext _context;
           public EnderecoRepository(TesteTecnicoCepDbContext context) => _context = context;
        
    }
}
