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
using ValidationComponents;
using Acconting.DateLayer;


namespace Acconting.App
{
    public partial class FormTransection : Form
    {

        private UnitOfWork db;
        public int AccontID = 0;
        public FormTransection()
        {
            InitializeComponent();
        }

        private void FormTransection_Load(object sender, EventArgs e)
        {
            db = new UnitOfWork();
            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.DataSource = db.customerRepository.GetNamesCustomer();
            if(AccontID != 0)
            {
                var Res = db.AccontingRepository.GetByID(AccontID);
                txtAmount.Text = Res.Amount.ToString();
                txtDescription.Text = Res.Description;
                txtName.Text = db.customerRepository.GetCustomerNameByID(Res.CustomerID);
                if(Res.TypeID == 1)
                {
                    rbResive.Checked = true;
                }
                else
                {
                    rbPay.Checked = true;
                }
                this.Text = "ویاریش";
                btnSave.Text = "ویرایش";
                db.Dispose();
            }
        }

        private void txtfilter_TextChanged(object sender, EventArgs e)
        {
            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.DataSource = db.customerRepository.GetNamesCustomer(txtfilter.Text);
        }



        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtName.Text = dgvCustomers.CurrentRow.Cells[1].Value.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (BaseValidator.IsFormValid(this.components))
            {
                if (rbPay.Checked||rbResive.Checked)
                {
                    db = new UnitOfWork();
                    DateLayer.Acconting acconting = new DateLayer.Acconting()
                    {
                        Amount = int.Parse(txtAmount.Value.ToString()),
                        CustomerID = db.customerRepository.GetCustomerIDByName(txtName.Text),
                        TypeID = (rbResive.Checked) ? 1 : 2,
                        DateTime = DateTime.Now,
                        Description = txtDescription.Text
                    };
                    if (AccontID == 0)
                    {
                        db.AccontingRepository.Insert(acconting);
                        
                    }
                    else
                    {
                        acconting.ID = AccontID;
                        db.AccontingRepository.Update(acconting);
                        
                    }
                    db.Save();
                    db.Dispose();
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    RtlMessageBox.Show("لطفا نوع تراکنش را انتخاب کنید ");
                }
            }
        
        }


    }
}

