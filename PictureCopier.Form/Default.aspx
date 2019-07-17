<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PictureCopier.Form._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Elements/css/bootstrap.min.css" rel="stylesheet" />
    <script src="Elements/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="col-8">
            <div class="form-group">
                <asp:Label runat="server" Text="Url: " />
                <asp:TextBox runat="server" ID="txtUrl" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtUrl" CssClass="text-danger" ErrorMessage="URL is not valid"/>
            </div>
            <div class="form-group">
                <asp:Label runat="server" Text="Save Path: " />
                <asp:TextBox runat="server" ID="txtSavePath" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtSavePath" CssClass="text-danger" ErrorMessage="Path is not valid"/>
            </div>

            <div class="form-group">
                <asp:Label runat="server" Text="Save Path: " />
                <asp:TextBox runat="server" ID="txtTimeToSleep" CssClass="form-control" Text="10" />
                <small class="form-text text-muted">Default value is 10 second. You can change according to your internet speed. </small>
                <asp:RegularExpressionValidator runat="server" ValidationExpression="^[0-9]*$" ControlToValidate="txtTimeToSleep" CssClass="text-danger" ErrorMessage="Time is not valid" />
            </div>

            <asp:Button runat="server" Text="Download Files" ID="btnDownloadFile" OnClick="btnDownloadFile_OnClick" CssClass="btn btn-primary" />
            
            <div class="form-group">
                <asp:Literal runat="server" Mode="Encode" ID="ltrMessage" />
            </div></div>
        </div>
    </form>
</body>
</html>
