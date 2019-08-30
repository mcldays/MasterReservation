$(document).ready(function () {

    // Открытие дропдовна в шапке
    $("#user-dropdown").slideUp(0);
    $("#userblock").on("click", function (e) {
        if ($("#user-dropdown").is(':visible')) {
            $("#user-dropdown").slideUp(300);
        }
        else {
            $("#user-dropdown").slideDown(300);
        }
    });
    $('#user-dropdown').click(function (e) {
        e.stopPropagation();
    });


    //переход на главную страницу при клике на лого
    $("#head-logo").on("click", function () {
        window.location = "WorkingPlacePage";
    });


});