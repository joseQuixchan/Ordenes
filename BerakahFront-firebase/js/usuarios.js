

function ObtenerUsuarios(){
  $("#tablaUsuarios td").remove();
  var settings = {
      "url": UrlApi + "Usuario",
      "method": "Get",
      "timeout": 0,
      "headers": {
        "Authorization": "Bearer " + token.token
      },
    };
    
    $.ajax(settings).done(function (response) {
      $.each(response, function(_index, data){
          if(data.rol.rolNombre == null){
            data.rol.rolNombre == "N/I"
          }
          var fila = "<tbody><tr><th scope='row'>" + data.usuarioId + 
          "</th><td>" + data.usuarioUsuario +
          "</td><td>" + data.usuarioNombre +
          "</td><td>" + data.usuarioApellido +
          "</td><td>" + data.usuarioCorreo +
          "</td><td>" + data.usuarioTelefono +
          "</td><td>" + data.rol.rolNombre +
          "</td><td><button type='button' class='btn btn-primary m-0' onClick='ObtenerDatosUsuario("+ data.usuarioId +") , Obtenerroles(1)' data-toggle='modal' data-target='#ModalActualizarUsuario'><i class='fa fad fa-edit'></i></button>" +
          "</td><td><button type='button' class='btn btn-danger m-0' onClick='EliminarUsuario("+ data.usuarioId +")'><i class='fa fad fa-ban'></i></button></tr></tbody>";
          $(fila).appendTo("#tablaUsuarios");
      });
    });
}

function ObtenerDatosUsuario(UsuarioId){
  ObtenerUsuario(UsuarioId);
}


function AgregarUsuario(){
  if($("#UsuarioUsuario").val() != "" || $("#UsuarioPass").val() != "" || $("#UsuarioNombre").val() != ""
  || $("#UsuarioApellido").val() !="" || $("#UsuarioCorreo").val() != "" || $("#UsuarioTelefono").val() != ""){
    var settings = {
      "url": UrlApi  + "Usuario/Registro",
      "method": "POST",  
      "timeout": 0,
      "headers": {
        "Authorization": "Bearer " + token.token,
        "Content-Type": "application/json"
      },
      "data": JSON.stringify({
          "usuario": $("#UsuarioUsuario").val(),
          "usuarioPass": $("#UsuarioPass").val(),
          "nombre": $("#UsuarioNombre").val(),
          "apellido": $("#UsuarioApellido").val(),
          "correo": $("#UsuarioCorreo").val(),
          "telefono": $("#UsuarioTelefono").val(),
          "usuarioRolId": $("#Roles").val(),
          "estado": true
      }),
      
    };
    
    $.ajax(settings).done(function (response) {      
        if(response==1){
          Swal.fire({
            icon: 'success',
            title: 'Usuario Agregado',
            showConfirmButton: false,
            timer: 2000
            })
            LimpiarFormulario();
            LimpiarTabla();
            ObtenerUsuarios();
        }else{
          Swal.fire({
            icon: 'error',
            title: response,
            showConfirmButton: false,
            timer: 2000
          })
        }
    }); 
  }else{
    Swal.fire({
      icon: 'error',
      title: 'Todos los datos son requeridos',
      showConfirmButton: false,
      timer: 2000
    })
  }
}

function ObtenerUsuario(UsuarioId){
  var settings = {
      "url": UrlApi + "Usuario/" + UsuarioId,
      "method": "Get",
      "timeout": 0,
      "headers": {
        "Authorization": "Bearer " + token.token,
        "Content-Type": "application/json"
      },
    };
    
    $.ajax(settings).done(function (response) {
        LimpiarFormulario();
        $("#UsuarioId").val(UsuarioId);
        $("#UsuarioUsuarioA").val(response.usuarioUsuario);
        $("#UsuarioNombreA").val(response.usuarioNombre);
        $("#UsuarioApellidoA").val(response.usuarioApellido);
        $("#UsuarioCorreoA").val(response.usuarioCorreo);
        $("#UsuarioTelefonoA").val(response.usuarioTelefono);
        var roles = "<option value= '"+ response.rol.rolId + "' selected>" + response.rol.rolNombre + "</option>";
        $(roles).appendTo("#RolesA");
    }); 
}

