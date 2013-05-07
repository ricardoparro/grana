using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grana.Model;

namespace Grana.DataLayer.Interfaces
{
    public interface IBankCardRepository
    {
        void UpdateBankCard(BankCard bankCard);
        BankCard GetBankCard(int bankCardId);
    }
}
