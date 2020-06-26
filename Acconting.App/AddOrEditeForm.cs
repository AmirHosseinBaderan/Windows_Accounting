using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Acconting.DateLayer.Context;
using Acconting.DateLayer;
using ValidationComponents;

namespace Acconting.App
{
    public partial class AddOrEditeForm : Form
    {
        public int customerID = 0;
        UnitOfWork db = new UnitOfWork();
        public AddOrEditeForm()
        {
            InitializeComponent();
        }


        private void btnBrowe_Click(object sender, EventArgs e)
        {
           
        }

        private void btnBrowe_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog penfile = new OpenFileDialog();
            if (penfile.ShowDialog() == DialogResult.OK)
            {

                pcCustomer.ImageLocation = penfile.FileName;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (BaseValidator.IsFormValid(this.components))
            {
                string IMGName = Guid.NewGuid().ToString() + Path.GetExtension(pcCustomer.ImageLocation);
                string path = Application.StartupPath + "/Images/";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                pcCustomer.Image.Save(path+IMGName);
                CustomerTB customer = new CustomerTB()
                {
                    Address = txtAddress.Text,
                    Email = txtEmail.Text,
                    Phone = txtPhone.Text,
                    FullName = txtName.Text,
                    CustomerIMG = IMGName
                };
                if (customerID == 0)
                {
                    db.customerRepository.InsertCustomer(customer);
                }
                else
                {
                    customer.CustomerID = customerID;
                    db.customerRepository.UpdateCustomer(customer);
                }
                db.Save();
                DialogResult = DialogResult.OK;
            }
        }

        private void AddOrEditeForm_Load(object sender, EventArgs e)
        {
            if(customerID != 0)
            {
                this.Text = "ویرایش شخص";
                btnSubmit.Text = "ویرایش";
                var customer = db.customerRepository.GetCustomerByID(customerID);
                txtEmail.Text = customer.Email;
                txtName.Text = customer.FullName;
                txtPhone.Text = customer.Phone;
                txtAddress.Text = customer.Address;
                pcCustomer.ImageLocation = Application.StartupPath + "/Images/" + customer.CustomerIMG;
            }
        }
    }
}
