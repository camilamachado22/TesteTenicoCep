using System;
using TesteTecnicoCep.Data;

namespace TesteTecnicoCep.Repositories
{
    public class ClienteRepository
    {
        private readonly TesteTecnicoCepDbContext _context;
        public ClienteRepository(TesteTecnicoCepDbContext context) => _context = context;
    }
}
