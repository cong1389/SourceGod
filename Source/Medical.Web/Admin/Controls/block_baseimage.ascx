<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="block_baseimage.ascx.cs"
    Inherits="Cb.Web.Admin.Controls.block_baseimage" %>
<!--block_baseimage-->
<script language="javascript" type="text/javascript">
    $(document).ready(function () {
        $('#<%=imgToCrop.ClientID%>').Jcrop({
            onSelect: getAreaToCrop,
            addClass: 'jcrop-light',
            bgColor: 'white',
            bgOpacity: .5,
            sideHandles: false,
             <%= minSize %>,
              <%= maxSize %>,
            <%=setSelect %>
        });

        //                $('#<%=imgToCrop.ClientID%>').Jcrop({
        //                    onSelect: getAreaToCrop,
        //                    addClass: 'jcrop-light',
        //                    bgColor: 'white',
        //                    bgOpacity: .5,
        //                    sideHandles: false,
        //                    minSize: [50, 50],
        //                    setSelect: [0, 0, 400, 230]
        //                });
    });
    function getAreaToCrop(c) {
        $('.XCoordinate').val(parseInt(c.x));
        $('.YCoordinate').val(parseInt(c.y));
        $('.Width').val(parseInt(c.w));
        $('.Height').val(parseInt(c.h));
    }
</script>
<blockquote>
    <p>
        Đinh dạng file upload</p>
    <footer>Hình ảnh <cite title="Source Title">jpg, jpeg, png, gif, bmp</cite></footer>
</blockquote>
<div id="block_baseimage" style="width: 540px">
    <fieldset>
        <table>
            <tr>
                <td>
                    Chọn hình ảnh
                </td>
                <td>
                    <asp:FileUpload ID="fuImage" runat="server" />
                </td>
                <td>
                    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click"
                        CssClass="btn btn-success btn-lg" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                  <br />  <asp:Image ID="imgCropped" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                   <br /> <asp:Button ID="btnReset" runat="server" Text="Reset" Visible="false" OnClick="btnReset_Click"
                        CssClass="btn btn-success btn-lg" />
                </td>
            </tr>
        </table>
        <asp:Panel ID="pnlCrop" runat="server" Visible="false">
            <table>
                <tr>
                    <td>
                        <asp:Image ID="imgToCrop" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                      <br />  <asp:Button ID="btnCrop" runat="server" Text="Crop & Save" OnClick="btnCrop_Click"
                            CssClass="btn btn-success btn-lg" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <input id="XCoordinate" type="hidden" runat="server" class="XCoordinate" />
                        <input id="YCoordinate" type="hidden" runat="server" class="YCoordinate" />
                        <input id="Width" type="hidden" runat="server" class="Width" />
                        <input id="Height" type="hidden" runat="server" class="Height" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </fieldset>
</div>
