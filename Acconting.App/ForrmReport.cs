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
using Acconting.Utilitys;
using Acconting.ViewModle.Customers;
using Acconting.Utilitys;

namespace Acconting.App
{
    public partial class ForrmReport : Form
    {
        public int TypeID = 0;
        public ForrmReport()
        {
            InitializeComponent();
        }

        private void ForrmReport_Load(object sender, EventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                List<ListCustomerViewModle> list = new List<ListCustomerViewModle>();



                list.Add(new ListCustomerViewModle()
                {
                    CustomerID = 0,
                    FullName = "انتخاب کنید"
                });
                list.AddRange(db.customerRepository.GetNamesCustomer());
                cbCustomer.DataSource = list;
                cbCustomer.ValueMember = "FullName";
                cbCustomer.ValueMember = "CustomerID";
            }
            if (TypeID == 1)
            {
                this.Text = "گزارش دریافتی ها";
            }
            else
            {
                this.Text = "گزارش پرداختی ها ";
            }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Filter();
        }
        void Filter()
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                List<DateLayer.Acconting> resualt = new List<DateLayer.Acconting>();
                DateTime? startTime;
                DateTime? EndTime;

                if (txtfromDate.Text != "    /  /")
                {
                    startTime = Convert.ToDateTime(txtfromDate.Text);
                    startTime = DateConvertor.Tomiladi(startTime.Value);
                    resualt = resualt.Where(r => r.DateTime >= startTime.Value).ToList();
                }
                if (txttoDate.Text != "    /  /")
                {
                    EndTime = Convert.ToDateTime(txttoDate.Text);
                    EndTime = DateConvertor.Tomiladi(EndTime.Value);
                    resualt = resualt.Where(r => r.DateTime <= EndTime.Value).ToList();
                }



                if ((int)cbCustomer.SelectedValue != 0)
                {
                    int CustomerId = int.Parse(cbCustomer.SelectedValue.ToString());
                    resualt.AddRange(db.AccontingRepository.Get(a => a.TypeID == TypeID && a.CustomerID == CustomerId));
                }
                else
                {
                    resualt.AddRange(db.AccontingRepository.Get());
                }





                //var resualt = db.AccontingRepository.Get(a => a.TypeID == TypeID);
                // dgvReport.AutoGenerateColumns = false;
                //dgvReport.DataSource = resualt;
                dgvReport.Rows.Clear();
                foreach (var acconting in resualt)
                {
                    string CustomerName = db.customerRepository.GetCustomerNameByID(acconting.CustomerID);
                    dgvReport.Rows.Add(acconting.ID, CustomerName, acconting.Amount, acconting.DateTime.Toshamsi(), acconting.Description);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Filter();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvReport.CurrentRow != null)
            {
                int Id = int.Parse(dgvReport.CurrentRow.Cells[0].Value.ToString());
                if (RtlMessageBox.Show("ایا از حذف مطمعن هستید؟", "هشدار", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    using (UnitOfWork db = new UnitOfWork())
                    {
                        db.AccontingRepository.Delete(Id);
                        db.Save();
                        Filter();
                    }
                }
            }
        }

        private void btnEdite_Click(object sender, EventArgs e)
        {
            if (dgvReport.CurrentRow != null)
            {
                int Id = int.Parse(dgvReport.CurrentRow.Cells[0].Value.ToString());
                FormTransection formTransection = new FormTransection();
                formTransection.AccontID = Id;
                
                if (formTransection.ShowDialog() == DialogResult.OK)
                {
                    Filter();
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataTable dtPrint = new DataTable();
            dtPrint.Columns.Add("CustomerID");
            dtPrint.Columns.Add("Customer");
            dtPrint.Columns.Add("Amount");
            dtPrint.Columns.Add("Date");
            dtPrint.Columns.Add("Description");
            foreach(DataGridViewRow Item in dgvReport.Rows)
            {
                dtPrint.Rows.Add(
                    Item.Cells[0].Value.ToString(),
                    Item.Cells[1].Value.ToString(),
                    Item.Cells[2].Value.ToString(),
                    Item.Cells[3].Value.ToString(),
                    Item.Cells[4].Value.ToString()
                    );
            }
            stiPrint.Load(Application.StartupPath + "/Report.mrt");
            stiPrint.RegData("DT", dtPrint);
            stiPrint.Show();
            
        }
    }
}
