<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="loginadmin.aspx.cs" Inherits="Cb.Web.loginadmin"
    EnableTheming="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="/Styles/bootstrap.min.css" />
    <link href="/Styles/font-awesome.min.css" rel="stylesheet" />
    <link href="/Styles/LoginAdmin.css" rel="stylesheet" />
    <script type="text/javascript" src="/Scripts/bootstrap.min.js"></script>
</head>
<body>
    <header class="header demo-header">
        <div class="row">
            <div class="demo-nav">
                <div class="demo-dropdown">
                    <h1>
                        QUẢN TRỊ ADMIN</h1>
                </div>
            </div>
        </div>
    </header>
    <form id="frmLogin" runat="server">
    <section class="container">
        <div class="login">
            <h1>
                <span class="glyphicon glyphicon-user"></span>ĐĂNG NHẬP</h1>
            <p>
                <input type="text" id="txtUserName" name="username" runat="server" />
                <asp:RequiredFieldValidator ID="reqTxtUserName" ControlToValidate="txtUserName" ValidationGroup="gLogin"
                    SetFocusOnError="true" runat="server" Display="Dynamic"></asp:RequiredFieldValidator></p>
            <p>
                <input type="password" id="txtPassword" name="password" runat="server" />
                <asp:RequiredFieldValidator ID="reqTxtPassword" ControlToValidate="txtPassword" ValidationGroup="gLogin"
                    SetFocusOnError="true" runat="server" Display="Dynamic"></asp:RequiredFieldValidator>
                &nbsp; <a href="#" onclick="return ClickForgotPass();">
                    <asp:Literal ID="ltrForgotPass" runat="server"></asp:Literal></a></p>
            <p style="display: none;">
                <asp:DropDownList ID="drpRole" runat="server">
                </asp:DropDownList>
            </p>
            <p class="remember_me">
                <input type="checkbox" name="rem" id="rem" runat="server" />
                <input type="hidden" id="hidOnChangePass" name="hidOnChangePass" runat="server" /><label
                    for="ctl02_ctl01_ctl00_chkRemember"><asp:Literal ID="ltrRememberPass" runat="server"></asp:Literal></label>
            </p>
            <p>
                <div id="trSecurityCode" runat="server">
                    <asp:TextBox ID="txt_Security_Code" runat="server" Columns="8" MaxLength="6" Style="float: left"></asp:TextBox>
                    <asp:Image ID="img_Security_Code" runat="server" Style="margin-bottom: -5px;" />
                    <asp:CustomValidator ID="cus_Same_Security_Code" runat="server" ValidationGroup="gLogin"
                        OnServerValidate="ValidateCheckSameSecurityCodeServer"></asp:CustomValidator>
                </div>
            </p>
            <p class="submit">
                <asp:Button ID="btn_login" ValidationGroup="gLogin" runat="server" CssClass="btn btn-info" /></p>
        </div>
    </section>
    </form>
</body>
</html>
