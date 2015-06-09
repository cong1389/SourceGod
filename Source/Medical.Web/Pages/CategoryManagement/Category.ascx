<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Category.ascx.cs" Inherits="Cb.Web.Pages.CategoryManagement.Category" %>
<!--Page Category-->
<%@ Register TagPrefix="dgc" TagName="block_search" Src="~/Controls/block_search.ascx" %>
<%@ Register TagPrefix="dgc" TagName="blog_category" Src="~/Controls/blog_category.ascx" %>
<%@ Register TagPrefix="dgc" TagName="block_hot" Src="~/Controls/block_hot.ascx" %>
<%@ Register TagPrefix="dgc" TagName="block_tagcloud" Src="~/Controls/block_tagcloud.ascx" %>
<%@ Register TagPrefix="dgc" TagName="block_archive" Src="~/Controls/block_archive.ascx" %>
<section id="category" class="container">
    <div class="row-fluid">
        <div class="col-sm-8 col-xs-8 col-md-8 col-lg-8 blog-item well">
            <dgc:blog_category ID="blog_category" runat="server" />
        </div>
        <aside class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
            <dgc:block_search ID="block_search" runat="server" />
            <!-- /.search -->
            <dgc:block_hot ID="block_hot" runat="server" />
            <!-- /.ads -->
            <div class="widget widget-popular">
                <dgc:block_tagcloud ID="block_tagcloud" runat="server" />
                <dgc:block_archive ID="block_archive" runat="server" />
                <!-- End Archive Widget -->
            </div>
        </aside>
    </div>
</section>
