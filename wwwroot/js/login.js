function showPassword() {
    var tagInput = document.getElementById('password');
    var button = document.getElementById("btn-pass");
    button.innerHTML = '<i style="color: #57557A;" class="fa-solid fa-eye-slash"></i>'
    console.warn(button);
    if (tagInput.type === "password")
        tagInput.type = "text";
    else
        tagInput.type = "password";
}
