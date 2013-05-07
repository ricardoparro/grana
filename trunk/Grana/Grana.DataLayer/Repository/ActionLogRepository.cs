using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grana.DataLayer.Interfaces;
using Grana.Model;

namespace Grana.DataLayer.Repository
{
    public class ActionLogRepository: BaseRepository, IActionLogRepository 
    {
        public void LogAction(string action, int applicationId, Guid userId, DateTime dateAdded)
        {
            ActionLog log = new ActionLog();

            log.ApplicationId = applicationId;
            log.DateExecuted = dateAdded;
            log.Action = action;
            log.UserId = userId;

            context.ActionLogs.InsertOnSubmit(log);
            context.SubmitChanges();
        }
    }
}
