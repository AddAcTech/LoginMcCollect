document.addEventListener("DOMContentLoaded", function () {
    const Enviar = document.getElementById("BtnIngresar");

    const NumIntentos = () => {
        let intentos = localStorage.getItem("intentos");
        if (!intentos) {
            localStorage.setItem("intentos", 1);
        }
        else if (intentos == 4) {
            Enviar.disabled = true;
            alert("Demasiados intentos, intente más tarde");
        }
        intentos++
        localStorage.setItem("intentos", intentos);
    }

    Enviar.addEventListener("click", NumIntentos);
});
