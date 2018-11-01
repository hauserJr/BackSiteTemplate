// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ajaxStart(function () {
    //console.log('ajaxStart');
    $('#Loading').modal({
        keyboard: false,
        backdrop: false
    });
});

$(document).ajaxError(function (event, jqxhr, settings, thrownError) {
    //處理AJAX錯誤
    //console.log('ajaxError' + jqxhr.status);  
    this.location.href = "/Home/Error";
});

//$(document).ajaxSuccess(function (event, jqxhr, settings, thrownError) {
//   // console.log('ajaxSuccess' + jqxhr.status);
//});

$(document).ajaxStop(function () {
   // console.log('ajaxStop');
    $('#Loading').modal('hide');
});

//disable Dts Alert
$.fn.dataTable.ext.errMode = 'none';
$('#example').on('error.dt', function (e, settings, techNote, message) {
}).DataTable();



