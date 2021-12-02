var user = {
    init: function () {
        user.registerEvent();
    },
    registerEvent: function () {

        $('#btnDelete').off('click').on('click', function (e) {
            e.preventDefault();
            var boxData = [];
            $("input[name='userDelete']:checked").each(function () {
                boxData.push($(this).val());
            });
           
            $.ajax({

                url: "DeleteSelectedCheckbox",
                data: { data: boxData },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        alert("Delete Success...");
                        window.location.href = "Index";
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
                url: "/Admin/User/ChangeStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status == true) {
                        btn.text('Actived');
                    } else {
                        btn.text('Lock');
                    }
                }
            });
        });

        
    }
}
user.init();
