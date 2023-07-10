using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration; 

namespace PDSWorker
{
    public class ConnectionManager
    {

        public SalesForce.SforceService service = new SalesForce.SforceService();

        private string _salesForceLogin = ConfigurationManager.AppSettings["SalesForceLogin"];
        private string _salesForcePassword = ConfigurationManager.AppSettings["SalesForcePassword"];

        string constr = ConfigurationManager.AppSettings["SFBackup"];

        public bool login()
        {
            string session = null;
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            if (service.SessionHeaderValue != null)
            {
                session = service.SessionHeaderValue.sessionId.ToString();
                if (session.Length > 0)
                    return true;
            }
            bool connection = false;
            SalesForce.LoginResult lr = service.login(_salesForceLogin, _salesForcePassword);
            service.Url = lr.serverUrl;
            service.Timeout = 160000;
            service.SessionHeaderValue = new SalesForce.SessionHeader();
            service.SessionHeaderValue.sessionId = lr.sessionId;
            session = service.SessionHeaderValue.sessionId.ToString();
            if (session.Length > 0)
                connection = true;

            pushJSON(@"Login to Salesforce.", "SF Login", "", "PortalDownloadService");

            return connection;
        }


        public int pushJSON(string json, string docType, string error, string app)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand();
            json = json.Replace("'", "''");
            error = error.Replace("'", "''");
            string sql = "insert [log] (DocumentType,  Response, ErrorMessage, app) values ('" + docType + "', '" + json + "', '" + error + "', '" + app + "'); select id = @@IDENTITY";
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
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }

        }



    }
}
