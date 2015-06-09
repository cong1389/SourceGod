<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="block_categoryrelate.ascx.cs"
    Inherits="Cb.Web.Controls.block_categoryrelate" %>
<%@ Register Namespace="Cb.WebControls" Assembly="Cb.WebControls" TagPrefix="cc" %>
<ul class="gallery">
    <!--Item 1-->
    <asp:Repeater runat="server" ID="rptResult" OnItemDataBound="rptResult_ItemDataBound">
        <ItemTemplate>
            <li class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                <asp:LinkButton runat="server" ID="lbnHeader1" OnCommand="lbnHeader1_Command" CssClass="nw-bigtlt"></asp:LinkButton>
                <div class="preview">
                    <a runat="server" id="hypImg">
                        <img runat="server" id="img" class="img-responsive img-thumbnail">
                        <div class="overlay">
                        </div>
                </div>
                <div class="desc">
                    <h5>
                        <h5>
                            <asp:Literal runat="server" ID="ltrBrief"></asp:Literal></h5>
                </div>
                </a></li>
        </ItemTemplate>
    </asp:Repeater>
</ul>
<!--Padding-->
<cc:Pager ID="pager" runat="server" EnableViewState="true" OnCommand="pager_Command"
    CompactModePageCount="10" MaxSmartShortCutCount="0" RTL="False" PageSize="9" />
