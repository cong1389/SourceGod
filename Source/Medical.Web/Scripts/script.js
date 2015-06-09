//Replace ký tự đặc biệt
function RemoveUnicode(text) {
    var result;

    //Đổi chữ hoa thành chữ thường
    result = text.toLowerCase();

    // xóa dấu
    result = result.replace(/(à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ)/g, 'a');
    result = result.replace(/(è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ)/g, 'e');
    result = result.replace(/(ì|í|ị|ỉ|ĩ)/g, 'i');
    result = result.replace(/(ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ)/g, 'o');
    result = result.replace(/(ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ)/g, 'u');
    result = result.replace(/(ỳ|ý|ỵ|ỷ|ỹ)/g, 'y');
    result = result.replace(/(đ)/g, 'd');
    // Xóa ký tự đặc biệt
    result = result.replace(/([^0-9a-z-\s])/g, '');
    // Xóa khoảng trắng thay bằng ký tự -
    result = result.replace(/(\s+)/g, '-');
    // xóa phần dự - ở đầu
    result = result.replace(/^-+/g, '');
    // xóa phần dư - ở cuối
    result = result.replace(/-+$/g, '');

    return result;
}

//$(function () {
//    var Page = (function () {

//        var $navArrows = $('#nav-arrows'),
//        slitslider = $('#slider').slitslider({
//            autoplay: true
//        }),

//        init = function () {
//            initEvents();
//        },
//        initEvents = function () {
//            $navArrows.children(':last').on('click', function () {
//                slitslider.next();
//                return false;
//            });

//            $navArrows.children(':first').on('click', function () {
//                slitslider.previous();
//                return false;
//            });
//        };

//        return { init: init };

//    })();

//    Page.init();
//});

//COLLAPSE HEADER ON SCRLL
$(window).scroll(function () {
    if ($(".navbar").offset().top > 40) {
        $(".navbar-fixed-top").addClass("navbar-pad-original");
    } else {
        $(".navbar-fixed-top").removeClass("navbar-pad-original");
    }
});
$(document).ready(function () {
    $('ul.dropdown-menu [data-toggle=dropdown]').on('click', function (event) {
        // Avoid following the href location when clicking
        event.preventDefault();
        // Avoid having the menu to close when clicking
        event.stopPropagation();
        // If a menu is already open we close it
        //$('ul.dropdown-menu [data-toggle=dropdown]').parent().removeClass('open');
        // opening the one you clicked on
        $(this).parent().addClass('open');

        var menu = $(this).parent().find("ul");
        var menupos = menu.offset();

        if ((menupos.left + menu.width()) + 30 > $(window).width()) {
            var newpos = -menu.width();
        } else {
            var newpos = $(this).parent().width();
        }
        menu.css({ left: newpos });

    });

});

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