var dataTable;
var idProduc;

$(document).ready(function () {
    cargarDatatable();
    
});


function cargarDatatable() {
    dataTable = $("#tblCompra").DataTable({
        "ajax": {
            "url": "/admin/Compra/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
           
            { "data": "proveedor.nombreEmpresa", "width": "20%" },
            { "data": "producto.nombre", "width": "10%" },
            { "data": "cantidad", "width": "10%" },
            { "data": "precio_Compra", "width": "20%" },
            {
                "data": {
                    "data1": "precio_Compra",
                    "data2": "cantida"
                },
                "render": function (data) {
                    return `${data["precio_Compra"] * data["cantidad"]}`;
                }, "width": "10%"
                },
            
            {
                "data": "idCompra",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/Compra/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer; width:80px;margin:0%;">
                                <i class="far fa-edit"></i>Editar
                                </a>
                                &nbsp;
                                <a onclick=Delete("/Admin/Compra/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer; width:80px;margin:0%;">
                                <i class="far fa-trash-alt"></i>Borrar
                                </a>
                                &nbsp;
                                 <a onclick=Detalles("/Admin/Compras/Detalles/${data}")  class='btn btn-info text-white' style='cursor:pointer; width:100px;'>
                            <i class='fa fa-eye'></i> Vista
                            </a>
                            </div>
                            `;
                }, "width": "30%"
            }
        ],
        "language": {
            "decimal": "",
            "emptyTable": "No hay registros",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
            "infoFiltered": "(Filtrado de _MAX_ total entradas)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
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
            console.log(data);

            for (var i = 0; i < data.listaProductos.length; i++) {
                if (data.listaProductos[i].value == data.compra.productoId) {
                    var producto = data.listaProductos[i].text;
                }
            }

            for (var i = 0; i < data.listaProveedores.length; i++) {
                if (data.listaProveedores[i].value == data.compra.proveedorId) {
                    var proveedor = data.listaProveedores[i].text;
                }
            }


            $("#cuerpoModalCompra").html(`
                <ul class="list-group">
                    <li class="list-group-item list-group-item-action"> <span class="font-weight-bold">Descripciòn: </span> ${data.compra.descripcion}</li>
                    <li class="list-group-item list-group-item-action"> <span class="font-weight-bold">Proveedor: </span> ${proveedor}</li>
                    <li class="list-group-item list-group-item-action"> <span class="font-weight-bold">Producto: </span> ${producto}</li>
                    <li class="list-group-item list-group-item-action"> <span class="font-weight-bold">Cantidad: </span> ${data.compra.cantidad}</li>
                    <li class="list-group-item list-group-item-action"> <span class="font-weight-bold">Precio Compra: </span> ${data.compra.precio_Compra}</li>
                    <li class="list-group-item list-group-item-action"> <span class="font-weight-bold">Total: </span> ${data.compra.cantidad * data.compra.precio_Compra}</li>
                    <li class="list-group-item list-group-item-action"> <span class="font-weight-bold">Fecha: </span> ${data.compra.fecha_compra}</li>
                </ul>



            `);

        }
    });
    $("#vistaCompra").modal("show");
}


function ProductosAutoComplete() {
    var todosProductos = [];
    $.ajax({
        type: "GET",
        url: "/Admin/Compra/GetAllProductos",
        dataType: "json",
        success: function (data) {
          
            for (var i = 0; i < data.length; i++) {
                todosProductos.push(data[i].nombre + " - codigo: " + data[i].idProductos)
            }
           
            $("#SeleccionProducto").autocomplete({
                source: todosProductos
            });
        }
    });

}

function cambiodeProducto() {
    var valor = document.querySelector("#SeleccionProducto").value.split(":");
    //console.log(valor[1])
    //console.log(valor[1].trim())
    document.querySelector("#idProducto").value = valor[1].trim();
}