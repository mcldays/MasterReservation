$(document).ready(function () {

    // открытие окна входа мастера
    $("#login-master").on("click", function () {
        $("#overlay-modal").slideDown(300);
        $("#window-login-master").slideDown(300);
    });

    // закрытие окна входа мастера
    $(".close-modal").on('click', function () {
        $("#overlay-modal").slideUp(300);
        $("#window-login-master").slideUp(300)[0].reset();
        $("#offers-wrap").empty();
    });

    // открытие окна входа салона
    $("#login-salon").on("click", function () {
        $("#overlay-modal").slideDown(300);
        $("#window-login-salon").slideDown(300);
    });

    // закрытие окна входа салона
    $(".close-modal").on('click', function () {
        $("#overlay-modal").slideUp(300);
        $("#window-login-salon").slideUp(300)[0].reset();
        $("#offers-wrap").empty();
    });

    // открытие окна регистрации салона
    $("#register-salon").on("click", function () {
        $("#overlay-modal").slideDown(300);
        $("#window-register-salon").slideDown(300);
    });

    // закрытие окна регистрации мастера
    $(".close-modal").on('click', function () {
        $("#overlay-modal").slideUp(300);
        $("#window-register-salon").slideUp(300)[0].reset();
    });

    // открытие окна регистрации мастера
    $("#register-master").on("click", function () {
        $("#overlay-modal").slideDown(300);
        $("#window-register-master").slideDown(300);
    });

    // закрытие окна регистрации мастера
    $(".close-modal").on('click', function () {
        $("#overlay-modal").slideUp(300);
        $("#window-register-master").slideUp(300)[0].reset();
    });













    // --------РЕГИСТРАЦИЯ МАСТЕРА-------



    // нажатие на аватар
    $(document).on("click", "#photo #pic", function () {
        $("#photo input").click();
    });

    // нажатие на крестик выбранной услуги
    $(document).on("click", ".offer img", function () {
        this.parentElement.remove();
        var offers = $("#hiddenInput").val().split(";").slice(0, -1);
        var changedOffers = "";
        for (var offer of offers) {
            if (offer != this.parentElement.getElementsByTagName("span")[0].innerText) {
                changedOffers += offer + ";";
            }
        }
        $("#hiddenInput").val(changedOffers);
        $("#input-offers").trigger("keyup");
    });

    // нажатие на "раскрыть список услуг"
    $("#input-offers-wrap img").on("click", function () {
        if (!this.parentElement.getElementsByClassName("dropdown-menu")[0].classList.contains("show")) {
            this.parentElement.getElementsByClassName("dropdown-menu")[0].classList.add("show");
        }
        else {
            this.parentElement.getElementsByClassName("dropdown-menu")[0].classList.remove("show");
        }
    });

    //клик не по списку
    $(document).on("click", function (e) {
        var div = $("#itemss"); 
        if (!div.is(e.target) && div.has(e.target).length === 0 && !$("#input-offers-wrap img").is(e.target)) {
            div.removeClass("show");
        }
    });

    // ввод символов в поле выбора услуг
    $("#input-offers").keyup(function () {
        $("#itemss div").remove();
        if (!this.parentElement.getElementsByClassName("dropdown-menu")[0].classList.contains("show")) {
            this.parentElement.getElementsByClassName("dropdown-menu")[0].classList.add("show");
        }
        if (this.value.length > 0) {
            for (var div of $("#hidden-offers div")) {
                if (~div.innerText.toLowerCase().indexOf(this.value.toLowerCase()) && !$("#offers-wrap").is("div:contains(" + div.innerText + ")")) {
                    $("#itemss").append('<div><span>' + div.innerText + '</span></div>');
                }
            }
        }
        else {
            for (var div of $("#hidden-offers div")) {
                if (!$("#offers-wrap").is("div:contains(" + div.innerText + ")")) {
                    $("#itemss").append('<div><span>' + div.innerText + '</span></div>');
                }
            }
        }
    });

    // нажатие на услугу
    $(document).on("click", "#itemss div", function () {
        if (this.parentElement.classList.contains("show")) {
            this.parentElement.classList.remove("show");
        }
        if (!$("#offers-wrap").is("div:contains(" + this.innerText + ")")) {
            $("#offers-wrap").append('<div class="offer"><span>' + this.innerText + '</span><img src="/Resources/img/cross.png" alt=""></div>');
            $("#hiddenInput").val($("#hiddenInput").val() + this.innerText + ";");
        }
        $("#input-offers").trigger("keyup");
    });


    //автокомплит города
    $("input[name='City']").each(function () {
        $(this).autocomplete({
            source: '/Authentication/GetCities'
        });
    });

    //загрузка фотографии
    document.getElementById('picField').onchange = function (evt) {
        var tgt = evt.target || window.event.srcElement,
            files = tgt.files;

        let loadingImage = loadImage(
            files[0],
            function(img) {
                document.getElementById("pic").remove();
                img.id = "pic";
                document.getElementById("photo").appendChild(img);
            },
            {
                orientation: true
            }
        );
    }




});