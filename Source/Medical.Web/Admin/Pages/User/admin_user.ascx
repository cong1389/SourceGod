<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="admin_user.ascx.cs"
    Inherits="Cb.Web.Admin.Pages.User.admin_user" %>
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
    function search_keypress(e) {
        var key;
        if (window.event)
            key = window.event.keyCode;     //IE
        else
            key = e.which;     //firefox
        //alert(key);
        if (key == 13) {
            document.getElementById('<%=btnSearch.ClientID%>').click();
        }
        else
            return true;

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
<%--<%=show_msg%>--%>
<!-- END show_msg -->
<div align="center" class="centermain">
    <div class="main">
        <table class="adminheading">
            <tr>
                <th class="user">
                    <asp:Literal ID="ltrAdminHeaderName" runat="server" Text="strManageUser"></asp:Literal>
                </th>
                <td nowrap="nowrap">
                    &nbsp;
                    <%--<asp:Literal ID="ltrAdminSearch" runat="server"></asp:Literal>--%>
                </td>
                <%--<td nowrap="nowrap">
                    <asp:Literal runat="server" ID="ltrNewsCategory"></asp:Literal>:
                </td>
                <td>
                    <asp:DropDownList ID="drpNewsCategory" runat="server">
                    </asp:DropDownList>
                </td>--%>
                <td>
                    <%--<asp:Literal ID="ltrName" runat="server" Text="strName"></asp:Literal>:--%>
                </td>
                <td style="padding-right: 10px;">
                    <input type="text" id="search" name="search" class="form-control" runat="server"
                        onkeypress="return search_keypress(event);" placeholder="Tìm kiếm" />
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success btn-sm" OnClick="btnSearch_Click"
                        Text="strFind" />
                </td>
                <td>
                </td>
            </tr>
        </table>
        <table class="table table-bordered table-hover">
            <tr>
                <th width="2%" class="title">
                    #
                </th>
                <th width="3%" class="title">
                    <input class="txt" type="checkbox" name="checkedAll" onclick="checkAll(<%=records%>);" />
                </th>
                <th class="title">
                    <asp:Literal ID="ltrAdminHeaderProductCategory" runat="server" Text="statement_username"></asp:Literal>
                </th>
                <th class="title">
                    <asp:Literal ID="Literal1" runat="server" Text="strName"></asp:Literal>
                </th>
                <th class="title">
                    <asp:Literal ID="Literal2" runat="server" Text="strEmail"></asp:Literal>
                </th>
                <th class="title">
                    <asp:Literal ID="Literal3" runat="server" Text="strRoleName"></asp:Literal>
                </th>
                <th class="title">
                    <asp:Literal ID="Literal4" runat="server" Text="statement_location"></asp:Literal>
                </th>
                <th class="title">
                    <asp:Literal ID="Literal5" runat="server" Text="ltrFooterAddress"></asp:Literal>
                </th>
                <th class="title">
                    <asp:Literal ID="Literal6" runat="server" Text="ltrFooterTelCompany"></asp:Literal>
                </th>
                <th width="120" class="title" nowrap="nowrap">
                    <asp:Literal ID="ltrAdminHeaderDate" runat="server" Text="strUpdateDate"></asp:Literal>
                </th>
                <th width="1%" nowrap="nowrap" class="title">
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
                        <td id="tdName" runat="server">
                            <a href="#">
                                <asp:Literal ID="ltrUserName" runat="server"></asp:Literal></a>
                        </td>
                        <td id="td1" runat="server">
                            <asp:Literal ID="ltrFullName" runat="server"></asp:Literal>
                        </td>
                        <td id="td2" runat="server">
                            <asp:Literal ID="ltrEmail" runat="server"></asp:Literal>
                        </td>
                        <td id="td3" runat="server">
                            <asp:Literal ID="ltrRole" runat="server"></asp:Literal>
                        </td>
                        <td id="td4" runat="server">
                            <asp:Literal ID="ltrLocation" runat="server"></asp:Literal>
                        </td>
                        <td id="td5" runat="server">
                            <asp:Literal ID="ltrAddress" runat="server"></asp:Literal>
                        </td>
                        <td id="td6" runat="server">
                            <asp:Literal ID="ltrPhone" runat="server"></asp:Literal>
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
