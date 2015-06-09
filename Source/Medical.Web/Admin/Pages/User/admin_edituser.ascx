<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="admin_edituser.ascx.cs"
    Inherits="Web.Admin.Pages.User.admin_edituser" %>
<script language="javascript" type="text/javascript">
    var txtFullName = document.getElementById('<%=txtFullName.ClientID%>');
    var txtEmail = document.getElementById('<%=txtEmail.ClientID%>');
    var drpPermission = document.getElementById('<%=drpPermission.ClientID%>');
    function CheckUserName(src, args) {
        var txtUsername = document.getElementById('<%=txtUsername.ClientID%>');
        args.IsValid = checkLength(txtUsername.value, 5, 50);
    }
    function CheckPassWord(src, args) {
        var txtPassword = document.getElementById('<%=txtPassword.ClientID%>');
        args.IsValid = checkLength(txtPassword.value, 8, 50);
    }

    function OnChangePer() {

    }
    window.onload = OnChangePer;
</script>
<style>
    select
    {
        width: 50% !important;
    }
    input[type="checkbox"], select
    {
        margin: 4px -2px 20px;
    }
    input[type="text"], input[type="password"]
    {
        display: block;
        width: 50%;
        height: 34px;
        padding: 6px 12px;
        font-size: 14px;
        line-height: 1.4;
        color: #555;
        background-color: #fff;
        background-image: none;
        border: 1px solid #ccc;
        border-radius: 4px;
        -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
        box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
        -webkit-transition: border-color ease-in-out .15s, -webkit-box-shadow ease-in-out .15s;
        -o-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
        transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
    }
</style>
<table width="100%" class="menubar">
    <tr>
        <td class="menudottedline" width="40%">
            <div class="pathway">
            </div>
        </td>
        <td class="menudottedline" align="right">
            <table cellpadding="0" cellspacing="0" border="0" id="toolbar">
                <tr valign="middle" align="center">
                    <td>
                        <a class="toolbar">
                            <asp:ImageButton CssClass="toolbar" ValidationGroup="adminuser" ID="btn_Save" runat="server"
                                AlternateText="Save" name="Save" title="Save" OnClick="btn_Save_Click" />
                            <br />
                            <asp:Literal ID="ltrAdminSave" runat="server" Text="strSave"></asp:Literal></a>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <a class="toolbar">
                            <asp:ImageButton CssClass="toolbar" ID="btn_Apply" runat="server" ValidationGroup="adminuser"
                                AlternateText="Apply" name="apply" title="Apply" OnClick="btn_Apply_Click" />
                            <br />
                            <asp:Literal ID="ltrAdminApply" runat="server" Text="strUpdate"></asp:Literal></a>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <a class="toolbar">
                            <asp:ImageButton CssClass="toolbar" ID="btn_Delete" runat="server" CausesValidation="false"
                                AlternateText="Delete" name="Delete" title="Delete" OnClick="btn_Delete_Click" />
                            <br />
                            <asp:Literal ID="ltrAdminDelete" runat="server" Text="strDelete"></asp:Literal></a>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <a class="toolbar">
                            <asp:ImageButton CssClass="toolbar" ID="btn_Cancel" runat="server" CausesValidation="false"
                                AlternateText="Cancel" name="Cancel" title="Cancel" OnClick="btn_Cancel_Click" />
                            <br />
                            <asp:Literal ID="ltrAdminCancel" runat="server" Text="strIgnore"></asp:Literal></a>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<br />
<!-- BEGIN show_msg -->
<div class="message">
    <%=show_msg%>
