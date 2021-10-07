// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$("#Submit").click(function() {
    var valueName = $('#Name').val();
    $('#Name').val(valueName.trim());
});

window.onload = function() {
    $('#keyBlockIsAdminLottery').slideUp(0);
};

$(document).ready(function() {
    $("#customCheck1").change(function() {
        $('#keyBlockIsAdminLottery').slideToggle('slow');

        //if ($(this).is(":checked")) {
        //    $('#keyBlockIsAdminLottery').slideDown(800);
        //}
        //if ($(this).is(":unchecked")) {
        //    $('#keyBlockIsAdminLottery').slideUp(800);
        //}
    });
});