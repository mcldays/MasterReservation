$(document).ready(function() {
    $("input[name='City']").each(function() {
        $(this).autocomplete({
            source: '/Test/TestMethod'
        });
    });
})