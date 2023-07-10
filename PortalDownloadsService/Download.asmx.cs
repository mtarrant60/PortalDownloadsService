using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using PDSWorker;

namespace PortalDownloadsService
{
    /// <summary>
    /// Summary description for Download
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Download : System.Web.Services.WebService
    {

    
        [WebMethod]
        public string GetClientUNCFolder(string legalCaseId)
        {
            Worker worker = new Worker();
            return worker.clientFolder(legalCaseId);
        }
        [WebMethod]
        public List<PortalDoc> GetFiles(string legalCaseId) 
        {
            Worker worker = new Worker();
            return worker.GetFiles(legalCaseId);

        }

        [WebMethod]
        public string CreateDocumentFolder(string legalCaseId) 
        { 
            Worker worker = new Worker();
            return worker.CreateDocumentFolder(legalCaseId);
        }
        [WebMethod]
        public byte[] DownloadFromSharePoint(string file, string clientFolder) 
        {
            Worker worker = new Worker();
            return worker.DownloadFromSP(file, clientFolder);
        }
        [WebMethod]
        public byte[] DownloadFile(string FName)
        {
            Worker worker = new Worker();
            return worker.DownloadFile(FName);

        }
    }
}
