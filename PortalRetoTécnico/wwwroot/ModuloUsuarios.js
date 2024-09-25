

//variable global para controlar la asignación de la URL del API, en caso de cambiar solo será necesario ajustar en el AppSettings
var urlApiUsuarios=$("#urlApiUsuarios").val();

// Inicializa la tabla DataTable
function initDataTable() {
    var table =  $('#TablaLista').DataTable({
        //processing: true,
        ajax: {
            url: urlApiUsuarios,
            type: 'GET',
            dataSrc: "",
            beforeSend: function () {
                // Mostrar el GIF de carga
                $("#progress").show();
            },
        },
        columns: [
            { data: "idusuario" },
            { data: "nombre" },
            { data: null, render: dateRenderer },
            { data: "usuario" },
            { data: null, render: passwordRenderer },
            { data: "estatus" },
            { data: null, render: actionsRenderer }
        ]
    });
    // También puedes ocultar el GIF cuando la tabla termine de procesar los datos.
    table.on('xhr', function () {
        $("#progress").hide();
    });
}

// Renderiza la fecha formateada
function dateRenderer(data, type, row) {
    const date = new Date(row.fechacreacion);
    return date.getDate()+'-'+(date.getMonth() + 1)+'-'+date.getFullYear();
}

// Renderiza el campo de contraseña con botón para mostrar/ocultar
function passwordRenderer(data, type, row) {
    return '<span class="password" data-password="'+row.password+'">*****</span>'+
        '<button class="toggle-password"><i class="fa fa-eye"></i></button>';
}

// Renderiza el estatus del usuario
function statusRenderer(data, type, row) {
    return row.estatus==1 ? 'Activo' : 'Inactivo';
}

// Renderiza los botones de acciones
function actionsRenderer(data, type, row) {
    return '<button class="btnEdit btn btn-sm btn-warning" data-id="'+row.idusuario+'">Editar</button> ' +
        '<button class="btnDelete btn btn-sm btn-danger" data-id="'+row.idusuario+'">Eliminar</button>';
}

// Resetea un formulario dado
function resetForm(formId) {
    $(formId)[0].reset();
    $('.text-danger').remove();
}

// Muestra el modal
function showModal(modalId) {
    $(modalId).modal('show');
}

// Carga los datos del usuario en el formulario de edición
function loadUserData(userId) {
  
    $.ajax({
        url: urlApiUsuarios+userId,
        type: 'GET',
        success: function (data) {
           
            $('#idusuario').val(data.idusuario);
            $('#nombre').val(data.nombre);           
            $('#usuario').val(data.usuario);
            $('#password').val(data.password);
            $('#fechacreacion').val(data.fechacreacion);
            $('#estatus').val(data.idestatus);
            showModal('#UserModal');
        },
        error: function (err) {
            console.error('Error obteniendo el usuario:', err);
        }
    });
}

// Maneja el submit del formulario
function handleFormSubmit(formId) {
    $(formId).on('submit', function (e) {
        e.preventDefault();
        //Verificamos que acción se esta ejecutando si es agregar o actualizar (POST o PUT).
        var action = $('#Action').val();     

        var urlApi = urlApiUsuarios;
        if (action == "PUT") {
            urlApi = urlApiUsuarios + $('#idusuario').val();
            var Usuario = {
                idusuario: $('#idusuario').val(),
                nombre: $('#nombre').val(),               
                usuario1: $('#usuario').val(),
                password: $('#password').val(),
                estatus: $('#estatus').val(),
                fechacreacion: $('#fechacreacion').val()
            };
        } else {
            var Usuario = {              
                nombre: $('#nombre').val(),
                usuario1: $('#usuario').val(),
                password: $('#password').val(),
                estatus: parseInt($('#estatus').val()),
                fechacreacion: $('#fechacreacion').val(),
                idperfil:1
            };

           
        }     
     
        $.ajax({
            url: urlApi,
            type: action,
            contentType: 'application/json',
            data: JSON.stringify(Usuario),
            success: function () {
                $('#UserModal').modal('hide');
                alert('Usuario ' + (action=="PUT"?'actualizado':'agregado')+' exitosamente');
                $('#TablaLista').DataTable().ajax.reload();
            },
            error: handleAjaxError
        });
    });
}

// Lógica para eliminar usuario
function deleteUser(userId) {
    if (confirm('¿Estás seguro de que deseas eliminar este usuario?')) {
        $("#progress").show();
        $.ajax({
            url: urlApiUsuarios + userId,
            type: 'DELETE',
            success: function () {
                alert('Usuario eliminado exitosamente');
                $('#TablaLista').DataTable().ajax.reload();
            },
            error: function (err) {
                console.error('Error eliminando el usuario:', err);
                $("#progress").hide();
            }
        });
    }
}

// Muestra u oculta la contraseña
function togglePasswordVisibility() {
    const row = $(this).closest('tr');
    const passwordField = row.find('.password');
    const currentValue = passwordField.text();

    if (currentValue === '*****') {
        passwordField.text(passwordField.data('password'));
        $(this).find('i').removeClass('fa-eye').addClass('fa-eye-slash');
    } else {
        passwordField.text('*****');
        $(this).find('i').removeClass('fa-eye-slash').addClass('fa-eye');
    }
}

// Para manejar los errores de AJAX de las acciones Agregar o editar que se lanzan desde el API.
function handleAjaxError(err) {
   
    // Limpiar mensajes de error previos
    $('.text-danger').remove();

    // Obtener la respuesta de error del servidor
    var response = err.responseJSON.errors;

    // Mostrar los errores debajo de cada campo
    if (response.Nombre) {
        $('#nombre').after('<span class="text-danger">' + response.Nombre[0] + '</span>');
    }

    if (response.Usuario1) {
        $('#usuario').after('<span class="text-danger">' + response.Usuario1[0] + '</span>');
    }
    if (response.Password) {
        $('#password').after('<span class="text-danger">' + response.Password[0] + '</span>');
    }
}


