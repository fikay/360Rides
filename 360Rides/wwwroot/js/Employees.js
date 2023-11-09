$(document).ready(function () {
    console.log("here");
    var query = window.location.search
    console.log(query);
    const urlParams = new URLSearchParams(query);
    console.log(urlParams);
    loadDataTable();
}

)

function loadDataTable() {
    dataTable = $('#myTable').DataTable({

        ajax: {
            url: `/Admin/Employees/GetAll`,
            dataSrc: 'employees'
        },
        columns: [

            { data: 'name' },
            { data: 'email' },
            { data: 'phoneNumber' },
            { data: 'address' },
            { data: 'role' },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role ="group">
                                <a href = "/admin/Employees/upsert?id=${data}" class="btn btn-primary text-center mx-2"><i class="bi bi-pencil-square " ></i>Edit</a>
                            </div>`
                },
                "width": "15%"
            }


        ]
    });

}