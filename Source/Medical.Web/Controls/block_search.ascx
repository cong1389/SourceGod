<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="block_search.ascx.cs"
    Inherits="Cb.Web.Controls.block_search" %>
<!--block_search-->
<script language="javascript" type="text/javascript">
    function submitButton(task) {
        var txtSearch = $("#<%=search.ClientID %>").val();
        var langId = '<%=LangId %>';
        if (txtSearch != "") {
            window.location = GetLink3Param('tim-kiem', langId, RemoveUnicode(txtSearch));
        }
        return false;
    }

    function GetLink3Param(pageName, langId, catId) {
        re = "/" + pageName + "/" + langId + "/" + catId;
        return re;
    }

    function checkEnter(e) { //e is event object passed from function invocation
        var characterCode //literal character code will be stored in this variable

        if (e && e.which) { //if which property of event object is supported (NN4)
            e = e;
            characterCode = e.which;  //character code is contained in NN4's which property
        }
        else {
            e = event;
            characterCode = e.keyCode;  //character code is contained in IE's keyCode property
        }
        if (characterCode == 13) { //if generated character code is equal to ascii 13 (if enter key)
            submitButton('search'); //submit the form
            return false;
        }
        else {
            return true;
        }
    }
</script>
<div class="widget search">
    <input type="text" id="search" name="search" class="form-control" onkeypress="return checkEnter(event)"
        runat="server" placeholder="Tìm kiếm" />
</div>
