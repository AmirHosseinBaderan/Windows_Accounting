using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acconting.DateLayer.Repository;
using Acconting.DateLayer.Servises;
namespace Acconting.DateLayer.Context
{
    public class UnitOfWork : IDisposable
    {
        AccontingEntities db = new AccontingEntities();
        private ICustomerRE _customerRepository;
        public ICustomerRE customerRepository
        {
            get
            {
                if(_customerRepository == null)
                {
                    _customerRepository = new CustomerRE(db);
                }
                return _customerRepository;
            }
        }

        private GenericRepository<Acconting> _AccontingRepository;
        public GenericRepository<Acconting> AccontingRepository
        {
            get
            {
                if(_AccontingRepository == null)
                {
                    _AccontingRepository = new  GenericRepository<Acconting>(db);
                }
                return _AccontingRepository;
            }
        }


        private GenericRepository<Login> _loginRepository;
        public GenericRepository<Login> loginRepository
        {
            get
            {
                if (_loginRepository == null)
                {
                    _loginRepository = new GenericRepository<Login>(db);
                }
                return _loginRepository;
            }
        }
        public void Save()
        {
            db.SaveChanges();
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
