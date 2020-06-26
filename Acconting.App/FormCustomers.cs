using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Acconting.DateLayer.Context;
using Acconting.DateLayer;

namespace Acconting.App
{
    public partial class FormCustomers : Form
    {
        public FormCustomers()
        {
            InitializeComponent();
        }

        private void FormCustomers_Load(object sender, EventArgs e)
        {
            Bingrade();
        }

        public void Bingrade()
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                dgvCustomers.AutoGenerateColumns = false;
                dgvCustomers.DataSource = db.customerRepository.GetAllCustomers();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Bingrade();
            txtSearch.Text = "";
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                dgvCustomers.DataSource = db.customerRepository.GetCustomerByFilte(txtSearch.Text);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string Name = dgvCustomers.CurrentRow.Cells[1].Value.ToString();
            if (RtlMessageBox.Show($"آیا میخواهید  {Name} را حذف کنید؟ ", "توجه", MessageBoxButtons.OKCancel, MessageBoxIcon.Hand) == DialogResult.OK)
            {
                using (UnitOfWork db = new UnitOfWork())
                {
                    int customer = int.Parse(dgvCustomers.CurrentRow.Cells[0].Value.ToString());
                    db.customerRepository.DeleteCustomer(customer);
                    db.Save();
                    Bingrade();
                }
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            AddOrEditeForm frmAddorEdite = new AddOrEditeForm();
            if (frmAddorEdite.ShowDialog() == DialogResult.OK)
            {
                Bingrade();
            }
        }

        private void btnEdite_Click(object sender, EventArgs e)
        {
            if(dgvCustomers.CurrentRow != null)
            {
                int customerID = int.Parse(dgvCustomers.CurrentRow.Cells[0].Value.ToString());
                AddOrEditeForm frmAddorEdite = new AddOrEditeForm();
                frmAddorEdite.customerID = customerID;
                if(frmAddorEdite.ShowDialog()== DialogResult.OK)
                {
                    Bingrade();
                }
            }
        }
    }
}
