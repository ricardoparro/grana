using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grana.DataLayer.Interfaces;
using Grana.Model;

namespace Grana.Service.Portugal
{
    public class BankAgencyService : BaseService
    {
        private readonly IBankAgencyRepository _bankAgencyRepository;

        public BankAgencyService(IBankAgencyRepository bankAgencyRepository )
        {
            _bankAgencyRepository = bankAgencyRepository;
        }

        public List<BankAgency> LookupAgency(float agencyCode)
        {
            List<BankAgency> agencies = _bankAgencyRepository.GetAgenciesByCode(agencyCode);

            return agencies;
        }
    }
}
