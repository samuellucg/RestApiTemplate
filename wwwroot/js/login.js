document.getElementById("form-login").addEventListener("submit", function (e) {
    e.preventDefault();
    var userData = {
        "Email": this.Email.value,
        "Password": this.Password.value
    }
    loginUser(userData);
})

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

async function loginUser(data) {
    const myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");
    try {
        const response = await fetch("/api/User/LoginUser", {
            method: 'POST',
            body: JSON.stringify(data),
            headers: myHeaders
        });
        //console.warn(response)
        if (response.ok) {
            window.location.href = '/Home/Dashboard'
        }
        if (!response.ok) {
            setTimeout(() =>
                $("#warn-message").css("display", "flex"), 1);
            setTimeout(() =>
                $("#warn-message").css("display", "none"), 10000);

            throw new Error(await response.text());
        }
    }
    catch (error) {
        console.error("Error: \n" + error);
    }
}

async function slabIdChanges(data) {
    closedModal()
    const myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");
    try {
        const response = await fetch("/Batches/UpdateSlabId", {
            method: 'PUT',
            body: JSON.stringify(data),
            headers: myHeaders
        });

        if (!response.ok) throw new Error(await response.text());
        openModal("Success", "Your change are saved successfuly!", "Ok", redirectToHome);
    }
    catch (error) {
        openModal("Error", "One error has ocurred, " + error, "Ok", closedModal);
        console.error('Error: \n' + error);
    }

}

