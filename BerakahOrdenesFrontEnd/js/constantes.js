

//const UrlApi = "https://www.berakahMultinegocios.somee.com/api/"; 
//const UrlApi = "http://35.185.219.24/api/api/";  
//const UrlApi = "http://josequixchan-001-site1.ctempurl.com/site1/api/";
//const UrlApi = "http://josequixchan-001-site1.ctempurl.com/site1/api/";
const UrlApi = "http://localhost:5091/api/";

let token = JSON.parse(localStorage.getItem("token"));

let usuarioPerfil = JSON.parse(localStorage.getItem("usuario"));

document.getElementById("usuario").innerHTML = usuarioPerfil.usuarioUsuario;
document.getElementById("nombre").innerHTML = usuarioPerfil.usuarioNombre + " " + usuarioPerfil.usuarioApellido;
document.getElementById("nombre1").innerHTML = usuarioPerfil.usuarioNombre + " " + usuarioPerfil.usuarioApellido;

function BorrarLocalStorage(){
    location.href = "index.html";
    localStorage.removeItem("token");
    localStorage.removeItem("usuario");
    
}

/* function CambioPass(){
    if(usuarioPerfil.usuarioCambioPass == false){
        location.href = "signup.html";
    }else{
        location.href = "index.html";
    }
}  */