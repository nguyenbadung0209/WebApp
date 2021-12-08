var user = {
    init: function () {
        user.registerEvent();
    },
    registerEvent: function () {

        $('#btnDelete').off('click').on('click', function (e) {
            e.preventDefault();
            var boxData = [];
            $("input[name='productDelete']:checked").each(function () {
                boxData.push($(this).val());
            });

            $.ajax({

                url: "/Admin/Product/DeleteSelectedCheckbox",
                data: { data: boxData },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        alert("Delete Success...");
                        window.location.href = "/Admin/Product/Index";
                    } else {
                        alert("Delete Error...");
                    }
                }
            })
        });       

    }
}
user.init();