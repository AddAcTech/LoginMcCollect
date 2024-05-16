document.addEventListener("DOMContentLoaded", function () {
    const Enviar = document.getElementById("BtnIngresar");

    const NumIntentos = () => {
        let intentos = sessionStorage.getItem("intentos");
        if (!intentos) {
            sessionStorage.setItem("intentos", 1);
        }
        else if (intentos == 4) {
            Enviar.disabled = true;
            alert("Demasiados intentos, intente más tarde");
        }
        intentos++
        sessionStorage.setItem("intentos", intentos);
    }

    Enviar.addEventListener("click", NumIntentos);
});
