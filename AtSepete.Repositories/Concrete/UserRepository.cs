using AtSepete.Entities.Data;
using AtSepete.Repositories.Abstract;
using AtSepete.Repositories.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Repositories.Concrete
{
    public class UserRepository:GenericRepository<User>,IUserRepository
    {
        public UserRepository(AtSepeteDbContext Context):base(Context)
        {

        }

    }
}
