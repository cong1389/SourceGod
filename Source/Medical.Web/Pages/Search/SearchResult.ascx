<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchResult.ascx.cs"
    Inherits="Cb.Web.Pages.Search.SearchResult" %>
<!--Page SearchResult-->
<%@ Register TagPrefix="dgc" TagName="block_search" Src="~/Controls/block_search.ascx" %>
<%@ Register TagPrefix="dgc" TagName="blog_category" Src="~/Controls/blog_category.ascx" %>
<section id="category" class="container">
    <div class="row-fluid">
        <div class="col-sm-8 col-xs-8 col-md-8 col-lg-8 blog-item well">
            <div>
                <h4>
                    <asp:Literal runat="server" ID="ltrTitle"></asp:Literal></h4>
            </div>
            <dgc:blog_category ID="blog_category" runat="server" />
        </div>
        <aside class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
            <dgc:block_search ID="block_search" runat="server" />
            <div class="widget widget-popular">
            </div>
        </aside>
    </div>
</section>
