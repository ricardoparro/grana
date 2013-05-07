using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using FlimFlan.ReadableRex;
using FluentValidation;
using FluentValidation.Results;
using Grana.DataLayer.Interfaces;
using Grana.DataLayer.Repository;
using Grana.Model;
using Grana.Model.ViewModel.ApplicationDetails;
using Grana.Service.Portugal;

namespace Grana.Service.Validation
{
    public class ApplicationDetailsValidator : ValidationFactory<ApplicationDetails>
    {
        private readonly IBankAgencyRepository _bankAgencyRepository;
        private const string nameStringRegex = @"^([A-Za-z ãâáàêéèîíìõôóòûúùç-]+ ?)*$";

        private const string identityNumberRegex = @"^([0-9 .-]+ ?)*$";

        private const string regexEstados =@"(A(C|L|P|M))|BA|(CE)|(DF)|(GO)|(ES)|(M(A|T|S|G))|(P(A|B|R|E|I))|(R(J|N|S|O|R))|(S(P|C|E))|(TO)";

        private const string regexCEP = @"(?<!(\w|\.))([0-9]{5}-?[0-9]{3})(?!\w|\.)";

        private const string regexCPF = @"^\d{3}.\d{3}.\d{3}-\d{2}$";

        private const string regexPhoneNumber = @"([(][0-9]{2}[)])\s[0-9]{4}\-[0-9]{4}";

        private const string regexMobile = @"[6-9][0-9]{3}-[0-9]{4}";

        private const string regexEmail = @"\b[\w\.-]+@[\w\.-]+\.\w{2,4}\b";

        private const string regexCard = @"[\d][0-9]{15}";

