function redirectToLogin() {
    window.location.href = "/Home/Login";
}

document.getElementById("form-register").addEventListener("submit", function (e) {
    console.warn("hereeeeee");
    e.preventDefault();
    var userData = {
        Name: this.Name.value,
        Age: this.Age.value,
        Email: this.Email.value,
        Password: this.Password.value,
    }
     registerUser(userData);
});


async function registerUser(data) {
    const myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");
    try
    {
        const response = await fetch("/api/User/RegisterUser", {
            method: 'POST',
            body: JSON.stringify(data),
            headers: myHeaders
        });

        if (response.ok) {
            window.location.href = '/Home/Login'
        }
        //if (!response.ok) {
        //    // Display warning message
        //}

    } catch (error) {
        console.error("Error: \n" + error);
    }
}