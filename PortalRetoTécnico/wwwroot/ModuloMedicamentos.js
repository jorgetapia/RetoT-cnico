

//variables global para controlar la asignación de la URL del API, en caso de cambiar solo será necesario ajustar en el AppSettings
var urlApiMedicamentos = $("#urlApiMedicamentos").val();

var urlApiFormas = $("#urlApiFormasFarmaceuticas").val();

// Inicializamos la tabla DataTable
function initDataTable() {
  
    var table =    $('#TablaLista').DataTable({       
        ajax: {
            url: urlApiMedicamentos,
            type: 'GET',
            dataSrc: "",
            beforeSend: function () {
                
                $("#progress").show();
            },
        }, 
        columns: [
            { data: "id" },
            { data: "nombre" },
            { data: "concentracion" },
            { data: "precio" },
            { data: "presentacion" },
            { data: "formasFarmaceuticas" },
            { data: "stock" },
            { data: "estatus" },
            { data: null, render: actionsRenderer }
        ]
    });
    table.on('xhr', function () {
        $("#progress").hide();
    });
}


// Renderiza el estatus del medicamento
function statusRenderer(data, type, row) {
    return row.estatus ? 'Activo' : 'Inactivo';
}

// Renderizamos los botones de acciones
function actionsRenderer(data, type, row) {
    return '<button class="btnEdit btn btn-sm btn-warning" data-id="' + row.id +'">Editar</button> ' +
        '<button class="btnDelete btn btn-sm btn-danger" data-id="' + row.id+'">Eliminar</button>';
}

// Reseteamos un formulario dado
function resetForm(formId) {
    $(formId)[0].reset();   
    $('.text-danger').remove();
}

// Muestramos el modal
function showModal(modalId) {
    $(modalId).modal('show');
}

function loadFormaFarmaceuticas(idForma) {  
    $("#idformafarmaceutica").html("");
    $.ajax({
        type: "GET",        
        url: urlApiFormas,
        success: function (data) {
            $("#idformafarmaceutica").append(
                $('<option>', {
                    value: '',
                    text: 'Seleccione',

                }));
            // Iteramos sobre el arreglo de datos json que regresa el API
            $.each(data, function (k, v) {
                var selected = (idForma == v.idformafarmaceutica ? "selected": "");
                
                $("#idformafarmaceutica").append('<option value="' + v.idformafarmaceutica + '" ' + selected +'>' + v.nombre + '</option>'); 
                
            });
        },
        dataType: "JSON"


    })
}

// Carga los datos del medicamento en el formulario de edición
function loadMedicamentoData(Idmedicamento) {
  
    $.ajax({
        url: urlApiMedicamentos + Idmedicamento,
        type: 'GET',
        success: function (data) {

            $('#idmedicamento').val(data.id);
            $('#nombre').val(data.nombre);
            $('#concentracion').val(data.concentracion);
            $('#precio').val(data.precio);
            $('#stock').val(data.stock);
            $('#presentacion').val(data.presentacion);
            $('#idformafarmaceutica').val(data.idFormaFarmaceutica);       
            $('#bhabilitado').val(data.idEstatus);
                     


            showModal('#MedicamentoModal');
        },
        error: function (err) {
            console.error('Error obteniendo el medicamento:', err);
        }
    });
}

// Maneja el submit de un formulario
function handleFormSubmit(formId) {
    $(formId).on('submit', function (e) {
        e.preventDefault();

        //Verificamos que acción se esta ejecutando si es agregar o actualizar (POST o PUT).
        var action = $('#Action').val();

        var urlApi = urlApiMedicamentos;
        var Medicamento = {};
        if (action == "PUT") {
            urlApi = urlApiMedicamentos + $('#idmedicamento').val();
             Medicamento = {
                 idmedicamento: $('#idmedicamento').val(),
                 nombre: $('#nombre').val(),
                 concentracion: $('#concentracion').val(),
                 precio: parseFloat($('#precio').val()),
                 stock: parseInt($('#stock').val()),
                 presentacion: $('#presentacion').val(),
                 bhabilitado: $('#bhabilitado').val(),
                 idFormaFarmaceutica: parseInt($('#idformafarmaceutica').val(), 10)

               
            };
        } else {
             Medicamento = {
                 nombre: $('#nombre').val(),
                 concentracion: $('#concentracion').val(),
                 precio: parseFloat($('#precio').val()),
                 stock: parseInt($('#stock').val()),
                 presentacion: $('#presentacion').val(),
                 bhabilitado: $('#bhabilitado').val(),
                 idFormaFarmaceutica: parseInt($('#idformafarmaceutica').val(), 10)
            };
        }              

        $.ajax({
            url: urlApi,
            type: action,
            contentType: 'application/json',
            data: JSON.stringify(Medicamento),
            success: function () {
                $('#MedicamentoModal').modal('hide');
                alert('Medicamento agregado exitosamente');
                $('#TablaLista').DataTable().ajax.reload();
            },
            error: handleAjaxError
        });
    });
}

// Lógica para eliminar medicamento
function deleteMedicamento(userId) {
    if (confirm('¿Estás seguro de que deseas eliminar este medicamento?')) {
        $("#progress").show();
        $.ajax({
            url: urlApiMedicamentos + userId,
            type: 'DELETE',
            success: function () {
                alert('medicamento eliminado exitosamente');
                $('#TablaLista').DataTable().ajax.reload();
            },
            error: function (err) {
                console.error('Error eliminando el medicamento:', err);
                $("#progress").hide();
            }
        });
    }
}


// Manejamos los errores de AJAX de Agregar y Editar
function handleAjaxError(err) {
   
     // Limpiamos los mensajes de error previos
    $('.text-danger').remove();

    // Obtenemos la respuesta de error del servidor
    var response = err.responseJSON.errors;

    // Mostramos los errores debajo de cada campo
    if (response.Nombre) {
        $('#nombre').after('<span class="text-danger">' + response.Nombre[0] + '</span>');
    }
    if (response.Concentracion) {
        $('#concentracion').after('<span class="text-danger">' + response.Concentracion[0] + '</span>');
    }
    if (response.Precio) {
        $('#precio').after('<span class="text-danger">' + response.Precio[0] + '</span>');
    }
    if (response.Stock) {
        $('#stock').after('<span class="text-danger">' + response.Stock[0] + '</span>');
    }
    if (response.Presentacion) {
        $('#presentacion').after('<span class="text-danger">' + response.Presentacion[0] + '</span>');
    }
    if (response.Idformafarmaceutica) {
        $('#Idformafarmaceutica').after('<span class="text-danger">' + response.Idformafarmaceutica[0] + '</span>');
    }
}
