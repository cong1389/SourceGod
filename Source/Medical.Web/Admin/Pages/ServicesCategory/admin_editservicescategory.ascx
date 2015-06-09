<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="admin_editservicescategory.ascx.cs" Inherits="Cb.Web.Admin.Pages.ServicesCategory.admin_editservicescategory" %>
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
                            <asp:Literal ID="ltrAdminSave" runat="server"></asp:Literal></a>
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
                            <asp:Literal ID="ltrAdminApply" runat="server"></asp:Literal></a>
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
                            <asp:Literal ID="ltrAdminDelete" runat="server"></asp:Literal></a>
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
                            <asp:Literal ID="ltrAdminCancel" runat="server"></asp:Literal></a>
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
                                <asp:Literal ID="ltrAminHeaderName" runat="server"></asp:Literal>
                            </th>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table class="adminform">
            <tr>
                <td width="18%">
                    <strong>
                        <asp:Literal ID="ltrAminPublish" runat="server"></asp:Literal></strong>
                </td>
                <td>
                    <input type="checkbox" name="chkPublished" id="chkPublished" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="left" width="185">
                    <strong>
                        <asp:Literal ID="ltrAminCategory" runat="server"></asp:Literal>
                        :</strong>
                </td>
                <td align="left">
                    <asp:DropDownList ID="drpCategory" runat="server">
                    </asp:DropDownList>
                    <asp:CustomValidator ID="csv_drpCategory" runat="server" ValidationGroup="adminproductCategory"
                        Text="*" OnServerValidate="csv_drpCategory_ServerValidate"></asp:CustomValidator>
                </td>
            </tr>
            <%--<tr>
                <td align="left" width="185">
                    <strong>
                        <asp:Literal ID="ltrShowProduct" runat="server"></asp:Literal>
                        :</strong>
                </td>
                <td align="left">
                    <input type="checkbox" id="chkShowProduct" runat="server" />
                </td>
            </tr>--%>
        </table>
        <div id="tabs" class="tab-page">
            <ul>
                <li><a href="#tabs-1">
                    <asp:Literal ID="ltrAminLangVi" runat="server"></asp:Literal></a></li>
                <li><a href="#tabs-2">
                    <asp:Literal ID="ltrAminLangEn" runat="server"></asp:Literal></a></li>
            </ul>
            <div id="tabs-1">
                <table class="adminform">
                    <tr>
                        <td align="left" width="185">
                            <strong>
                                <asp:Literal ID="ltrAminName" runat="server"></asp:Literal>
                                :</strong>
                        </td>
                        <td align="left">
                            <input type="text" name="txtName" id="txtName" size="60" maxlength="125" runat="server" />
                            <asp:RequiredFieldValidator ID="reqv_txtNameVi" ControlToValidate="txtName" Text="*"
                                runat="server" ValidationGroup="adminproductCategory" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <%--<tr>
                        <td align="left" width="185">
                            <strong>
                                <asp:Literal ID="ltrAdminIntro" runat="server"></asp:Literal>
                                :</strong>
                        </td>
                        <td align="left">
                            <uc:CKEditorControl runat="server" Language="vi" ID="editDetailVi">
                            </uc:CKEditorControl>
                        </td>
                    </tr>--%>
                </table>
            </div>
            <div id="tabs-2">
                <table class="adminform">
                    <tr>
                        <td align="left" width="185">
                            <strong>
                                <asp:Literal ID="ltrAminName_En" runat="server"></asp:Literal></strong>
                        </td>
                        <td align="left">
                            <input type="text" name="txtName_En" id="txtName_En" size="60" maxlength="125" runat="server" />
                            <asp:RequiredFieldValidator ID="reqv_txtNameEn" ControlToValidate="txtName" Text="*" Enabled="false"
                                runat="server" ValidationGroup="adminproductCategory" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <%--<tr>
                        <td align="left" width="185">
                            <strong>
                                <asp:Literal ID="ltrAdminIntro_En" Text="Intro" runat="server"></asp:Literal></strong>
                        </td>
                        <td align="left">
                            <uc:CKEditorControl runat="server" ID="editDetailEn">
                            </uc:CKEditorControl>
                        </td>
                    </tr>--%>
                </table>
            </div>
        </div>
    </div>
</div>
<input type="hidden" name="task" value="" />
<input type="hidden" name="id" value="<%=productcategoryId%>" />