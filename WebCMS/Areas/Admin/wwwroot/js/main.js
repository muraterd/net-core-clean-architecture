var toaster = $('#toaster');

toastr.options = {
    closeButton: true,
    debug: false,
    newestOnTop: false,
    progressBar: true,
    positionClass: "toast-top-right",
    preventDuplicates: false,
    onclick: null,
    showDuration: "300",
    hideDuration: "1000",
    timeOut: "5000",
    extendedTimeOut: "1000",
    showEasing: "swing",
    hideEasing: "linear",
    showMethod: "fadeIn",
    hideMethod: "fadeOut"
};

function showToaster(type, title, message) {
    switch (type) {
        case "success":
            return toastr.success(message, title);
        case "error":
            return toastr.error(message, title);
        case "warning":
            return toastr.warning(message, title);
        case "info":
            return toastr.info(message, title);
    }
}

function showErrorToaster(title, message) {
    toastr.error(message, title);
}

function showWarningToaster(title, message) {
    toastr.warning(message, title);
}