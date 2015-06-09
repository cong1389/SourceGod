<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="block_breakumb.ascx.cs"
    Inherits="Cb.Web.Controls.block_breakumb" %>
<!--block_breakumb-->
<section id="breakumb" class="title">
    <div class="container">
        <div class="row-fluid">
            <div class="span6">
                <h1><asp:Literal runat="server" ID="ltrPageHeader"></asp:Literal></h1>
            </div>
            <div class="span6">
                <ul class="breadcrumb pull-right">
                    <li><a runat="server" id="hypHome">Trang chủ</a> <span class="divider">/</span></li>
                    <li><a runat="server" id="hypPageSub"><asp:Literal runat="server" ID="ltrPageSub"></asp:Literal></a> <span class="divider"></span></li>                    
                </ul>
            </div>
        </div>
    </div>
</section>
