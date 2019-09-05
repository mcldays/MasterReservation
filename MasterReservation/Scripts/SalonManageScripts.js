$(document).ready(function() {
    //автокомплит города
    $("#city-input").each(function () {
        $(this).autocomplete({
            source: '/Authentication/GetCities'
        });
    });
});