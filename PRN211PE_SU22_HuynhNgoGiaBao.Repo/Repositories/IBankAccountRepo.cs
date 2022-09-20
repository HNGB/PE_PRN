using PRN211PE_SU22_HuynhNgoGiaBao.Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN211PE_SU22_HuynhNgoGiaBao.Repo.Repositories
{
    public interface IBankAccountRepo
    {
        IEnumerable<BankAccount> GetAccounts();
        BankAccount GetAccountByID(string accountId);
        void InsertAccount(BankAccount account);
        void DeleteAccount(string accountId);
        void UpdateAccount(BankAccount account);
        IEnumerable<BankAccount> SearchByName(string branchName);
    }
}
