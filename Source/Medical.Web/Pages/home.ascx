<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="home.ascx.cs" Inherits="Cb.Web.Pages.home" %>
<%@ Register TagPrefix="dgc" TagName="block_silder" Src="~/Controls/block_silder.ascx" %>
<%@ Register TagPrefix="dgc" TagName="block_slogan" Src="~/Controls/block_slogan.ascx" %>
<%@ Register TagPrefix="dgc" TagName="block_translatedbook" Src="~/Controls/block_translatedbook.ascx" %>
<%@ Register TagPrefix="dgc" TagName="block_lecture" Src="~/Controls/block_lecture.ascx" %>
<%@ Register TagPrefix="dgc" TagName="block_meditate" Src="~/Controls/block_meditate.ascx" %>
<%--<%@ Register TagPrefix="dgc" TagName="block_bottom" Src="~/Controls/block_bottom.ascx" %>--%>
<dgc:block_silder ID="block_silder" runat="server" />
<dgc:block_slogan ID="block_slogan" runat="server" />
<dgc:block_lecture ID="block_lecture" runat="server" />
<dgc:block_translatedbook ID="block_translatedbook" runat="server" />
<dgc:block_meditate ID="block_meditate" runat="server" />
<%--<dgc:block_bottom ID="block_bottom" runat="server" />--%>
