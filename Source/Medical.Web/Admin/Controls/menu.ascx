<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="menu.ascx.cs" Inherits="Cb.Web.Admin.Controls.menu" %>
<table width="100%" cellpadding="0" cellspacing="0" border="0">
    <tbody>
        <tr class="menubackgr">
            <td>
                <div id="main-nav">
                </div>
            </td>
        </tr>
    </tbody>
</table>
<script language="JavaScript" type="text/javascript">
    var myMenu =
	[
		['<span class="glyphicon glyphicon-th-large" />', 'Hệ thống', null, null, 'Hệ thống',
		    ['<span class="glyphicon glyphicon-home" />', 'Trang chủ', '<%=link_menu_outside%>', null, 'Trang chủ'],
            ['<span class="glyphicon glyphicon-cog" />', 'Cấu hình', '<%=link_menu_config%>', null, 'Cấu hình'],
            ['<span class="glyphicon glyphicon-home" />', 'Quản lý User', '<%=link_menu_user_manager%>', null, 'Quản lý User'],			
			['<span class="glyphicon glyphicon-off" />', '<%=l_menu_logout%>', '<%=link_menu_logout%>', null, '<%=l_menu_logout%>'],
		],
		_cmSplit,
		['<span class="glyphicon glyphicon-road" />', '<%=l_menu_tool%>', null, null, '<%=l_menu_tool%>',
            ['<span class="glyphicon glyphicon-th-large"/>', 'Danh mục sản phẩm', '<%=link_menu_tool_productcategory %>', null, 'Danh mục sản phẩm'],
            ['<span class="glyphicon glyphicon-th"/>', 'Sản phẩm', '<%=link_menu_tool_product %>', null, 'Sản phẩm'],
            ['<span class="glyphicon glyphicon-retweet"/>', 'Slider', '<%=link_menu_tool_slider%>', null, 'Slider'],
            ['<span class="glyphicon glyphicon-retweet"/>', 'Dịch vụ', '<%=link_menu_tool_services %>', null, 'Dịch vụ'],



         ],

	];
    cmDraw('main-nav', myMenu, 'hbr', cmThemeOffice, 'ThemeOffice');
</script>
