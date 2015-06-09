function CheckYahoo(nick, imgID) {
    $.ajax(
    {
        url: "http://mail.opi.yahoo.com/online"
        ,
        type: "GET",
        data: "u=" + nick + "&m=t&t=1"
        , beforeSend: function () {
            $('#' + imgID).attr('src', '/images/ajaxloading.gif');
        }
	    , success: function (req) {
	        var strResponse = req;
	        if (strResponse == "01") {
	            $('#' + imgID).attr('src', '/images/template-Product1-brown_47.gif');
	            $.cookies.set(nick, 'true', { hoursToLive: 1 });
	        } else {
	            $('#' + imgID).attr('src', '/images/template-Product1-brown_50.gif');
	            $.cookies.set(nick, 'false', { hoursToLive: 1 });
	        }
	    }
	    , error: function (x, e) {
	        $('#' + imgID).attr('src', '/images/template-Product1-brown_50.gif');
	        $.cookies.set(nick, 'false', { hoursToLive: 1 });

	    }
    });
}

function CheckSkype(nick, imgID) {
    $.ajax(
    {
        url: "http://mystatus.skype.com/" + nick + ".num"
        , beforeSend: function () {
            $('#' + imgID).attr('src', '/images/ajaxloading.gif');
        }
	    , success: function (req) {

	        var strResponse = req.responseText;
	        switch (strResponse) {
	            case "2":
	            case "3":
	            case "7":
	                $('#' + imgID).attr('src', '/images/skype_on.jpg');
	                $.cookies.set(nick, 'true', { hoursToLive: 1 });
	                break;
	            default:
	                $('#' + imgID).attr('src', '/images/skype_off.jpg');
	                $.cookies.set(nick, 'false', { hoursToLive: 1 });
	                break;
	        }

	    }
	    , error: function (x, e) {

	        $('#' + imgID).attr('src', '/images/skype_off.jpg');
	        $.cookies.set(nick, 'false', { hoursToLive: 1 });
	    }
    });
}
