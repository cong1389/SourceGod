<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="servicedetails.ascx.cs"
    Inherits="Cb.Web.Pages.Service.servicedetails" %>
<div class="row-fluid" id="page" >
    <div id="page-wrapper">
        <div id="gallery-page">
            <div class="row-fluid" id="title-global">
                <asp:Literal runat="server" ID="ltrService" Text="ltrService"></asp:Literal>
            </div>
            <div class="row-fluid" id="main-gallery">
                <asp:Literal runat="server" ID="ltrDetail"></asp:Literal>
            </div>
        </div>
        <!-- end #contact-page -->
    </div>
    <!-- end #page-wrapper -->
</div>
<!--Facebook-->
<div class="comment" id="divcomentface">
    <asp:HiddenField ID="hdfLinkFace" runat="server" />
    <div class="fb-comments" data-href="<%=hdfLinkFace.Value %>" data-num-posts="2" data-width="900">
    </div>
</div>
