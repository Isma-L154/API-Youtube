function confirmDeletion(event, button) {
    event.preventDefault();
    bootbox.confirm({
        title: "Confirmar Eliminación",
        message: "¿Estás seguro de que quieres eliminar este video de la lista?",
        buttons: {
            cancel: {
                label: 'Cancelar',
                className: 'btn-secondary'
            },
            confirm: {
                label: 'Eliminar',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            if (result) {
                // Si el usuario confirma, enviar el formulario
                button.closest('form').submit();            }
        }
    });
}

function ObtenerIdURL() {
    const urlParams = new URLSearchParams(window.location.search);
    return urlParams.get('id'); 
}
document.addEventListener("DOMContentLoaded", function () {
    const hiddenInputs = document.querySelectorAll('.hiddenIdLista');
    const idLista = ObtenerIdURL();

    if (idLista) {
        hiddenInputs.forEach(input => {
            input.value = idLista;
        });
    } else {
        console.warn("ID de la lista no encontrado en la URL.");
    }
});