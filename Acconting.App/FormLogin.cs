using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValidationComponents;
using Acconting.DateLayer.Context;

namespace Acconting.App
{
    public partial class FormLogin : Form
    {
        public bool isEdite = false;
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (BaseValidator.IsFormValid(this.components))
            {
                using (UnitOfWork db = new UnitOfWork())
                {
                    if (isEdite)
                    {
                        var login = db.loginRepository.Get().First();
                        login.UserId = txtUserName.Text;
                        login.password = txtPassword.Text;
                        db.loginRepository.Update(login);
                        db.Save();
                        Application.Restart();
                    }
                    else
                    {

                        if (db.loginRepository.Get(l => l.UserId == txtUserName.Text && l.password == txtPassword.Text).Any())
                        {
                            DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            RtlMessageBox.Show("کاربری یافت نشو");
                        }
                    }
                }
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            if(isEdite == true)
            {
                this.Text = "ویرایش مشخصات ورود";
                btnLogin.Text = "تغییر مشخصات";
                using (UnitOfWork db = new UnitOfWork())
                {
                    var login = db.loginRepository.Get().First();
                    txtUserName.Text = login.UserId;
                    txtPassword.Text = login.password;
                }
            }
        }
    }
}
