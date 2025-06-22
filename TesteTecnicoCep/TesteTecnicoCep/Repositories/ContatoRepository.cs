using System;
using TesteTecnicoCep.Data;

namespace TesteTecnicoCep.Repositories
{
    public class ContatoRepository
    {
        private readonly TesteTecnicoCepDbContext _context;
        public ContatoRepository(TesteTecnicoCepDbContext context) => _context = context;
       
    }
}