function Obtenerroles(Modifica){
  $('#Roles option').remove();
  $('#RolesA option').remove();
  var settings = {
      "url": UrlApi + "Rol",
      "method": "Get",
      "timeout": 0,
      "headers": {
        "Authorization": "Bearer " + token.token,
      },
    };
    if(Modifica != 1){
      $.ajax(settings).done(function (response) {
        $.each(response, function(_index, data){
          var roles = "<option value= '"+ data.rolId + "' >" + data.rolNombre + "</option>";
          $(roles).appendTo("#Roles");
        });
      }); 
    }
    
    $.ajax(settings).done(function (response) {
      $.each(response, function(_index, data){
        var roles = "<option value= '"+ data.rolId + "' >" + data.rolNombre + "</option>";
        $(roles).appendTo("#RolesA");
      });
    }); 
}

function ActualizarUsuario(){
  var settings = {
      "url": UrlApi  + "Usuario/ActualizarUsuario",
      "method": "Put",  
      "timeout": 0,
      "headers": {
        "Authorization": "Bearer " + token.token,
        "Content-Type": "application/json"
      },
      "data": JSON.stringify({
          "usuarioId": $("#UsuarioId").val(),
          "usuarioUsuario": $("#UsuarioUsuarioA").val(),
          "usuarioNombre": $("#UsuarioNombreA").val(),
          "usuarioApellido": $("#UsuarioApellidoA").val(),
          "usuarioCorreo": $("#UsuarioCorreoA").val(),
          "usuarioTelefono": $("#UsuarioTelefonoA").val(),
          "UsuarioRolId": $("#RolesA").val(),
          "usuarioWstado": true
      }),
      
    };
    
    $.ajax(settings).done(function (response) {
        if(response>0){
          Swal.fire({
            icon: 'success',
            title: 'Usuario Actualizado',
            showConfirmButton: false,
            timer: 2000
            })
            LimpiarTabla();
            ObtenerUsuarios();
        }else{
          Swal.fire({
            icon: 'error',
            title: response,
            showConfirmButton: false,
            timer: 2000
            })
        }
    }); 
}

function EliminarUsuario(usuarioId){
  var settings = {
    "url": UrlApi + "Usuario/BorrarUsuario",
    "method": "Put",
    "timeout": 0,
    "headers": {
      "Authorization": "Bearer " + token.token,
      "Content-Type": "application/json"
    },
    "data": JSON.stringify({
      UsuarioId: usuarioId
  }),
  };

  $.ajax(settings).done(function (response) {
    if(response == 1){
      Swal.fire({
        title: 'Usuario eliminado',
        showConfirmButton: false,
        timer: 2000
        })
        LimpiarTabla();
        ObtenerUsuarios();
    }else{
      Swal.fire({
        icon: 'error',
        title: response,
        showConfirmButton: false,
        timer: 2000
        })
    }
  });
}

function LimpiarFormulario(){
  $("#UsuarioUsuario").val("");
  $("#UsuarioPass").val("");
  $("#UsuarioNombre").val("");
  $("#UsuarioApellido").val("");
  $("#UsuarioCorreo").val("");
  $("#UsuarioTelefono").val("");
  $("#UsuarioId").val(0);
}

function LimpiarTabla(){
  $("#tablaUsuarios").empty();
  var fila2 = "<thead><tr><th scope='col'>" + "#Id" + 
          "</th><th scope='col'>" + "Usuario" +
          "</th><th scope='col'>" + "Nombres" +
          "</th><th scope='col'>" + "Apellidos" +
          "</th><th scope='col'>" + "Correo" +
          "</td><th scope='col'>" + "Teléfono" +
          "</td><th scope='col'>" + "Editar" +
          "</td><th scope='col'>" + "Eliminar" +
          "<tr></thead>";
          $(fila2).appendTo("#tablaUsuarios"); 
}

