$(document).ready(function () {
    // Manejador de clic en el botón de inicio de sesión con QR
    $("#startQrButton").click(function () {
        $("#reader").show(); // Muestra el lector QR

        // Inicializa el escáner
        const html5QrCode = new Html5Qrcode("reader");

        // Iniciar el escáner usando la cámara predeterminada
        html5QrCode.start(
            { facingMode: "user" },
            {
                fps: 10,
                qrbox: { width: 250, height: 250 }
            },
            function (qrCodeMessage) {
                const matricula = qrCodeMessage.trim(); // Asegúrate de que no haya espacios en blanco
                const codigoCarrera = matricula.substring(0, 6); // Obtener los primeros 6 dígitos

                // Validar la matrícula
                const carreraValida = Object.keys(carreras).find(carrera => carreras[carrera] === codigoCarrera);

                if (carreraValida) {
                    alert(`Bienvenido a la carrera de ${carreraValida}`);
                    // Guardar la carrera seleccionada en localStorage
                    localStorage.setItem("carreraSeleccionada", carreraValida);
                    // Redirigir a la página de inicio
                    window.location.href = "/Inicio/Inicio"; // Asegúrate de que la URL esté correcta
                } else {
                    alert("Matrícula errónea, intenta de nuevo.");
                }
            },
            function (errorMessage) {
                console.warn(`Error de escaneo: ${errorMessage}`);
            }
        ).catch(err => {
            console.error("Error al iniciar el escáner: ", err);
            alert("No se pudo iniciar el escáner. Asegúrate de que la cámara esté disponible.");
        });
    });

    // Definir las carreras y sus códigos
    const carreras = {
        "Ingeniería en Tecnologías de la Información": "132137",
        "Ingeniería Industrial": "222222",
        "Ingeniería Mecánica": "333333",
        "Ingeniería Civil": "444444",
        "Ingeniería en Sistemas Computacionales": "555555",
        "Licenciatura en Administración": "666666",
        "Licenciatura en Psicología": "777777"
    };

    // Al cargar la página, verifica si hay una carrera seleccionada
    const carreraSeleccionada = localStorage.getItem("carreraSeleccionada");

    // Manejador de clic en el enlace de libros
    $('.libros-link').on('click', function (e) {
        e.preventDefault(); // Evita la navegación predeterminada
        if (!carreraSeleccionada) {
            alert("Por favor escanea tu código QR para acceder a los libros de tu carrera.");
        } else {
            // Redirigir a la página correspondiente de la carrera seleccionada
            const urlCarrera = carreras[carreraSeleccionada].url; // Obtén la URL de la carrera
            window.location.href = urlCarrera; // Redirige a la URL de la carrera
        }
    });

    // Opción para cerrar sesión y limpiar el localStorage
    document.getElementById("logoutButton").addEventListener("click", function () {
        localStorage.removeItem("carreraSeleccionada");
        alert("Has cerrado sesión.");
        window.location.href = "/"; // Redirigir a la página de inicio o login
    });
});
