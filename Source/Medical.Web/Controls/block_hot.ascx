<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="block_hot.ascx.cs" Inherits="Cb.Web.Controls.block_hot" %>
<!--block_hot-->
<div class="widget ads">
    <h3>
        <asp:Literal runat="server" ID="ltrTilte"></asp:Literal>
    </h3>
    <div class="row-fluid">
        <asp:Repeater runat="server" ID="rptResult" OnItemDataBound="rptResult_ItemDataBound">
            <ItemTemplate>
                <div class="span6">
                    <asp:LinkButton runat="server" ID="lbnHeader1" OnCommand="lbnHeader1_Command" CssClass="nw-bigtlt"></asp:LinkButton>
                    <a runat="server" id="hypImg">
                        <img alt=" " runat="server" id="img" class="img-responsive img-thumbnail" /></a>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
