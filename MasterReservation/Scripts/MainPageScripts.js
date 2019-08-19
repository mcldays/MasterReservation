﻿$(document).ready(function () {

    // открытие окна входа мастера
    $("#login-master").on("click", function () {
        $("#overlay-modal").slideDown(300);
        $("#window-login-master").slideDown(300);
    });

    // закрытие окна регистрации мастера
    $(".close-modal").on('click', function () {
        $("#overlay-modal").slideUp(300);
        $("#window-login-master").slideUp(300);;
    });


    // открытие окна регистрации салона
    $("#register-salon").on("click", function () {
        $("#overlay-modal").slideDown(300);
        $("#window-register-salon").slideDown(300);
    });

    // закрытие окна регистрации мастера
    $(".close-modal").on('click', function () {
        $("#overlay-modal").slideUp(300);
        $("#window-register-salon").slideUp(300);;
    });












    // --------РЕГИСТРАЦИЯ МАСТЕРА-------


    // открытие окна регистрации мастера
    $("#register-master").on("click", function () {
        $("#overlay-modal").slideDown(300);
        $("#window-register-master").slideDown(300);
    });

    // закрытие окна регистрации мастера
    $(".close-modal").on('click', function () {
        $("#overlay-modal").slideUp(300);
        $("#window-register-master").slideUp(300);;
    });

    // нажатие на аватар
    $("#photo img").on("click", function () {
        $("#photo input").click();
    });

    // нажатие на крастик выбранной услуги
    $(document).on("click", ".offer img", function () {
        this.parentElement.remove();

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
            $("#offers-wrap").append('<div class="offer"><span>' + this.innerText + '</span><img src="cross.png" alt=""></div>');
        }
        $("#input-offers").trigger("keyup");
    });

});