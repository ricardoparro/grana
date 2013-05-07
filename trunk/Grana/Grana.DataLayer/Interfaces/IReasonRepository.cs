using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grana.Model.EnumModel;

namespace Grana.DataLayer.Interfaces
{
    public interface IReasonRepository
    {
        void AddReasonLog(ApplicationStatuses status, int applicationId, int reasonId, Guid userId, DateTime date);
    }
}
