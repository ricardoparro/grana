using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grana.Model;

namespace Grana.DataLayer.Interfaces
{
    public interface IAddressRepository
    {
        void UpdateAddress(Address address);
        Address GetAddress(int addressId);
    }
}
