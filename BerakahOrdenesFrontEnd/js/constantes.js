

const UrlApi = "https://win5040.site4now.net:8172/MsDeploy.axd?site=josequixchan-001-site1/api/";

let token = JSON.parse(localStorage.getItem("token"));

let usuarioPerfil = JSON.parse(localStorage.getItem("usuario"));

document.getElementById("usuario").innerHTML = usuarioPerfil.usuarioUsuario;
document.getElementById("nombre").innerHTML = usuarioPerfil.usuarioNombre + " " + usuarioPerfil.usuarioApellido;
document.getElementById("nombre1").innerHTML = usuarioPerfil.usuarioNombre + " " + usuarioPerfil.usuarioApellido;

function BorrarLocalStorage(){
    location.href = "signin.html";
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