</div>
<!-- END show_msg -->
<div class="centermain">
    <div class="main">
        <asp:ValidationSummary ID="sumv_SumaryValidate" ValidationGroup="adminuser" DisplayMode="BulletList"
            ShowSummary="false" ShowMessageBox="true" runat="server" />
        <table class="adminheading">
            <tr>
                <th class="user">
                    <%=header_name%>
                </th>
            </tr>
        </table>
        <table class="adminform">
            <tr>
                <td width="50px">
                    <strong>
                        <%=l_publish%></strong>
                </td>
                <td width="200px">
                    <input type="checkbox" name="chkPublished" id="chkPublished" runat="server" />
                </td>
            </tr>
            <!-- BEGIN show_new_username -->
            <tr>
                <td>
                    <strong>
                        <%=l_username%><span style="color: #FF0000">*</span></strong>
                </td>
                <td>
                    <input type="text" name="txtUsername" id="txtUsername" maxlength="50" runat="server" />
                    <asp:RequiredFieldValidator ID="reqv_txtUsername" ControlToValidate="txtUsername"
                        SetFocusOnError="true"  Enabled="false" Text="*" runat="server" ValidationGroup="adminuser"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cusv_txtUsername" ControlToValidate="txtUsername" ValidationGroup="adminuser"
                        ClientValidationFunction="CheckUserName" runat="server"></asp:CustomValidator>
                    <i style="color: Red">
                        <asp:Literal ID="ltrNoteUsername" runat="server"></asp:Literal></i>
                </td>
            </tr>
            <!-- END show_new_username -->
            <tr>
                <td>
                    <strong>
                        <%=l_password%></strong>
                </td>
                <td>
                    <input type="password" name="txtPassword" id="txtPassword" size="20" maxlength="25"
                        runat="server" cssclass="form-control" />
                    <asp:RequiredFieldValidator ID="reqv_txtPassword" ControlToValidate="txtPassword"
                        Text="*" runat="server" ValidationGroup="adminuser"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cus_checkPassWord" ControlToValidate="txtPassword" ValidationGroup="adminuser"
                        ClientValidationFunction="CheckPassWord" Text="*" runat="server"></asp:CustomValidator>
                    <i style="color: Red">
                        <asp:Literal ID="ltrNotePassword" runat="server"></asp:Literal></i>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>
                        <%=l_confirmpassword%></strong>
                </td>
                <td>
                    <input type="password" name="txtConfirmpassword" id="txtConfirmpassword" size="20"
                        maxlength="25" runat="server" />
                    <asp:RequiredFieldValidator ID="reqvc_txtConfirmpassword" ControlToValidate="txtConfirmpassword"
                        Text="*" runat="server" ValidationGroup="adminuser" ErrorMessage="<%=msg_user_confirmpassword%>"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="comv_Password" runat="server" ControlToValidate="txtConfirmpassword"
                        Text="*" ControlToCompare="txtPassword" ValidationGroup="adminuser" ErrorMessage="<%=msg_user_not_same_password%>"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>
                        <%=l_name%><span style="color: #FF0000">*</span></strong>
                </td>
                <td>
                    <input type="text" name="txtName" id="txtFullName" size="60" maxlength="50" runat="server" />
                    <asp:RequiredFieldValidator ID="reqvc_txtFullName" ControlToValidate="txtFullName"
                        Text="*" runat="server" ValidationGroup="adminuser" ErrorMessage="<%=msg_user_name%>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>
                        <%=l_email%><span style="color: #FF0000">*</span></strong>
                </td>
                <td>
                    <input type="text" name="txtEmail" id="txtEmail" size="30" maxlength="50" runat="server" />
                    <asp:RequiredFieldValidator ID="reqvc_txtEmail" ControlToValidate="txtEmail" Text="*"
                        runat="server" ValidationGroup="adminuser" ErrorMessage="<%=msg_empty_email%>"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regv_Email" ControlToValidate="txtEmail" runat="server"
                        Text="*" ValidationExpression="" ValidationGroup="adminuser" ErrorMessage="<%=msg_invalid_email%>"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>
                        <%=l_phone%></strong>
                </td>
                <td>
                    <input type="text" name="txtPhone" id="txtPhone" maxlength="15" runat="server" header_name />
                    <asp:RegularExpressionValidator ID="regv_txtPhone" ControlToValidate="txtPhone" runat="server"
                        ValidationGroup="adminuser" Text="*"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>
                        <asp:Literal ID="asdfsdf" runat="server" Text="ltrFooterTelCompany"></asp:Literal>:</strong>
                </td>
                <td>
                    <input type="text" name="txtMobile" id="txtMobile" maxlength="15" runat="server"
                        cssclass="form-control" />
                    <asp:RegularExpressionValidator ID="regv_txtMobile" ControlToValidate="txtMobile"
                        runat="server" ValidationGroup="adminuser" Text="*"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>
                        <asp:Literal ID="asdfasfas" runat="server" Text="statement_location"></asp:Literal>:</strong>
                </td>
                <td>
                    <asp:DropDownList ID="drpCity" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>
                        <%=l_address%></strong>
                </td>
                <td>
                    <input type="text" name="txtAddress" id="txtAddress" size="50" maxlength="125" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <strong>
                        <%=l_permission%></strong>
                </td>
                <td>
                    <asp:DropDownList ID="drpPermission" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>
                        <asp:Literal ID="Literal1" runat="server" Text="strNewsPromo"></asp:Literal></strong>
                </td>
                <td>
                    <asp:CheckBox ID="cbxNewsPromo" runat="server" />
                </td>
            </tr>
        </table>
    </div>
</div>
<input type="hidden" name="task" value="" />
<input type="hidden" name="id" value="<%=id%>" />
