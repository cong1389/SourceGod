<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="admin_product.ascx.cs"
    Inherits="Cb.Web.Admin.Pages.Products.admin_product" %>
<!--admin_product-->
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
                        <button runat="server" id="btnSave" class="btn btn-success btn-xs" title="Lưu" onserverclick="btnSave_Click">
                            <span class="glyphicon glyphicon-floppy-disk btn-lg"></span>Lưu
                        </button>
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
                <th style="width: 150px">
                    <asp:Literal ID="strHeaderProduct" runat="server" Text="strHeaderProduct"></asp:Literal>
                </th>
                <td style="width: 150px">
                    <asp:DropDownList ID="drpNewsCategory" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </td>
                <td style="width: 10px">
                    &nbsp;
                </td>
                <td class="col-lg-4 pull-right">
                    <div style="display: table">
                        <input type="text" id="search" name="search" class="form-control" runat="server"
                            onkeypress="return search_keypress(event);" placeholder="Tìm kiếm" />
                        <div class="input-group-btn">
                            <button id="btnSearch" runat="server" class="btn btn-default " onserverclick="btnSearch_Click"
                                style="height: 34px !important">
                                <i class="glyphicon glyphicon-search"></i>
                            </button>
                        </div>
                    </div>
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
                    <asp:Literal ID="ltrAdminHeaderProductCategory" runat="server" Text="Tên sản phẩm"></asp:Literal>
                </th>
                <th>
                    <asp:Literal ID="ltrListServices" runat="server" Text="Danh mục cha"></asp:Literal>
                </th>
                <th width="10%">
                    <asp:Literal ID="ltrAdminHeaderOrder" runat="server" Text="strOrdering"></asp:Literal>
                </th>
                <th width="15%" nowrap="nowrap">
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
