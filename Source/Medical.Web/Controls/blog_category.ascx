<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="blog_category.ascx.cs"
    Inherits="Cb.Web.Controls.CategoryManagement.blog_category" %>
<!--blog_category-->
<%@ Register Namespace="Cb.WebControls" Assembly="Cb.WebControls" TagPrefix="cc" %>
<ul class="gallery col-4">
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
<div class="clearfix">
</div>
<!--Padding-->
<cc:Pager ID="pager" runat="server" EnableViewState="true" OnCommand="pager_Command"
    CompactModePageCount="10" MaxSmartShortCutCount="0" RTL="False" PageSize="9" />
