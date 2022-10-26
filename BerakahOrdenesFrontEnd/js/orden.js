function Ordenes(){
  var settings = {
      "url": UrlApi + "Orden",
      "method": "Get",
      "timeout": 0,
      "headers": {
        "Authorization": "Bearer " + token.token
      },
    };
    
    $.ajax(settings).done(function (response) {
      $.each(response, function(_index, data){
        let ff = new Date(data.ordenFechaCreacion);
        let fff = new Date(data.ordenFechaEntrega);
        if(data.clienteTelefono == null){
          data.clienteTelefono = "N/I"
        }
        if(data.clienteNit == null){
          data.clienteNit = "N/I"
        }
        
        var OrdenList = "<tr><th>" + data.clienteNombre + 
        "</th><td>" + data.clienteTelefono +
        "</th><td>" + data.clienteNit +
        "</td><td >" + ff.toLocaleDateString('en-GB') +
        "</td><td>" + fff.toLocaleDateString('en-GB')  +
        "</td><td>" + "Q." + data.total +
        "</td><td>" + data.usuarioNombre +
        "</td><td style='display: flex; justify-content: space-evenly;'><button type='button' class='btn btn-primary m-0' onclick='Ocultar("+ data.ordenId +")'>Detalles</button>" +
        "<button type='button' class='btn btn-primary m-0'  onclick='GenerarPdf(" + data.ordenId + ")'><i class='fas fad fa-print'></i></button>" +
        "</tr>";
        $(OrdenList).appendTo("#tablaOrdenes");
      });
    });
}

function GenerarPdf(ordenId){
  window.open(UrlApi + "Orden/GenerarPdf?ordenId=" + ordenId, "_blank");
}

function Ocultar(ordenId){
  LimpiarTabla();
  var settings = {
    "url": UrlApi + "Orden/OrdenDetalles?ordenId=" + ordenId,
    "method": "Get",
    "timeout": 0,
    "headers": {
      "Authorization": "Bearer " + token.token
    },
  };

  $.ajax(settings).done(function (response) {
    $.each(response, function(_index, data){
      
      let ff = new Date(data.ordenDetalleFechaCreacion);
      if(data.descripcion == null){
        data.descripcion = "N/I"
      }
      var OrdenDetalleList = "<tr><th>" + data.nombreProducto + 
      "</th><td>" + data.descripcion +
      "</th><td>" + "Q." + data.precioUniario +
      "</td><td >" + data.cantidad +
      "</td><td>" + "Q." + data.precioUniario * data.cantidad +
      "</td><td>" + ff.toLocaleDateString('en-GB')  +
      "</td></tr>";
      $(OrdenDetalleList).appendTo("#tablaDetalles");
    });
  });

  $("#Ordenes").hide();
  $("#Detalles").show();

}

function OcultarDetalles(){
  $("#Detalles").hide();
}

function RegresarOrdenes(){
  $("#Ordenes").show();
  $("#Detalles").hide();
}

function LimpiarTabla(){
  $(document).ready(function() { $("#tablaDetalles").find("tr:gt(0)").remove(); });

}

