<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="admin_editmanagementid.ascx.cs"
    Inherits="Cb.Web.Admin.Pages.ManagementID.admin_editmanagementid" %>
<%@ Register Assembly="Cb.WebControls" Namespace="Cb.WebControls" TagPrefix="uc" %>
<script language="javascript" type="text/javascript">
    function checkForm() {
        return true;
    }
    function submitButton(pressbutton) {
        var f = document.adminForm;
        submitForm(f, pressbutton);
    }
    function CheckProvider(src, args) {
        if (args.Value == '0')
            args.IsValid = false;
    }

    $(function () {
        $("#tabs").tabs();
        $("a.zoom-image").fancybox();
    });

</script>
<table width="100%" class="menubar" cellpadding="0" cellspacing="0" border="0">
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
                            <asp:ImageButton CssClass="toolbar" ValidationGroup="adminproductCategory" ID="btn_Save"
                                runat="server" AlternateText="Save" name="Save" title="Save" ImageUrl="~/admin/images/save_f2.png"
                                OnClick="btn_Save_Click" />
                            <br />
                            <asp:Literal ID="ltrAdminSave" runat="server" Text="strSave"></asp:Literal></a>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <a class="toolbar">
                            <asp:ImageButton CssClass="toolbar" ID="btn_Apply" runat="server" ValidationGroup="adminproductCategory"
                                AlternateText="Apply" name="apply" title="Apply" ImageUrl="~/admin/images/apply_f2.png"
                                OnClick="btn_Apply_Click" />
                            <br />
                            <asp:Literal ID="ltrAdminApply" runat="server" Text="strUpdate"></asp:Literal></a>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <a class="toolbar">
                            <asp:ImageButton CssClass="toolbar" ID="btn_Delete" runat="server" CausesValidation="false"
                                AlternateText="Delete" name="Delete" title="Delete" ImageUrl="~/admin/images/delete_f2.png"
                                OnClick="btn_Delete_Click" />
                            <br />
                            <asp:Literal ID="ltrAdminDelete" runat="server" Text="strDelete"></asp:Literal></a>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <a class="toolbar">
                            <asp:ImageButton CssClass="toolbar" ID="btn_Cancel" runat="server" CausesValidation="false"
                                AlternateText="Cancel" name="Cancel" title="Cancel" ImageUrl="~/admin/images/cancel_f2.png"
                                OnClick="btn_Cancel_Click" />
                            <br />
                            <asp:Literal ID="ltrAdminCancel" runat="server" Text="strIgnore"></asp:Literal></a>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<br />
<div class="centermain">
    <div class="main">
        <asp:ValidationSummary ID="sumv_SumaryValidate" ValidationGroup="adminproductCategory"
            DisplayMode="BulletList" ShowSummary="false" ShowMessageBox="true" runat="server" />
        <table cellpadding="1" cellspacing="1" border="0" width="100%">
            <tr>
                <td width="250">
                    <table class="adminheading">
                        <tr>
                            <th nowrap="nowrap" class="config">
                                <asp:Literal ID="ltrAminHeaderName" runat="server" Text="Chỉnh sửa ID"></asp:Literal>
                            </th>
                        </tr>
                        <tr>
                            <td style="font-size: 15px;">
                                <asp:Literal ID="Literal2" runat="server" Text="Qui tắc: [Tên Usc]_[Name]"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <table class="adminform">
            <tr>
                <td width="18%">
                    <strong>
                        <asp:Literal ID="ltrAminPublish" runat="server" Text="strPublish"></asp:Literal></strong>
                </td>
                <td>
                    <input type="checkbox" name="chkPublished" id="chkPublished" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="left" width="185">
                    <strong>
                        <asp:Literal ID="ltrAminName" runat="server" Text="strName"></asp:Literal>
                        :</strong>
                </td>
                <td align="left">
                    <input type="text" name="txtName" id="txtName" size="50" runat="server" />
                    <asp:RequiredFieldValidator ID="reqv_txtNameVi" ControlToValidate="txtName" Text="*"
                        runat="server" ValidationGroup="adminproductCategory" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="left" width="185">
                    <strong>
                        <asp:Literal ID="Literal1" runat="server" Text="Value"></asp:Literal>
                        :</strong>
                </td>
                <td align="left">
                    <input type="text" name="txtName" id="txtValue" size="50" runat="server" />
                    <asp:RequiredFieldValidator ID="reqv_txtValueVi" ControlToValidate="txtValue" Text="*"
                        runat="server" ValidationGroup="adminproductCategory" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </div>
</div>
<input type="hidden" name="task" value="" />
<input type="hidden" name="id" value="<%=productcategoryId%>" />