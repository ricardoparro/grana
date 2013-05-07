using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grana.Model;

namespace Grana.DataLayer.Interfaces
{
    public interface IBankAccountRepository
    {
        void UpdateBankAccount(BankAccount bankAccount);
    }
}
