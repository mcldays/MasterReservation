$(document).ready(function () {

    // Открытие дропдовна в шапке
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
        window.location = "FindWorkPlaces";
    });

    //переход на страницу личных данных
    $("#to-personal-data-btn").on("click", function() {
        window.location = "PersonalData";
    });

    //переход на страницу забронированного времени
    $("#to-booked-time-btn").on("click", function () {
        window.location = "ViewBookedTime";
    });

    //переход на страницу избранного
    $("#to-favorites-btn").on("click", function () {
        window.location = "ViewFavorites";
    });


});