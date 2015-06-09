<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="contact.ascx.cs" Inherits="Cb.Web.Pages.Contact.contact" %>


<div class="container" runat="server" id="div">
    <div class="row-fluid" id="page">
        <div id="page-wrapper">
            <div id="contact-page">
                <div class="headerContact" id="title" runat="server">
                    <asp:Literal runat="server" ID="ltrContact"></asp:Literal>
                </div>
                <div class="row-fluid" id="main-contact">
                    <div class="title row-fluid">
                        <asp:Literal runat="server" ID="ltrCompanyName"></asp:Literal>
                    </div>
                    <div class="span8 left-contact">
                        <table class="table tbl-fix">
                            <tr>
                                <td>
                                    <asp:Literal runat="server" ID="ltrAddress"></asp:Literal>
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="ltrAdderss1"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Literal runat="server" ID="ltrPhone"></asp:Literal>
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="ltrPhone1"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Literal runat="server" ID="ltrFax" Text="Fax"></asp:Literal>
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="ltrFax1"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Literal runat="server" ID="ltrEmail" Text="Email(*)"></asp:Literal>
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="ltrEmail1"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Literal runat="server" ID="ltrWebsite" Text="Website"></asp:Literal>
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="ltrWebsite1"></asp:Literal>
                                </td>
                            </tr>
                        </table>
                        <div class="row-fluid form-contact">
                            <form method="post">
                            <p>
                                <input runat="server" id="txtName" class="span7 my " type="text" name="name" placeholder="Họ Tên" />
                            </p>
                            <p>
                                <input runat="server" id="txtEmail" class="span7 my" type="text" name="email" placeholder="Email" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="submitcontact"
                                    ControlToValidate="txtEmail" Text="*" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                                    Text="*" Display="Dynamic" ValidationGroup="submitcontact" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </p>
                            <p>
                                <textarea runat="server" id="txtMessage" class="span7 my" name="message"   style="width:450px" ></textarea>                </p>
                            <p>
                                <asp:Literal runat="server" ID="ltrCode" Text="ltrCode"></asp:Literal>
                            </p>
                            <p>
                                <img runat="server" id="img_Security_Code">
                            </p>
                            <p>
                                <input class="span3 my " type="text" name="captcha" runat="server" id="txt_Security_Code">
                                <asp:RequiredFieldValidator ID="req_Security_e" runat="server" Display="Dynamic"
                                    ControlToValidate="txt_Security_Code" ValidationGroup="register">*</asp:RequiredFieldValidator>
                                <asp:CustomValidator ID="cus_Same_Security_Code" runat="server" ValidationGroup="submitcontact"
                                    OnServerValidate="ValidateCheckSameSecurityCodeServer"></asp:CustomValidator>
                            </p>
                            <asp:Button ID="btn_Send" runat="server" CssClass="btn span2" ValidationGroup="submitcontact"
                                Text="btn_Send" OnClick="Submit_ServerClick" />
                            </form>
                        </div>
                    </div>
                    <div class="span4 right-contact">
                        <div class="row-fluid" id="support-online">
                            <div class="title">
                                <a runat="server" id="hypInvitationHeader" onclick='Chat_OpenMessengerDialog()' class="InvitationHeader" style="font-size: 25px" >
                                    <img src="../../Images/support.png" />
                                    <asp:Literal ID="ltrSupport" Text="strSupport" runat="server"></asp:Literal></a>
                            </div>
                            <div class="support-content">
                                
                            </div>
                        </div>
                        <div id="googlemap">
                            
                        </div>
                        <%--   <div class="row-fluid" id="maps">
                            <div>
                                <iframe width="265" height="198" frameborder="0" scrolling="no" marginheight="0"
                                    marginwidth="0" src="https://maps.google.com/maps?f=q&amp;source=s_q&amp;hl=vi&amp;geocode=&amp;q=Vi%E1%BB%87t+nam&amp;aq=&amp;sll=37.0625,-95.677068&amp;sspn=41.224889,79.013672&amp;ie=UTF8&amp;hq=&amp;hnear=Vi%E1%BB%87t+Nam&amp;t=m&amp;z=5&amp;ll=14.058324,108.277199&amp;output=embed">
                                </iframe>
                            </div>
                        </div>
                        <div class="clearfix">
                        </div>
                        <div class="row-fluid" id="contact-info">
                            <div class="logo">
                                <img src="../../Styles/assets/img/logo3.png" />
                            </div>
                            <div class="content">
                                <p>
                                    <asp:Literal ID="ltrMapHelper1" Text="ltrMapHelper1" runat="server"></asp:Literal>
                                </p>
                                <p>
                                    <asp:Literal ID="ltrMapHelper2" Text="ltrMapHelper2" runat="server"></asp:Literal>
                            </div>
                        </div>--%>
                    </div>
                </div>
            </div>
            <!-- end #contact-page -->
        </div>
        <!-- end #page-wrapper -->
    </div>
    <!-- end #page -->
</div>
<!-- end .container -->
