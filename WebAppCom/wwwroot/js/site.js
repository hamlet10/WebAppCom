
getOrigen();
getProduct();


//Metodo GET Llama al modal para agregar
showInPopup = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');
        }
    })
}

//Metodo POST Agrega
jQueryAjaxPostmodal = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $('#view-all').html(res.html)
                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    $('#form-modal').modal('hide');
                    Swal.fire(
                        'Bien!',
                        'Se guardó satisfactoriamente!',
                        'success'
                    ),
                        location.reload();
                    Init("#view-all");
                }
                else
                    $('#form-modal .modal-body').html(res.html);
            },
            error: function (err) {
                console.error(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

//Metodo POST para eliminar
function deleteItem(id, router) {
    Swal.fire({
        title: 'Estas Seguro?',
        text: "Esta Acción no se podra revertir!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        cancelButtonText: 'Cancelar',
        confirmButtonText: 'Si, Eliminar!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                url: router,
                data: { 'id': id },
                success: function (data) {
                    if (data) {
                        location.reload();
                        Swal.fire(
                            'Eliminado!',
                            'El registro ha sido eliminada.',
                            'success'
                        )
                    } else {
                        Swal.fire({
                            icon: 'error',
                            title: 'No se puede Eliminar',
                            text: 'Este registro esta ligado!',
                        })
                    }
                }
            })
        }
    })
}

//Metodo POST para eliminar Productos
function deleteProduct(id, router) {
    Swal.fire({
        title: 'Estas Seguro?',
        text: "Deseas eliminar el Codigo de Barras de este Producto!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        cancelButtonText: 'Cancelar',
        confirmButtonText: 'Si, Eliminar Producto!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                url: router,
                data: { 'id': id },
                success: function (data) {
                    if (data) {
                        location.href = '/Products/Index';
                        Swal.fire(
                            'Eliminado!',
                            'El Codigo de barras del Producto.',
                            'success'
                        )
                    } else {
                        Swal.fire({
                            icon: 'error',
                            title: 'No se puede Eliminar',
                            html: 'Verifique lo siguiente: <br />-si el producto tiene Movimientos. <br />-Codigo de Barras. <br />-paramentros de Bodega.',
                        })
                    }
                }
            })
        }
    })
}

//Metodo POST para eliminar BodegasProductos
function deleteBP(bId, pId, router) {
    Swal.fire({
        title: 'Estas Seguro?',
        text: "Deseas eliminar los Parametros de Bodega!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        cancelButtonText: 'Cancelar',
        confirmButtonText: 'Si, Eliminar!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                url: router,
                data: { 'bId': bId, 'pId': pId },
                success: function (data) {
                    if (data) {
                        location.reload();
                        Swal.fire(
                            'Eliminado!',
                            'Se han eliminado los Parametros de Bodegas.',
                            'success'
                        )
                    } else {
                        Swal.fire({
                            icon: 'error',
                            title: 'No se puede Eliminar',
                            text: 'Este Parametro de Bodega tiene Movimientos!',
                        })
                    }
                }
            })
        }
    })
}

//Metodo POST Agrega
jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    location.reload();
                    Swal.fire(
                        'Bien!',
                        'Se guardó satisfactoriamente!',
                        'success'
                    )
                }
                else {
                    Swal.fire(
                        'Error!',
                        res.message,
                        'warning'
                    )
                }
            },
            error: function (err) {
                console.error(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

$(".money").mask('000,000,000.00', { reverse: true });
$('.identify').mask('000-0000000-0', { reverse: false });
$('.mDate').mask('00/00/0000');
$('.phone').mask('(000) 000-0000');


//Seleccion de Departamentos
function getOrigen() {
    $("#SelectDpto").select2({
        placeholder: "Select",
        theme: "bootstrap4",
        allowClear: true,
        ajax: {
            url: "/Base/Getdepartment",
            contentType: "application/json; charset=utf-8",
            data: function (params) {
                var query =
                {
                    term: params.term,
                };
                return query;
            },
            processResults: function (result) {
                return {
                    results: $.map(result, function (item) {
                        return {
                            id: item.description,
                            text: item.description
                        };
                    }),
                };
            }
        }
    });
}


//Seleccion de Bodega
function getBodegas() {
    $("#selectBodegas").select2({
        placeholder: "Select",
        theme: "bootstrap4",
        allowClear: true,
        ajax: {
            url: "/Base/GetBodegas",
            contentType: "application/json; charset=utf-8",
            data: function (params) {
                var query =
                {
                    term: params.term,
                };
                return query;
            },
            processResults: function (result) {
                return {
                    results: $.map(result, function (dpItem) {
                        return {
                            id: dpItem.bodegaId,
                            text: dpItem.description
                        };
                    }),
                };
            }
        }
    });
}


//Seleccion de Product
function getProduct() {
    $("#SelectProduct").select2({
        placeholder: "Select",
        theme: "bootstrap4",
        allowClear: true,
        ajax: {
            url: "/Base/GetProducts",
            contentType: "application/json; charset=utf-8",
            data: function (params) {
                var query =
                {
                    term: params.term,
                };
                return query;
            },
            processResults: function (result) {
                return {
                    results: $.map(result, function (item) {
                        return {
                            id: item.description,
                            text: item.description
                        };
                    }),
                };
            }
        }
    });
}


$.datepicker.regional['es'] = {
    closeText: 'Cerrar',
    prevText: '<Ant',
    nextText: 'Sig>',
    currentText: 'Hoy',
    monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
    monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
    dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
    dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
    dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
    weekHeader: 'Sm',
    dateFormat: 'dd/mm/yy',
    firstDay: 1,
    isRTL: false,
    showMonthAfterYear: false,
    yearSuffix: ''
};

$.datepicker.setDefaults($.datepicker.regional['es']);

$('#datepicker').datepicker();


$('#dp1,#dp2').datetimepicker({
    format: 'MM/DD/YYYY',
});

//Full Datatable
$('.settingsTable').DataTable();

//DataTable Personalizada
var table = $('.myTable').DataTable({
    "destroy": true,
    "searching": true,
    "info": true,
    "autoWidth": false,
    "ordering": false,
    "responsive": true,
    "autoFill": true,
    "language": {
        "url": "/plugins/datatables/lang/Spanish.json"
    },
    dom:
        '<"top"Bf>rt<"bottom"ip><"clear">',
    buttons: {
        buttons: [
            {
                extend: "excel",
                text: 'Exportar a Excel',
                title: 'MINISTERIO DE MEDIO AMBIENTE Y RECURSOS NATURALES',
                messageTop: 'VISITANTES DE LA INSTITUCION',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6]
                }
            },
            {
                extend: "pdf",
                text: 'Exportar a Pdf',
                title: 'MINISTERIO DE MEDIO AMBIENTE Y RECURSOS NATURALES',
                messageTop: 'VISITANTES DE LA INSTITUCION',
                orientation: 'portrait',
                exportOptions: {
                    columns: [0, 1, 2, 4, 5, 6]
                },
                customize: function (win) {
                    win.defaultStyle.fontSize = 8;

                }
            }
        ]
    },
    columnDefs: [
        { responsivePriority: 1, targets: 0 },
        { responsivePriority: 2, targets: -1 },
        { responsivePriority: 3, targets: -2 },
        { responsivePriority: 4, targets: -3 },
        { responsivePriority: 5, targets: -4 },
        { responsivePriority: 6, targets: -5 },
        { responsivePriority: 7, targets: -6 }
    ],
    select: true
});