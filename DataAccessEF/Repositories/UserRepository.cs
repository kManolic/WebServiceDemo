using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Interface;

namespace DataAccessEF.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {     
        public UserRepository(ApiDemoContext db):base(db)
        {

        }

    }
}
