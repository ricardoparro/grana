using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grana.DataLayer.Interfaces;
using Grana.Model;
using Grana.Model.EnumModel;

namespace Grana.DataLayer.Repository
{
    public class ReasonRepository : BaseRepository, IReasonRepository
    {
        public void AddReasonLog(ApplicationStatuses status, int applicationId, int reasonId, Guid userId, DateTime date)
        {
            ReasonLog log = new ReasonLog();
            log.ApplicationId = applicationId;
            log.DateLogged = date;
            log.ReasonId = reasonId;
            log.UserAdderId = userId;

            context.ReasonLogs.InsertOnSubmit(log);
            context.SubmitChanges();
        }
    }
}
