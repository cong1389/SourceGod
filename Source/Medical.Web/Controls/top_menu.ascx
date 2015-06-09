<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="top_menu.ascx.cs" Inherits="Cb.Web.Controls.top_menu" %>
<!--block_topmenu-->
<header>
    <div class="navbar navbar-default navbar-fixed-top scroll-me ">
        <!-- pass scroll-me class above a tags to starts scrolling -->
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar">
                    </span>
                </button>
                <a class="navbar-brand" runat="server" id="hypImgHomePage">
                    <img runat="server" id="imgLogo" src="/Images/logo.png" alt="" />
                </a>
            </div>
            <div class="navbar-collapse collapse">
                <ul class="nav navbar-nav navbar-right">
                    <asp:Repeater runat="server" ID="rptResult" OnItemDataBound="rptResult_ItemDataBound">
                        <ItemTemplate>
                            <li class="menu-item"><a runat="server" id="hypName">
                                <h4>
                                  <asp:Literal runat="server" ID="ltrIconHome"></asp:Literal>
                                    <%--<span class="icon-homeChurch"></span>--%>
                                    <asp:Literal runat="server" ID="ltrName"></asp:Literal>
                                    <asp:Literal runat="server" ID="ltrIconSub"></asp:Literal></h4>
                            </a>
                                <!--Sub Menu Level 2-->
                                <ul class="dropdown-menu nav navbar-nav">
                                    <asp:Repeater runat="server" ID="rptResultSub2" OnItemDataBound="rptResultSub2_ItemDataBound">
                                        <ItemTemplate>
                                            <li class="menu-item dropdown "><a class="dropdown-toggle" runat="server" id="hypNameSub2">
                                                <h4>
                                                    <asp:Literal runat="server" ID="ltrNameSub2"></asp:Literal></h4>
                                            </a></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
    </div>
</header>
<asp:HiddenField runat="server" ID="hddParentNameUrl" />
