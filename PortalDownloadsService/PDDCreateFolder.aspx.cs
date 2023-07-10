using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PortalDownloadsService
{
    public partial class PDDCreateFolder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //btnClose.Attributes.Add("OnClick", "window.close();");
                
                string legalcaseid = Request.QueryString["LegalcaseId"];
                if (legalcaseid != null)
                {
                    PDS.Download d = new PDS.Download();

                    //"https://bbins365.sharepoint.com/"


                    hyperlinkFolder.NavigateUrl = d.CreateDocumentFolder(legalcaseid);
                    if (hyperlinkFolder.NavigateUrl.Substring(hyperlinkFolder.NavigateUrl.Length - 18) == legalcaseid)
                    {
                        lblFolder.Text = "Folder Created:";
                        lblFolder.Visible = true;
                    }
                    else
                    { 
                        lblFolder.Text = "Failed to create folder";
                        lblFolder.Visible = true;
                    }
                }

            }
        }
    }
}