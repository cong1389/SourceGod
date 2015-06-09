<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="logo_language.ascx.cs"
    Inherits="Cb.Web.Controls.logo_language" %>

    <div class="language" style="float: right; margin-right: -24px;">
        <table width="200" border="0">
            <tr>
                <td width="56">
                    <span style="color: #fff;">
                        <asp:Literal ID="ltrLangue" runat="server" Text="ltrLangue"></asp:Literal>
                    </span>
                </td>
                <td width="16">
                    <asp:Literal ID="ltrLangVn" runat="server"></asp:Literal>
                </td>
                <td width="31">
                    <asp:Literal ID="ltrLangEn" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
    </div>

