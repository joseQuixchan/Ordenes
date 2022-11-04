function ObtenerMenus(){
    
    if(!localStorage.getItem("token")){
        location.href = "../404.html";
    }else if(usuarioPerfil.usuarioCambioPass == true){
        location.href = "../404.html";
    }
    var settings = {
        "url": UrlApi + "Usuario/MenusPorUsuario",
        "method": "Get",
        "timeout": 0,
        "headers": {
          "Authorization": "Bearer " + token.token
        },
      };
      
      $.ajax(settings).done(function (response) {
        if(response != null){
            $.each(response, function(_index, data){
            
                var fila = "<a href=/html/"+ data.menuNombre +".html " + 
                "class='nav-item nav-link '>" +
                "<i class='" + data.menuImagen +
                " me-2'></i>" + data.menuNombre +
                "</a>"
                $(fila).appendTo("#Menus");
            });
            
        }else{
            console.log("response")
            Swal.fire({
                icon: 'error',
                title: 'Error al cargar cliente',
                showConfirmButton: false,
                timer: 2000
            })
        }
        
      });
}



function CambiarPasword(){
    var pass1 = $("#Password1").val()
    var pass2 = $("#Password2").val()

    if(pass1 == pass2){
        var settings = {
            "url": UrlApi + "Usuario/ActualizarPassword",
            "method": "Put",
            "timeout": 0,
            "headers": {
              "Authorization": "Bearer " + token.token,
              "Content-Type": "application/json"
            },
            "data": JSON.stringify({
                "passwordNueva": $("#Password1").val()
            }),
          };
          
          $.ajax(settings).done(function (response) {
            if(response == 1){
                Swal.fire({
                  icon: 'success',
                  title: 'Contraseña actualizada!',
                  showConfirmButton: false,
                  timer: 2000
                  })
                  location.href = "dashboard.html";
                  BorrarLocalStorage();
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
            title: 'Las contraseñas no son iguales',
            showConfirmButton: false,
            timer: 2000
        })
    }
}

function CambiarPaswordUsuario(){
    var pass1 = $("#Password1").val()
    var pass2 = $("#Password2").val()  
    if(pass1 == pass2){
        var settings = {
            "url": UrlApi + "Usuario/ActualizarPasswordUsuario",
            "method": "Put",
            "timeout": 0,
            "headers": {
              "Authorization": "Bearer " + token.token,
              "Content-Type": "application/json"
            },
            "data": JSON.stringify({
                "passwordNueva": $("#Password1").val(),
                "passwordVieja": $("#PasswordActual").val()
            }),
          };
          
          $.ajax(settings).done(function (response) {
            if(response == 1){
                Swal.fire({
                  icon: 'success',
                  title: 'Contraseña actualizada!',
                  showConfirmButton: false,
                  timer: 2000
                  })
                  limpiarFormulario();
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
            title: 'Las contraseñas no son iguales',
            showConfirmButton: false,
            timer: 2000
        })
    }
}


function limpiarFormulario(){
    $("#Password1").val(''),
    $("#Password2").val(''),
    $("#PasswordActual").val('')
}

function OrdenesD(){
    var totalHoy = 0;
    var totalventa;
    var settings = {
        "url": UrlApi + "Orden/OrdenesHoy",
        "method": "Get",
        "timeout": 0,
        "headers": {
          "Authorization": "Bearer " + token.token
        },
      };
      
      $.ajax(settings).done(function (response) {
        totalventa = response.length;
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
          "</tr>";
          totalHoy = totalHoy + data.total;
          $(OrdenList).appendTo("#tablaOrdenesD");

          var notificacion = "<a href='#' class='dropdown-item'><h6 class='fw-normal mb-0'>" + "Orden agregada: " + data.ordenId +
          "</h6><small>"+ data.clienteNombre + ", " + "Q." + data.total + ", "+"</small>" +
          "<small>" + ff.toLocaleDateString('en-GB') +
          "</small></a>";
          $(notificacion).appendTo("#notificaciones");
        });
        document.getElementById("totalHoy").innerHTML = "Q." + totalHoy;
        document.getElementById("totalventa").innerHTML = totalventa;
    });
  }

function ObtenerTareas(){
    var settings = {
        "url": UrlApi + "Tarea",
        "method": "Get",
        "timeout": 0,
        "headers": {
          "Authorization": "Bearer " + token.token
        },
      };
      
      $.ajax(settings).done(function (response) {
        totalventa = response.length;
        $.each(response, function(_index, data){
            let ff = new Date(data.tareaFechaCreacion);
            var tareas = "<div class='d-flex align-items-center border-bottom py-2'>" +
            "<div class='w-100 ms-3'>" +
            "<div class='d-flex w-100 align-items-center justify-content-between'>" +
            "<span>" + data.tareaDescripcion + " || " + ff.toLocaleDateString('en-GB') + "</span>" +
            "<button class='btn btn-sm'  onclick='EliminarTareas(" + data.tareaId + ");'><i class='fa fa-times'></i></button>" + 
            "</div></div></div>";
            $(tareas).appendTo("#tareas");
        });
        
    });
}

function CrarTareas(){
    if($("#NuevaTarea").val() != ""){
        var settings = {
            "url": UrlApi  + "Tarea",
            "method": "POST",  
            "timeout": 0,
            "headers": {
              "Authorization": "Bearer " + token.token,
              "Content-Type": "application/json"
            },
            "data": JSON.stringify({
                "tareaDescripcion": $("#NuevaTarea").val(),
            }),
            
          };
          
          $.ajax(settings).done(function (response) {      
              if(response==1){
                Swal.fire({
                  title: 'Tarea agrgada',
                  showConfirmButton: false,
                  timer: 1500
                  })
                  limpiarTarea();
                  ObtenerTareas();
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

    }
    
}


function EliminarTareas(tareaId){
        var settings = {
            "url": UrlApi  + "Tarea/" + tareaId,
            "method": "Put",  
            "timeout": 0,
            "headers": {
              "Authorization": "Bearer " + token.token,
              "Content-Type": "application/json"
            },
          };
          $.ajax(settings).done(function (response) {
            limpiarTarea();
            ObtenerTareas();
          });
}

function limpiarTarea(){
    $("#NuevaTarea").val('')
    $(document).ready(function() { $("#tareas").find("div:gt(0)").remove(); });
}


(function ($) {
    "use strict";

    // Spinner
    var spinner = function () {
        setTimeout(function () {
            if ($('#spinner').length > 0) {
                $('#spinner').removeClass('show');
            }
        }, 1);
    };
    spinner();
    
    
    // Back to top button
    $(window).scroll(function () {
        if ($(this).scrollTop() > 300) {
            $('.back-to-top').fadeIn('slow');
        } else {
            $('.back-to-top').fadeOut('slow');
        }
    });
    $('.back-to-top').click(function () {
        $('html, body').animate({scrollTop: 0}, 1500, 'easeInOutExpo');
        return false;
    });


    // Sidebar Toggler
    $('.sidebar-toggler').click(function () {
        $('.sidebar, .content').toggleClass("open");
        return false;
    });

    // Calender
    $('#calender').datetimepicker({
        inline: true,
        format: 'L'
    });
    
})(jQuery);

