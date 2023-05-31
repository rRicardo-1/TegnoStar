var dataTable;

$(document).ready(function () {
    cargarDatatable();
    
});


function cargarDatatable() {
    dataTable = $("#tblProductos").DataTable({
        "ajax": {
            "url": "/admin/productos/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
           
            { "data": "nombre", "width": "20%" },
            { "data": "precio", "width": "10%" },
            { "data": "stock", "width": "10%" },
            { "data": "categoria.nombre", "width": "20%" },
            
            {
                "data": "idProducto",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/Productos/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer; width:80px;margin:0%;">
                                <i class="far fa-edit"></i>Editar
                                </a>
                                &nbsp;
                                <a onclick=Delete("/Admin/Productos/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer; width:80px;margin:0%;">
                                <i class="far fa-trash-alt"></i>Borrar
                                </a>
                                &nbsp;
                                 <a onclick=Detalle("/Admin/Productos/Detalle/${data}") class="btn btn-info text-white" data-toggle="modal" data-target="#Detalle2" style="cursor:pointer; width:80px; margin:4%;">
                                <i class="far fa-eye"></i>Detalles
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
    });
}

function Detalle(url) {
    console.log(url);
    $.ajax({
        type: 'GET',
        url: url,
        dataType: 'json',
        success: function (data) {
            for (var i = 0; i < data.listaCategorias.length; i++) {
                if (data.listaCategorias[i].value == data.productos.categoriaId) {
                    var categoria = data.listaCategorias[i].text;
                }
            }

            $("#cuerpoModalProducto").html(`
                  <ul class="list-group" style= "color:white">
                      <li class="list-group-item"style= "color:white"> Imagen: <img src="${data.productos.urlImagen}" width='100% class="img-fluid scale-down" ' /></li>
                      <li class="list-group-item" style= "color:white">${data.productos.nombre}</li>
                      <li class="list-group-item" style= "color:white">${data.productos.descripcion}</li>
                       <li class="list-group-item" style= "color:white">${data.productos.precio}</li>
                       <li class="list-group-item" style= "color:white">${data.productos.stock}</li>
                       <li class="list-group-item" style= "color:white">${categoria}</li>
                  </ul>`
            );

            $("#Detalle2").modal("show")
        }

    });
}