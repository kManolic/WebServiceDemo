using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IUnitOfWork
    {
        public IUserRepository User { get; }
        public int Commit();
        public void Rollback();
    }
}
