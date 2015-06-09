<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="admin_slider.ascx.cs"
    Inherits="Cb.Web.Admin.Pages.Slider.admin_slider" %>
<%@ Register Namespace="Cb.WebControls" Assembly="Cb.WebControls" TagPrefix="cc" %>
<script language="javascript" type="text/javascript">
    function submitButton(task) {
        var frm = document.getElementById('aspnetForm');
        //alert(task);
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
            <div class="pathway">
            </div>
        </td>
        <td class="menudottedline" align="right">
            <table cellpadding="0" cellspacing="0" border="0" id="toolbar">
                <tr valign="middle" align="center">
                    <td>
                        <a class="btn btn-info btn-xs" href="javascript:submitButton('publish');"><span class="glyphicon glyphicon-ok-circle btn-lg">
                        </span>
                            <asp:Literal ID="ltrAdminPublish" runat="server" Text="strAdminPublish"></asp:Literal></a>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <a class="btn btn-warning btn-xs" href="javascript:submitButton('unpublish');"><span
                            class="glyphicon glyphicon-remove-circle btn-lg"></span>
                            <asp:Literal ID="ltrAminUnpublish" runat="server" Text="str_admin_unpublish"></asp:Literal></a>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <a class="btn btn-success btn-xs" href="javascript:submitButton('new');"><span class="glyphicon glyphicon-new-window btn-lg">
                        </span>
                            <asp:Literal ID="ltrAdminAddNew" runat="server" Text="strAdminAddNew"></asp:Literal>
                        </a>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <a class="btn btn-primary btn-xs"><span class="glyphicon glyphicon-floppy-disk btn-lg">
                        </span>
                            <asp:Literal ID="ltrAdminSave" runat="server" Text="strSave"></asp:Literal>
                        </a>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td runat="server" id="tdEdit">
                        <a class="btn btn-success btn-xs" href="javascript:submitButton('edit');"><span class="glyphicon glyphicon-edit btn-lg">
                        </span>
                            <asp:Literal ID="ltrAdminEdit" runat="server" Text="strEdit"></asp:Literal></a>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td runat="server" id="tdDelete">
                        <a class="btn btn-danger btn-xs" href="javascript:submitButton('delete');"><span
                            class="glyphicon glyphicon-remove btn-lg"></span>
                            <asp:Literal ID="ltrAdminDelete" runat="server" Text="strDelete"></asp:Literal></a>
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
        <form action="{action}" method="post" name="aspnetForm">
        <table class="adminheading">
            <tr>
                <th class="user">
                    <asp:Literal ID="ltrAdminHeaderName" runat="server" Text="Quản lý slider"></asp:Literal>
                </th>
                <td nowrap="nowrap">
                    &nbsp;
                    <%--<asp:Literal ID="ltrAdminSearch" runat="server"></asp:Literal>--%>
                </td>
                <td nowrap="nowrap">
                    <asp:Literal runat="server" ID="ltrPosition" Text="strPosition"></asp:Literal>:
                </td>
                <td>
                    <asp:DropDownList ID="drpPosition" runat="server" class="form-control">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <input type="text" id="search" name="search" class="form-control" runat="server"
                        onkeypress="return checkEnter(event)" placeholder="Tìm kiếm" />
                </td>
                <td>
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
                    <asp:Literal ID="ltrAdminHeaderProductCategory" runat="server" Text="strName"></asp:Literal>
                </th>
                <th>
                    <asp:Literal ID="Literal1" runat="server" Text="strPosition"></asp:Literal>
                </th>
                <th>
                    <asp:Literal ID="ltrAdminHeaderOrder" runat="server" Text="strOrdering"></asp:Literal>
                </th>
                <th>
                    <asp:Literal ID="Literal2" runat="server" Text="Hình ảnh"></asp:Literal>
                </th>
                <th width="150" nowrap="nowrap">
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
                            <asp:Literal ID="ltrPosition" runat="server" />
                        </td>
                        <td>
                            <input id="txtOrder" onkeypress="return checkNumericKeyInfo(event.keyCode, event.which);"
                                style="text-align: center;" maxlength="4" size="3" runat="server" />
                        </td>
                        <td>
                            <cc:DGCBannerControl ID="ucBanner" runat="server" Width="155px" Height="80px" />
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
        </form>
    </div>
</div>
