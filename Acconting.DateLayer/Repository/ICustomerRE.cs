using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acconting.ViewModle.Customers;

namespace Acconting.DateLayer.Repository
{
    public interface ICustomerRE
    {
        IEnumerable<CustomerTB> GetAllCustomers();
        CustomerTB GetCustomerByID(int CustomerID);
        IEnumerable<CustomerTB> GetCustomerByFilte(string parameter);
        IEnumerable<ListCustomerViewModle> GetNamesCustomer(string Filter = "");
        bool InsertCustomer(CustomerTB customer);
        bool UpdateCustomer(CustomerTB customer);
        bool DeleteCustomer(CustomerTB customer);
        bool DeleteCustomer(int CustomerID);
        int GetCustomerIDByName(string name);
        string GetCustomerNameByID(int CustomerID);
        
    }
}
