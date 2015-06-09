<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="block_tagcloud.ascx.cs"
    Inherits="Cb.Web.Controls.CategoryManagement.block_tagcloud" %>
<!--block_tagcloud-->
<div class="widget">
    <h3>
        <asp:Literal runat="server" ID="ltrTilte"></asp:Literal>
    </h3>
    <ul class="tag-cloud unstyled">
        <asp:Repeater runat="server" ID="rptResult" OnItemDataBound="rptResult_ItemDataBound">
            <ItemTemplate>
                <li><a class="btn btn-mini btn-primary" runat="server" id="hypItem">
                    <asp:Literal runat="server" ID="ltrItem"></asp:Literal></a></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
<!-- /block_tagcloud -->
