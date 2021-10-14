using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interface;
using DataAccessEF.Repositories;

namespace DataAccessEF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApiDemoContext db;

        public UnitOfWork(ApiDemoContext db)
        {
            this.db = db;
            User = new UserRepository(db); 
        }

        public IUserRepository User { get; private set; }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public void Rollback()
        {
            db.Dispose();
        }
    }
}
