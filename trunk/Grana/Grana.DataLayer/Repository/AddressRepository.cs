using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grana.DataLayer.Interfaces;
using Grana.Model;

namespace Grana.DataLayer.Repository
{
    public class AddressRepository : BaseRepository, IAddressRepository 
    {
        public void UpdateAddress(Address address)
        {
            Address originalAddr = GetAddress(address.AddressId);

            originalAddr.Address1 = address.Address1;
            originalAddr.City = address.City;
            originalAddr.Complement = address.Complement;
            originalAddr.Neighborhood = address.Neighborhood;
            originalAddr.Number = address.Number;
            originalAddr.Postcode = address.Postcode;
            originalAddr.State = address.State;
            
            context.SubmitChanges();
        }

        public Address GetAddress(int addressId)
        {
            Address address = (from addr in context.Addresses
                                      where addr.AddressId == addressId
                                      select addr).FirstOrDefault();
            return address;
        }
    }
}
