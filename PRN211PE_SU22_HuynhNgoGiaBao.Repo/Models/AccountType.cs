using System;
using System.Collections.Generic;

#nullable disable

namespace PRN211PE_SU22_HuynhNgoGiaBao.Repo.Models
{
    public partial class AccountType
    {
        public AccountType()
        {
            BankAccounts = new HashSet<BankAccount>();
        }

        public string TypeId { get; set; }
        public string TypeName { get; set; }
        public string TypeDesc { get; set; }

        public virtual ICollection<BankAccount> BankAccounts { get; set; }
    }
}
