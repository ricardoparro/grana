using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grana.Model.ViewModel.Documents
{
    public class DocumentRow
    {
        public string Path { get; set; }
        public string FileName { get; set; }
        public DateTime TimeAdded { get; set; }
        public string Username { get; set; }

        public string Error { get; set; }
    }
}
