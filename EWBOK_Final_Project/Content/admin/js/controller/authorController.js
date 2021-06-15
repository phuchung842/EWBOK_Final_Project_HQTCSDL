﻿var author = {
    init: function () {
        author.registerEvents();
    },
    registerEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/Author/ChangeStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    console.log(response);
                    if (response.status == true) {
                        btn.html('<i class="fa fa-check"></i>');
                    }
                    else {
                        btn.html('<i class="fa fa-lock"></i>');
                    }
                }
            });
        });
    }
}
author.init();