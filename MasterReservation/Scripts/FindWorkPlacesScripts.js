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


    //Нажатие на сердце
    $(".heart path").on("click", function (e) {
        if ($(this).parent().parent().hasClass("red-heart")) {
            $.ajax({
                type: "GET",
                url: "/Favorite/RemoveFavorite",
                data: "data=" + $(this).parent().parent().parent().parent().parent().find(".card-id").val(),
                success: function (data) {
                    if (data == "1") {
                        console.log($(e.target));
                        $(e.target).parent().parent().removeClass("red-heart");
                    }
                },
                error: function () {
                    alert("Произошла ошибка, попробуйте позже");
                }
            });
        }
        else {
            $.ajax({
                type: "GET",
                url: "/Favorite/SetFavorite",
                data: "data=" + $(this).parent().parent().parent().parent().parent().find(".card-id").val(),
                success: function (data) {
                    if (data == "1") {
                        $(e.target).parent().parent().addClass("red-heart");
                    }
                },
                error: function () {
                    alert("Произошла ошибка, попробуйте позже");
                }
            });

        }

    });

    
    

    $(".input-time").change(function () {
        $("#times-field").val($(".input-time")[0].value + "-" + $(".input-time")[1].value);
        if ($("#times-field").val().includes("_")) {
            $("#times-field").val("");
        }
    });

    $(".chosen-select1").change(function (e) {
        let list = $(this).val();
        let val = "";
        for (let i = 0; i < list.length; i++) {
            val += list[i] + ",";
        }
        $("#offers-field").val(val);
    });

    $(".chosen-select2").change(function (e) {
        let list = $(this).val();
        let val = "";
        for (let i = 0; i < list.length; i++) {
            val += list[i] + ",";
        }
        $("#ad-cond-field").val(val);
    });


});