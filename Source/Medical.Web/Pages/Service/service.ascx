<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="service.ascx.cs" Inherits="Cb.Web.Pages.Service.service
" %>
<%@ Register Namespace="Cb.WebControls" Assembly="Cb.WebControls" TagPrefix="cc" %>
<div class="row-fluid" id="page">
    <div id="page-wrapper">
        <div id="gallery-page">
            <div class="row-fluid" id="title-global">
                <asp:Literal runat="server" ID="ltrService" Text="ltrService"></asp:Literal>
            </div>
            <div class="row-fluid" id="main-gallery">
                <asp:DataList ID="dtlstService" runat="server" RepeatColumns="3" Width="100%" OnItemDataBound="dtlstService_ItemDataBound">
                    <ItemTemplate>
                        <div class="row-fluid list-gallery">
                            <div class="span4 srv-item">
                                <div style="width: 250px;">
                                    <a runat="server" id="hypImg">
                                        <img runat="server" id="Img" /></a></div>
                                <div class='row-fluid title'>
                                    <h2>
                                        <a runat="server" id="hypTitle">
                                            <asp:Literal runat="server" ID="ltrTitle"></asp:Literal>
                                        </a>
                                    </h2>
                                </div>
                                <div class='row-fluid content'>
                                    <asp:Literal runat="server" ID="ltrIntro"></asp:Literal></div>
                            </div>
                            <!-- end .srv-item -->
                        </div>
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
        <!-- end #contact-page -->
    </div>
    <!-- end #page-wrapper -->
</div>
    <!-- end .container -->
    <cc:Pager ID="pager" runat="server" EnableViewState="true" OnCommand="pager_Command"
        CompactModePageCount="10" MaxSmartShortCutCount="0" RTL="False" PageSize="9" />
