using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PnP.Framework;
using PnP.Core;
using Microsoft.SharePoint.Client;
using PDSWorker;
using Microsoft.Graph;

namespace ScratchPad
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start test");


            string siteUrl = @"https://bbins365.sharepoint.com/sites/565-LTDPortalDocuments";
            string clientFolderBase = @"Information Technology/Development/TestDocDownloads/UNUM/a0R4P00000EK5guUAD/Carlos Remedios 8969";
            clientFolderBase = @"Shared Documents/PortalDocs";

            string fullUrl = @"https://bbins365.sharepoint.com/sites/565-LTDPortalDocuments/Shared Documents/PortalDocs";

            string xx = fullUrl.Replace(siteUrl + "/", string.Empty);


            //"Information Technology/Development/TestDocDownloads/UNUM/a0R4P00000EK5guUAD/Carlos Remedios 8969"
            //string fileName = "TEST_Vento_43754.pdf";
            //string localPath = @"C:\Cert\TEST_Vento_43754.pdf";


            List<PortalDoc> docs = new List<PortalDoc>();
            try
            {
                //var authManager = new AuthenticationManager("5fbe3484-065c-4f84-8112-719c7af45cb0", @"C:\Cert\BBAbsenceOCR.pfx", @"{PDAOCR_2022}", "bbins.com");

                var authManager = new AuthenticationManager("2572246d-9cf0-4a9b-996b-e672e96a6b44", @"C:\Cert\PDDCert.pfx", @"!P0rtAlDoc23", "bbins.com");
                using (var cc = authManager.GetContext(siteUrl))
                //new AuthenticationManager().GetAzureADAppOnlyAuthenticatedContext(siteUrl, "5fbe3484-065c-4f84-8112-719c7af45cb0", "bbins.com", @"C:\Cert\BBAbsenceOCR.pfx", @"{PDAOCR_2022}"))
                {
                    //Folder folder = cc.Web.GetFolderByServerRelativeUrl(@"Information Technology/Development/Developer Documentation");
                    Microsoft.SharePoint.Client.Folder folder = cc.Web.GetFolderByServerRelativeUrl(clientFolderBase);
                    

                    cc.RequestTimeout = int.Parse(TimeSpan.FromMinutes(180).TotalMilliseconds.ToString()); //-1;
                    //Folder folder = cc.Web.GetFolderByServerRelativeUrl(SPFolder);
                    cc.Load(folder);
                    cc.Load(folder.Files);
                    cc.Load(folder.Folders);
                    cc.ExecuteQuery();



                    foreach (var file in folder.Files)
                    {
                        PortalDoc doc = new PortalDoc();
                        doc.File = file.Name;
                        doc.Extension = file.Name.Split('.')[file.Name.Split('.').Length - 1];
                        doc.ClientFolder = clientFolderBase;

                        //ListItem li = file.ListItemAllFields;
                        //var s = li["File_x0020_Size"];
                        Console.WriteLine(file.Name);
                        Console.WriteLine(file.TimeCreated);
                    }


                    //var f = cc.Web.Folders;
                    //cc.Load(f);
                    //cc.ExecuteQuery();
                    //int i = f.Count;



                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error message: " + ex.Message);
            }


        }
        public static Microsoft.SharePoint.Client.Folder AddSubFolder(ClientContext context, Microsoft.SharePoint.Client.Folder ParentFolder, string folderName)
        {
            Microsoft.SharePoint.Client.Folder resultFolder = ParentFolder.Folders.Add(folderName);
            context.ExecuteQuery();
            return resultFolder;
        }
    }
    public class PortalDoc
    {
        public string File { get; set; }
        public string ClientFolder { get; set; }
        public string Extension { get; set; }
        public DateTime Created { get; set; }
        public long Size { get; set; }
    }

}
