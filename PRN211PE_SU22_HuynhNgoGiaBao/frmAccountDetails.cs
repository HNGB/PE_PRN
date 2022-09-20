using Microsoft.Extensions.Configuration;
using PRN211PE_SU22_HuynhNgoGiaBao.Repo.Models;
using PRN211PE_SU22_HuynhNgoGiaBao.Repo.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRN211PE_SU22_HuynhNgoGiaBao
{
    public partial class frmAccountDetails : Form
    {
        public IBankAccountRepo AccountRepo { get; set; }
        public bool InsertOrUpdate { get; set; }
        public BankAccount book { get; set; }
        public frmAccountDetails()
        {
            this.AccountRepo = new BankAccountRepo();
            InitializeComponent();
            ShowCbTypeID();
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

        public void ShowCbTypeID()
        {
            var cs = GetConnectionString();
            BankAccountTypeContext db = new BankAccountTypeContext(cs);
            var type = (from x in db.AccountTypes select x).ToList();
            BindingSource publisherSoucre = new BindingSource();
            publisherSoucre.DataSource = type;
            cbTypeID.DataSource = null;
            cbTypeID.DataSource = publisherSoucre;
            cbTypeID.ValueMember = "TypeID";
            this.cbTypeID.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string accountID = txtAccountID.Text;
                string accountName = txtAccountName.Text;
                DateTime openDate = DateTime.Parse (txtOpenDate.Text);
                string branchName = txtBranchName.Text;
                string typeID = cbTypeID.Text;
                if (string.IsNullOrEmpty(txtAccountID.Text.Trim()))
                {
                    throw new Exception("Account ID be not null!");
                }
                if (string.IsNullOrEmpty(txtAccountName.Text.Trim()))
                {
                    throw new Exception("Account Name be not null!");
                }
                if (string.IsNullOrEmpty(txtOpenDate.Text.Trim()))
                {
                    throw new Exception("Open Date be not null!");
                }
                if (string.IsNullOrEmpty(txtBranchName.Text.Trim()))
                {
                    throw new Exception("Branch Name be not null!");
                }
                if (txtBranchName.Text.Length < 5)
                {
                    throw new Exception("Branch Name must be greater than 5 character!");
                }
                BankAccount book = new BankAccount
                {
                    AccountId = accountID,
                    AccountName = accountName,
                    OpenDate = openDate,
                    BranchName = branchName,
                    TypeId = typeID
                };

                if (InsertOrUpdate == false)
                {
                    AccountRepo.InsertAccount(book);
                    MessageBox.Show(@"Add successfully!!", @"Add a new account", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    AccountRepo.UpdateAccount(book);
                    MessageBox.Show(@"Update successfully!!", @"Update Account", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, InsertOrUpdate == false ? "Add a new book" : "Update book");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) => Close();

    }
}
