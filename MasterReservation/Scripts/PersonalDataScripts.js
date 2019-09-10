$(document).ready(function () {

    
    // нажатие на аватар
    $("#avatar").on("click", function () {
        $("#picField").click();
    });

    // нажатие на крестик выбранной услуги
    $(document).on("click", ".offer img", function () {
        this.parentElement.remove();

        $("#input-offers").trigger("keyup");

        console.log(this.parentElement.innerText.trim() + ";");
        $("#hidden-offers").val($("#hidden-offers").val().replace(this.parentElement.innerText.trim() + ";", ""));
        

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
            $("#offers-wrap").append('<div class="offer"><span>' + this.innerText + '</span><img src="../Resources/img/cross-black.png" alt=""></div>');
            $("#hidden-offers").val($("#hidden-offers").val() + this.innerText + ";");
        }
        $("#input-offers").trigger("keyup");
    });

    //редактирование полей личной информации
    //$(".editbutton").on("click", function () {
    //    this.parentElement.getElementsByTagName("input")[0].removeAttribute("disabled");
    //});
    //$(document).on('click', function (e) {
    //    if (!$(e.target).closest(".input-wrap").length) {
    //        $(".areastyle").attr("disabled", "true");
    //    }
    //    e.stopPropagation();
    //});


    $("#input-offers").trigger("keyup");
    $(".dropdown-menu").removeClass("show");

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