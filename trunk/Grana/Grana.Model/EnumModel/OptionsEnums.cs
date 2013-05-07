using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grana.Model.EnumModel
{
    public enum ContactsOfType
    {
        MobilePhone = 1,
        HomePhone = 2,
        EmailAddress = 3
    }

    public enum ApplicationStatuses
    {
        New,
        Approved,
        Declined,
        Undecided
    }

 

    public struct FormSections
    {
        public const string Name = "Nome";
        public const string PersonalInfo = "DadosPessoais";
        public const string LoanInfo = "DadosEmprestimo";
        public const string Employment = "Emprego";
        public const string Contacts = "Contatos";
        public const string Address = "Endereco";
        public const string BankAccount = "ContaBancaria";
        public const string BankCard = "Cartao";
    }
}

