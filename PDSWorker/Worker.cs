using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;
using System.IO;
//using System.Net;
using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core;
using System.Web;
using System.Runtime.Remoting.Contexts;
using System.Runtime.ConstrainedExecution;

namespace PDSWorker
{
    public class Worker
    {
        string constr = ConfigurationManager.AppSettings["SFBackup"];
        const string EventSourceName = "PortalDownloadService";
        ConnectionManager connection = new ConnectionManager();
        string _name = ConfigurationManager.AppSettings["ServiceAccount"];
        string _pw = ConfigurationManager.AppSettings["ServiceAccountPW"];


        public Worker()
        {
            connection.login();
        }

        public byte[] DownloadFromSP(string file, string clientFolder)
        {
            string siteUrl = ConfigurationManager.AppSettings["SharePointURL"];

            string SharePointURL = ConfigurationManager.AppSettings["SharePointURL"];
            string ClientFolderBase = ConfigurationManager.AppSettings["ClientFolderBase"];
            string SharePointClientId = ConfigurationManager.AppSettings["SharePointClientId"];
            string SharePointTenant = ConfigurationManager.AppSettings["SharePointTenant"];
            string Cert = ConfigurationManager.AppSettings["Cert"];
            string CertPW = ConfigurationManager.AppSettings["CertPW"];


            try
            {

                //using (var cc = new AuthenticationManager().GetAzureADAppOnlyAuthenticatedContext(siteUrl, "5fbe3484-065c-4f84-8112-719c7af45cb0", "bbins.com", @"C:\Cert\BBAbsenceOCR.pfx", @"{PDAOCR_2022}"))
                using (var cc = new AuthenticationManager().GetAzureADAppOnlyAuthenticatedContext(SharePointURL, SharePointClientId, SharePointTenant, Cert, CertPW))
                {
                    //Folder folder = cc.Web.GetFolderByServerRelativeUrl(@"Information Technology/Development/Developer Documentation");
                    Folder folder = cc.Web.GetFolderByServerRelativeUrl(clientFolder);

                    cc.RequestTimeout = int.Parse(TimeSpan.FromMinutes(180).TotalMilliseconds.ToString()); //-1;
                    //Folder folder = cc.Web.GetFolderByServerRelativeUrl(SPFolder);
                    cc.Load(folder);
                    cc.Load(folder.Files);
                    cc.ExecuteQuery();

                    var download = folder.Files.Where(d => d.Name == file);
                    
                    //if(download.Any()) 
                    //{
                    //    FileInformation fileInfo = file.OpenBinaryDirect(context, fileRef.ToString());

                    //}

                    foreach (Microsoft.SharePoint.Client.File f2d in folder.Files)
                    {
                        if( f2d.Name == file)
                        {
                            ClientResult<System.IO.Stream> data = f2d.OpenBinaryStream();
                            cc.Load(f2d);
                            cc.ExecuteQuery();
                            using (System.IO.MemoryStream mStream = new System.IO.MemoryStream())
                            {
                                if (data != null)
                                {
                                    data.Value.CopyTo(mStream);
                                    byte[] result = mStream.ToArray();
                                    pushJSON(@"File downloaded" + file, "DownloadFromSP", "", EventSourceName);
                                    return result;
                                    //string b64String = Convert.ToBase64String(imageArray);
                                }
                            }
                        }
                    }


                    //var f = cc.Web.Folders;
                    //cc.Load(f);
                    //cc.ExecuteQuery();
                    //int i = f.Count;



                }
            }
            catch (Exception x)
            {
                Console.WriteLine("Error message: " + x.Message);
                EventLogEntry(x.ToString(), EventSourceName, EventLogEntryType.Error);
                pushJSON(JsonConvert.SerializeObject(x), "DownloadFromSP Exception", x.Message, EventSourceName);
                throw x;

            }

            
            byte[] f = new byte[1024];
            return f;

        }
        public byte[] DownloadFile(string FName)
        {
            try
            {
                using (new Impersonator(_name, "ARCADVANTAGE", _pw))
                {
                    System.IO.FileStream fs1 = null;
                    fs1 = System.IO.File.Open(FName, FileMode.Open, FileAccess.Read);
                    byte[] b1 = new byte[fs1.Length];
                    fs1.Read(b1, 0, (int)fs1.Length);
                    fs1.Close();
                    return b1;
                }
            }
            catch (Exception x)
            {
                Debug.WriteLine(x.Message);
                EventLogEntry(x.ToString(), EventSourceName, EventLogEntryType.Error);
                pushJSON(JsonConvert.SerializeObject(x), "DownloadFile Exception", x.Message, EventSourceName);
                throw x;
            }

        }

