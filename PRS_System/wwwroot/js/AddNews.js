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

var btnUpload = $("#upload_file"),
    btnOuter = $(".button_outer");
btnUpload.on("change", function (e) {
    var ext = btnUpload.val().split('.').pop().toLowerCase();
    if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg']) == -1) {
        $(".error_msg").text("Not an Image...");
    } else {
        $(".error_msg").text("");
        btnOuter.addClass("file_uploading");
        setTimeout(function () {
            btnOuter.addClass("file_uploaded");
        }, 3000);
        var uploadedFile = URL.createObjectURL(e.target.files[0]);
        setTimeout(function () {
            $("#uploaded_view").append('<img src="' + uploadedFile + '" />').addClass("show");
            $(".panel").hide();
        }, 3500);
    }
});
$(".file_remove").on("click", function (e) {
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

var btnfileUpload = $('#upload_file_news'),
    btnOuterfile = $('.button_outer_file');
btnfileUpload.on("change", function (e) {
    var extfile = btnfileUpload.val().split('.').pop().toLowerCase();
    if ($.inArray(extfile, ['pdf', 'dos']) == -1) {
        $(".error_msg").text("Not an PDF or DOS.....");
    } else {
        $(".erro_msg").text("");
        btnOuterfile.addClass("file_uploading_file");
        setTimeout(function () {
            btnOuterfile.addClass("file_uploaded_file");
        }, 3000);
        var uploadedFile = URL.createObjectURL(e.target.files[0]);
        setTimeout(function () {
            $("#uploaded_view").append('<img src="' + uploadedFile + '" />').addClass("show");
            $(".panel").hide();
        }, 3500);


    }
});
$(".file_news_remove").on("click", function (e) {
    $("#upload_news_view").removeClass("show");
    $("#upload_news_view").find("img").remove();
    btnOuterfile.removeClass("file_uploading_file");
    btnOuterfile.removeClass("file_uploaded_file");
    $("#.panel_file").show();
});

var input = document.getElementById('upload_file_news');
var infoArea = document.getElementById('file-upload-filename');

input.addEventListener('change', showFileName);

function showFileName(event) {

    // the change event gives us the input it occurred in 
    var input = event.srcElement;

    // the input has an array of files in the `files` property, each one has a name that you can use. We're just using the name here.
    var fileName = input.files[0].name;

    // use fileName however fits your app best, i.e. add it into a div
    infoArea.textContent = 'File name: ' + fileName;
}