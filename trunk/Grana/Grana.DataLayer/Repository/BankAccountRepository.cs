using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grana.DataLayer.Interfaces;
using Grana.Model;

namespace Grana.DataLayer.Repository
{
    public class BankAccountRepository : BaseRepository, IBankAccountRepository 
    {
        public void UpdateBankAccount(BankAccount bankAccount)
        {
            BankAccount originalAccount = GetBankAccount(bankAccount.BankAccountId);

            originalAccount.AccountHolder = bankAccount.AccountHolder;
            originalAccount.AccountNumber = bankAccount.AccountNumber;
            originalAccount.BankAgencyCode = bankAccount.BankAgencyCode;
            originalAccount.BankName = bankAccount.BankName;
            context.SubmitChanges();
        }

        public BankAccount GetBankAccount(int bankAccountId)
        {
            BankAccount account = (from bankAccount in context.BankAccounts
                                          where bankAccount.BankAccountId == bankAccountId
                                          select bankAccount).FirstOrDefault();
            return account;
        }
    }
}
