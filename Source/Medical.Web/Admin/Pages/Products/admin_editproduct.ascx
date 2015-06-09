<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="admin_editproduct.ascx.cs"
    Inherits="Cb.Web.Admin.Pages.Products.admin_editproduct" %>
<%@ Register Assembly="Cb.WebControls" Namespace="Cb.WebControls" TagPrefix="uc" %>
<%@ Register TagPrefix="dgc" TagName="block_baseimage" Src="~/Admin/Controls/block_baseimage.ascx" %>
<%@ Register TagPrefix="dgc" TagName="block_uploadimage" Src="~/Admin/Controls/block_uploadimage.ascx" %>
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
                                <asp:Literal ID="ltrAminHeaderName" runat="server" Text="Cập nhật"></asp:Literal>
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
                        <asp:Literal ID="ltrAminPublish" runat="server" Text="strPublish"></asp:Literal>
                    </strong>
                </td>
                <td>
                    <input type="checkbox" name="chkPublished" id="chkPublished" runat="server" />
                </td>
                <td style="display: none;">
                    <strong>
                        <asp:Literal ID="Literal8" runat="server" Text="Tình trạng"></asp:Literal>:</strong>
                </td>
                <td style="display: none;">
                    <input type="text" name="txtPrice" id="txtStatus" runat="server" style="width: 250px" />
                </td>
            </tr>
            <tr>
                <td>
                    <strong>
                        <asp:Literal ID="Literal27" runat="server" Text="Danh mục"></asp:Literal></strong>
                </td>
                <td>
                    <asp:DropDownList ID="drpCategory" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                    <asp:CustomValidator ID="csv_drpCategory" runat="server" ValidationGroup="adminproductCategory"
                        Text="*" OnServerValidate="csv_drpCategory_ServerValidate"></asp:CustomValidator>
                </td>
                <td>
                    <strong>
                        <asp:Literal ID="Literal24" runat="server" Text="Chuyên đề"></asp:Literal>
                    </strong>
                </td>
                <td>
                    <div>
                        <input type="radio" name="Published" id="chkPublishedHot" runat="server" />Sách
                        hay nhất
                        <input type="radio" name="Published" id="chkPublishedFeature" runat="server" />Sách
                        nổi bật</div>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td style="display: none;">
                    <strong>
                        <asp:Literal ID="Literal7" runat="server" Text="Mã căn hộ"></asp:Literal>:</strong>
                </td>
                <td style="display: none;">
                    <input type="text" name="txtPrice" id="txtCode" runat="server" style="width: 250px" />
                </td>
            </tr>
            <tr>
                <td>
                    <strong>
                        <asp:Literal ID="Literal3" runat="server" Text="Upload pdf file"></asp:Literal>
                    </strong>
                </td>
                <td>
                    <asp:FileUpload ID="fuImage" runat="server" EnableViewState="true" CssClass="fuImage" />
                    <asp:Button ID="btnUploadImage" runat="server" Text="strUpload" OnClick="btnUploadImage_Click"
                        CssClass="btn btn-success btn-sm" />
                    <asp:LinkButton ID="lbnView" runat="server" Text="strView" Visible="false" OnClick="btnViewPdf_Click"><span class="glyphicon glyphicon-fullscreen btn-sm">
                        </span></asp:LinkButton>
                    <asp:LinkButton ID="lbnDelete" runat="server" Text="strDelete" Visible="false" OnClick="lbnDeleteImage_Click"><span class="glyphicon glyphicon-trash btn-sm">
                        </span></asp:LinkButton>
                </td>
                <td>
                    <strong>
                        <asp:Literal ID="Literal10" runat="server" Text="Website"></asp:Literal></strong>
                </td>
                <td>
                    <input type="text" name="txtPrice" id="txtWebsite" runat="server" style="width: 250px"
                        class="form-control" />
                </td>
            </tr>
            <tr>
                <td>
                    <strong>
                        <asp:Literal ID="lbPrice" runat="server" Text="strPrice"></asp:Literal>
                        (Triệu)</strong>
                </td>
                <td>
                    <input type="text" name="txtPrice" id="txtPrice" runat="server" class="form-control" />
                </td>
                <td>
                    <strong>
                        <asp:Literal ID="Literal26" runat="server" Text="Tiền tệ"></asp:Literal></strong>
                </td>
                <td>
                    <asp:DropDownList ID="drpCost" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                    <%--   <asp:RegularExpressionValidator ID="reqE_Price" ControlToValidate="txtPrice" runat="server"
                        Text="*" ValidationGroup="adminproductCategory"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="req_Price" ControlToValidate="txtPrice" Text="*"
                        runat="server" ValidationGroup="adminproductCategory" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>
                        <asp:Literal ID="Literal6" runat="server" Text="Phòng tắm"></asp:Literal>:</strong>
                </td>
                <td>
                    <input type="text" name="txtPrice" id="txtBathRoom" runat="server" class="form-control" />
                    <asp:RegularExpressionValidator ID="reqE_BathRoom" ControlToValidate="txtBathRoom"
                        runat="server" Text="*" ValidationGroup="adminproductCategory"></asp:RegularExpressionValidator>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr runat="server" id="divPage">
                <td>
                    <strong>
                        <abbr title="Đường dẫn chứa trang ascx. Ví dụ: Pages/CategoryManagement/CategoryDetail.ascx">
                            Đường dẫn</abbr>
                    </strong>
                </td>
                <td colspan="4">
                    <asp:TextBox runat="server" ID="txtPage" TextMode="MultiLine" Width="99.5%" Rows="1"
                        CssClass="form-control" />
                </td>
            </tr>
            <tr style="display: none;">
                <td>
                    <strong>
                        <asp:Literal ID="Literal4" runat="server" Text="Diện tích (m²)(*)"></asp:Literal>:</strong>
                </td>
                <td>
                    <input type="text" name="txtPrice" id="txtArea" runat="server" style="width: 250px" />
                    <asp:RegularExpressionValidator ID="reqE_Area" ControlToValidate="txtArea" runat="server"
                        Text="*" ValidationGroup="adminproductCategory"></asp:RegularExpressionValidator>
                </td>
                <td>
                    <strong>
                        <asp:Literal ID="Literal1" runat="server" Text="Phòng ngủ"></asp:Literal>:</strong>
                </td>
                <td>
                    <input type="text" name="txtPrice" id="txtBedRoom" runat="server" style="width: 250px" />
                    <asp:RegularExpressionValidator ID="req_BedRoom" ControlToValidate="txtBedRoom" runat="server"
                        Text="*" ValidationGroup="adminproductCategory"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr style="display: none;">
                <td>
                    <strong>
                        <asp:Literal ID="ltrProvince" runat="server" Text="Tỉnh/ Thành phố (**)"></asp:Literal>:</strong>
                </td>
                <td>
                    <asp:DropDownList ID="drpProvince" runat="server" CssClass="drpSearch">
                    </asp:DropDownList>
                </td>
                <td>
                    <strong>
                        <asp:Literal ID="Literal5" runat="server" Text="Quận/ Huyện (*)"></asp:Literal>:</strong>
                </td>
                <td>
                    <asp:DropDownList ID="drpDistrict" runat="server" CssClass="drpSearch">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="display: none;">
                <td>
                    <strong>
                        <asp:Literal ID="Literal9" runat="server" Text="Vĩ độ (*)"></asp:Literal>:</strong>
                </td>
                <td>
                    <input type="text" name="txtMap" id="txtLatitude" runat="server" style="width: 250px;" />
                    <asp:RegularExpressionValidator ID="req_Latitude" ControlToValidate="txtLatitude"
                        runat="server" Text="*" ValidationGroup="adminproductCategory"></asp:RegularExpressionValidator>
                </td>
                <td>
                    <strong>
                        <asp:Literal ID="Literal23" runat="server" Text="Kinh độ (*)"></asp:Literal>:</strong>
                </td>
                <td>
                    <input type="text" name="txtMap" id="txtLongitude" runat="server" />
                    <asp:RegularExpressionValidator ID="req_Longitude" ControlToValidate="txtLongitude"
                        runat="server" Text="*" ValidationGroup="adminproductCategory"></asp:RegularExpressionValidator>
                </td>
            </tr>
        </table>
        <!--  Detail-->
        <div id="tabs" class="tab-page">
            <ul>
                <li><a href="#tabs-1">
                    <asp:Literal ID="ltrAminLangVi" runat="server" Text="strVietName"></asp:Literal></a></li>
                <li><a href="#tabs-2">
                    <asp:Literal ID="ltrAminLangEn" runat="server" Text="strEnglish_en"></asp:Literal></a></li>
                <li><a href="#tabs-3">
                    <asp:Literal ID="ltrAvartarImages" runat="server" Text="Hình đại diện"></asp:Literal></a></li>
                <li><a href="#tabs-4">
                    <asp:Literal ID="ltrUploadFile" runat="server" Text="Upload Files"></asp:Literal></a></li>
            </ul>
            <div id="tabs-1">
                <table class="adminform">
                    <tr>
                        <td>
                            <strong>
                                <asp:Literal ID="ltrAminName" runat="server" Text="Tên"></asp:Literal>
                            </strong>
                        </td>
                        <td>
                            <input type="text" name="txtName" id="txtName" size="150" maxlength="25" runat="server"
                                class="form-control" />
                            <asp:RequiredFieldValidator ID="reqv_txtNameVi" ControlToValidate="txtName" Text="*"
                                runat="server" ValidationGroup="adminproductCategory" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <asp:Literal ID="ltrIntro" runat="server" Text="Mô tả"></asp:Literal>
                            </strong>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtIntro" TextMode="MultiLine" Rows="5" CssClass="form-control" />
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
                                <asp:TextBox runat="server" ID="txtMetaDescription" TextMode="MultiLine" Rows="5"
                                    Columns="60" placeholder="Meta Description" />
                                <asp:TextBox runat="server" ID="txtMetaKeyword" TextMode="MultiLine" Rows="5" Columns="60"
                                    placeholder="Meta Keywords" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <asp:Literal ID="ltrAdminIntro" runat="server" Text="Chi tiết"></asp:Literal>
                            </strong>
                        </td>
                        <td>
                            <uc:CKEditorControl runat="server" Language="vi" ID="editBriefVi">
                            </uc:CKEditorControl>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td>
                            <strong>
                                <asp:Literal ID="Literal11" runat="server" Text="Nhà sản xuất"></asp:Literal>
                            </strong>
                        </td>
                        <td>
                            <uc:CKEditorControl runat="server" Language="vi" ID="editPositionVi">
                            </uc:CKEditorControl>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <abbr title="Url bài viết muốn hiển thị dạng  nổi bật. Ví dụ: sach-boi-linh">
                                Url bài viết nổi bật</abbr>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="editUtilityVi" TextMode="MultiLine" placeholder="Tag Url" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <abbr title="Tên bài viết muốn hiển thị dạng nổi bật. Ví dụ: Sách bồi linh">
                                Tên bài viết nổi bật</abbr>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="editDesignVi" placeholder="Tag Name" CssClass="form-control" />
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td>
                            <strong>
                                <asp:Literal ID="Literal14" runat="server" Text="Hình ảnh"></asp:Literal>
                                :</strong>
                        </td>
                        <td>
                            <uc:CKEditorControl runat="server" Language="vi" ID="editPicturesVi">
                            </uc:CKEditorControl>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td>
                            <strong>
                                <asp:Literal ID="Literal15" runat="server" Text="Thanh toán"></asp:Literal>
                                :</strong>
                        </td>
                        <td>
                            <uc:CKEditorControl runat="server" Language="vi" ID="editPaymentVi">
                            </uc:CKEditorControl>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td>
                            <strong>
                                <asp:Literal ID="Literal16" runat="server" Text="Liên hệ"></asp:Literal>
                                :</strong>
                        </td>
                        <td>
                            <uc:CKEditorControl runat="server" Language="vi" ID="editContactVi">
                            </uc:CKEditorControl>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="tabs-2">
                <table class="adminform">
                    <tr>
                        <td>
                            <strong>
                                <asp:Literal ID="ltrAminName_En" runat="server" Text="strTitle_en"></asp:Literal></strong>
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
                                <asp:Literal ID="Literal2" runat="server" Text="Description"></asp:Literal>
                            </strong>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtIntroEn" TextMode="MultiLine" Rows="5" CssClass="form-control" />
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
                                <asp:TextBox runat="server" ID="txtMetaTitleEng" TextMode="MultiLine" Rows="5" Columns="23"
                                    placeholder="Meta Title" />
                                <asp:TextBox runat="server" ID="txtMetaDescriptionEng" TextMode="MultiLine" Rows="5"
                                    Columns="60" placeholder="Meta Description" /></div>
                            <asp:TextBox runat="server" ID="txtMetaKeywordEng" TextMode="MultiLine" Rows="5"
                                Columns="60" placeholder="Meta Keywords" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <asp:Literal ID="ltrAdminIntro_En" Text="Detail" runat="server"></asp:Literal></strong>
                        </td>
                        <td>
                            <uc:CKEditorControl runat="server" ID="editBriefEn">
                            </uc:CKEditorControl>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <asp:Literal ID="Literal22" runat="server" Text="Manufacture"></asp:Literal>
                            </strong>
                        </td>
                        <td>
                            <uc:CKEditorControl runat="server" Language="vi" ID="editPositionEn">
                            </uc:CKEditorControl>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td>
                            <strong>
                                <asp:Literal ID="Literal17" runat="server" Text="Material"></asp:Literal>
                            </strong>
                        </td>
                        <td>
                            <uc:CKEditorControl runat="server" ID="editUtilityEn">
                            </uc:CKEditorControl>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td>
                            <strong>
                                <asp:Literal ID="Literal18" runat="server" Text="Design"></asp:Literal>
                            </strong>
                        </td>
                        <td>
                            <uc:CKEditorControl runat="server" ID="editDesignEn">
                            </uc:CKEditorControl>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td>
                            <strong>
                                <asp:Literal ID="Literal19" runat="server" Text="Pictures"></asp:Literal>
                            </strong>
                        </td>
                        <td>
                            <uc:CKEditorControl runat="server" ID="editPicturesEn">
                            </uc:CKEditorControl>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td>
                            <strong>
                                <asp:Literal ID="Literal20" runat="server" Text="Payment"></asp:Literal>
                            </strong>
                        </td>
                        <td>
                            <uc:CKEditorControl runat="server" ID="editPaymentEn">
                            </uc:CKEditorControl>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td>
                            <strong>
                                <asp:Literal ID="Literal21" runat="server" Text="Contact"></asp:Literal>
                            </strong>
                        </td>
                        <td>
                            <uc:CKEditorControl runat="server" ID="editContactEn">
                            </uc:CKEditorControl>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="tabs-3">
                <dgc:block_baseimage ID="block_baseimage" runat="server" />
            </div>
            <div id="tabs-4">
                <dgc:block_uploadimage Id="block_uploadimage" runat="server" />
            </div>
        </div>
    </div>
</div>
<input type="hidden" name="task" value="" />
<input type="hidden" name="id" value="<%=productcategoryId%>" />