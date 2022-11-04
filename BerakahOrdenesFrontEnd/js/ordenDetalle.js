var pepa = [];
var id = 0;

function ObtenerClientesyProductos(){
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
          var clinte = "<option value= '"+ data.clienteId + "' >" + data.clienteNombre + " " + data.clienteApellido +"</option>";
          $(clinte).appendTo("#selectClientes");
      });
    });

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
          var produdto = "<option value= '"+ data.productoId + "' >" + data.productoNombre + "</option>";
          $(produdto).appendTo("#producto");
      });
    });
}

function agregarProductos(){
  try{
    if($("#cantidad").val() == null || $("#cantidad").val() == 0){
      $("#cantidad").val(1)
    }
    var fila = "<tbody id='" + id + "'><tr><th scope='row'>" + id + 
          "</th><td>" + $("#ProductoNombre").val() +
          "</th><td>" + document.getElementById("descripcion").value +
          "</td><td>" + $("#cantidad").val() +
          "</td><td>" + "Q." + $("#precio").val() +
          "</td><td>" + "Q." + ( $("#precio").val() * $("#cantidad").val()) +
          "</td><td><button type='button'  class='btn btn-danger m-0'><i class='fa fad fa-ban' onClick='quitarProductos("+ id +")'></i></button></tr></tbody>";
          $(fila).appendTo("#tablaProductos");
          

    var subTotal = ($("#subTotal").val() * 1 + $("#precio").val() * $("#cantidad").val());  
    $("#subTotal").val(subTotal);
    $("#abono").val(0);

    var items = {
        "correlativo": id,
        "cantidad": $("#cantidad").val(),
        "descripcion": $("#descripcion").val(),
        "nombreProducto": $("#ProductoNombre").val(),
        "precioUniario": $("#precio").val(),
        "ordenDetalleEstado": true,
    }
    pepa.push(items);
    id++;
    console.log(pepa);
    LimpiarFormulario();
    
  }catch(error){
    console.error(error);
  }
}

function quitarProductos(id){

  pepa = pepa.filter(item => item.correlativo != id);
  $("#" + id).remove();
  
  console.log(pepa);
}


function agregarOrden(){
  if($("#fecha").val() != ""){
  
    var settings = {
      "url": UrlApi  + "Orden",
      "method": "POST",  
      "timeout": 0,
      "headers": {
        "Authorization": "Bearer " + token.token,
        "Content-Type": "application/json"
      },
      "data": JSON.stringify({
          "clienteNombre": $("#ClienteNombre").val(),
          "usuarioNombre": usuarioPerfil.usuarioUsuario,
          "clienteNit": $("#nit").val(),
          "clienteCorreo": $("#correo").val(),
          "clienteDireccion": $("#direccion").val(),
          "clienteTelefono": $("#telefono").val(),
          "abono": $("#abono").val(),
          "ordenDetalles": pepa,
          "ordenEstado": true,
          "ordenFechaEntrega": $("#fecha").val()
      }),
      
      
    };
    
    $.ajax(settings).done(function (response) {
        if(response==1){
          Swal.fire({
            icon: 'success',
            title: 'Orden creada correctamente',
            showConfirmButton: false,
            timer: 2500
            })
            LimpiarFormulario2();
        }else{
          Swal.fire({
            icon: 'error',
            title: response,
            showConfirmButton: false,
            timer: 2500
            })
        }
    }); 
  }else{
    Swal.fire({
      title: "Es necesario que ingrese una fecha de entrega",
      showConfirmButton: false,
      timer: 2500
      })
  }
  
}

function getCliente(Cliente){
  var settings = {
      "url": UrlApi + "Cliente/" + Cliente.value,
      "method": "Get",
      "timeout": 0,
      "headers": {
        "Authorization": "Bearer " + token.token,
        "Content-Type": "application/json"
      },
    };
    
    $.ajax(settings).done(function (response) {
        $("#ClienteNombre").val(response.clienteNombre + " " + response.clienteApellido),   
        $("#telefono").val(response.clienteTelefono),
        $("#correo").val(response.clienteCorreo),
        $("#nit").val(response.clienteNit),
        $("#direccion").val(response.clienteDireccion)
    }); 
}

function getProducto(Producto)
{
    var settings = {
        "url": UrlApi + "Producto/" + Producto.value,
        "method": "Get",
        "timeout": 0,
        "headers": {
          "Authorization": "Bearer " + token.token,
          "Content-Type": "application/json"
        },
      };
      
      $.ajax(settings).done(function (response) {
        if(response){
          $("#ProductoNombre").val(response.productoNombre),
          $("#descripcion").val(response.productoDescripcion)
          $("#precio").val(response.productoPrecio)
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
  $("#cantidad").val("");
  $("#precio").val("");
  $("#descripcion").val("");
}

function LimpiarFormulario2(){
  $("#cantidad").val("");
  $("#precio").val("");
  $("#descripcion").val("");
  $("#descripcion").val("");
  $("#nit").val("");
  $("#correo").val("");
  $("#direccion").val("");
  $("#telefono").val("");
  $("#fecha").val("");
  $("#selectClientes").val("Nombre del Cliente");
  $("#producto").val("Producto");
  document.getElementById("subTotal").value = "";
  LimpiarTabla();
}

function LimpiarTabla(){
  $("#tablaProductos").empty();
  var fila2 = "<thead><tr><th scope='col'>" + "Id" + 
          "</th><th scope='col'>" + "Producto" +
          "</th><th scope='col'>" + "Descripción" +
          "</th><th scope='col'>" + "Cantidad" +
          "</th><th scope='col'>" + "Precio unitario" +
          "</td><th scope='col'>" + "Total" +
          "</td><th scope='col'>" + "Eliminar" +
          "<tr></thead>";
          $(fila2).appendTo("#tablaProductos"); 
}

