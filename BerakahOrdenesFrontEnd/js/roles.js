function ObtenerRoles(){
  var settings = {
      "url": UrlApi + "Rol",
      "method": "Get",
      "timeout": 0,
      "headers": {
        "Authorization": "Bearer " + token.token
      },
    };
    
    $.ajax(settings).done(function (response) {
      $.each(response, function(_index, data){
        
          var fila = "<tbody><tr><th scope='row'>" + data.rolId + 
          "</th><td>" + data.rolNombre +
          "</td><td>" + data.rolDescripcion +
          "</td><td><button type='button' class='btn btn-primary m-0' onClick='ObtenerDatosRol("+ data.rolId +")' data-toggle='modal' data-target='#ModalActualizarUsuario'><i class='fa fad fa-edit'></i></button>" +
          "</td><td><button type='button' class='btn btn-primary m-0' onClick='ObtenerDatosRol("+ data.rolId +"); ObtenerMenusRol("+ data.rolId +"); ObtenerMenuss();' data-toggle='modal' data-target='#ModalMenus'><i class='fa fad fa-list'></i></button>" +
          "</td><td><button type='button' class='btn btn-danger m-0' onClick='EliminarRol("+ data.rolId +")'><i class='fa fad fa-ban'></i></button></tr></tbody>";
          $(fila).appendTo("#tablaRoles");
      });
    });
}

function ObtenerDatosRol(rolId){
  ObtenerRol(rolId);
}


