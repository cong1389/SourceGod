<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="admin_config.ascx.cs"
    Inherits="Cb.Web.Admin.Pages.Config.admin_config" %>
<script language="javascript" type="text/javascript">

    $(function () {
        $("#tabs").tabs();
        $("a.zoom-image").fancybox();
    });
</script>
<table width="100%" class="menubar" cellpadding="0" cellspacing="0" border="0">
    <tr>
        <td class="menudottedline" align="right">
            <table cellpadding="0" cellspacing="0" border="0" id="toolbar">
                <tr valign="middle" align="center">
                    <td>
                        <a class="toolbar">
                            <asp:ImageButton CssClass="toolbar" ValidationGroup="adminconfig" ID="btn_Save" runat="server"
                                AlternateText="Save" name="Save" title="Save" ImageUrl="/admin/images/save_f2.png"
                                OnClick="btn_Save_Click" />
                            <br />
                            <asp:Literal ID="ltrAdminSave" runat="server" Text="strSave"></asp:Literal></a>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <%--<td>
                        <a class="toolbar">
                            <asp:ImageButton CssClass="toolbar" ID="btn_Cancel" runat="server" CausesValidation="false"
                                AlternateText="Cancel" name="Cancel" title="Cancel" ImageUrl="~/admdgc/images/cancel_f2.png"
                                OnClick="btn_Cancel_Click" />
                            <br />
                            <asp:Literal ID="ltrAdminCancel" runat="server"></asp:Literal></a>
                    </td>--%>
                </tr>
            </table>
        </td>
    </tr>