        public List<PortalDoc> GetFiles(string legalCaseId)
        {
            //a0R4P00000Ogj8vUAB
            List<PortalDoc> docs = new List<PortalDoc>();
            //string folder = string.Empty;

            //string siteUrl = @"https://bbins365.sharepoint.com/sites/565-bbabsence";
            string siteUrl = ConfigurationManager.AppSettings["SharePointURL"];
            string SharePointURL = ConfigurationManager.AppSettings["SharePointURL"];
            string ClientFolderBase = ConfigurationManager.AppSettings["ClientFolderBase"];
            string SharePointClientId = ConfigurationManager.AppSettings["SharePointClientId"];
            string SharePointTenant = ConfigurationManager.AppSettings["SharePointTenant"];
            string Cert = ConfigurationManager.AppSettings["Cert"];
            string CertPW = ConfigurationManager.AppSettings["CertPW"];




            string clientFolder = GetDocUrlFromSF(legalCaseId);
            if (clientFolder != "No Folder")
            {
                pushJSON(@"Folder retrieved from SF: " + clientFolder, "GetDocUrlFromSF", "", EventSourceName);
                //clientFolder = clientFolder.Replace(siteUrl + @"/", string.Empty);
                clientFolder = clientFolder.Replace(SharePointURL + @"/", string.Empty);

            }
            else
            {
                return docs;
            }

            try
            {

                //using (var cc = new AuthenticationManager().GetAzureADAppOnlyAuthenticatedContext(siteUrl, "5fbe3484-065c-4f84-8112-719c7af45cb0", "bbins.com", @"C:\Cert\BBAbsenceOCR.pfx", @"{PDAOCR_2022}"))
                using (var cc = new AuthenticationManager().GetAzureADAppOnlyAuthenticatedContext(SharePointURL, SharePointClientId, SharePointTenant, Cert, CertPW))
                {
                    //Folder folder = cc.Web.GetFolderByServerRelativeUrl(@"Information Technology/Development/Developer Documentation");
                    Folder folder = cc.Web.GetFolderByServerRelativeUrl(clientFolder);

                    cc.RequestTimeout = int.Parse(TimeSpan.FromMinutes(180).TotalMilliseconds.ToString()); //-1;
                    //Folder folder = cc.Web.GetFolderByServerRelativeUrl(SPFolder);
                    cc.Load(folder);
                    cc.Load(folder.Files);
                    cc.ExecuteQuery();
                    foreach (var file in folder.Files)
                    {
                        PortalDoc doc = new PortalDoc();
                        doc.File = file.Name;
                        doc.Extension = file.Name.Split('.')[file.Name.Split('.').Length - 1];
                        doc.ClientFolder = clientFolder;
                        doc.Created = file.TimeCreated;

                        docs.Add(doc);

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

                Debug.WriteLine(ex.Message);
                EventLogEntry(ex.ToString(), EventSourceName, EventLogEntryType.Error);
                pushJSON(JsonConvert.SerializeObject(ex), "GetFiles Exception", ex.Message.ToString(), EventSourceName);
                PortalDoc doc = new PortalDoc();
                doc.File = ex.Message.ToString().Substring(0, ex.Message.Length > 50 ? 50 : ex.Message.Length);
                docs.Add(doc);

            }

            return docs;




            //\\arcadvantage.local\clientdata01\Guardian - 3920\David, Chandler - 7961\Claimant Uploaded Documents
        }

        public List<PortalDoc> GetFiles(string legalCaseId, string old)
        {
            //a0R4P00000Ogj8vUAB
            List<PortalDoc> files = new List<PortalDoc>();
            string folder = string.Empty;
            try
            {

                using (new Impersonator(_name, "ARCADVANTAGE", _pw))
                {
                    folder = clientFolder(legalCaseId);
                    folder += ConfigurationManager.AppSettings["SubFolder"]; //@"\Claimant Uploaded Documents";



                    foreach (string file in Directory.GetFiles(folder))
                    {
                        //Console.WriteLine(file);
                        FileInfo info = new FileInfo(file);
                        PortalDoc doc = new PortalDoc();
                        doc.File = info.Name;
                        //doc.FileFullName = info.FullName;
                        doc.Extension = info.Extension;
                        doc.Created = info.CreationTime;
                        doc.Size = info.Length;
                        files.Add(doc);
                    }
                }
                return files;
            }
            catch (Exception x)
            {
                Debug.WriteLine(x.Message);
                EventLogEntry(x.ToString(), EventSourceName, EventLogEntryType.Error);
                pushJSON(JsonConvert.SerializeObject(x), "GetFiles Exception", x.Message, EventSourceName);
                PortalDoc doc = new PortalDoc();
                doc.File = "No Documents Found";
                files.Add(doc);

                return files;
                //throw x;
            }


            //\\arcadvantage.local\clientdata01\Guardian - 3920\David, Chandler - 7961\Claimant Uploaded Documents
        }

        public string GetDocUrlFromSF(string legalCaseId)
        {
            string result = "No Folder";
            try
            {
                string session = connection.service.SessionHeaderValue.sessionId.ToString();
                if (session.Length == 0)
                    connection.login();


                string soql = @"select l.id, l.Portal_Document_Folder__c from legalcase__c l where l.id = '" + legalCaseId + @"' ";

                var queryResult = connection.service.query(soql);
                SalesForce.sObject[] records = queryResult.records;
                if (queryResult.records != null)
                {
                    if (queryResult.records.Length > 0)
                    {
                        SalesForce.LegalCase__c row = (SalesForce.LegalCase__c)records[0];
                        result = row.Portal_Document_Folder__c != null ? row.Portal_Document_Folder__c.ToString() : "No Folder";
                    }
                }
            }
            catch (Exception x)
            {
                Debug.WriteLine(x.Message);
                pushJSON(JsonConvert.SerializeObject(x), "GetDocUrlFromSF Exception", x.Message, EventSourceName);
                EventLogEntry(x.ToString(), EventSourceName, EventLogEntryType.Error);
                throw x;
            }

            return result;
        }
        public string CreateDocumentFolder(string legalCaseId)
        {
            string SharePointURL = ConfigurationManager.AppSettings["SharePointURL"];
            string ClientFolderBase = ConfigurationManager.AppSettings["ClientFolderBase"];
            string SharePointClientId = ConfigurationManager.AppSettings["SharePointClientId"];
            string SharePointTenant = ConfigurationManager.AppSettings["SharePointTenant"];
            string Cert = ConfigurationManager.AppSettings["Cert"];
            string CertPW = ConfigurationManager.AppSettings["CertPW"];
            string result = string.Empty;

            DocFolder docFolder = CreateDocFolderName(legalCaseId);

            try
            {
                using (var cc = new AuthenticationManager().GetAzureADAppOnlyAuthenticatedContext(SharePointURL, SharePointClientId, SharePointTenant, Cert, CertPW))
                {
                    Microsoft.SharePoint.Client.Folder folder = cc.Web.GetFolderByServerRelativeUrl(ClientFolderBase);
                    cc.RequestTimeout = int.Parse(TimeSpan.FromMinutes(180).TotalMilliseconds.ToString()); //-1;
                    cc.Load(folder);
                    cc.Load(folder.Files);
                    cc.Load(folder.Folders);
                    cc.ExecuteQuery();

                    var x = folder.Folders.Where(f => f.Name == docFolder.Customer_Contract_Name__c);
                    if (x.Count() == 0)
                    {
                        Microsoft.SharePoint.Client.Folder resultFolder = AddSubFolder(cc, folder, docFolder.Customer_Contract_Name__c);
                        Microsoft.SharePoint.Client.Folder caseId = AddSubFolder(cc, resultFolder, docFolder.LegalCaseName + "_" + docFolder.LegalCaseId);
                    }
                    else
                    {
                        Microsoft.SharePoint.Client.Folder contract = folder.Folders.GetByUrl(ClientFolderBase + "/" + docFolder.Customer_Contract_Name__c);
                        cc.Load(contract);
                        cc.Load(contract.Folders);
                        cc.ExecuteQuery();

                        var caseFolder = contract.Folders.Where(c => c.Name == docFolder.LegalCaseName + "_" + docFolder.LegalCaseId);
                        if (caseFolder.Count() == 0)
                        {
                            Debug.WriteLine("Folder not found make it");
                            Microsoft.SharePoint.Client.Folder resultFolder = AddSubFolder(cc, contract, docFolder.LegalCaseName + "_" + docFolder.LegalCaseId);
                        }
                        else
                        {
                            //folder exists
                            Debug.WriteLine("Folder Exists");
                        }
                    }
                     result = SharePointURL + "/" + ClientFolderBase + "/" + docFolder.Customer_Contract_Name__c + "/" + docFolder.LegalCaseName + "_" + docFolder.LegalCaseId;
                    //Add url to Legalcase__c.Portal_Document_Folder__c
                    UpdateSFDocFolder(result, legalCaseId);
                }
            }
            catch (Exception x) 
            {
                Debug.WriteLine(x.Message);
                pushJSON(JsonConvert.SerializeObject(x), "CreateDocumentFolder Exception", x.Message, EventSourceName);
                EventLogEntry(x.ToString(), EventSourceName, EventLogEntryType.Error);
                throw x;
            }
            
            return result;
        }

        public string UpdateSFDocFolder(string SPFolderURL, string legalCaseId)
        {
            string result = "return message";
            try
            {
                string session = connection.service.SessionHeaderValue.sessionId.ToString();
                if (session.Length == 0)
                    connection.login();

                SalesForce.LegalCase__c lcase = new SalesForce.LegalCase__c();
                lcase.Id = legalCaseId;
                lcase.Portal_Document_Folder__c = SPFolderURL;

                SalesForce.SaveResult[] saveResults = connection.service.update(new SalesForce.sObject[] { lcase });

                if (saveResults[0].errors != null)
                {
                    Debug.WriteLine("Deal with error");
                }

                //string soql = @"select l.id, l.name, l.Customer_Contract_Name__c from legalcase__c l where l.id = '" + legalCaseId + @"' ";

                    //var queryResult = connection.service.query(soql);
                    //SalesForce.sObject[] records = queryResult.records;
                    //if (queryResult.records.Length > 0)
                    //{
                    //    SalesForce.LegalCase__c row = (SalesForce.LegalCase__c)records[0];
                    //    docFolder.Customer_Contract_Name__c = row.Customer_Contract_Name__c != null ? row.Customer_Contract_Name__c.ToString() : "No Contract";
                    //    docFolder.LegalCaseName = row.Name.ToString();
                    //    docFolder.LegalCaseId = row.Id.ToString();
                    //}
            }
            catch (Exception x)
            {
                Debug.WriteLine(x.Message);
                pushJSON(JsonConvert.SerializeObject(x), "UpdateSFDocFolder Exception", x.Message, EventSourceName);
                EventLogEntry(x.ToString(), EventSourceName, EventLogEntryType.Error);
                throw x;
            }

            return result;
        }
        public DocFolder CreateDocFolderName(string legalCaseId)
        {
            DocFolder docFolder = new DocFolder();
            docFolder.ClientFolderBase = ConfigurationManager.AppSettings["ClientFolderBase"];
            try
            {
                string session = connection.service.SessionHeaderValue.sessionId.ToString();
                if (session.Length == 0)
                    connection.login();


                string soql = @"select l.id, l.name, l.Customer_Contract_Name__c from legalcase__c l where l.id = '" + legalCaseId + @"' ";

                var queryResult = connection.service.query(soql);
                SalesForce.sObject[] records = queryResult.records;
                if (queryResult.records.Length > 0)
                {
                    SalesForce.LegalCase__c row = (SalesForce.LegalCase__c)records[0];
                    docFolder.Customer_Contract_Name__c = row.Customer_Contract_Name__c != null ? row.Customer_Contract_Name__c.ToString() : "No Contract";
                    docFolder.LegalCaseName = row.Name.ToString();
                    docFolder.LegalCaseId = row.Id.ToString();
                }
            }
            catch (Exception x)
            {
                Debug.WriteLine(x.Message);
                pushJSON(JsonConvert.SerializeObject(x), "GetDocFolder Exception", x.Message, EventSourceName);
                EventLogEntry(x.ToString(), EventSourceName, EventLogEntryType.Error);
                throw x;
            }
            return docFolder;
        }

        public string clientFolder(string legalCaseId)
        {
            string folder = null;
            try
            {

                string session = connection.service.SessionHeaderValue.sessionId.ToString();
                if (session.Length == 0)
                    connection.login();


                string soql = @"select name, id, ClientFolderUNC__c from legalcase__c where id = '" + legalCaseId + @"' ";

                var cfUNC = connection.service.query(soql);

                SalesForce.sObject[] records = cfUNC.records;
                if (cfUNC.records.Length > 0)
                {
                    SalesForce.LegalCase__c row = (SalesForce.LegalCase__c)records[0];
                    folder = row.ClientFolderUNC__c.ToString();

                    //pushJSON(folder, "ClientFolderUNC__c", "", EventSourceName);
                }
            }
            catch (Exception x)
            {
                Debug.WriteLine(x.Message);
                pushJSON(JsonConvert.SerializeObject(x), "clientFolder Exception", x.Message, EventSourceName);
                EventLogEntry(x.ToString(), EventSourceName, EventLogEntryType.Error);
                throw x;
            }

            /*
             select name, id, ClientFolderUNC__c from legalcase__c where id = 'a0R4P00000Ogj8vUAB'
             */
            return folder;
        }

        public Microsoft.SharePoint.Client.Folder AddSubFolder(ClientContext context, Microsoft.SharePoint.Client.Folder ParentFolder, string folderName)
        {
            Microsoft.SharePoint.Client.Folder resultFolder = ParentFolder.Folders.Add(folderName);
            context.ExecuteQuery();
            return resultFolder;
        }
        public int pushJSON(string json, string docType, string error, string app)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand();
            json = json.Replace("'", "''");
            error = error.Replace("'", "''");
            //string sql = "insert [log] (DocumentType,  Response, ErrorMessage, app) values ('" + docType + "', '" + json + "', '" + error + "', '" + app + "'); select id = @@IDENTITY";
            string sql = "insert [log] (DocumentType,  Response, ErrorMessage, app) values ('" + docType + "', '" + json + "', left('" + error + "',500), '" + app + "'); select id = @@IDENTITY";

            Debug.WriteLine(sql);
            try
            {
                //con.Open();                
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                da.Fill(dt);
                var r = dt.Rows[0][0];
                return int.Parse(r.ToString());
            }
            catch (SqlException ex)
            {
                EventLogEntry(ex.ToString(), EventSourceName, EventLogEntryType.Error);
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }

        }

        public static void EventLogEntry(string message, string sourceName, System.Diagnostics.EventLogEntryType eType)
        {
            //using Microsoft.Win32;
            string eventLogName = "Application";
            //string sourceName = "AssurePreHearing";
            EventLog eLog;
            eLog = new EventLog();
            eLog.Log = eventLogName;

            // set default event source (to be same as event log name) if not passed in
            //if ((sourceName == null) || (sourceName.Trim().Length == 0))
            //{
            //    sourceName = eventLogName;
            //}

            eLog.Source = sourceName;

            // Extra Raw event data can be added (later) if needed
            byte[] rawEventData = Encoding.ASCII.GetBytes("");

            /// Check whether the Event Source exists. It is possible that this may
            /// raise a security exception if the current process account doesn't
            /// have permissions for all sub-keys under
            /// HKEY_LOCAL_MACHINE\System\CurrentControlSet\Services\EventLog

            // Check whether registry key for source exists

            string keyName = @"SYSTEM\CurrentControlSet\Services\EventLog\" + eventLogName;

            RegistryKey rkEventSource = Registry.LocalMachine.OpenSubKey(keyName + @"\" + sourceName);

            // Check whether key exists
            if (rkEventSource == null)
            {
                /// Key does not exist. Create key which represents source
                Registry.LocalMachine.CreateSubKey(keyName + @"\" + sourceName);
                object eventMessageFile = null; // rkEventSource.GetValue("EventMessageFile");

                /// If the event Source Message File is not set, then set the Event Source message file.
                if (eventMessageFile == null)
                {
                    /// Source Event File Doesn't exist - determine .NET framework location,
                    /// for Event Messages file.
                    RegistryKey dotNetFrameworkSettings = Registry.LocalMachine.OpenSubKey(
                        @"SOFTWARE\Microsoft\.NetFramework\");

                    if (dotNetFrameworkSettings != null)
                    {

                        object dotNetInstallRoot = dotNetFrameworkSettings.GetValue(
                            "InstallRoot",
                            null,
                            RegistryValueOptions.None);

                        if (dotNetInstallRoot != null)
                        {
                            string eventMessageFileLocation =
                dotNetInstallRoot.ToString() +
                "v" +
                System.Environment.Version.Major.ToString() + "." +
                System.Environment.Version.Minor.ToString() + "." +
                System.Environment.Version.Build.ToString() +
                @"\EventLogMessages.dll";

                            /// Validate File exists
                            if (System.IO.File.Exists(
                eventMessageFileLocation))
                            {
                                /// The Event Message File exists in the anticipated location on the
                                /// machine. Set this value for the new Event Source

                                // Re-open the key as writable
                                rkEventSource = Registry.LocalMachine.OpenSubKey(
                                    keyName + @"\" + sourceName,
                                    true);

                                // Set the "EventMessageFile" property
                                rkEventSource.SetValue(
                                    "EventMessageFile",
                                    eventMessageFileLocation,
                                    RegistryValueKind.String);
                            }
                        }
                    }

                    dotNetFrameworkSettings.Close();
                }

            }

            /// Now validate that the .NET Event Message File, EventMessageFile.dll (which correctly
            /// formats the content in a Log Message) is set for the event source

            rkEventSource.Close();

            /// Log the message
            /// 
            eLog.WriteEntry(message, eType);

        }


    }
}
