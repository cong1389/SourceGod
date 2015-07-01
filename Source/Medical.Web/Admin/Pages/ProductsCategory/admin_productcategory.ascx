﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="admin_productcategory.ascx.cs"
    Inherits="Cb.Web.Admin.Pages.ProductsCategory.admin_productcategory" %>
<!--admin_productcategory-->
<%@ Register Namespace="Cb.WebControls" Assembly="Cb.WebControls" TagPrefix="cc" %>
<script language="javascript" type="text/javascript">
    function submitButton(task) {
        var frm = document.getElementById('aspnetForm');
        if (task == 'new' || task == 'search' || task == 'edit' || task == 'delete' || task == 'publish' || task == 'unpublish' || ('<%=msg_no_selected_item%>')) {
            switch (task) {
                case 'delete':
                    if (!confirm('<%=msg_confirm_delete_item%>')) {
                        break;
                    }
                default:
                    submitForm(frm, task);
            }
        }
    }
</script>
<table width="100%" class="menubar" cellpadding="0" cellspacing="0" border="0">
    <tr>
        <td class="menudottedline" width="40%">
        </td>
        <td class="menudottedline" align="right">
            <table id="toolbar">
                <tr>
                    <td>
                        <a class="btn btn-info btn-xs" href="javascript:submitButton('publish');"><i class="fa fa-check-circle-o fa-2x">
                        </i>
                            <asp:Literal ID="ltrAdminPublish" runat="server" Text="strAdminPublish"></asp:Literal></a>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <a class="btn btn-warning btn-xs" href="javascript:submitButton('unpublish');"><i
                            class="fa fa-times-circle-o fa-2x "></i>
                            <asp:Literal ID="ltrAminUnpublish" runat="server" Text="str_admin_unpublish"></asp:Literal></a>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td runat="server" id="tdNew">
                        <a class="btn btn-success btn-xs" href="javascript:submitButton('new');"><i class="fa fa-search-plus fa-2x">
                        </i>
                            <asp:Literal ID="ltrAdminAddNew" runat="server" Text="Thêm mới"></asp:Literal>
                        </a>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <button runat="server" id="btnSave" class="btn btn-success btn-xs" title="Lưu" onserverclick="btnSave_Click">
                            <i class="fa fa-floppy-o fa-2x"></i>&nbsp Lưu
                        </button>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td runat="server" id="tdEdit">
                        <a class="btn btn-success btn-xs" href="javascript:submitButton('edit');"><i class="fa fa-pencil fa-2x">
                        </i>&nbsp
                            <asp:Literal ID="ltrAdminEdit" runat="server" Text="Chỉnh sửa"></asp:Literal></a>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td runat="server" id="tdDelete">
                        <a class="btn btn-danger btn-xs" href="javascript:submitButton('delete');"><i class="fa fa-trash-o fa-lg fa-2x">
                        </i>
                            <asp:Literal ID="ltrAdminDelete" runat="server" Text="Xóa"></asp:Literal></a>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<br />
<!-- BEGIN show_msg -->
<%=show_msg%>
<!-- END show_msg -->
<div align="center" class="centermain">
    <div class="main">
        <table class="adminheading">
            <tr>
                <th class="user">
                    <asp:Literal ID="ltrAdminHeaderName" runat="server" Text="Quản lý danh mục"></asp:Literal>
                </th>
                <td>
                </td>
                <td>
                    <input type="text" id="search" name="search" class="form-control" onkeypress="return checkEnter(event)"
                        runat="server" placeholder="Tìm kiếm" />
                </td>
                <td width="right">
                </td>
            </tr>
        </table>
        <table class="table table-bordered table-hover">
            <tr class="success">
                <th width="2%">
                    #
                </th>
                <th width="3%">
                    <input class="txt" type="checkbox" name="checkedAll" onclick="checkAll(<%=records%>);" />
                </th>
                <th>
                    <asp:Literal ID="ltrAdminHeaderProductCategory" runat="server" Text="Tên danh mục"></asp:Literal>
                </th>
                <th width="10%">
                    <asp:Literal ID="ltrAdminHeaderOrder" runat="server" Text="strOrdering"></asp:Literal>
                </th>
                <th width="15%">
                    <asp:Literal ID="ltrAdminHeaderDate" runat="server" Text="strUpdateDate"></asp:Literal>
                </th>
                <th width="1%" nowrap="nowrap">
                    <asp:Literal ID="ltrAdminHeaderPublic" runat="server" Text="strAdminPublish"></asp:Literal>
                </th>
            </tr>
            <!-- BEGIN list -->
            <asp:Repeater ID="rptResult" runat="server" OnItemDataBound="rptResult_ItemDataBound">
                <ItemTemplate>
                    <tr id="trList" runat="server">
                        <td>
                            <input type="button" id="btId" style="display: none" runat="server" />
                        </td>
                        <td>
                            <asp:Literal ID="ltrchk" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <asp:HyperLink ID="hdflink" runat="server">
                                <asp:Literal ID="ltrName" runat="server"></asp:Literal>
                            </asp:HyperLink>
                        </td>
                        <td>
                            <input id="txtOrder" onkeypress="return checkNumericKeyInfo(event.keyCode, event.which);"
                                style="text-align: center;" maxlength="4" size="3" runat="server" />
                        </td>
                        <td id="trUpdateDate" runat="server">
                            <%# Eval("UpdateDate")%>
                        </td>
                        <td align="center" id="tdbtn" runat="server">
                            <asp:ImageButton CssClass="toolbar" ID="btnPublish" runat="server" Width="12" Height="12"
                                ValidationGroup="admincontent" AlternateText="Publish" title="Publish" ImageUrl="~/admdgc/images/write_f2.png" />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <table>
            <tr>
                <th colspan="2">
                    <!-- BEGIN paging -->
                    <div style="padding: 3px;" align="center">
                        <cc:Pager ID="pager" runat="server" EnableViewState="true" OnCommand="pager_Command"
                            CompactModePageCount="10" MaxSmartShortCutCount="0" RTL="False" />
                    </div>
                    <!-- END paging -->
                </th>
            </tr>
        </table>
        <input type="hidden" name="boxchecked" value="0" />
        <input type="hidden" name="task" value="" />
    </div>
</div>
