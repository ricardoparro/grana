using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grana.DataLayer.Interfaces;
using Grana.Model;

namespace Grana.DataLayer.Repository
{
    public class BankCardRepository: BaseRepository, IBankCardRepository 
    {
        public void UpdateBankCard(BankCard bankCard)
        {
            BankCard originalCard = GetBankCard(bankCard.BankCardId);

            originalCard.CardHolder = bankCard.CardHolder;
            originalCard.CardNumber = bankCard.CardNumber;
            originalCard.ExpiryDate = bankCard.ExpiryDate;
            originalCard.SecurityCode = bankCard.SecurityCode;
            originalCard.StartDate = bankCard.StartDate;
           
            context.SubmitChanges();
        }

        public BankCard GetBankCard(int bankCardId)
        {
            BankCard card = (from bankCard in context.BankCards
                                       where bankCard.BankCardId == bankCardId
                                       select bankCard).FirstOrDefault();
            return card;
        }
    }
}
