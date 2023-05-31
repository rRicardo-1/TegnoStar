var dataTable;

$(document).ready(function () {
    cargarDatatable();
});


function cargarDatatable() {
    dataTable = $("#tblProveedor").DataTable({
        "ajax": {
            "url": "/admin/proveedores/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            /*{ "data": "idProveedor", "width": "5%" },*/
            { "data": "nombreEmpresa", "width": "10%" },
            { "data": "nombreContacto", "width": "10%" },
            { "data": "telefono", "width": "7%" },  
            { "data": "correoElectronico", "width": "20 %" }, 
            { "data": "direccion", "width": "10%" }, 
            {
                "data": "acivo",
                "render": function (data) {
                    if (data == true) {
                        return `<div  style="width: 100%; heigth: 100%; background: green; color:white;">Activo</div>`
                    } else {
                        return `<div  style="width: 100%; heigth: 100%; background: red; color:white;">No Activo</div>`
                    }
                }, "width": "5%"}, 
                
            {
                "data": "idProveedor",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/Proveedores/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer; width:80px; margin:0%;">
                                <i class="far fa-edit"></i>Editar
                                </a>
                                &nbsp;
                                <a onclick=Delete("/Admin/Proveedores/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer; width:80px;margin:0%;">
                                <i class="far fa-trash-alt"></i>Borrar
                                </a>
                                &nbsp;
                                <a onclick=Detalle("/Admin/Proveedores/Detalle/${data}") class="btn btn-info text-white" data-toggle="modal" data-target="#Detalle" style="cursor:pointer; width:80px; margin:4%;">
                                <i class="far fa-eye"></i>Detalles
                                </a>
                            </div>
                            `;
                }, "width": "20%"
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

function Detalle(url) {
    var activo

    $.ajax({
        type: 'GET',
        url: url,
        dataType: 'json',
        success: function (data) {

            if (data.acivo == true) {
                activo = "Activo"
            } else {
                activo="No Activo"
            }

            $("#cuerpoModalProveedor").html(`
                  <ul class="list-group" style= "color:white">
                      <li class="list-group-item" style= "color:white">${data.nombreEmpresa}</li>
                      <li class="list-group-item" style= "color:white">${data.nombreContacto}</li>
                       <li class="list-group-item" style= "color:white">${data.telefono}</li>
                       <li class="list-group-item" style= "color:white">${data.correoElectronico}</li>
                       <li class="list-group-item" style= "color:white">${data.direccion}</li>
                        <li class="list-group-item" style= "color:white">${activo}</li>
                  </ul>`
            );

            $("#Detalle").modal("show")
        }

        });
}