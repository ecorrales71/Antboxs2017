$(function () {
    $('btnAddAddress').submit(function (e) {
        e.preventDefault();

        var DataPost = JSON.stringify($(""))


        if ($(this).valid()) {
            $.ajax({
                url: this.action,
                type: this.method,
                data: $(this).serialize(),
                success: function (result) {
                    $('#result').html(result);
                }
            });
        }
        return false;
    });
});