</table>
<div class="centermain">
    <div class="main">
        <table cellpadding="1" cellspacing="1" border="0" width="100%">
            <tr>
                <td width="250">
                    <table class="adminheading">
                        <tr>
                            <th nowrap="nowrap" class="config">
                                <span class="glyphicon glyphicon-cog" />
                                <asp:Literal ID="ltrHeader" runat="server" Text="strConfig"></asp:Literal>
                            </th>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div id="tabs" class="tab-page">
            <ul>
                <li><a href="#tabs-1">
                    <asp:Literal ID="ltrSite" runat="server" Text="Tiếng Việt"></asp:Literal></a></li>
                <li><a href="#tabs-2">
                    <asp:Literal ID="ltrMetaTag" runat="server" Text="SEO Description"></asp:Literal></a></li>
                <li runat="server" id="tabWebconfig"><a href="#tabs-3">
                    <asp:Literal ID="ltrCo" runat="server" Text="Web.config"></asp:Literal></a></li>
            </ul>
            <div id="tabs-1">
                <table class="adminform">
                    <tr>
                        <td>
                            <strong>
                                <asp:Literal ID="ltrSiteName" runat="server" Text="strSiteName"></asp:Literal></strong>
                        </td>
                        <td>
                            <input type="text" id="txt_config_sitename" runat="server" class="form-control" maxlength="50"
                                size="30" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <asp:Literal ID="ltrPne" runat="server" Text="company"></asp:Literal></strong>
                        </td>
                        <td>
                            <input type="text" id="txt_config_phone" runat="server" class="form-control" maxlength="50"
                                size="30" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <asp:Literal ID="Literal1" runat="server" Text="strFax"></asp:Literal></strong>
                        </td>
                        <td>
                            <input type="text" id="txtFax" runat="server" class="form-control" maxlength="50"
                                size="30" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <asp:Literal ID="ltrEmail" runat="server" Text="ltrEmail"></asp:Literal></strong>
                        </td>
                        <td>
                            <input type="text" id="txt_config_email" runat="server" class="form-control" maxlength="50"
                                size="30" />
                            <asp:RegularExpressionValidator ID="regv_Email" ControlToValidate="txt_config_email"
                                runat="server" Text="*" ValidationExpression="" ValidationGroup="adminconfig"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <asp:Literal ID="ltrPhone_Support" runat="server" Text="enuChat_yahoo"></asp:Literal></strong>
                        </td>
                        <td>
                            <input type="text" id="txtYahoo" runat="server" class="form-control" maxlength="50"
                                size="30" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <asp:Literal ID="ltrPhone_Consultan" runat="server" Text="enuChat_skype"></asp:Literal>:</strong>
                        </td>
                        <td>
                            <input type="text" id="txtSkype" runat="server" class="form-control" maxlength="50"
                                size="30" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <asp:Literal ID="Literal2" runat="server" Text="ltrCompanyName"></asp:Literal>:</strong>
                        </td>
                        <td>
                            <input type="text" id="txtCompanyName" runat="server" class="form-control" maxlength="50"
                                size="60" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <asp:Literal ID="Literal3" runat="server" Text="ltrFooterAddress"></asp:Literal>:</strong>
                        </td>
                        <td>
                            <input type="text" id="txtAddress" runat="server" class="form-control" maxlength="50"
                                size="60" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <asp:Literal ID="Literal6" runat="server" Text="ltrFooterAddress1"></asp:Literal>:</strong>
                        </td>
                        <td>
                            <input type="text" id="txtAddress1" runat="server" class="form-control" maxlength="50"
                                size="60" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <asp:Literal ID="Literal7" runat="server" Text="Logo Header"></asp:Literal>
                                :</strong>
                        </td>
                        <td>
                            <asp:FileUpload ID="fuImageHeader" runat="server" EnableViewState="true" />
                            <asp:Button ID="btnUploadImageHeader" runat="server" Text="strUpload" OnClick="btnUploadImageHeader_Click" />
                            <asp:LinkButton ID="lbnViewHeader" runat="server" Text="strView" Visible="false"
                                CssClass="zoom-image"><span class="glyphicon glyphicon-fullscreen btn-sm">
                        </span></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lbnDeleteHeader" runat="server" Text="strDelete" Visible="false"
                                OnClick="lbnDeleteImageHeader_Click"><span class="glyphicon glyphicon-trash btn-sm">
                        </span></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <asp:Literal ID="Literal4" runat="server" Text="Logo Footer"></asp:Literal>
                                :</strong>
                        </td>
                        <td>
                            <asp:FileUpload ID="fuImageFooter" runat="server" EnableViewState="true" />
                            <asp:Button ID="btnUploadImageFooter" runat="server" Text="strUpload" OnClick="btnUploadImageFooter_Click" />
                            <asp:LinkButton ID="lbnViewFooter" runat="server" Text="strView" Visible="false"
                                CssClass="zoom-image"><span class="glyphicon glyphicon-fullscreen btn-sm">
                        </span></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lbnDeleteFooter" runat="server" Text="strDelete" Visible="false"
                                OnClick="lbnDeleteImageFooter_Click"><span class="glyphicon glyphicon-trash btn-sm">
                        </span></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <asp:Literal ID="Literal5" runat="server" Text="Sơ đồ đường đi"></asp:Literal>
                                :</strong>
                        </td>
                        <td>
                            <asp:FileUpload ID="fuLocation" runat="server" EnableViewState="true" CssClass="btn btn-info btn-xs" />
                            <asp:Button ID="btnUploadLocation" runat="server" Text="strUpload" OnClick="btnUploadLocation_Click"
                                CssClass="btn btn-info btn-xs" />
                            <asp:LinkButton ID="lbnViewLocation" runat="server" Text="strView" Visible="false"
                                CssClass="zoom-image"><span class="glyphicon glyphicon-fullscreen btn-sm">
                        </span></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lbnDeleteLocation" runat="server" Text="strDelete" Visible="false"
                                OnClick="lbnDeleteLocation_Click"><span class="glyphicon glyphicon-trash btn-sm">
                        </span></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="tabs-2">
                <table class="adminform">
                    <tr>
                        <td>
                            <strong>
                                <asp:Literal ID="Literal8" runat="server" Text="Title"> </asp:Literal></strong>
                        </td>
                        <td>
                            <textarea id="txtTitle" runat="server" class="form-control"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <asp:Literal ID="ltrMetaDesc" runat="server" Text="Meta Description"> </asp:Literal></strong>
                        </td>
                        <td>
                            <textarea id="txtMetaDescription" runat="server" class="form-control"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <asp:Literal ID="ltrMetaKey" runat="server" Text="Meta Keyword"></asp:Literal></strong>
                        </td>
                        <td>
                            <textarea id="txtMetaKeyword" runat="server" class="form-control"></textarea>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="tabs-3">
                <table class="adminform">
                    <tr>
                        <td>
                            <strong>
                                <asp:Literal ID="Literal9" runat="server" Text="Key"> </asp:Literal></strong>
                        </td>
                        <td>
                            <input type="text" id="txtWebConfigKey" name="search" class="form-control inputSearch"
                                runat="server" placeholder="Nhập key" />
                            <button id="btnGetValue" runat="server" class="btn btn-default  " onserverclick="btnGetValue_Click"
                                style="height: 34px !important" title="Find">
                                <i class="glyphicon glyphicon-search"></i>
                            </button>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <asp:Literal ID="Literal10" runat="server" Text="Value"> </asp:Literal></strong>
                        </td>
                        <td>
                            <input type="text" id="txtWebConfigValue" class="form-control inputSearch" runat="server"
                                placeholder="Value By Key" />
                            <button id="btnSetValue" runat="server" class="btn btn-default  " onserverclick="btnSetValue_Click"
                                style="height: 34px !important" title="Save">
                                <i class="glyphicon glyphicon-floppy-save"></i>s
                            </button>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <asp:Literal ID="Literal11" runat="server" Text="Web.config"></asp:Literal></strong>
                        </td>
                        <td>
                            <textarea id="txtWebConfig" runat="server" class="form-control" rows="50"></textarea>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</div>
<input type="hidden" name="task" value="" />
