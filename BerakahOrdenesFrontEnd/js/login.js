function iniciodesesion(){
    var settings = {
        "url": UrlApi + "Usuario/Login",
        "method": "POST",
        "timeout": 0,
        "headers": {
          "Content-Type": "application/json"
        },
        "data": JSON.stringify(
        {
          "Usuario":$("#Usuario").val(),
          "UsuarioPass":$("#Password").val(),}),
       };
       
       $.ajax(settings).done(function (response) {
        console.log(response);
         if(!response.token){
            Swal.fire({
              icon: 'error',
              title: response,
              timer: 2300,
            })
          }else{
            localStorage.setItem( "token", JSON.stringify(response.token))
            localStorage.setItem( "usuario", JSON.stringify(response.usuario))
            Redirigir(response.usuario.usuarioCambioPass);
          }
    }); 
}

function CambioPass(cambioPass){
  console.log(cambioPass)
  if(cambioPass == true){
      location.href = "CambioPassword.html";
  }else{
      location.href = "dashboard.html";
  }
} 

function Redirigir(cambioPass){
    CambioPass(cambioPass)
}