function AgregarRol(){
  if($("#RolNombre").val() == "" || $("#RolDescripcion").val() == ""){
    Swal.fire({
      icon: 'error',
      title: "EL nombre y descripccion son requeridos",
      showConfirmButton: false,
      timer: 2000
      })
  }else{
    var settings = {
      "url": UrlApi  + "Rol",
      "method": "POST",  
      "timeout": 0,
      "headers": {
        "Authorization": "Bearer " + token.token,
        "Content-Type": "application/json"
      },
      "data": JSON.stringify({
          "rolNombre": $("#RolNombre").val(),
          "rolDescripcion": $("#RolDescripcion").val(),
          "rolEstado": true
      }),
      
    };
    $.ajax(settings).done(function (response) {
        if(response == 1){
          Swal.fire({
            icon: 'success',
            title: 'Rol Agregado',
            showConfirmButton: false,
            timer: 2000
            })
            LimpiarFormulario();
            LimpiarTabla();
            ObtenerRoles();
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

function ObtenerRol(RolId){
  var settings = {
      "url": UrlApi + "Rol/" + RolId,
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
        $("#RolId").val(RolId);
        $("#RolNombreA").val(response.rolNombre),
        $("#RolDescripcionA").val(response.rolDescripcion)
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

function ActualizarRol(){
  if($("#RolNombreA").val() == "" || $("#RolDescripcionA").val() == ""){
    Swal.fire({
      icon: 'error',
      title: "El nombre y descripccion son requeridos",
      showConfirmButton: false,
      timer: 2000
      })
  }else{
    var settings = {
      "url": UrlApi  + "Rol/" + $("#RolId").val(),
      "method": "Put",  
      "timeout": 0,
      "headers": {
        "Authorization": "Bearer " + token.token,
        "Content-Type": "application/json"
      },
      "data": JSON.stringify({
          "rolId": $("#RolId").val(),
          "rolNombre": $("#RolNombreA").val(),
          "rolDescripcion": $("#RolDescripcionA").val(),
      }),
    };
    
    $.ajax(settings).done(function (response) {
        if(response == 1){
          Swal.fire({
            icon: 'success',
            title: 'Rol Actualizado',
            showConfirmButton: false,
            timer: 2000
            })
            LimpiarTabla();
            ObtenerRoles();
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

function ObtenerMenuss(){
  $('#menusRol option').remove();
  var settings = {
      "url": UrlApi + "Menu",
      "method": "Get",
      "timeout": 0,
      "headers": {
        "Authorization": "Bearer " + token.token
      },
    };
    
    $.ajax(settings).done(function (response) {
      $.each(response, function(_index, data){
        var menus = "<option value= '"+ data.menuId + "' >" + data.menuNombre + "</option>";
        $(menus).appendTo("#menusRol");
      });
    });
}

function ObtenerMenusRol(rolId){
  LimpiarTablaMenusUsuario();
  $("#RolIdAgregar").val(rolId);
  var settings = {
      "url": UrlApi + "RolMenu/" + rolId,
      "method": "Get",
      "timeout": 0,
      "headers": {
        "Authorization": "Bearer " + token.token
      },
    };
    
    $.ajax(settings).done(function (response) {
      $.each(response, function(_index, data){
        var fila = "<tbody><tr><td>" + data.menu.menuNombre + 
          "</td><td>" + data.menu.menuDescripcion +
          "</td><td><button type='button' class='btn btn-primary m-0' onClick='CargarPermisos("+ data.rolMenuId +")' data-toggle='modal' data-target='#ModalPermisos'><i class='fa fad fa-edit'></i></button>" +
          "</td><td><button type='button' class='btn btn-danger m-0' onClick='QuitarRolMenu("+ data.rolId +" , "+ data.menuId +")'><i class='fa fad fa-ban'></i></button></tr></tbody>";
          $(fila).appendTo("#tablaMenus");
      });
    });
}

function CargarPermisos(rolMenuId){
  var settings = {
    "url": UrlApi + "RolMenu/MenuRol/",
    "method": "Get",
    "timeout": 0,
    "headers": {
      "Authorization": "Bearer " + token.token,
      "Content-Type": "application/json"
    },
    "data": ({
       rolMenuId
  }),
  };

  $.ajax(settings).done(function (response) {
    $("#NombreMenu").val(response.menu.menuNombre);
    $("#RolMenuPermisoId").val(response.rolMenuId);
    $("#Crear").prop("checked", response.agregar);
    $("#Modificar").prop("checked", response.modificar);
    $("#Consultar").prop("checked", response.consultar);
    $("#Eliminar").prop("checked", response.eliminar);
  });
}

function ActualizarPermisos(){
  var settings = {
    "url": UrlApi  + "RolMenu/Permisos/",
    "method": "Put",  
    "timeout": 0,
    "headers": {
      "Authorization": "Bearer " + token.token,
      "Content-Type": "application/json"
    },
    "data": JSON.stringify({
        "rolMenuId": $("#RolMenuPermisoId").val(),
        "agregar": $("#Crear").prop("checked"),
        "modificar": $("#Modificar").prop("checked"),
        "consultar": $("#Consultar").prop("checked"),
        "eliminar": $("#Eliminar").prop("checked")
    }),
    
  };
  $.ajax(settings).done(function (response) {
      if(response == 2){
        Swal.fire({
          icon: 'error',
          title: 'Algo Salio mal, contacta al administrador',
          showConfirmButton: false,
          timer: 2000
          })
          LimpiarFormulario();
          LimpiarTabla();
          ObtenerRoles();
      }else{
        Swal.fire({
          icon: 'success',
          title: 'Permisos Actualizados',
          timer: 2000
          })
        CargarPermisos($("#RolMenuPermisoId").val());
      }
  }); 
}

function AgregarRolMenu(){
  var settings = {
      "url": UrlApi  + "RolMenu",
      "method": "POST",  
      "timeout": 0,
      "headers": {
        "Authorization": "Bearer " + token.token,
        "Content-Type": "application/json"
      },
      "data": JSON.stringify({
          "rolId": $("#RolIdAgregar").val(),
          "menuId": $("#menusRol").val(),
      }),
      
    };
    $.ajax(settings).done(function (response) {
        if(response == 2){
          Swal.fire({
            icon: 'error',
            title: 'Algo Salio mal, contacta al administrador',
            showConfirmButton: false,
            timer: 2000
            })
        }else{
            LimpiarTablaMenusUsuario();
            ObtenerMenusRol($("#RolIdAgregar").val());
        }
    }); 
}

function QuitarRolMenu(rolId, menuId){
  var settings = {
      "url": UrlApi  + "RolMenu",
      "method": "Put",  
      "timeout": 0,
      "headers": {
        "Authorization": "Bearer " + token.token,
        "Content-Type": "application/json"
      },
      "data": JSON.stringify({
          "rolId": rolId,
          "menuId": menuId,
      }),
      
    };
    $.ajax(settings).done(function (response) {
        if(response == 2){
          Swal.fire({
            icon: 'error',
            title: 'Algo Salio mal, contacta al administrador',
            showConfirmButton: false,
            timer: 2000
            })
            LimpiarFormulario();
            LimpiarTabla();
            ObtenerRoles();
        }else{
            LimpiarTablaMenusUsuario();
            ObtenerMenusRol(rolId);
        }
    }); 
}


function EliminarRol(rolId){
  var settings = {
    "url": UrlApi + "Rol/BorrarRol",
    "method": "Put",
    "timeout": 0,
    "headers": {
      "Authorization": "Bearer " + token.token,
      "Content-Type": "application/json"
    },
    "data": JSON.stringify({
      rolId: rolId
  }),
  };

  $.ajax(settings).done(function (response) {
    if(response == 1){
      Swal.fire({
        title: 'Rol eliminado',
        showConfirmButton: false,
        timer: 2000
        })
        LimpiarTabla();
        ObtenerRoles();
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
  $("#RolNombre").val("");
  $("#RolDescripcion").val("");
}

function LimpiarTabla(){
  $(document).ready(function() { $("#tablaRoles").find("tr:gt(0)").remove(); });
}

function LimpiarTablaMenusUsuario(){
  $(document).ready(function() { $("#tablaMenus").find("tr:gt(0)").remove(); });
}


