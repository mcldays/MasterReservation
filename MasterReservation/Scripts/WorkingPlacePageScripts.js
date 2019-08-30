$(document).ready(function(){

    monthA = 'Января,Февраля,Марта,Апреля,Мая,Июня,Июля,Августа,Сентября,Октября,Ноября,Декабря'.split(',');

    //слайдеры
    $('#slider-photo').slick({
        dots: true,
        infinite: true
    });

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

    //обработка календаря
    var calendar1 = $('#calendar-wrap').datepicker({
        minDate: new Date(),
        toggleSelected: false,
        onRenderCell: function (date, cellType) {
            
            var currentDate = date.getDate();
            var currentMonth = date.getMonth();
            var currentYear = date.getFullYear();
        },
        onSelect: function onSelect(fd, date, inst) {
            var currentDate = date.getDate();
            $("#date").text(currentDate + " " + monthA[date.getMonth()] + ",");
            $("#input-date").text(currentDate + " " + monthA[date.getMonth()]);
            $("#chosen-date").val(fd);
        }
    }).data('datepicker');

    //ереход на главную страницу при клике на лого
    $("#head-logo").on("click", function(){
        window.location = "WorkingPlacePage";
    });

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

    //обработка нажатий на даты
    $(document).on("click", ".white-button", function(){
        $(this).removeClass("white-button");
        $(this).addClass("active-button");
        $("#input-times").val($("#input-times").val() + $(this).text() + ";");
    });
    $(document).on("click", ".active-button", function(){
        $(this).removeClass("active-button");
        $(this).addClass("white-button");
        var old_data = $("#input-times").val();
        $("#input-times").val(old_data.replace($(this).text() + ";", ""));
    });


    //обработка чекбокса Забронировать весь день
    $("#full-day-checkbox").change(function(){
        if(this.checked){
            $(".white-button").addClass("active-button");
            $(".white-button").removeClass("white-button");
            var all_data = $(".active-button");
            for(var data of all_data){
                $("#input-times").val($("#input-times").val() + data.innerText + ";");
            }
        }
        else{
            $(".active-button").addClass("white-button");
            $(".active-button").removeClass("active-button");
            $("#input-times").val("");
        }
    });

    //нажатие на стрелочки при выборе даты
    var now = new Date();
    calendar1.selectDate(now);
    $("#next-day").on("click", function(){
        var date = calendar1.selectedDates[0];
        date.setTime(date.getTime() + 86400000);
        calendar1.selectDate(date);
    });
    $("#prev-day").on("click", function(){
        var now = new Date();
        if($("#input-date").text() != (now.getDate() + " " + monthA[now.getMonth()])){
            var date = calendar1.selectedDates[0];
            date.setTime(date.getTime() - 86400000);
            calendar1.selectDate(date);
        }
    });

    $(".heart").on("click", function(){
        if($(this).hasClass("red-heart")){
            $.ajax({
                type: "GET",
                url: "/WorkingPlacePage/RemoveFavorite",
                data: "data=123",
                success: function(data){
                    $(".heart").removeClass("red-heart");
                },
                error: function(){
                    alert("Произошла ошибка, попробуйте позже");
                }
            });
        }
        else{
            $.ajax({
                type: "GET",
                url: "/WorkingPlacePage/SetFavorite",
                data: "data=123",
                success: function (data) {
                    $(".heart").addClass("red-heart");
                },
                error: function(){
                    alert("Произошла ошибка, попробуйте позже");
                }
            });

        }
        
    });
});