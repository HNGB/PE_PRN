using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
using System.IO;
using PRN211PE_SU22_HuynhNgoGiaBao.Repo.Models;

namespace PRN211PE_SU22_HuynhNgoGiaBao
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txtUserID.Text.Trim();
            string password = txtPassword.Text.Trim();
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Email or Password is empty!", "@Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string cs = GetConnectionString();
                using (var db = new BankAccountTypeContext(cs))
                {
                    var user = db.Users.Where(a => a.UserId == userName && a.Password == password).FirstOrDefault();
                    if (user == null)
                    {
                        MessageBox.Show("Invalid User or Password!", "@Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (user.UserRole == 1)
                        {
                            frmAccountManagement frmAM = new frmAccountManagement();
                            frmAM.Closed += (_, _) => this.Close();
                            this.Hide();
                            frmAM.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("You are not allow to acess this function", "@Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

            }
        }

        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            var strConn = config["ConnectionStrings:BankAccountTypeDB"];
            return strConn;
        }
    }

}
