<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="block_archive.ascx.cs"
    Inherits="Cb.Web.Controls.block_archive" %>
<!--block_archive-->
<div class="widget">
    <h3>
        Dòng thời gian</h3>
    <ul class="archive arrow">
        <asp:Repeater runat="server" ID="rptResult" OnItemDataBound="rptResult_ItemDataBound">
            <ItemTemplate>
                <li><a runat="server" id="hypItem">
                    <asp:Literal runat="server" ID="ltrItem"></asp:Literal>
                </a></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
