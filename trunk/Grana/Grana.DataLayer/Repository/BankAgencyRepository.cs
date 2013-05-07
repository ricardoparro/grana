using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grana.DataLayer.Interfaces;
using Grana.Model;

namespace Grana.DataLayer.Repository
{
    public class BankAgencyRepository : BaseRepository, IBankAgencyRepository 
    {
        public List<BankAgency> GetAgenciesByCode(float agencyCode)
        {
            List<BankAgency> bankAgencies = (from bankAgency in context.BankAgencies
                                 where bankAgency.AgencyCode == agencyCode
                                 select bankAgency).ToList();

            return bankAgencies;
        }
    }
}
