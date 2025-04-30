function showPassword() {
    var tagInput = document.getElementById('password');
    var button = document.getElementById("btn-pass");
    var isPressed = button.getAttribute("aria-pressed")

    if (isPressed === "false") {
        button.setAttribute("aria-pressed", "true")
        button.innerHTML = '<i style="color: #57557A;" class="fa-solid fa-eye-slash"></i>'
        tagInput.type = "text";
    } else {
        button.setAttribute("aria-pressed", "false")
        button.innerHTML = '<i style="color: #57557A;" class="fa-solid fa-eye"></i>'
        tagInput.type = "password";
    }
}