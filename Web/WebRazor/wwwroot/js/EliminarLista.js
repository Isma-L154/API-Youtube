function confirmDeletion() {
    bootbox.confirm({
        title: "Confirmar Eliminación",
        message: "¿Estás seguro de que quieres eliminar esta Lista?",
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
                document.getElementById('deleteForm').submit();
            }
        }
    });
}
