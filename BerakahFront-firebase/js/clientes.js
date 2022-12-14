function ObtenerClientes(){
  $("#tablaUsuarios td").remove();
  var settings = {
      "url": UrlApi + "Cliente",
      "method": "Get",
      "timeout": 0,
      "headers": {
        "Authorization": "Bearer " + token.token
      },
    };
    
    $.ajax(settings).done(function (response) {
      $.each(response, function(_index, data){
          var fila = "<tbody><tr><th scope='row'>" + data.clienteId + 
          "</th><td>" + data.clienteNombre +
          "</td><td>" + data.clienteApellido +
          "</td><td>" + data.clienteTelefono +
          "</td><td>" + data.clienteCorreo +
          "</td><td>" + data.clienteNit +
          "</td><td>" + data.clienteDireccion +
          "</td><td><button type='button' class='btn btn-primary m-0' onClick='ObtenerDatosCliente("+ data.clienteId +")' data-toggle='modal' data-target='#ModalActualizarUsuario'><i class='fa fad fa-edit'></i></button>" +
          "</td><td><button type='button' class='btn btn-danger m-0' onClick='EliminarCliente("+ data.clienteId +")'><i class='fa fad fa-ban'></i></button></tr></tbody>";
          $(fila).appendTo("#tablaClientes");
      });
    });
}

function ObtenerDatosCliente(cleinteId){
  ObtenerCliente(cleinteId);
}


function AgregarCliente(){
  if($("#ClienteNombre").val() != ""){
    var settings = {
      "url": UrlApi  + "Cliente",
      "method": "POST",  
      "timeout": 0,
      "headers": {
        "Authorization": "Bearer " + token.token,
        "Content-Type": "application/json"
      },
      "data": JSON.stringify({
          "clienteNombre": $("#ClienteNombre").val(),
          "clienteApellido": $("#ClienteApellido").val(),
          "clienteTelefono": $("#ClienteTelefono").val(),
          "clienteCorreo": $("#ClienteCorreo").val(),
          "clienteNit": $("#ClienteNit").val(),
          "clienteDireccion": $("#ClienteDireccion").val(),
          "clienteEstado": true
      }),
      
    };
    $.ajax(settings).done(function (response) {
        if(response>0){
          Swal.fire({
            icon: 'success',
            title: 'Usuario Agregado',
            showConfirmButton: false,
            timer: 2000
            })
            LimpiarFormulario();
            LimpiarTabala();
            ObtenerClientes();
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
      title: 'El nombre es requerido',
      showConfirmButton: false,
      timer: 2000
      })
  }
}

function ObtenerCliente(ClienteId){
  var settings = {
      "url": UrlApi + "Cliente/" + ClienteId,
      "method": "Get",
      "timeout": 0,
      "headers": {
        "Authorization": "Bearer " + token.token,
        "Content-Type": "application/json"
      },
    };
    
    $.ajax(settings).done(function (response) {
        LimpiarFormulario();
        $("#ClienteId").val(ClienteId);
        $("#ClienteNombreA").val(response.clienteNombre),
        $("#ClienteApellidoA").val(response.clienteApellido),
        $("#ClienteTelefonoA").val(response.clienteTelefono),
        $("#ClienteCorreoA").val(response.clienteCorreo),
        $("#ClienteNitA").val(response.clienteNit),
        $("#ClienteDireccionA").val(response.clienteDireccion)
    }); 
}

function ActualizarCliente(){
  if($("#ClienteNombreA").val() != ""){
    var settings = {
      "url": UrlApi  + "Cliente/" + $("#ClienteId").val(),
      "method": "Put",  
      "timeout": 0,
      "headers": {
        "Authorization": "Bearer " + token.token,
        "Content-Type": "application/json"
      },
      "data": JSON.stringify({
          "clienteId": $("#ClienteId").val(),
          "clienteNombre": $("#ClienteNombreA").val(),
          "clienteApellido": $("#ClienteApellidoA").val(),
          "clienteTelefono": $("#ClienteTelefonoA").val(),
          "clienteCorreo": $("#ClienteCorreoA").val(),
          "clienteNit": $("#ClienteNitA").val(),
          "clienteDireccion": $("#ClienteDireccionA").val(),
          "clienteEstado": true
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
            LimpiarTabala();
            ObtenerClientes();
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
      title: 'El nombre es requerido',
      showConfirmButton: false,
      timer: 2000
      })
  }
  
}

function EliminarCliente(clienteId){
  var settings = {
    "url": UrlApi + "Cliente/BorrarCliente",
    "method": "Put",
    "timeout": 0,
    "headers": {
      "Authorization": "Bearer " + token.token,
      "Content-Type": "application/json"
    },
    "data": JSON.stringify({
      clienteId: clienteId
  }),
  };

  $.ajax(settings).done(function (response) {
    if(response == 1){
      Swal.fire({
        title: 'Cliene eliminado',
        showConfirmButton: false,
        timer: 2000
        })
        LimpiarTabala();
        ObtenerClientes();
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

function LimpiarTabala(){
  $("#tablaClientes").empty();
  var fila2 = "<thead><tr><th scope='col'>" + "#Id" + 
          "</th><th scope='col'>" + "Nombres" +
          "</th><th scope='col'>" + "Apellidos" +
          "</td><th scope='col'>" + "Tel??fono" +
          "</th><th scope='col'>" + "Correo" +
          "</th><th scope='col'>" + "Nit" +
          "</th><th scope='col'>" + "Dirrecci??n" +
          "</td><th scope='col'>" + "Editar" +
          "</td><th scope='col'>" + "Eliminar" +
          "<tr></thead>";
          $(fila2).appendTo("#tablaClientes");
}


