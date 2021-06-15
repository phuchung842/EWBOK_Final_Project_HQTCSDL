var cart = {
    init: function () {
        cart.regEvent();
    },
    regEvent: function () {

        $('#btnUpdate').off('click').on('click', function () {
            var listproduct = $('.txtQuantity');
            var cartList = [];
            $.each(listproduct, function (i, item) {
                cartList.push({
                    Quantity: $(item).val(),
                    Product: {
                        ID: $(item).data('id')
                    }
                });
            });

            $.ajax({
                url: '/Cart/Update',
                data: { cartModel: JSON.stringify(cartList) },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/Cart";
                    }
                }
            });
        });
    }
}
cart.init();