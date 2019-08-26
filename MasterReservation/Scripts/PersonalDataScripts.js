$(document).ready(function () {

    $("#input-offers").trigger("keyup");
    // нажатие на аватар
    $("#avatar").on("click", function () {
        $("#picField").click();
    });

    // нажатие на крестик выбранной услуги
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
            $("#offers-wrap").append('<div class="offer"><span>' + this.innerText + '</span><img src="../Resources/img/cross-black.png" alt=""></div>');
        }
        $("#input-offers").trigger("keyup");
    });

    //редактирование полей личной информации
    $(".editbutton").on("click", function () {
        this.parentElement.getElementsByTagName("input")[0].removeAttribute("disabled");
    });
    $(document).on('click', function (e) {
        if (!$(e.target).closest(".input-wrap").length) {
            $(".areastyle").attr("disabled", "true");
        }
        e.stopPropagation();
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

    //загрузка фотографии
    document.getElementById('picField').onchange = function (evt) {
        var tgt = evt.target || window.event.srcElement,
            files = tgt.files;

        // FileReader support
        if (FileReader && files && files.length) {
            var fr = new FileReader();
            fr.onload = function () {
                document.getElementById("avatar").src = fr.result;
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