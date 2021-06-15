// Call the dataTables jQuery plugin
//$.fn.dataTable.ext.order['dom-checkbox'] = function (settings, col) {
//    return this.api().column(col, { order: 'index' }).nodes().map(function (td, i) {
//        return $('input', td).prop('checked') ? '1' : '0';
//    });
//}

//$(document).ready(function () {
//    $('#dataTable').DataTable({
//        columnDefs: [
//            {
//                targets: [0, 1, 2],
//                orderDataType: 'dom-checkbox'
//            }
//        ]
//    });

//    $(':checkbox').on('change', function (e) {
//        var row = $(this).closest('tr');
//        var hmc = row.find(':checkbox:checked').length;
//        var kluj = parseInt(hmc);
//        row.find('td.counter').text(kluj);
//        table.row(row).invalidate('dom');
//    });
//});

$(document).ready(function () {
    var table = $('#dataTable').DataTable();
    $('#frm-example').on('submit', function (e) {
        var form = this;

        // Encode a set of form elements from all pages as an array of names and values
        var params = table.$('input,select,textarea').serializeArray();

        // Iterate over all form elements
        $.each(params, function () {
            // If element doesn't exist in DOM
            if (!$.contains(document, form[this.name])) {
                // Create a hidden element
                $(form).append(
                    $('<input>')
                        .attr('type', 'hidden')
                        .attr('name', this.name)
                        .val(this.value)
                );
            }
        });
    });
});