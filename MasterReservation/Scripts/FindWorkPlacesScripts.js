$(document).ready(function () {

    //слайдеры
    $('.photos-place').slick({
        dots: true,
        infinite: true
    });


    // календарь
    var freeDates = [{
        days: [1, 10, 12, 22],
        month: 7,
        year: 2019
    },
    {
        days: [4, 5, 6, 7],
        month: 8,
        year: 2019
    }];


    var calendar1 = $('#calendar').datepicker({
        minDate: new Date(),
        toggleSelected: false,
        onRenderCell: function (date, cellType) {

            var currentDate = date.getDate();
            var currentMonth = date.getMonth();
            var currentYear = date.getFullYear();
        },
        onSelect: function onSelect(fd, date, inst) {
            var currentDate = date.getDate();
            monthA = 'Января,Февраля,Марта,Апреля,Мая,Июня,Июля,Августа,Сентября,Октября,Ноября,Декабря'.split(',');

            $("#input-for-date").val(fd);

            //$("#date").text(currentDate + " " + monthA[date.getMonth()] + ",");
        }
    }).data('datepicker');

    //дропдовны
    $(".offers-selected-wrap").on("click", function () {
        if ($(this).next().is(":visible")) {
            $(this).next().slideUp('300');
        }
        else {
            $(this).next().slideDown('300');
        }
    });
    $(".dropdown-option").on("click", function () {
        var value_input = $(this).parent().parent().find(".hidden-input").val();
        if ($(this).is(".active-option")) {
            $(this).removeClass("active-option");
            $(this).parent().parent().find(".hidden-input").val(value_input.replace(this.innerText + ",", ""));
        }
        else {
            $(this).addClass("active-option");
            $(this).parent().parent().find(".hidden-input").val(value_input + this.innerText + ",");
        }

        // $(this).parent().prev().find($("span")).text(this.innerText);
        // $(this).parent().slideUp('300');
    });


    $(".result-button").on("click", function () {
        if (!$(this).is(".active-button")) {
            $(".active-button").removeClass("active-button");
            $(this).addClass("active-button");
            if ($(this).is("#show-list")) {
                $("#results-map").hide();
                $("#results-list").show();
            }
            else {
                $("#results-list").hide();
                $("#results-map").show();
            }
        }
    });


    $("#full-day").on("change", function () {
        if ($(this).is(":checked")) {
            $(".input-time").attr("disabled", "true");
        }
        else {
            $(".input-time").removeAttr('disabled');
        }
    });

    //автокомплит города
    $("#input-city").each(function () {
         $(this).autocomplete({
             source: '/test/testmethod'
         });
    });



});