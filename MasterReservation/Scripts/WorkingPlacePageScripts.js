$(document).ready(function(){

    monthA = 'Января,Февраля,Марта,Апреля,Мая,Июня,Июля,Августа,Сентября,Октября,Ноября,Декабря'.split(',');

    //слайдеры
    $('#slider-photo').slick({
        dots: true,
        infinite: true
    });

    //обработка календаря
    var calendar1 = $('#calendar-wrap').datepicker({
        minDate: new Date(),
        maxDate: new Date(+new Date() + 1123200000),
        toggleSelected: false,
        onRenderCell: function (date, cellType) {
            
            var currentDate = date.getDate();
            var currentMonth = date.getMonth();
            var currentYear = date.getFullYear();
        },
        onSelect: function onSelect(fd, date, inst) {
            $("#input-times").val("");
            $("#total-bold").text("");
            $("#total-sum").text("");
            $("#full-day-checkbox").prop("checked", false);
            var currentDate = date.getDate();
            $("#date").text(currentDate + " " + monthA[date.getMonth()] + ",");
            $("#input-date").text(currentDate + " " + monthA[date.getMonth()]);
            $("#chosen-date").val(fd);

            $("#input-time-wrap").empty();
            for (var date of $("#" + fd.replace(/\./g, "-"))) {
                for (var slot of $(date).find("div")) {
                    if (slot.innerText[0] == "+") {
                        $("#input-time-wrap")
                            .append($("<button type='button' class='button-time'>" + slot.innerText.substring(1) + "</button>"));
                    } else if (slot.innerText[0] == "-"){
                        $("#input-time-wrap")
                            .append($("<button type='button' class='button-time white-button'>" + slot.innerText.substring(1) + "</button>"));
                    } else {
                        $("#input-time-wrap")
                            .append($("<button type='button' class='button-time green-button'>" + slot.innerText.substring(1) + "</button>"));
                    }
                }
            }
        }
    }).data('datepicker');

    //нажатие на кнопку Календарь
    $("#calendar-btn").on("click", function(){
        if($("#calendar-wrap").is(":visible")){
            $("#calendar-wrap").slideUp(300);
        }
        else{
            $("#calendar-wrap").slideDown(300);
        }
    });
    $("#calendar-wrap").click(function (e) {
        e.stopPropagation();
    });
    $(document).on('click', function(e) {
        if (!($(e.target).is("#calendar-wrap") || $(e.target).is("#calendar-btn"))) {
            $("#calendar-wrap").slideUp(300);
        }
    });


    function updateTotalBold() {
        var normTimes = "";
        if ($(".active-button").length == 1) {
            normTimes = $(".active-button").text();
        } else {
            let firstSlot = $(".active-button").first().text();
            let lastSlot = $(".active-button").last().text();
            normTimes += "с " + firstSlot.split("-")[0] + " до " + lastSlot.split("-")[1];
            
        }
        
        if ($(".active-button").length >= 1) {
            var hoursTitle = function () {
                if ($(".active-button").length == 1 || $(".active-button").length == 21) {
                    return " час";
                }else if ($(".active-button").length == 2 || $(".active-button").length == 3 || $(".active-button").length == 4 || $(".active-button").length == 22 || $(".active-button").length == 23 || $(".active-button").length == 24){
                    return " часа";
                } else {
                    return " часов";
                }
            };
            $("#total-bold").text($("#input-date").text() + ", " + normTimes + " (" + $(".active-button").length + hoursTitle() + ")");
            if ($(".active-button").length == $(".button-time").length && $("#rateday").length != 0) {
                var sum = parseFloat($("#rateday").text().replace(/,/g, ".")).toFixed(2);
            } else {
                var sum = ($(".active-button").length * parseFloat($("#rate1h").text().replace(/,/g, "."))).toFixed(2);
            }
            $("#total-sum").text(sum + "р");

            $("#booked-btn").attr("disabled", false);
            $("#booked-btn").removeClass("disabled-btn");
        } else {
            $("#total-bold").text("");
            $("#total-sum").text("");
            $("#booked-btn").attr("disabled", true);
            $("#booked-btn").addClass("disabled-btn");
        }
    }

    //обработка нажатий на даты
    $(document).on("click", ".white-button", function(){
        if ($(".active-button").length == 0) {
            $(this).removeClass("white-button");
            $(this).addClass("active-button");
            $("#input-times").val($(this).text() + ";");
        }
        else if ($(".active-button").length == 1) {
            let activeId = $(".button-time").index($(".active-button")[0]);
            let thisId = $(".button-time").index(this);
            if (thisId > activeId) {
                for (let i = activeId+1; i < thisId + 1; i++) {
                    if (!$(".button-time:eq(" + i + ")").is(".white-button")) {
                        console.log(i);
                        return;
                    }
                }
            } else {
                for (let i = thisId; i < activeId; i++) {
                    if (!$(".button-time:eq(" + i + ")").is(".white-button")) {
                        return;
                    }
                }
            }
            
            $("#input-times").val("");
            if (thisId > activeId) {
                for (let i = activeId; i < thisId + 1; i++) {
                    $(".button-time:eq(" + i + ")").removeClass("white-button");
                    $(".button-time:eq(" + i + ")").addClass("active-button");
                    $("#input-times").val($("#input-times").val() + $(".button-time:eq(" + i + ")").text() + ";");
                }
            } else {
                for (let i = thisId; i < activeId + 1; i++) {
                    $(".button-time:eq(" + i + ")").removeClass("white-button");
                    $(".button-time:eq(" + i + ")").addClass("active-button");
                    $("#input-times").val($("#input-times").val() + $(".button-time:eq(" + i + ")").text() + ";");
                }
            }
        }

        if ($(".active-button").length == $(".button-time").length) {
            $("#full-day-checkbox").prop("checked", true);
        }

        updateTotalBold();

    });
    $(document).on("click", ".active-button", function () {

        $("#full-day-checkbox").prop("checked", false);

        for (let elem of $(".active-button")) {
            $(elem).removeClass("active-button");
            $(elem).addClass("white-button");
        }

        $("#input-times").val("");

        updateTotalBold();

    });


    //обработка чекбокса Забронировать весь день
    $("#full-day-checkbox").change(function(){
        if (this.checked) {
            $("#input-times").val("");
            $(".white-button").addClass("active-button");
            $(".white-button").removeClass("white-button");
            var all_data = $(".active-button");
            for(var data of all_data){
                $("#input-times").val($("#input-times").val() + data.innerText + ";");
            }
            updateTotalBold();
        }
        else{
            $(".active-button").addClass("white-button");
            $(".active-button").removeClass("active-button");
            $("#input-times").val("");
            updateTotalBold();
        }
    });

    //нажатие на стрелочки при выборе даты
    var now = new Date();
    calendar1.selectDate(now);
    var maxDate = new Date(+new Date() + 1123200000);
    var minDate = new Date(new Date());
    $("#next-day").on("click", function () {
        if ($("#input-date").text() != (maxDate.getDate() + " " + monthA[maxDate.getMonth()])) {
            var date = calendar1.selectedDates[0];
            date.setTime(date.getTime() + 86400000);
            calendar1.selectDate(date);
        }
    });
    $("#prev-day").on("click", function(){
        if ($("#input-date").text() != (minDate.getDate() + " " + monthA[minDate.getMonth()])){
            var date = calendar1.selectedDates[0];
            date.setTime(date.getTime() - 86400000);
            calendar1.selectDate(date);
        }
    });


    //Нажатие на сердце
    $(".heart path").on("click", function(){
        if ($(this).parent().parent().hasClass("red-heart")){
            $.ajax({
                type: "GET",
                url: "/Favorite/RemoveFavorite",
                data: "data=" + $("#PlaceId").val(),
                success: function (data) {
                    if (data == "1") {
                        $(".heart").removeClass("red-heart");
                    }
                },
                error: function(){
                    alert("Произошла ошибка, попробуйте позже");
                }
            });
        }
        else{
            $.ajax({
                type: "GET",
                url: "/Favorite/SetFavorite",
                data: "data=" + $("#PlaceId").val(),
                success: function (data) {
                    if (data == "1") {
                        $(".heart").addClass("red-heart");
                    }
                },
                error: function(){
                    alert("Произошла ошибка, попробуйте позже");
                }
            });

        }
        
    });

    //карта
    ymaps.ready(function () {
        let width = screen.width;
        if (width > 1199) {
            var map = new ymaps.Map("map", {
                center: [56.846377, 53.255902],
                zoom: 10
            });
        } else {
            var map = new ymaps.Map("mobile-map", {
                center: [56.846377, 53.255902],
                zoom: 10
            });
        }

        var myGeocoder = ymaps.geocode($(".address")[0].innerText);
        var coords;
        myGeocoder.then(function (res) {
            //coords = res.geoObjects.get(0).geometry.getCoordinates();
            map.geoObjects.add(res.geoObjects);
            //var myGeoObject = new ymaps.GeoObject({
            //    geometry: {
            //        type: "Point",
            //        coordinates: coords
            //    },
            //    properties: {
            //        balloonContentBody: 'Текст балуна № '
            //    }
            //});
            //map.geoObjects.add(myGeoObject);
        });


        
        
    });

    $(".show-map-btn:visible").on("click", function () {
        if ($("#map", this).is(":visible")) {
            $("#map", this).hide();
        } else {
            $("#map", this).show();
        }
        
        if ($("#mobile-map", this).is(":visible")) {
            $("#mobile-map", this).hide();
        } else {
            $("#mobile-map", this).show();
        }
    });

    $("#map").on("click",function(e) {
        e.stopPropagation();
    });
    $("#mobile-map").on("click", function (e) {
        e.stopPropagation();
    });

    $("#arrow-style").on("click", function () {
        if ($("#description-body").is(":visible")) {
            $("#description-body").slideUp(300);
        }
        else {
            $("#description-body").slideDown(300);
        }
    });





});