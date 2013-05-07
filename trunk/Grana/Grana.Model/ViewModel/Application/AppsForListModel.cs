using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grana.Model.ViewModel.Application
{
    public class AppsForListModel
    {
        public int ApplicationsCount { get; set; }
        public List<ApplicationForList> ApplicationsRows { get; set; }
    }
}
