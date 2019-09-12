$(document).ready(function () {

    //слайдеры
    $('.photos-place').slick({
        dots: true,
        infinite: true
    });


    // календарь
    var calendar1 = $('#calendar').datepicker({
        minDate: new Date(),
        toggleSelected: true,
        onRenderCell: function (date, cellType) {

            var currentDate = date.getDate();
            var currentMonth = date.getMonth();
            var currentYear = date.getFullYear();
        },
        onSelect: function onSelect(fd, date, inst) {
            //var currentDate = date.getDate();
            

            $("#date-field").val(fd);
            if (inst.selectedDates.length == 0) {
                $("#input-time-wrap").slideUp(300);
                $(".input-time").val("");
                $("#times-field").val("");
            } else {
                $("#input-time-wrap").slideDown(300);
            }

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

    $(document).on("click", function (e) {
        var div = $(".offers-options");
        if (!div.is(e.target) && div.has(e.target).length === 0 && !$(".offers-selected-wrap").is(e.target) && $(".offers-selected-wrap").has(e.target).length === 0) {
            div.slideUp(300);
        }
    });

    //$(".dropdown-option").on("click", function () {
        //var value_input = $(this).parent().parent().find(".hidden-input").val();
        //if ($(this).is(".active-option")) {
            //$(this).removeClass("active-option");
            //$(this).parent().parent().find(".hidden-input").val(value_input.replace(this.innerText + ",", ""));
        //}
        //else {
        //    $(this).addClass("active-option");
        //    $(this).parent().parent().find(".hidden-input").val(value_input + this.innerText + ",");
        //}

        // $(this).parent().prev().find($("span")).text(this.innerText);
        // $(this).parent().slideUp('300');
    //});

    $(".offers").on("click", function() {
        if ($(this).is(".active-option")) {
            $(this).removeClass("active-option");
            $("#offers-field").val($("#offers-field").val().replace(this.innerText + ",", ""));
        }
        else {
            $(this).addClass("active-option");
            $("#offers-field").val($("#offers-field").val() + this.innerText + ",");
        }
    });

    $(".ad-cond").on("click", function () {
        if ($(this).is(".active-option")) {
            $(this).removeClass("active-option");
            $("#ad-cond-field").val($("#ad-cond-field").val().replace(this.innerText + ",", ""));
        }
        else {
            $(this).addClass("active-option");
            $("#ad-cond-field").val($("#ad-cond-field").val() + this.innerText + ",");
        }
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
             source: '/Authentication/GetCities'
         });
    });

    $("#input-city").change(function() {
        $("#city-field").val($("#input-city").val());
    });

    $(".checkbox1").change(function () {
        if ($(this).is(":checked")) {
            $("#types-field-1").val("true");
        } else {
            $("#types-field-1").val("false");
        }
    });

    $("#types-field-1").val("false");
    $("#types-field-2").val("false");

    $(".checkbox2").change(function () {
        if ($(this).is(":checked")) {
            $("#types-field-2").val("true");
        } else {
            $("#types-field-2").val("false");
        }
    });




    ymaps.ready(function () {
        // Указывается идентификатор HTML-элемента.
        //console.log(ymaps.geocode("Ижевск, Пушкинская, 239"));

        

        var map = new ymaps.Map("map", {
            center: [56.846377, 53.255902],
            zoom: 10
        });

        var myGeocoder = ymaps.geocode("Ижевск, Пушкинская, 239");
        myGeocoder.then(function (res) {
            //console.log(res.geoObjects);
            map.geoObjects.add(res.geoObjects);
        });

        
    });
    monthA = 'Января,Февраля,Марта,Апреля,Мая,Июня,Июля,Августа,Сентября,Октября,Ноября,Декабря'.split(',');
    $("#send-filter-data").on("click", function () {
        var placesIds = "";
        if ($("#date-field").val() != "" && $("#times-field").val() != "") {
            $.ajax({
                        type: "GET",
                        url: "../../BookingSlots/CheckFreeSlotsAjax",
                        data: "date=" + $("#date-field").val() + "&times=" + $("#times-field").val(),
                        async: false,
                        success: function (data) {
                            placesIds = data;
                        },
                        error: function () {
                            alert("Произошла ошибка, попробуйте позже");
                        }
                    });
        }
        if (($("#date-field").val() != "")) {
            $("#date").text($("#date-field").val().substring(0, 2) + " " + monthA[parseInt($("#date-field").val().substring(3, 5))] + ",");
        } else {
            $("#date").text("");
        }
        if ($("#times-field").val() != "") {
            $("#time").text($("#times-field").val() + ",");
        } else {
            $("#time").text("");
        }
        if ($("#offers-field").val() != "") {
            $("#offers").text($("#offers-field").val());
        } else {
            $("#offers").text("");
        }
        if ($("#city-field").val() != "") {
            $("#city").text($("#city-field").val() + ",");
        } else {
            $("#city").text("");
        }

        var places = $(".card");
        for (var place of places) {
            var ajaxSalonsInclude = false;
            //alert(placesIds);
            //alert($(place).find($(".card-id")).val());
            if (placesIds.includes($(place).find($(".card-id")).val())) {
                ajaxSalonsInclude = true;
            }
            if ($("#date-field").val() == "" || $("#times-field").val() == "") {
                ajaxSalonsInclude = true;
            }


            var offersInclude = false;
            var offers = $(place).find($(".card-offers")).val().split(",");
            for (var i = 0; i < offers.length - 1; i++) {
                if ($("#offers-field").val().includes(offers[i])) {
                    offersInclude = true;
                }
            }
            if ($("#offers-field").val() == "") {
                offersInclude = true;
            }

            var cityInclude = false;
            var city = $(place).find($(".card-city")).val();
            if ($("#city-field").val() == city) {
                cityInclude = true;
            }
            if ($("#city-field").val() == "") {
                cityInclude = true;
            }

            var typeInclude = false;
            var type = $(place).find($(".card-place-type")).val();
            if ($("#types-field-1").val() == "true" && type == "Отдельный кабинет") {
                typeInclude = true;
            }
            if ($("#types-field-2").val() == "true" && type == "Общее пространство") {
                typeInclude = true;
            }
            if ($("#types-field-1").val() == "false" && $("#types-field-2").val() == "false") {
                typeInclude = true;
            }


            var adCondInclude = false;
            var addCond = $(place).find($(".card-ad-cond")).val().split(",");
            for (var i = 0; i < addCond.length - 1; i++) {
                if ($("#ad-cond-field").val().includes(addCond[i])) {
                    adCondInclude = true;
                }
            }
            if ($("#ad-cond-field").val() == "") {
                adCondInclude = true;
            }


            if (!offersInclude || !cityInclude || !typeInclude || !adCondInclude || !ajaxSalonsInclude) {
                $(place).slideUp(300);
            } else {
                $(place).slideDown(300);
            }
        }
    });

    $(".input-time").keypress(function () {
        $("#times-field").val($(".input-time")[0].value + "-" + $(".input-time")[1].value);
        if ($("#times-field").val().includes("_")) {
            $("#times-field").val("");
        }
    });


});