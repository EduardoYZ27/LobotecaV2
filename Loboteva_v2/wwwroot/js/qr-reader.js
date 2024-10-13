$(document).ready(function () {
    $("#startQrButton").click(function () {
        $("#reader").show(); // Muestra el lector QR

        // Inicializa el escáner
        const html5QrCode = new Html5Qrcode("reader");

        // Iniciar el escáner usando la cámara predeterminada
        html5QrCode.start(
            { facingMode: "user" }, // Usar la cámara frontal en laptops y dispositivos móviles
            {
                fps: 10, // Frames por segundo
                qrbox: { width: 200, height: 200 } // Tamaño del cuadro de escaneo reducido
            },
            function (qrCodeMessage) {
                // Éxito al escanear
                $("#resultado").text("Código QR escaneado: " + qrCodeMessage);
                $("#scannedResult").val(qrCodeMessage); // Muestra el contenido en el cuadro de texto
            },
            function (errorMessage) {
                // Error de escaneo
                console.warn(`Error de escaneo: ${errorMessage}`);
            }
        ).catch(err => {
            console.error("Error al iniciar el escáner: ", err);
            alert("No se pudo iniciar el escáner. Asegúrate de que la cámara esté disponible.");
        });
    });
});
