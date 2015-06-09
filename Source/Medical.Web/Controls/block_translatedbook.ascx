<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="block_translatedbook.ascx.cs"
    Inherits="Cb.Web.Controls.block_translatedbook" %>
<!--block_translatedbook:sách dịch-->
<section id="block_translatedbook">
    <div class="container">
        <div class="center">
            <h3>Sách dịch</h3>
        </div>        
        <div class="row">
         <asp:Repeater runat="server" ID="rptResult" OnItemDataBound="rptResult_ItemDataBound">
            <ItemTemplate>
		    	<div class="col-lg-6 item">
                <asp:LinkButton runat="server" ID="lbnHeader1" OnCommand="lbnHeader1_Command" CssClass="nw-bigtlt"></asp:LinkButton>
                    <div class="papers text-center">
                    <a runat="server" id="hypImg">
					    <img runat="server" id="img" /></br>
				
					    <h4 class="notopmarg nobotmarg"><asp:Literal runat="server" ID="ltrTitle"></asp:Literal></h4>
					    <p>
                            <asp:Literal runat="server" ID="ltrBrief"></asp:Literal>
                        </p>
                    </a>
				</div>
			</div>
              </ItemTemplate>
            </asp:Repeater>  		
		</div>
     </div>
  </section>
<!--/block_silder-->
