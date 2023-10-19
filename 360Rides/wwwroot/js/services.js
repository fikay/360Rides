$(document).ready(function () {
    console.log("here")
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
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role ="group">
                                <a href = "/admin/services/upsert?id=${data}" class="btn btn-primary text-center mx-2"><i class="bi bi-pencil-square " ></i>Edit</a>
                                <a  onClick = Alert("/admin/services/deleteservice?id=${data}")   class="btn btn-danger text-center mx-2""><i class="bi bi-trash3 "></i>Delete</a>
                            </div>`
                },
                "width": "15%"
            }


        ]
    });

 
}