        public ApplicationDetailsValidator(IBankAgencyRepository bankAgencyRepository)
        {
            _bankAgencyRepository = bankAgencyRepository;
            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;

            //name section
            RuleFor(x => x.Applicant.FirstName).NotEmpty().WithMessage("Nome inválido - campo obrigatório").Matches(nameStringRegex).WithMessage("Nome inválido")
                .Length(1,50).WithMessage("O nome é demasiado longo");
            RuleFor(x => x.Applicant.MiddleNames).Matches(nameStringRegex).WithMessage("Nome inválido")
                .Length(0,50).WithMessage("O nome é demasiado longo");
            RuleFor(x => x.Applicant.LastName).NotEmpty().WithMessage("Sobrenome inválido - campo obrigatório").Matches(nameStringRegex).WithMessage("Sobrenome inválido")
                .Length(1,50).WithMessage("O nome é demasiado longo");
            
            //application section
            RuleFor(x => x.Application.Amount).NotNull().WithMessage("O valor do empréstimo não é válido")
                .Must(a => a > 0).WithMessage("O valor do empréstimo tem que ser maior que 0")
                .Must(a => a < 501).WithMessage("O valor do empréstimo tem que ser menor ou igual a R$500");

            RuleFor(x => x.Application.PaybackDate).GreaterThan(DateTime.Now).WithMessage("A data não pode ser no passado");

         
            //personal info section
            //CPF
            RuleFor(x => x.Applicant.SocialSecurityNumber).Matches(regexCPF).WithMessage("O CPF não é válido");
            Custom(ValidateSocialSecurityNumber);
            
            //identity number 
            RuleFor(x => x.Applicant.IdentityNumber).Matches(identityNumberRegex).WithMessage("O formato do RG não é válido");
            
            Custom(ValidateIdentityNumber);

            RuleFor(x => x.Applicant.BirthDate).NotEmpty().WithMessage("Data de nascimento inválida - campo obrigatório");
            RuleFor(x => x.Applicant.MaritalStatus).NotEmpty().WithMessage("Estado Civil inválido - campo obrigatório");
            RuleFor(x => x.Applicant.NumberOfDependants).NotNull().WithMessage("Agregado Familiar inválido - campo obrigatório");
            RuleFor(x => x.Applicant.HomeStatus).NotNull().WithMessage("Possui Imóvel invalido - campo obrigatório");

            //Employment section
            RuleFor(x => x.Employment.MonthlyIncome).NotNull().WithMessage("O salário não é válido").Must(a => a > 0).WithMessage("O salário tem que ser maior que 0");

            RuleFor(x => x.Employment.EmployersName).NotEmpty().WithMessage("Nome da empresa inválido - o campo é obrigatorio")
                .Length(1, 50).WithMessage("O nome da empresa é demasiado longo");

            RuleFor(x => x.Employment.Position).NotEmpty().WithMessage("Cargo inválido - o campo é obrigatório")
                .Length(1, 150).WithMessage("O cargo é demasiado longo");

            RuleFor(x => x.Employment.TimeAtEmployer).NotNull().WithMessage("Tempo na empresa inválido").Must(x => x > 0)
                .WithMessage("Tempo tem que ser maior que 0");

            RuleFor(x => x.Employment.WorkPhone).NotEmpty().WithMessage("Telefone inválido - campo obrigatório")
                .Matches(regexPhoneNumber).WithMessage("O telefone é inválido");

            RuleFor(x => x.Employment.EmploymentStatus).NotEmpty().WithMessage("Tipo de trabalho inválido - campo obrigatório");
            RuleFor(x => x.Employment.IncomeFrequency).NotEmpty().WithMessage("Frequência inválida - campo obrigatório");
            RuleFor(x => x.Employment.Industry).NotEmpty().WithMessage("Indústria inválida - campo obrigatório");
            RuleFor(x => x.Employment.PayDate).NotEmpty().WithMessage("Data de pagamento inválida - campo obrigatório")
                .GreaterThan(DateTime.Now).WithMessage("A data não pode ser no passado");


          //Adress section

            RuleFor(x => x.Address.Address1).NotEmpty().WithMessage("Endereço Inválido - campo obrigatório");

            RuleFor(x => x.Address.Postcode).NotEmpty().WithMessage("CEP Inválido - campo obrigatório")
                .Matches(regexCEP).WithMessage("O CEP não é válido");

            RuleFor(x => x.Address.State).NotEmpty().WithMessage("Estado Inválido - campo obrigatório")
                .Matches(regexEstados).WithMessage("O estado não é válido");

            RuleFor(x => x.Address.City).NotEmpty().WithMessage("Endereço inválido - campo obrigatório")
                .Length(1, 50).WithMessage("O endereço e demasiado longo");

            RuleFor(x => x.Address.Number).NotEmpty().WithMessage("Número inválido - campo obrigatório");

            RuleFor(x => x.Address.Neighborhood).NotEmpty().WithMessage("Bairro inválido - campo obrigatório");

            //Contacts section

            RuleFor(x => x.HomePhone.ContactDetail).NotEmpty().WithMessage("Telefone Resedêncial inválido - campo obrigatório").Matches(regexPhoneNumber).WithMessage("O Telefone resedêncial não é válido");
            RuleFor(x => x.MobilePhone.ContactDetail).NotEmpty().WithMessage("Telefone Celular inválido - campo obrigatório").Matches(regexMobile).WithMessage("O Telefone celular não é válido");
            RuleFor(x => x.EmailAddress.ContactDetail).NotEmpty().WithMessage("Email inválido - campo obrigatório").Matches(regexEmail).WithMessage("O Email celular não é válido");

           //bank account section

            RuleFor(a => a.BankAccount.AccountHolder).NotEmpty().WithMessage("Nome inválido -  campo obrigatório").Matches(nameStringRegex).WithMessage("O Nome não é valido");
            RuleFor(a => a.BankAccount.AccountNumber).NotEmpty().WithMessage("Número da Conta Corrente inválida -  campo obrigatório");
            RuleFor(a => a.BankAccount.BankName).NotEmpty().WithMessage("Nome do banco inválido - campo obrigatório").Matches(nameStringRegex).WithMessage("O nome do banco não é válido");
            RuleFor(a => a.BankAccount.BankAgencyCode).NotNull().NotEmpty().WithMessage("O código da agência não é válido");
            Custom(ValidateAgency);
            //Card section
            RuleFor(c => c.BankCard.CardNumber).NotEmpty().WithMessage("Número do cartão inválido - campo obrigatório");
            Custom(IsCreditCardValid);
            RuleFor(a => a.BankCard.ExpiryDate).NotEmpty().WithMessage("Data de Validade ssão inválida - campo obrigatório");
            RuleFor(a => a.BankCard.StartDate).NotEmpty().WithMessage("Data de Emissão ssão inválida - campo obrigatório");
            RuleFor(a => a.BankCard.SecurityCode).NotEmpty().WithMessage("Código de segurança inválido - campo obrigatório");
            RuleFor(a => a.BankCard.ExpiryDate).GreaterThan(DateTime.Now).WithMessage("Data inválida - O cartão já expirou");

        }

        private ValidationFailure ValidateAgency(ApplicationDetails applicationDetails)
        {
            float agencyCode;
            string errorMessage = "O código da agência não é válido";


            bool isValid = float.TryParse(applicationDetails.BankAccount.BankAgencyCode, out agencyCode);

            if(!isValid)
            {
                return new ValidationFailure("BankAccount.BankAgencyCode", errorMessage); ;
            }


            List<BankAgency> lookupAgency = _bankAgencyRepository.GetAgenciesByCode(agencyCode);


            if(lookupAgency.Count == 0 )
            {
                return new ValidationFailure("BankAccount.BankAgencyCode", errorMessage); ;
            }

            return null;
        }

