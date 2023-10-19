$(document).ready(function () {
    loadDataTable();
}
    
)

function loadDataTable() {
    dataTable = $('#myTable').DataTable({

        ajax: {
            url: '/Admin/Services/GetAll',
            dataSrc: 'services'
        },
        columns: [

            { data: 'serviceName' },
            { data: 'updatedBy' },
            {data: 'createdBy'},
            {
                data: 'updatedDate',
                render: function (data) {
                    const timestamp = new Date(data);

                    // Format the date components
                    const day = String(timestamp.getDate()).padStart(2, '0');
                    const month = String(timestamp.getMonth() + 1).padStart(2, '0'); // Month is zero-based
                    const year = timestamp.getFullYear();

                    // Create the readable format in day-month-year format
                    const readableFormat = `${day}-${month}-${year}`;
                    return readableFormat;
                }
            },
            {
                data: 'priceperkm',
                render: function (data) {
                    const formattedNumber = data.toLocaleString('en-US', {
                        style: 'currency',
                        currency: 'USD'
                    });
                    return formattedNumber;
                }
            },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role ="group">
                                <a href = "/admin/services/upsert?id=${data}" class="btn btn-primary text-center mx-2"><i class="bi bi-pencil-square " ></i>Edit</a>
                                <a id="delete" onClick = Alert("/admin/services/deleteservice?id=${data}")   class="btn btn-danger text-center mx-2""><i class="bi bi-trash3 "></i>Delete</a>
                            </div>`
                },
                "width": "15%"
            }


        ]
    });
 
}

function Alert(action) {
    const notice = PNotify.notice({
        title: 'DELETE ACTION',
        text: 'Are you sure ?',
        icon: 'fas fa-question-circle',
        hide: false,
        closer: false,
        sticker: false,
        destroy: true,
        stack: new PNotify.Stack({
            dir1: 'down',
            modal: true,
            firstpos1: 25,
            overlayClose: false
        }),
        modules: new Map([
            ...PNotify.defaultModules,
            [PNotifyConfirm, {
                confirm: true
            }]
        ])
    });
    notice.on('pnotify:confirm', () => {
        $.ajax({
            url: action,
            type: 'DELETE',
            success: function (data) {
                dataTable.ajax.reload();
                PNotify.success({
                    title: 'Success!',
                    text: `${data.message.toString()}`
                });
                
            }
        })
    });
    notice.on('pnotify:cancel', () => { });
}


