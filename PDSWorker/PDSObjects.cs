using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDSWorker
{
    public class PortalDoc
    {
        public string File { get; set; }
        public string ClientFolder { get; set; }
        public string FileFullName { get; set; }
        public string Extension { get; set; }
        public DateTime Created { get; set; }
        public long Size { get; set; }
    }

    public class DocFolder
    {

        public string Customer_Contract_Name__c { get; set; }
        public string LegalCaseName { get; set; }
        public string LegalCaseId { get; set; }
        public string ClientFolderBase { get; set; }

    }
}
