using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grana.DataLayer.Interfaces
{
    public interface IActionLogRepository
    {
        void LogAction(string toString, int applicationId, Guid userId, DateTime dateAdded);
    }
}
