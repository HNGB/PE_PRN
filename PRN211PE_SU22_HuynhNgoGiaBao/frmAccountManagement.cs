using PRN211PE_SU22_HuynhNgoGiaBao.Repo.Models;
using PRN211PE_SU22_HuynhNgoGiaBao.Repo.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRN211PE_SU22_HuynhNgoGiaBao
{
    public partial class frmAccountManagement : Form
    {
        private IBankAccountRepo accountRepo;
        BindingSource source;
        public frmAccountManagement()
        {
            this.accountRepo = new BankAccountRepo();
            InitializeComponent();
        }

        private void ClearText()
        {
            txtAccountID.Text = string.Empty;
            txtAccountName.Text = string.Empty;
            txtOpenDate.Text = string.Empty;
            txtBranchName.Text = string.Empty;
            txtTypeID.Text = string.Empty;
        }

        private void LoadAccountList()
        {
            var books = this.accountRepo.GetAccounts();
            try
            {
                // The BindingSource component is designed simplify
                // the process of binding controls to an underlying data source
                //this.dgvBookGrid.DataSource = books.ToList();
                source = new BindingSource();
                source.DataSource = books.ToList();

                txtAccountID.DataBindings.Clear();
                txtAccountName.DataBindings.Clear();
                txtOpenDate.DataBindings.Clear();
                txtBranchName.DataBindings.Clear();
                txtTypeID.DataBindings.Clear();

                txtAccountID.DataBindings.Add("Text", source, "AccountID");
                txtAccountName.DataBindings.Add("Text", source, "AccountName");
                txtOpenDate.DataBindings.Add("Text", source, "OpenDate");
                txtBranchName.DataBindings.Add("Text", source, "BranchName");
                txtTypeID.DataBindings.Add("Text", source, "TypeID");

                dgvBankAccountManagement.DataSource = null;
                dgvBankAccountManagement.DataSource = source;
                if (books.Count() == 0)
                {
                    ClearText();
                    btnDelete.Enabled = false;
                }
                else
                {
                    btnDelete.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load car list");
            }
        }

        private void frmAccountManagement_Load(object sender, EventArgs e)
        {
            txtAccountID.Enabled = false;
            txtAccountName.Enabled = false;
            txtOpenDate.Enabled = false;
            txtBranchName.Enabled = false;
            txtTypeID.Enabled = false;
            LoadAccountList();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmAccountDetails frmBD = new frmAccountDetails
            {
                Text = "Add Account",
                InsertOrUpdate = false,
                AccountRepo = accountRepo
            };
            if (frmBD.ShowDialog() == DialogResult.OK)
            {
                LoadAccountList();
                source.Position = source.Count - 1;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to delete this account", "Delete account", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var book = GetBookObject();
                    accountRepo.DeleteAccount(book.AccountId);
                    LoadAccountList();
                    MessageBox.Show(@"Delete successfully!!", @"Delete Account", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete a Account");
            }
        }

        private BankAccount GetBookObject()
        {
            try
            {
                return new  BankAccount
                {
                    AccountId = txtAccountID.Text,
                    AccountName = txtAccountName.Text,
                    OpenDate = DateTime.Parse(txtOpenDate.Text),
                    BranchName = txtBranchName.Text,
                    TypeId = txtTypeID.Text,
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot get Information Book", "@Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            frmAccountDetails frmBD = new frmAccountDetails
            {
                Text = "Update Book",
                InsertOrUpdate = true,
                AccountRepo = accountRepo,
                book = GetBookObject()
            };
            if (frmBD.ShowDialog() == DialogResult.OK)
            {
                LoadAccountList();
                source.Position = source.Count - 1;
            }
        }
    }
}
