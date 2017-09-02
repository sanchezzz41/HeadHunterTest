using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HeadHunterTest.Domain.Entities;

namespace HeadHunterTest.Domain.Interfaces
{
    public interface IUserService
    {
        Task<Guid> Add(User us);
    }
}
