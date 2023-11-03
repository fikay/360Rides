$(document).ready(function () {
    console.log("here");
    var query = window.location.search
    console.log(query);
    const urlParams = new URLSearchParams(query);
    console.log(urlParams);
    loadDataTable(query);
}
    
)

function loadDataTable(query) {
    dataTable = $('#myTable').DataTable({

        ajax: {
            url: `/Admin/Request/GetAll${query}`,
            dataSrc: 'admin'
        },
        columns: [

            { data: 'orderId' },
            { data: 'orderStatus' },
            { data: 'requesterName' },
            { data: 'requesterEmail' },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role ="group">
                                <a href = "/admin/request/processing?id=${data}" class="btn btn-primary text-center mx-2"><i class="bi bi-pencil-square " ></i>Start Processing</a>
                            </div>`
                },
                "width": "15%"
            }


        ]
    });
 
}

//function Alert(action) {
//    const notice = PNotify.notice({
//        title: 'DELETE ACTION',
//        text: 'Are you sure ?',
//        icon: 'fas fa-question-circle',
//        hide: false,
//        closer: false,
//        sticker: false,
//        destroy: true,
//        stack: new PNotify.Stack({
//            dir1: 'down',
//            modal: true,
//            firstpos1: 25,
//            overlayClose: false
//        }),
//        modules: new Map([
//            ...PNotify.defaultModules,
//            [PNotifyConfirm, {
//                confirm: true
//            }]
//        ])
//    });
//    notice.on('pnotify:confirm', () => {
//        $.ajax({
//            url: action,
//            type: 'DELETE',
//            success: function (data) {
//                dataTable.ajax.reload();
//                PNotify.success({
//                    title: 'Success!',
//                    text: `${data.message.toString()}`
//                });
                
//            }
//        })
//    });
//    notice.on('pnotify:cancel', () => { });
//}


