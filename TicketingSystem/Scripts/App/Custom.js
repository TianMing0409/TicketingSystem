﻿//Datepicker for departureDate and returnDate
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


// Attach an event listener to the form's submit event
$('#myForm').on('submit', function (event) {
    // Check if the form is valid based on HTML validation rules
    var isFormValid = this.checkValidity();

    // Check if the model is valid based on jQuery Unobtrusive Validation rules
    var isModelStateValid = $('#myForm').valid();

    if (isFormValid && isModelStateValid) {
        // Display the loading animation
        document.getElementById('loading').style.display = 'block';
        $("#submit-button").prop("disabled", true);
        $("#submit-button").val("Processing...");

        // Replace 2000 with the time it takes for your form to process
        setTimeout(function () {
            document.getElementById('loading').style.display = 'none';
        }, 5000);
    } else {
        event.preventDefault();
        // You can also display an error message to the user, if desired
    }
});


$(document).ready(function () {
    $("#apply-promo-code").click(function () {
        var promoCode = $("#promo-code").val();
        var totalAmount = parseFloat($("#total-amount").text().replace("RM ", ""));
        $.ajax({
            type: "POST",
            url: "ApplyPromoCode",
            data: { promoCode: promoCode, totalAmount: totalAmount },
            success: function (data) {
                if (data.error) {
                    // Show an error message to the user
                    alert(data.error);
                } else {
                    // Update the payment information on the client side based on the data returned by the server
                    $("#discount").text("- " + data.discountAmt);
                    $("#total-amount").text("RM  " + data.totalAmount);

                    // Disable the Apply button
                    $("#promo-code").prop("disabled", true);
                    $("#apply-promo-code").prop("disabled", true);
                    $("#apply-promo-code").text("Applied");

                    // Show a success message to the user
                    alert("Promo code applied successfully!");
                }
            },
            error: function (xhr, status, error) {
                // Show an error message to the user
                alert("Failed to apply promo code. Error message: " + error);
            }
        });
    });
});


$('#myModal').on('shown.bs.modal', function () {
    $('#myForm').submit();
});


//$('#showPopupButton').click(function () {
//    $.ajax({
//        url: 'PurchaseTickets',
//        type: 'GET',
//        success: function (result) {
//            var popup = window.open('', '', 'width=600,height=400');
//            popup.document.write(result);
//        },
//        error: function (xhr, status, error) {
//            // handle the error response
//            alert(error);
//        }
//    });
//});


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
