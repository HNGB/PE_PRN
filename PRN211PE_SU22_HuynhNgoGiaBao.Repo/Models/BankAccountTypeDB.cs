using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN211PE_SU22_HuynhNgoGiaBao.Repo.Models
{
    public partial class BankAccountTypeContext : DbContext
    {
        public BankAccountTypeContext(string conn)
        {
            this.Database.SetConnectionString(conn);
        }
    }
}
