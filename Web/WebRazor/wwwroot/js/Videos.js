document.addEventListener('DOMContentLoaded', function () {
    var addToListModal = document.getElementById('addToListModal');
    addToListModal.addEventListener('show.bs.modal', function (event) {
        var button = event.relatedTarget;
        var videoId = button.getAttribute('data-video-id');
        var videoIdInput = document.getElementById('videoIdInput');
        videoIdInput.value = videoId;
    });
});
