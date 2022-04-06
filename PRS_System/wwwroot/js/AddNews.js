$(document).ready(function () {

    $('input').focus(function () {
        $(this).parent().find(".label-txt").addClass('label-active');
    });

    $("input").focusout(function () {
        if ($(this).val() == '') {
            $(this).parent().find(".label-txt").removeClass('label-active');
        };
    });

    // Tab

    $(".tab-menu a").click(function (event) {
        event.preventDefault();
        $(this).parent().addClass("current");
        $(this).parent().siblings().removeClass("current");
        var tab = $(this).attr("href");
        $(".tab-content").not(tab).css("display", "none");
        $(tab).show();
    });

    $(".tab-menu-news a").click(function (event) {
        event.preventDefault();
        $(this).parent().addClass("current-news");
        $(this).parent().siblings().removeClass("current-news");
        var tab = $(this).attr("href");
        $(".tab-content-news").not(tab).css("display", "none");
        $(tab).show();
    });

    $(".tab-menu-section a").click(function (event) {
        event.preventDefault();
        $(this).parent().addClass("current-section");
        $(this).parent().siblings().removeClass("current-section");
        var tab = $(this).attr("href");
        $(".tab-content-section").not(tab).css("display", "none");
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
        $(".error_msg").hide();
        btnOuter.addClass("file_uploading");
        setTimeout(function () {
            btnOuter.addClass("file_uploaded");
        }, 3000);
        var uploadedFile = URL.createObjectURL(e.target.files[0]);
        setTimeout(function () {
            $("#uploaded_view").append('<img src="' + uploadedFile + '" />').addClass("show");
            $("#uploaded_view").append('<input type="hidden" id="Header" name="Header" />');
            $("#Header").val("รูปปก");
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
// Upload file
function togglefile() {
    var blur = document.getElementById('blur');
    blur.classList.toggle('active-blur');
    var popupfile = document.getElementById('popup-file');
    popupfile.classList.toggle('active-blur');
}

var btnfileUpload = $('.btn_upload_file #FilePDF');
var btnOuterfile = $('.button_outer_file');

//var text_header = $('#tabHeader').val();
//var text_header = $('.current-news').find('a').text();
//console.log(text_header);
//var input_file = document.getElementById('FilePDF');

btnfileUpload.on("change", function (e) {
    var text_header = $('.current-news').find('a').text();
    console.log(text_header);
    var extfile = btnfileUpload.val().split('.').pop().toLowerCase();
    if ($.inArray(extfile, ['xlsx', 'xls', 'doc', 'docx', 'ppt', 'pptx', 'pdf']) == -1) {
        $(".error_msg").show();
    } else {
        $(".error_msg").hide();
        btnOuterfile.addClass("file_uploading_file");
        setTimeout(function () {
            btnOuterfile.addClass("file_uploaded_file");
        }, 3000);
        var uploadedFile = URL.createObjectURL(e.target.files[0]);
        setTimeout(function () {
            $("#uploaded_news_view").append('<a href="' + uploadedFile + '" target="_blank">' + e.target.files[0].name + '</a>');
            $("#uploaded_news_view").append('<input type="hidden" id="Header" name="Header" />');
            $("#Header").val(text_header);
            $("#uploaded_news_view").addClass("show");
            $(".panel-file").hide();
            $(".btn-create-file").show();
        }, 3500);
    }
});
$(".file_news_remove").on("click", function (e) {
    $(".btn-create-file").hide();
    $("#uploaded_news_view").removeClass("show");
    $("#uploaded_news_view").find("a").remove();
    btnOuterfile.removeClass("file_uploading_file");
    btnOuterfile.removeClass("file_uploaded_file");
    $(".panel-file").show();
});

// ********************************************************************************************
// Upload section file
function togglesection() {
    var blur = document.getElementById('blur');
    blur.classList.toggle('active-blur');
    var popupfile = document.getElementById('popup-section');
    popupfile.classList.toggle('active-blur');
}

var btnsectionUpload = $('.btn_upload_section #FilePDF');
var btnOutersection = $('.button_outer_section');

//var text_header = $('#tabHeader').val();
//var text_header = $('.current-news').find('a').text();
//console.log(text_header);
//var input_section = document.getElementById('FilePDF');

btnsectionUpload.on("change", function (e) {
    var text_section = $('.current-section').find('a').text();
    console.log(text_section);
    var extfile = btnsectionUpload.val().split('.').pop().toLowerCase();
    if ($.inArray(extfile, ['xlsx', 'xls', 'doc', 'docx', 'ppt', 'pptx', 'pdf']) == -1) {
        $(".error_msg").show();
    } else {
        $(".error_msg").hide();
        btnOutersection.addClass("file_uploading_section");
        setTimeout(function () {
            btnOutersection.addClass("file_uploaded_section");
        }, 3000);
        var uploadedSection = URL.createObjectURL(e.target.files[0]);
        setTimeout(function () {
            $("#uploaded_section_view").append('<a href="' + uploadedSection + '" target="_blank">' + e.target.files[0].name + '</a>');
            $("#uploaded_section_view").append('<input type="hidden" id="Header" name="Header" value="เอกสารดาวน์โหลด"/>');
            $("#uploaded_section_view").append('<input type="hidden" id="Section" name="Section" />');
            $("#Section").val(text_section);
            $("#uploaded_section_view").addClass("show");
            $(".panel-section").hide();
            $(".btn-create-file").show();
        }, 3500);
    }
});
$(".file_section_remove").on("click", function (e) {
    $(".btn-create-file").hide();
    $("#uploaded_section_view").removeClass("show");
    $("#uploaded_section_view").find("a").remove();
    btnOutersection.removeClass("file_uploading_section");
    btnOutersection.removeClass("file_uploaded_section");
    $(".panel-section").show();
});