using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Acconting.Utilitys;
using Acconting.Businus;
using Acconting.ViewModle.Acconting;

namespace Acconting.App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (RtlMessageBox.Show("ایا میخواهید برنامه را ببندید ", "توجه", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            if (RtlMessageBox.Show("ایا میخواهید فرم طرف حساب هارا باز کنید ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                FormCustomers frmCustomer = new FormCustomers();
                frmCustomer.Show();
            }
        }

        private void btnTransection_Click(object sender, EventArgs e)
        {
            FormTransection frmtransection = new FormTransection();
            frmtransection.ShowDialog();
            Report();
            
        }

        private void btnReportPay_Click(object sender, EventArgs e)
        {
            ForrmReport frmreport = new ForrmReport();
            frmreport.TypeID = 2;
            frmreport.ShowDialog();
            Report();
        }

        private void btnReportResive_Click(object sender, EventArgs e)
        {
            ForrmReport formReport = new ForrmReport();
            formReport.TypeID = 1;
            formReport.ShowDialog();
            Report();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            this.Hide();
            FormLogin frmligin = new FormLogin();
            if (frmligin.ShowDialog() == DialogResult.OK)
            {
                this.Show();
                lblDate.Text = DateTime.Now.Toshamsi();
                lbltime.Text = DateTime.Now.ToString("HH:mm:ss");
                Report();
            }
            else
            {
                Application.Exit();
            }
            /*/++++++++++++++++++++++++++++++++++++++++++++
             =========
             =========
             =========
             */
          

        }
        public void Report()
        {
            ReposrtNewModle report = Accont.reportformMain();
            txtPay.Text = report.Pay.ToString("#,0");
            txtReceive.Text = report.Receive.ToString("#,0");
            txtAccontingBulanse.Text = report.AccontingBalance.ToString("#,0");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbltime.Text = DateTime.Now.ToString("HH:mm:ss");
        }



        private void btnEditeLogin_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            formLogin.isEdite = true;
            formLogin.ShowDialog();
        }

        private void lblDate_Click(object sender, EventArgs e)
        {

        }
    }
}
