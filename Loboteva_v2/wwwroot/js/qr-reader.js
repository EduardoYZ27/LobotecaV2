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
                if (codigoCarrera === "898989") {
                    // Si es el código del admin
                    alert("Bienvenido al panel de administrador");
                    window.location.href = "/Admin/Admin"; // Redirigir al panel del admin
                } else {
                    // Validar las carreras
                    const carreraValida = Object.keys(carreras).find(carrera => carreras[carrera].codigo === codigoCarrera);

                    if (carreraValida) {
                        alert(`Bienvenido a la carrera de ${carreraValida}`);
                        // Guardar la carrera seleccionada en localStorage
                        localStorage.setItem("carreraSeleccionada", carreraValida);
                        // Oculta el lector QR después de escanear
                        $("#reader").hide();
                        // Redirigir directamente a la URL de la carrera
                        const urlCarrera = carreras[carreraValida].url; // Obtén la URL de la carrera
                        window.location.href = urlCarrera; // Redirige a la URL de la carrera
                    } else {
                        alert("Matrícula errónea, intenta de nuevo.");
                    }
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

    // Definir las carreras y sus códigos y URL
    const carreras = {
        "Ingeniería en Tecnologías de la Información": { codigo: "132137", url: "/Tecnologias/Tecnologias" },
        "Ingeniería Industrial": { codigo: "222222", url: "/Industri/Industri" },
        "Ingeniería Mecánica": { codigo: "333333", url: "/Mecanica/Mecanica" },
        "Ingeniería Mecatronica": { codigo: "444444", url: "/Mecatronica/Mecatronica" },
        "Licenciatura en Negocios": { codigo: "555555", url: "/Negocios/Negocios" },
        "Ingeniería en Energia": { codigo: "666666", url: "/Energia/Energia" },
        "Ingeniería en Biotecnologia": { codigo: "777777", url: "/Biotecnologia/Biotecnologia" }
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
});
