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

        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: '/Admin/Product/ChangeStatus',
                data: { id: id },
                dataType: 'Json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        btn.text('Active');
                    } else {
                        btn.text('Lock');
                    }
                }
            });
        });

    }
}
user.init();