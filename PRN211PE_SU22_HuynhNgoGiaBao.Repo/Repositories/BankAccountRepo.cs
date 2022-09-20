using Microsoft.Extensions.Configuration;
using PRN211PE_SU22_HuynhNgoGiaBao.Repo.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PRN211PE_SU22_HuynhNgoGiaBao.Repo.Repositories
{
    public class BankAccountRepo : IBankAccountRepo
    {
        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            var strConn = config["ConnectionStrings:BankAccountTypeDB"];
            return strConn;
        }
        private BankAccountTypeContext db;
        public BankAccountRepo()
        {
            var cs = GetConnectionString();
            db = new BankAccountTypeContext(cs);
        }
        public void DeleteAccount(string accountId)
        {
            var book = db.BankAccounts.Where(x => x.AccountId == accountId).FirstOrDefault();
            db.BankAccounts.Remove(book);
            db.SaveChanges();
        }

        public BankAccount GetAccountByID(string accountId)
        {
            var book = db.BankAccounts.Where(x => x.AccountId == accountId).FirstOrDefault();
            return book;
        }

        public IEnumerable<BankAccount> GetAccounts()
        {
            return this.db.BankAccounts;
        }

        public void InsertAccount(BankAccount account)
        {
            var checkBook = GetAccountByID(account.AccountId);
            if (checkBook != null)
            {
                throw new Exception("Account ID existed!");
            }
            else
            {
                db.BankAccounts.Add(account);
                db.SaveChanges();
            }
        }

        public IEnumerable<BankAccount> SearchByName(string branchName)
        {
            throw new NotImplementedException();
        }

        public void UpdateAccount(BankAccount account)
        {
            db.BankAccounts.Where(a => a.AccountId == account.AccountId).ToList()
               .ForEach((a) =>
               {
                   a.AccountName = account.AccountName;
                   a.OpenDate = account.OpenDate;
                   a.BranchName = account.BranchName;
                   a.TypeId = account.TypeId;
               }
               );

            db.SaveChanges();
        }
    }
}
