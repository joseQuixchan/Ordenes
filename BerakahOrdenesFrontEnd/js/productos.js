function ObtenerProductos(){
  $("#tablaProveedores td").remove();
  var settings = {
      "url": UrlApi + "Producto",
      "method": "Get",
      "timeout": 0,
      "headers": {
        "Authorization": "Bearer " + token.token
      },
    };
    
    $.ajax(settings).done(function (response) {
      $.each(response, function(_index, data){
          if(data.productoPrecio == null){
            data.productoPrecio = "N/I";
          }
          var fila = "<tbody><tr><th scope='row'>" + data.productoId + 
          "</th><td>" + data.productoNombre +
          "</td><td>" + data.productoDescripcion +
          "</td><td>" + "Q." + data.productoPrecio +
          "</td><td><button type='button' class='btn btn-primary m-0' onClick='ObtenerDatosProveedor("+ data.productoId +")' data-toggle='modal' data-target='#ModalActualizarUsuario'><i class='fa fad fa-edit'></i></button>" +
          "</td><td><button type='button' class='btn btn-danger m-0' onClick='EliminarProducto("+ data.productoId +")'><i class='fa fad fa-ban'></i></button></tr></tbody>";
          $(fila).appendTo("#tablaProductos");
      });
    });
}

function ObtenerDatosProveedor(productoId){
  ObtenerProducto(productoId);
}


function AgregarProducto(){
  if($("#ProductoNombre").val() == "" ||  $("#ProductoDescripcion").val() == "" || $("#ProductoPrecio").val() == ""){
    Swal.fire({
      icon: 'error',
      title: 'Todos los campos son requeridos',
      showConfirmButton: false,
      timer: 2000
      })
  }else{
    var settings = {
      "url": UrlApi  + "Producto",
      "method": "POST",  
      "timeout": 0,
      "headers": {
        "Authorization": "Bearer " + token.token,
        "Content-Type": "application/json"
      },
      "data": JSON.stringify({
          "productoNombre": $("#ProductoNombre").val(),
          "productoDescripcion": $("#ProductoDescripcion").val(),
          "productoPrecio": $("#ProductoPrecio").val(),
          "productoEstado": true
      }),
      
    };
    $.ajax(settings).done(function (response) {
        if(response == 1){
          Swal.fire({
            icon: 'success',
            title: 'Producto Agregado',
            showConfirmButton: false,
            timer: 2000
            })
            LimpiarFormulario();
            LimpiarTabla();
            ObtenerProductos();
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
  
}

function ObtenerProducto(ProductoId){
  var settings = {
      "url": UrlApi + "Producto/" + ProductoId,
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
        $("#ProductoId").val(ProductoId);
        $("#ProductoNombreA").val(response.productoNombre),
        $("#ProductoDescripcionA").val(response.productoDescripcion)
        $("#ProductoprecioA").val(response.productoPrecio)
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

function ActualizarProducto(){
  if($("#ProductoNombreA").val() == "" ||  $("#ProductoDescripcionA").val() == "" || $("#ProductoPrecioA").val() == ""){
    Swal.fire({
      icon: 'error',
      title: 'Todos los campos son requeridos',
      showConfirmButton: false,
      timer: 2000
      })
  }else{
    var settings = {
      "url": UrlApi  + "Producto/" + $("#ProductoId").val(),
      "method": "Put",  
      "timeout": 0,
      "headers": {
        "Authorization": "Bearer " + token.token,
        "Content-Type": "application/json"
      },
      "data": JSON.stringify({
          "productoId": $("#ProductoId").val(),
          "productoNombre": $("#ProductoNombreA").val(),
          "productoDescripcion": $("#ProductoDescripcionA").val(),
          "productoPrecio": $("#ProductoPrecioA").val(),
      }),
      
    };
    
    $.ajax(settings).done(function (response) {
        if(response == 1){
          Swal.fire({
            icon: 'success',
            title: 'Producto Actualizado',
            showConfirmButton: false,
            timer: 2000
            })
            LimpiarTabla();
            ObtenerProductos();
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
  
}

function EliminarProducto(productoId){
  var settings = {
    "url": UrlApi + "Producto/BorrarProducto",
    "method": "Put",
    "timeout": 0,
    "headers": {
      "Authorization": "Bearer " + token.token,
      "Content-Type": "application/json"
    },
    "data": JSON.stringify({
      productoId: productoId
  }),
  };

  $.ajax(settings).done(function (response) {
    if(response == 1){
      Swal.fire({
        title: 'Producto eliminado',
        showConfirmButton: false,
        timer: 2000
        })
        LimpiarTabla();
        ObtenerProductos();
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
  $(document).ready(function() { $("#tablaProductos").find("tr:gt(0)").remove(); });
}


