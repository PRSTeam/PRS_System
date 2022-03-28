$(document).ready(function () {

    $('input').focus(function () {
        $(this).parent().find(".label-txt").addClass('label-active');
    });

    $("input").focusout(function () {
        if ($(this).val() == '') {
            $(this).parent().find(".label-txt").removeClass('label-active');
        };
    });

});

// Tab

$(document).ready(function () {
    $(".tab-menu a").click(function (event) {
        event.preventDefault();
        $(this).parent().addClass("current");
        $(this).parent().siblings().removeClass("current");
        var tab = $(this).attr("href");
        $(".tab-content").not(tab).css("display", "none");
        $(tab).show();
    });
});

$(document).ready(function () {
    $(".tab-menu-news a").click(function (event) {
        event.preventDefault();
        $(this).parent().addClass("current-news");
        $(this).parent().siblings().removeClass("current-news");
        var tab = $(this).attr("href");
        $(".tab-content-news").not(tab).css("display", "none");
        $(tab).show();
    });
});





// *******************************************************************************************
// Upload img
function toggle() {
    var blur = document.getElementById('blur');
    blur.classList.toggle('active-blur');
    var popup = document.getElementById('popup');
    popup.classList.toggle('active-blur');
}

var btnUpload = $("#FilePic");
var btnOuter = $(".button_outer");

//var text_header = $('#tabHeader').val();

btnUpload.on("change", function (e) {
    var ext = btnUpload.val().split('.').pop().toLowerCase();
    if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg']) == -1) {
        $(".error_msg").show();
    }
    else {
        $(".error_msg").hide;
        btnOuter.addClass("file_uploading");
        setTimeout(function () {
            btnOuter.addClass("file_uploaded");
        }, 3000);
        var uploadedFile = URL.createObjectURL(e.target.files[0]);
        setTimeout(function () {
            $("#uploaded_view").append('<img src="' + uploadedFile + '" />').addClass("show");
            $("#uploaded_view").append('<input type="text" id="Header" name="Header" style="display:none;" />');
            $("#Header").val("#tabHeader")
            $(".panel").hide();
            $(".btn-create").show();
        }, 3500);
    }
});
$(".file_remove").on("click", function (e) {
    $(".btn-create").hide();
    $("#uploaded_view").removeClass("show");
    $("#uploaded_view").find("img").remove();
    btnOuter.removeClass("file_uploading");
    btnOuter.removeClass("file_uploaded");
    $(".panel").show();
});

// ********************************************************************************************
// แก้
function togglefile() {
    var blur = document.getElementById('blur');
    blur.classList.toggle('active-blur');
    var popupfile = document.getElementById('popup-file');
    popupfile.classList.toggle('active-blur');
}

var btnfileUpload = $('#FilePDF');
var btnOuterfile = $('.button_outer_file');

//var text_header = $('#tabHeader').val();
var input = document.getElementById('FilePDF');

btnfileUpload.on("change", function (e) {
    var extfile = btnfileUpload.val().split('.').pop().toLowerCase();
    if ($.inArray(extfile, ['pdf', 'dos']) == -1) {
        $(".error_msg").show();
    } else {
        $(".error_msg").hide();
        btnOuterfile.addClass("file_uploading_file");
        setTimeout(function () {
            btnOuterfile.addClass("file_uploaded_file");
        }, 3000);
        var uploadedFile = URL.createObjectURL(e.target.files[0]);
        setTimeout(function () {
            $("#upload_news_view").append('<a href="' + uploadedFile + '" target="_blank">' + e.target.files[0].name + '</a>');
            $("#upload_news_view").append('<input type="text" id="Header" name="Header" style="display:none;" />');
            $("#Header").val("#tabHeader")
            $("#upload_news_view").addClass("show");
            $(".panel-file").hide();
            $(".btn-create-file").show();
        }, 3500);


    }
});
$(".file_news_remove").on("click", function (e) {
    $(".btn-create-file").hide();
    $("#upload_news_view").removeClass("show");
    $("#upload_news_view").find("a").remove();
    btnOuterfile.removeClass("file_uploading_file");
    btnOuterfile.removeClass("file_uploaded_file");
    $(".panel-file").show();
});

function addtabname() {
    var tabname = $("#tabname").val();
    if (tabname == "" || tabname == undefined || tabname.trim() == "") {
        alert("กรุณาระบุชื่อแท็บ");
    }
    else {
        window.location.href = "@Url.Action('Action Name', 'Controller Name')";
    }
}