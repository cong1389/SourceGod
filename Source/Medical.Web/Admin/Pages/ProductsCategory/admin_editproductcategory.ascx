<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="admin_editproductcategory.ascx.cs"
    Inherits="Cb.Web.Admin.Pages.ProductsCategory.admin_editproductcategory" %>
<!--admin_editproductcategory-->
<%@ Register Assembly="Cb.WebControls" Namespace="Cb.WebControls" TagPrefix="uc" %>
<%@ Register TagPrefix="dgc" TagName="block_baseimage" Src="~/Admin/Controls/block_baseimage.ascx" %>
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
                    <td runat="server" id="tdDelete">
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
                                <asp:Literal ID="ltrAminHeaderName" runat="server" Text="Chỉnh sửa danh mục"></asp:Literal>
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
                <td width="185">
                    <strong>
                        <asp:Literal ID="ltrAminCategory" runat="server"></asp:Literal>
                    </strong>
                </td>
                <td>
                    <asp:DropDownList ID="drpCategory" runat="server" CssClass="form-control" Width="20%">
                    </asp:DropDownList>
                    <asp:CustomValidator ID="csv_drpCategory" runat="server" ValidationGroup="adminproductCategory"
                        Text="*" OnServerValidate="csv_drpCategory_ServerValidate"></asp:CustomValidator>
                </td>
            </tr>
            <tr runat="server" id="trPage">
                <td width="185">
                    <strong>
                        <abbr title="Đường dẫn chứa trang ascx. Ví dụ: Pages/CategoryManagement/Category.ascx">
                            Đường dẫn</abbr>
                    </strong>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtPage" TextMode="MultiLine" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPage"
                        Text="*" Enabled="false" runat="server" ValidationGroup="adminproductCategory"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <%--<tr>
                <td  width="185">
                    <strong>
                        <asp:Literal ID="ltrShowProduct" runat="server"></asp:Literal>
                        :</strong>
                </td>
                <td >
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
                <li><a href="#tabs-3">
                    <asp:Literal ID="ltrCategoryImages" runat="server" Text="Hình đại diện"></asp:Literal></a></li>
            </ul>
            <div id="tabs-1">
                <table class="adminform">
                    <tr>
                        <td width="185">
                            <strong>
                                <asp:Literal ID="ltrAminName" runat="server"></asp:Literal>
                            </strong>
                        </td>
                        <td>
                            <input type="text" name="txtName" id="txtName" size="60" maxlength="125" runat="server"
                                class="form-control" />
                            <asp:RequiredFieldValidator ID="reqv_txtNameVi" ControlToValidate="txtName" Text="*"
                                runat="server" ValidationGroup="adminproductCategory" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <asp:Literal ID="ltrIntro" runat="server" Text="Giới thiệu"></asp:Literal>
                            </strong>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtIntro" TextMode="MultiLine" Width="99.5%" Rows="5"
                                CssClass="form-control" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <asp:Literal ID="Literal25" runat="server" Text="Meta Information"></asp:Literal>
                            </strong>
                        </td>
                        <td>
                            <div>
                                <asp:TextBox runat="server" ID="txtMetaTitle" TextMode="MultiLine" Rows="5" Columns="23"
                                    placeholder="Meta Title" />
                                <asp:TextBox runat="server" ID="txtMetaKeyword" TextMode="MultiLine" Rows="5" Columns="60"
                                    placeholder="Meta Keywords" />
                                <asp:TextBox runat="server" ID="txtMetaDescription" TextMode="MultiLine" Rows="5"
                                    Columns="60" placeholder="Meta Description" /></div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <asp:Literal ID="ltrAdminIntro" runat="server" Text="Chi tiết"></asp:Literal>
                            </strong>
                        </td>
                        <td>
                            <uc:CKEditorControl runat="server" Language="vi" ID="txtDetail">
                            </uc:CKEditorControl>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="tabs-2">
                <table class="adminform">
                    <tr>
                        <td width="185">
                            <strong>
                                <asp:Literal ID="ltrAminName_En" runat="server"></asp:Literal></strong>
                        </td>
                        <td>
                            <input type="text" name="txtName_En" id="txtName_En" size="60" maxlength="125" runat="server"
                                class="form-control" />
                            <asp:RequiredFieldValidator ID="reqv_txtNameEn" ControlToValidate="txtName" Text="*"
                                Enabled="false" runat="server" ValidationGroup="adminproductCategory" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <asp:Literal ID="fsad" runat="server" Text="Introduction"></asp:Literal>
                            </strong>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtIntro_Eng" TextMode="MultiLine" Width="99.5%"
                                Rows="5" CssClass="form-control" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <asp:Literal ID="ltrMetaInfo" runat="server" Text="Meta Information"></asp:Literal>
                            </strong>
                        </td>
                        <td>
                            <div>
                                <asp:TextBox runat="server" ID="txtMetaTitle_Eng" TextMode="MultiLine" Rows="5" Columns="23"
                                    placeholder="Meta Title" />
                                <asp:TextBox runat="server" ID="txtMetaKeyword_Eng" TextMode="MultiLine" Rows="5"
                                    Columns="60" placeholder="Meta Keywords" />
                                <asp:TextBox runat="server" ID="txtMetaDescription_Eng" TextMode="MultiLine" Rows="5"
                                    Columns="60" placeholder="Meta Description" /></div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <asp:Literal ID="Literal3" runat="server" Text="Detail"></asp:Literal>
                            </strong>
                        </td>
                        <td>
                            <uc:CKEditorControl runat="server" Language="vi" ID="txtDetail_Eng">
                            </uc:CKEditorControl>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="tabs-3">
                <dgc:block_baseimage ID="block_baseimage" runat="server" />
            </div>
        </div>
    </div>
</div>
<input type="hidden" name="task" value="" />
<input type="hidden" name="id" value="<%=productcategoryId%>" />