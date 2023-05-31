var dataTable;

$(document).ready(function () {
    cargarDatabable();
})

function cargarDatabable() {
    dataTable = $("#TablaVentas").DataTable({

        "ajax": {
            "url": "/Admin/Ventas/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "productos.nombre", "width": "10%" },
            { "data": "cliente", "width": "10%" },
            { "data": "cantidad", "width": "5%" },
            { "data": "productos.precio", "width": "5%" },
            {
                "data": {
                    "dato1": "productos.precio",
                    "dato2": "cantidad",
                },
                "render": function (data) {
                    
                    return `${data["cantidad"] * data.productos["precio"]}`;

                }, "width": "10%"
            },
          
            {
                "data": "id",

                
                "render": function (data) {
                 
                        return `
                            <a onclick=Detalles("/Admin/Ventas/DetallesVenta/${data}") class='btn btn-info text-white' style='cursor:pointer; width:120px;'>
                            <i class='fas fa-edit'></i> Detalles
                            </a>
   
                            
                         
                            <a onclick=Delete("/Admin/Ventas/Delete/${data}") class='btn btn-danger text-white' style='cursor:pointer; width:100px;'>
                            <i class='fas fa-trash-alt'></i> Borrar
                            </a>
                            `;
                    

                }, "width": "50%"
            }
        ],
        "language": {
            "url": '//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json'
        },
        "width": "100%"

    });
}

function Delete(url) {
    swal({
        title: "Esta seguro de borrar?",
        text: "Este contenido no se puede recuperar!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si, borrar!",
        closeOnconfirm: true
    }, function () {
        $.ajax({
            type: 'DELETE',
            url: url,
            success: function (data) {
                if (data.success) {
                    toastr.success(data.message);
                    dataTable.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        });
    })
}

function Detalles(url) {
    var activo;
    $.ajax({
        type: "GET",
        url: url,
        dataType: "json",
        success: function (data) {
            console.log(data.data[0])

            $("#cuerpoModalVentaProducto").html(`

                <ul class="list-group">
                    <h3 class="text-center">Datos Venta</h3>
                    <img src="${data.data[0].productos.urlImagen}" style="height: 200px; width: 100%" alt="Alternate Text" />
                    <br /> 
                    <li class="list-group list-group-item" > <span class="font-weight-bold">Producto: </span> ${data.data[0].productos.nombre} </li>
                    <li class="list-group list-group-item" > <span class="font-weight-bold">Cliente: </span> ${data.data[0].cliente} </li>
                    <li class="list-group list-group-item" > <span class="font-weight-bold">Descrición: </span> ${data.data[0].descripcion}</li>
                    <li class="list-group list-group-item" > <span class="font-weight-bold">Cantidad: </span> ${data.data[0].cantidad}</li>
                    <li class="list-group list-group-item" > <span class="font-weight-bold">Precio: </span> ${data.data[0].productos.precio_venta}</li>
                    <li class="list-group list-group-item" > <span class="font-weight-bold">Total: </span> ${data.data[0].productos.precio_venta * data.data[0].cantidad}</li>

                </ul>
            `);
         
        }
    });
    $("#vistaVenta").modal("show");
}
