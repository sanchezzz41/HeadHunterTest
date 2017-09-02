using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HeadHunterTest.Database;
using HeadHunterTest.Domain.Entities;
using HeadHunterTest.Domain.Interfaces;

namespace HeadHunterTest.Domain.Services
{
    public class UserService:IUserService
    {
        private readonly DatabaseContext _context;
        public UserService(DatabaseContext context)
        {
            _context = context;
            _context.Initialize(null).Wait();
        }
        public async Task<Guid> Add(User us)
        {
           return Guid.NewGuid();
        }
    }
}
