using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acconting.DateLayer.Repository;
using System.Data.Entity;
using Acconting.ViewModle.Customers;

namespace Acconting.DateLayer.Servises
{
    public class CustomerRE : ICustomerRE
    {
        private AccontingEntities db;
        public CustomerRE(AccontingEntities Context)
        {
            db = Context;
        }
        public bool DeleteCustomer(CustomerTB customer)
        {
            try
            {
                db.Entry(customer).State = EntityState.Deleted;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteCustomer(int CustomerID)
        {
            try
            {
                var res = GetCustomerByID(CustomerID);
                DeleteCustomer(res);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<CustomerTB> GetAllCustomers()
        {
            return db.CustomerTB.ToList();
        }

        public IEnumerable<CustomerTB> GetCustomerByFilte(string parameter)
        {
            return db.CustomerTB.Where(search =>
            search.FullName.Contains(parameter) ||
            search.Email.Contains(parameter) ||
            search.Phone.Contains(parameter) ||
            search.Address.Contains(parameter)).ToList();
        }

        public CustomerTB GetCustomerByID(int CustomerID)
        {
            return db.CustomerTB.Find(CustomerID);
        }

        public int GetCustomerIDByName(string name)
        {
            return db.CustomerTB.First(c => c.FullName == name).CustomerID;
        }

        public string GetCustomerNameByID(int CustomerID)
        {
            return db.CustomerTB.Find(CustomerID).FullName;
        }

        public IEnumerable<ListCustomerViewModle> GetNamesCustomer(string Filter = "")
        {
            if (Filter == null)
            {
                return db.CustomerTB.Select(c => new ListCustomerViewModle()
                {
                    CustomerID = c.CustomerID,
                    FullName = c.FullName
                }).ToList();
            }
            return db.CustomerTB.Where(c => c.FullName.Contains(Filter)).Select(c => new ListCustomerViewModle()
            {
                CustomerID = c.CustomerID,
                FullName = c.FullName
            }).ToList();
        }

        public bool InsertCustomer(CustomerTB customer)
        {
            try
            {
                db.CustomerTB.Add(customer);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateCustomer(CustomerTB customer)
        {

            var Local = db.Set<CustomerTB>()
                .Local
                .FirstOrDefault(f => f.CustomerID == customer.CustomerID);
            if (Local != null)
            {
                db.Entry(Local).State = EntityState.Detached;
            };
            db.Entry(customer).State = EntityState.Modified;
            return true;
        }
    }
}
