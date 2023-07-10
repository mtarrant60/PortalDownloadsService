<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PDDCreateFolder.aspx.cs" Inherits="PortalDownloadsService.PDDCreateFolder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        <asp:Label runat="server" id="lblFolder" Text=""></asp:Label>
        <br />
        <asp:HyperLink ID="hyperlinkFolder" runat="server" Text="Go to folder" NavigateUrl="https://bbins365.sharepoint.com/sites/565-LTDPortalDocuments">Go to folder</asp:HyperLink>
        </div>
        
    </form>
</body>
</html>
