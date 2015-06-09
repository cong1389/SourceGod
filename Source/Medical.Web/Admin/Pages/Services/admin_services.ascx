<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="admin_services.ascx.cs"
    Inherits="Cb.Web.Admin.Pages.Services.admin_services" %>
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
                        <a class="toolbar" href="javascript:submitButton('publish');">
                            <img src="<%=template_path%>/Images/publish_f2.png" alt="Publish" name="publish"
                                title="Publish" align="middle" border="0" /><br />
                            <asp:Literal ID="ltrAdminPublish" runat="server" Text="strAdminPublish"></asp:Literal></a>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <a class="toolbar" href="javascript:submitButton('unpublish');">
                            <img src="<%=template_path%>/images/unpublish_f2.png" alt="Unpublish" name="unpublish"
                                title="unpublish" align="middle" border="0" /><br />
                            <asp:Literal ID="ltrAminUnpublish" runat="server" Text="str_admin_unpublish"></asp:Literal></a>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <a class="toolbar" href="javascript:submitButton('new');">
                            <img src="<%=template_path%>/images/new_f2.png" alt="New" name="new" title="New"
                                align="middle" border="0" /><br />
                            <asp:Literal ID="ltrAdminAddNew" runat="server" Text="strAdminAddNew"></asp:Literal></a>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <a class="toolbar">
                            <asp:ImageButton CssClass="toolbar" ID="btn_Save" runat="server" AlternateText="Save"
                                name="Save" title="Save" ImageUrl="/admin/images/save_f2.png" OnClick="btn_Save_Click" />
                            <br />
                            <asp:Literal ID="ltrAdminSave" runat="server" Text="strSave"></asp:Literal>
                        </a>
                        <%--<a class="toolbar" href="javascript:submietButton('save')">
                            <img src="<%= template_path %>/images/save_f2.png" alt="Save" title="Save" align="middle"
                                border="0" /><br />
                        </a>--%>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <a class="toolbar" href="javascript:submitButton('edit');">
                            <img src="<%=template_path%>/images/edit_f2.png" alt="Edit" name="edit" title="Edit"
                                align="middle" border="0" /><br />
                            <asp:Literal ID="ltrAdminEdit" runat="server" Text="strEdit"></asp:Literal></a>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <a class="toolbar" href="javascript:submitButton('delete');">
                            <img src="<%=template_path%>/images/delete_f2.png" alt="Delete" name="delete" title="Delete"
                                align="middle" border="0" /><br />
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
                <th class="config">
                    <asp:Literal ID="ltrAdminHeaderName" runat="server" Text="strHeaderServices"></asp:Literal>
                </th>
                <td nowrap="nowrap">
                    &nbsp;
                    <%--<asp:Literal ID="ltrAdminSearch" runat="server"></asp:Literal>--%>
                </td>
                <td nowrap="nowrap">
                    <asp:Literal runat="server" ID="ltrNewsCategory" Text="strServicesCategory"></asp:Literal>:
                </td>
                <td>
                    <asp:DropDownList ID="drpNewsCategory" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Literal ID="ltrName" runat="server" Text="strName"></asp:Literal>:
                </td>
                <td>
                    <input type="text" id="search" name="search" class="inputbox" runat="server" onkeypress="return search_keypress(event);" />
                    <%--<asp:TextBox ID="search" runat="server" MaxLength="100" Width="200px"></asp:TextBox>--%>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" CssClass="imgButton" OnClick="btnSearch_Click"
                        Text="strSearch" />
                </td>
                <td width="right">
                </td>
            </tr>
        </table>
        <table class="adminlist">
            <tr>
                <th width="2%" class="title">
                    #
                </th>
                <th width="3%" class="title">
                    <input class="txt" type="checkbox" name="checkedAll" onclick="checkAll(<%=records%>);" />
                </th>
                <th class="title">
                    <asp:Literal ID="ltrAdminHeaderProductCategory" runat="server" Text="strName"></asp:Literal>
                </th>
                <th class="title">
                    <asp:Literal ID="ltrListServices" runat="server" Text="ltrListServices"></asp:Literal>
                </th>
                <th class="title">
                    <asp:Literal ID="ltrAdminHeaderOrder" runat="server" Text="strOrdering"></asp:Literal>
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
                        <td>
                             <asp:HyperLink ID="hdflink" runat="server">
                                <asp:Literal ID="ltrName" runat="server"></asp:Literal>
                            </asp:HyperLink>
                        </td>
                        <td>
                            <asp:Literal ID="ltrNewsCategory" runat="server"></asp:Literal>
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
        <table class="adminlist">
            <tr>
                <th colspan="2">
                    &nbsp;
                </th>
            </tr>
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