        /// <summary>
        /// This validator uses the rules for verified digits in brazil. Basically a social security number has 9 + 2 digits where
        /// we start to sum the result of multiplying the first number with 10 then second number with 9 , etc . In the end the rest of the division of the total by 11 is used to define
        /// if the number is valid or not.
        /// the rest of the algorithm is self explainable in the code
        /// </summary>
        /// <param name="applicationDetails"></param>
        /// <returns></returns>
        private ValidationFailure ValidateSocialSecurityNumber(ApplicationDetails applicationDetails)
        {
            string errorMessage = "O CPF não é válido"; 


            if(string.IsNullOrEmpty(applicationDetails.Applicant.SocialSecurityNumber))
            {
                return new ValidationFailure("Applicant.SocialSecurityNumber", errorMessage);
                
            }


            string cpf = applicationDetails.Applicant.SocialSecurityNumber;
            cpf = cpf.Replace(".", "").Replace("-", "").Replace(" ", "");

            double result;

            //verify if is a valid number after removing . and -
            bool isNumberValid = Double.TryParse(cpf, out result);
            decimal rest = 1;
            if (isNumberValid)
            {
                int size = cpf.Length;
                int total = 0;
                int[] calculation = new int[size];
                int multiplicator = 10;
                for (int i = 0; i <= 8; i++)
                {
                    total += Int32.Parse(cpf[i].ToString())*multiplicator;
                    multiplicator--;
                }

                rest = total%11;
            }


            if(rest == 0 || rest == 1)
            {
                if(cpf[10] != 0)
                {
                    return new ValidationFailure("Applicant.SocialSecurityNumber", errorMessage);
                }
            }

            List<decimal> possibleResults = new List<decimal>(){2, 3, 4, 5, 6, 7, 8, 9, 10};

            if(possibleResults.Contains(rest) &&  Int32.Parse(cpf[9].ToString()) == (11 - rest))
            {
                return null;
            }
            
            return new ValidationFailure("Applicant.SocialSecurityNumber", errorMessage);
           

        }


        private ValidationFailure ValidateIdentityNumber(ApplicationDetails applicationDetails)
        {

            string errorMessage = "O RG não é válido";

            if(string.IsNullOrEmpty(applicationDetails.Applicant.IdentityNumber))
            {
                return new ValidationFailure("Applicant.IdentityNumber", errorMessage);
                
            }

            string identityNumber = applicationDetails.Applicant.IdentityNumber;

            identityNumber = identityNumber.Replace(".", "").Replace("-", "").Replace(" ", "");

            int result;

            //verify if is a valid number after removing . and -
            bool isNumberValid = Int32.TryParse(identityNumber, out result);

            decimal resto = 1;

            if (isNumberValid)
            {
                int size = identityNumber.Length;

                int total = 0;

                for (int i = 0; i <= size; i++)
                {
                    if (size >= i+1)
                    {
                        int multiplicator = i + 2;

                        if (i == 8)
                            multiplicator = 100;

                        total += Int32.Parse(identityNumber[i].ToString()) * multiplicator;
                        
                    }
                }
                resto = total%11;

            }


            if (resto != 0)
            {
                //invalid
                return new ValidationFailure("Applicant.IdentityNumber", errorMessage);
            }

            return null;
        }
        //Luhn algorythm to validate credit card format
        public ValidationFailure IsCreditCardValid(ApplicationDetails applicationDetails)
        {
            string cardNumber = applicationDetails.BankCard.CardNumber;

            string errorMessage = "O cartão não é válido";


            if(string.IsNullOrEmpty(cardNumber))
            {
                return new ValidationFailure("BankCard.CardNumber", errorMessage);           
            }

            const string allowed = "0123456789";
            int i;

            StringBuilder cleanNumber = new StringBuilder();
            for (i = 0; i < cardNumber.Length; i++)
            {
                if (allowed.IndexOf(cardNumber.Substring(i, 1)) >= 0)
                    cleanNumber.Append(cardNumber.Substring(i, 1));
            }


            if (cleanNumber.Length < 13 || cleanNumber.Length > 16)
            {
                return new ValidationFailure("BankCard.CardNumber", errorMessage);
            }
            for (i = cleanNumber.Length + 1; i <= 16; i++)
                cleanNumber.Insert(0, "0");

            int multiplier, digit, sum, total = 0;
            string number = cleanNumber.ToString();

            for (i = 1; i <= 16; i++)
            {
                multiplier = 1 + (i%2);
                int startIndex = i - 1;
                digit = int.Parse(number.Substring(startIndex,1));
                sum = digit*multiplier;
                if (sum > 9)
                    sum -= 9;
                total += sum;
            }
            if(total%10 != 0)
            {
                //invalid
                return new ValidationFailure("BankCard.CardNumber", errorMessage);

            }

            return null;
        }
    }
}
