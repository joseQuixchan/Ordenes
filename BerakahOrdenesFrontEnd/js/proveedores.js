function ObtenerProveedores(){
  $("#tablaProveedores td").remove();
  var settings = {
      "url": UrlApi + "Proveedor",
      "method": "Get",
      "timeout": 0,
      "headers": {
        "Authorization": "Bearer " + token.token
      },
    };
    
    $.ajax(settings).done(function (response) {
      $.each(response, function(_index, data){
          var fila = "<tbody><tr><th scope='row'>" + data.proveedorId + 
          "</th><td>" + data.proveedorNombre +
          "</td><td>" + data.proveedorTelefono +
          "</td><td>" + data.proveedorCorreo +
          "</td><td>" + data.proveedorNit +
          "</td><td>" + data.proveedorDireccion +
          "</td><td><button type='button' class='btn btn-primary m-0' onClick='ObtenerDatosProveedor("+ data.proveedorId +")' data-toggle='modal' data-target='#ModalActualizarUsuario'><i class='fa fad fa-edit'></i></button>" +
          "</td><td><button type='button' class='btn btn-danger m-0'><i class='fa fad fa-ban'></i></button></tr></tbody>";
          $(fila).appendTo("#tablaProveedores");
      });
    });
}

function ObtenerDatosProveedor(proveedorId){
  ObtenerProveedor(proveedorId);
}


function AgregarProveedor(){
  var settings = {
      "url": UrlApi  + "Proveedor",
      "method": "POST",  
      "timeout": 0,
      "headers": {
        "Authorization": "Bearer " + token.token,
        "Content-Type": "application/json"
      },
      "data": JSON.stringify({
          "proveedorNombre": $("#ProveedorNombre").val(),
          "proveedorTelefono": $("#ProveedorTelefono").val(),
          "proveedorCorreo": $("#ProveedorCorreo").val(),
          "proveedorNit": $("#ProveedorNit").val(),
          "proveedorDireccion": $("#ProveedorDireccion").val(),
          "proveedorEstado": true
      }),
      
    };
    $.ajax(settings).done(function (response) {
        if(response>0){
          Swal.fire({
            icon: 'success',
            title: 'Proveedor Agregado',
            showConfirmButton: false,
            timer: 2000
            })
            LimpiarFormulario();
            LimpiarTabla();
            ObtenerProveedores();
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

function ObtenerProveedor(ProveedorId){
  var settings = {
      "url": UrlApi + "Proveedor/" + ProveedorId,
      "method": "Get",
      "timeout": 0,
      "headers": {
        "Authorization": "Bearer " + token.token,
        "Content-Type": "application/json"
      },
    };
    
    $.ajax(settings).done(function (response) {
      if(response){
        LimpiarFormulario();
        $("#ProveedorId").val(ProveedorId);
        $("#ProveedorNombreA").val(response.proveedorNombre),
        $("#ProveedorTelefonoA").val(response.proveedorTelefono),
        $("#ProveedorCorreoA").val(response.proveedorCorreo),
        $("#ProveedorNitA").val(response.proveedorNit),
        $("#ProveedorDireccionA").val(response.proveedorDireccion)
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

function ActualizarProveedor(){
  var settings = {
      "url": UrlApi  + "Proveedor/" + $("#ProveedorId").val(),
      "method": "Put",  
      "timeout": 0,
      "headers": {
        "Authorization": "Bearer " + token.token,
        "Content-Type": "application/json"
      },
      "data": JSON.stringify({
          "proveedorId": $("#ProveedorId").val(),
          "proveedorNombre": $("#ProveedorNombreA").val(),
          "proveedorTelefono": $("#ProveedorTelefonoA").val(),
          "proveedorCorreo": $("#ProveedorCorreoA").val(),
          "proveedorNit": $("#ProveedorNitA").val(),
          "proveedorDireccion": $("#ProveedorDireccionA").val(),
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
            ObtenerProveedores();
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
  $("#ProveedorNombre").val("");
  $("#ProveedorTelefono").val("");
  $("#ProveedorCorreo").val("");
  $("#ProveedorNit").val("");
  $("#ProveedorDireccion").val("");
}

function LimpiarTabla(){
  $("#tablaProveedores").empty();
  var fila2 = "<thead><tr><th scope='col'>" + "#Id" + 
          "</th><th scope='col'>" + "Nombres" +
          "</td><th scope='col'>" + "Teléfono" +
          "</th><th scope='col'>" + "Correo" +
          "</th><th scope='col'>" + "Nit" +
          "</th><th scope='col'>" + "Dirrección" +
          "</td><th scope='col'>" + "Editar" +
          "</td><th scope='col'>" + "Eliminar" +
          "<tr></thead>";
          $(fila2).appendTo("#tablaProveedores");
}


