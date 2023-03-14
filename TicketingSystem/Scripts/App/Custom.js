//Datepicker for departureDate and returnDate
$(function () {
    // Initialize the datepicker with no minimum date
    $("#departureDate").datepicker({
        dateFormat: "dd-MM-yy",
        minDate: new Date(),
        
        onSelect: function (dateText) {
            // When the user selects a date, set it as the minimum date for the second datepicker
            var selectedDate = new Date(dateText);
            $("#returnDate").datepicker("option", "minDate", selectedDate);
        }
    });

    // Initialize the second datepicker with no minimum date
    $("#returnDate").datepicker({
        dateFormat: "dd-MM-yy"
    });
});

$(document).ready(function () {
    $('#busCompanyLogo').change(function (event) {
        var reader = new FileReader();
        reader.onload = function () {
            $('#imagePreview').attr('src', reader.result);
        }
        reader.readAsDataURL(event.target.files[0]);
    });
});

$(document).ready(function () {
    $('#promotionImage').change(function (event) {
        var reader = new FileReader();
        reader.onload = function () {
            $('#promoImagePreview').attr('src', reader.result);
        }
        reader.readAsDataURL(event.target.files[0]);
    });
});


////Zooming in and out images
//$(document).ready(function () {
//    $(".adv-img").mousemove(function (event) {
//        var imgWidth = $(this).find("img").width();
//        var imgHeight = $(this).find("img").height();
//        var mouseX = event.pageX - $(this).offset().left;
//        var mouseY = event.pageY - $(this).offset().top;
//        var centerX = imgWidth / 2;
//        var centerY = imgHeight / 2;
//        var distanceFromCenterX = Math.abs(mouseX - centerX);
//        var distanceFromCenterY = Math.abs(mouseY - centerY);
//        var zoomAmount = 1.5;

//        $(this).find("img").removeClass("zoom");
//        if (mouseX < centerX && mouseY < centerY) {
//            var percentX = distanceFromCenterX / centerX;
//            var percentY = distanceFromCenterY / centerY;
//            var percent = (percentX > percentY) ? percentX : percentY;
//            var zoomX = (1 + (zoomAmount - 1) * percent) * 100;
//            var zoomY = (1 + (zoomAmount - 1) * percent) * 100;
//            $(this).find("img").css("transform", "scale(" + zoomX / 100 + ", " + zoomY / 100 + ")");
//        } else {
//            $(this).find("img").addClass("zoom");
//        }
//    });

//    $(".adv-img").mouseleave(function () {
//        $(this).find("img").removeClass("zoom");
//        $(this).find("img").css("transform", "scale(1)");
//    });
//});
