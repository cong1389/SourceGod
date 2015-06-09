<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="admin_editbanner.ascx.cs" Inherits="Cb.Web.Admin.Pages.Banner.admin_editbanner" %>
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
                                <asp:Literal ID="ltrAminHeaderName" runat="server" Text="strEditBanner"></asp:Literal>
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
                        <asp:Literal ID="ltrAminPublish" runat="server" Text="strPublish"></asp:Literal></strong>
                </td>
                <td>
                    <input type="checkbox" name="chkPublished" id="chkPublished" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="left" width="185">
                    <strong>
                        <asp:Literal ID="ltrAminCategory" runat="server" Text="strPosition"></asp:Literal>
                        :</strong>
                </td>
                <td align="left">
                    <asp:DropDownList ID="drpCategory" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="drpCategory"
                        Text="*" ErrorMessage="strNoSelectedIitem" runat="server" ValidationGroup="adminproductCategory"></asp:RequiredFieldValidator>
                </td>
            </tr>
               <tr>
                <td width="18%">
                    <strong>
                        <asp:Literal ID="Literal2" runat="server" Text="Link ngoài trang"></asp:Literal></strong>
                </td>
                <td>
                    <input type="checkbox" name="chkOutPage" id="chkOutPage" runat="server" />(đến 1 liên kết website khác)
                </td>
            </tr>
            <tr>
                <td>
                    <strong>
                        <asp:Literal ID="Literal3" runat="server" Text="strImage"></asp:Literal>
                        :</strong>
                </td>
                <td>
                    <asp:FileUpload ID="fuImage" runat="server" EnableViewState="true" />
                    <asp:Button ID="btnUploadImage" runat="server" Text="strUpload" OnClick="btnUploadImage_Click" />
                    <asp:LinkButton ID="lbnView" runat="server" Text="strView" Visible="false" CssClass="zoom-image"></asp:LinkButton>
                    <asp:LinkButton ID="lbnDelete" runat="server" Text="strDelete" Visible="false" OnClick="lbnDelete_Click"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="left" width="185">
                    <strong>
                        <asp:Literal ID="ltrAminName" runat="server" Text="strName"></asp:Literal>
                        :</strong>
                </td>
                <td align="left">
                    <input type="text" name="txtName" id="txtName" size="50" maxlength="50" runat="server" />
                    <asp:RequiredFieldValidator ID="reqv_txtNameVi" ControlToValidate="txtName" Text="*"
                        runat="server" ValidationGroup="adminproductCategory" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </td>
            </tr>
             <tr>
                <td align="left" width="185">
                    <strong>
                        <asp:Literal ID="Literal4" runat="server" Text="Link"></asp:Literal>
                        :</strong>
                </td>
                <td align="left">
                    <input type="text" name="txtLink" id="txtLink" size="50" maxlength="50" runat="server" />
                  (Nếu liên kết trong trang thì dang /abc/index.html. Nếu liên kết ngoài thì dạng http://abc.com/index.html)
                </td>
            </tr>
            <tr>
                <td align="left" width="185">
                    <strong>
                        <asp:Literal ID="Literal1" runat="server" Text="strPage"></asp:Literal>
                        :</strong>
                </td>
                <td align="left">
                    <asp:CheckBoxList runat="server" ID="chkPage">
                    </asp:CheckBoxList>
                </td>
            </tr>
        </table>
    </div>
</div>
<input type="hidden" name="task" value="" />
<input type="hidden" name="id" value="<%=productcategoryId%>" />