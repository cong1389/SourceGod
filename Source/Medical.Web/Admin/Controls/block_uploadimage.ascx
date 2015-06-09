<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="block_uploadimage.ascx.cs"
    Inherits="Cb.Web.Admin.Controls.block_uploadimage" %>
<div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
    <blockquote>
        <p>
            Upload hình, audio</p>
        <footer>Hình ảnh <cite title="Source Title">jpg, jpeg, png, gif, bmp</cite></footer>
        <footer>Audio <cite title="Source Title">mp3</cite></footer>
        <br />
        <asp:FileUpload ID="fuImage" Width="300px" runat="server" CssClass="" />
    </blockquote>
</div>
<div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
    <blockquote>
        <p>
            Upload video</p>
        <footer>Video lấy từ 
<abbr title="Đường dẫn Share Youtube chỉ lấy ID cuối cùng sau dấu '/'https://youtu.be/ UOh9FWZqRSg">
    Video</abbr></footer>
        <br />
        <input type="text" name="txtIdVideo" id="txtIdVideo" runat="server" class="form-control" />
    </blockquote>
</div>
<div class="clearfix">
</div>
<div id="uploadImage">
    <asp:Button ID="btnUploadImage" runat="server" Text="Lưu" OnClick="btnUploadImage_Click"
        ValidationGroup="vg" CssClass="btn btn-success btn-lg" />
    <asp:Label ID="lblMsg" runat="server" ForeColor="Green" Text=""></asp:Label></div>
<br />
<asp:GridView ID="grdImage" runat="server" EmptyDataText="No files found!" AutoGenerateColumns="False"
    Font-Names="Verdana" AllowPaging="true" PageSize="50" Width="100%" OnPageIndexChanging="grdImage_PageIndexChanging"
    BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" OnRowDataBound="grdImage_RowDataBound"
    OnRowDeleting="grdImage_RowDeleting" DataKeyNames="Id" CssClass="table table-bordered table-hover">
    <AlternatingRowStyle />
    <HeaderStyle CssClass="success" />
    <Columns>
        <asp:BoundField DataField="id" HeaderText="STT" HeaderStyle-Width="6%" />
        <asp:BoundField DataField="Name" HeaderText="Tên file" HeaderStyle-Width="35%" />
        <asp:TemplateField HeaderText="Ảnh" HeaderStyle-Width="10%">
            <ItemTemplate>
                <asp:Image ID="colImage" runat="server" Height="35px" Width="35px" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="imagepath" HeaderText="Đường dẫn lưu hình" HeaderStyle-Width="50%"
            HtmlEncode="false" />
        <asp:CommandField ShowDeleteButton="True" ControlStyle-Font-Bold="true" />
    </Columns>
</asp:GridView>
