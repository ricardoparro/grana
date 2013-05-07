using System.Collections.Generic;
using Grana.Model;
using Grana.Model.EnumModel;
using Grana.Model.ViewModel.Application;
using Grana.Model.ViewModel.ApplicationDetails;

namespace Grana.DataLayer.Interfaces
{
    public interface IApplicationRepository
    {
        ApplicationDetails GetApplicationDetailsById(int applicationId);
        void UpdateApplication(Application application);
        List<ApplicationForList> GetApplicationsByStatus(ApplicationStatuses applicationStatus);
        void UpdateApplicationStatus(ApplicationStatuses applicationStatus, int applicationId);
        List<UndecidedApplication> GetUndecidedApplications(ApplicationStatuses applicationStatus);
    }
}
