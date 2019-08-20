$(document).ready(function () {

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

    //автокомплит
    $("input[name='City']").each(function () {
        $(this).autocomplete({
            source: '/Test/TestMethod'
        });
    });


    document.getElementById('picField').onchange = function (evt) {
        var tgt = evt.target || window.event.srcElement,
            files = tgt.files;

        // FileReader support
        if (FileReader && files && files.length) {
            var fr = new FileReader();
            fr.onload = function () {
                document.getElementById("pic").src = fr.result;
            }
            fr.readAsDataURL(files[0]);
        }

        // Not supported
        else {
            // fallback -- perhaps submit the input to an iframe and temporarily store
            // them on the server until the user's session ends.
        }
    }

});