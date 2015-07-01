<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="menu.ascx.cs" Inherits="Cb.Web.Admin.Controls.menu" %>
<table width="100%">
    <tbody>
        <tr class="menubackgr">
            <td>
                <div id="main-nav">
                </div>
            </td>
            <td>
                <div class="divUserName">
                    <span><i class="fa fa-user"></i>
                        <asp:Literal runat="server" ID="ltrUserName"></asp:Literal></span> <a runat="server"
                            id="hypLogOut"><i class="fa fa-power-off"></i></a>
                </div>
            </td>
        </tr>
    </tbody>
</table>
<script language="JavaScript" type="text/javascript">
    var myMenu =
	[
		['<i class="fa fa-th"></i>', 'Hệ thống', null, null, 'Hệ thống',
		    ['<i class="fa fa-home"></i>', 'Trang chủ', '<%=linkMenuHomePage%>', null, 'Trang chủ'],
            ['<i class="fa fa-cog"></i>', 'Cấu hình', '<%=link_menu_config%>', null, 'Cấu hình'],
            ['<i class="fa fa-users"></i>', 'Quản lý User', '<%=link_menu_user_manager%>', null, 'Quản lý User'],
		],
		_cmSplit,
		['<i class="fa fa-list-alt"></i>', 'Công cụ', null, null, 'Công cụ',
            ['<i class="fa fa-pencil"></i>', 'Danh mục sản phẩm', '<%=link_menu_tool_productcategory %>', null, 'Danh mục sản phẩm'],
            ['<i class="fa fa-pencil"></i>', 'Sản phẩm', '<%=link_menu_tool_product %>', null, 'Sản phẩm'],
            ['<i class="fa fa-pencil"></i>', 'Slider', '<%=link_menu_tool_slider%>', null, 'Slider'],
         ],

	];
    cmDraw('main-nav', myMenu, 'hbr', cmThemeOffice, 'ThemeOffice');
</script>
