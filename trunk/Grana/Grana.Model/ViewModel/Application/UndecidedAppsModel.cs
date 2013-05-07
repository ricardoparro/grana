using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grana.Model.ViewModel.Application
{
    public class UndecidedAppsModel
    {

        public int ApplicationsCount { get; set; }
        public List<UndecidedApplication> UndecidedApplicationsRows { get; set; }
    }
}
