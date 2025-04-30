function redirectToLogin() {
    window.location.href = "/Home/Login";
}

document.getElementById("form-register").addEventListener("submit", function (e) {
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
    try {
        const response = await fetch("/api/Account/CreateUser", {
            method: 'POST',
            body: JSON.stringify(data),
            headers: myHeaders
        });

        if (response.ok) {
            setTimeout(() => {
                document.getElementById("message").innerHTML = "Sua conta foi registrada com sucesso! Aguarde 5 segundos para poder se logar.";
                $("message").css("color", "green");
                $("message").css("display", "flex");
            }, 1);

            setTimeout(() => {
                window.location.href = "/Home/Login";
            }, 5000);
        }

        if (response.status == 409) {
            setTimeout(() => {
                document.getElementById("message").innerHTML = "Não foi possível criar a sua conta, tente novamente mais tarde.";
                $("message").css("color", "red");
                $("message").css("display", "flex");
            }, 1);

            setTimeout(() => {
                $("message").css("display", "none");
            }, 10000);
        }

        else {
            setTimeout(() => {
                document.getElementById("message").innerHTML = "Erro desconhecido pelo desenvolvedor.";
                $("#message").css("color", "red");
                $("message").css("display", "flex");
            }, 1);

            setTimeout(() => {
                $("message").css("display", "none");
            }, 10000);
        }
    }
    catch (error) {
        console.error("Error: \n", error);
    }
}


//async function registerUser(data) {
//    const myHeaders = new Headers();
//    myHeaders.append("Content-Type", "application/json");
//    try {
//        const response = await fetch("/api/User/RegisterUser", {
//            method: 'POST',
//            body: JSON.stringify(data),
//            headers: myHeaders
//        });

//        if (response.ok) {
//            setTimeout(() => {
//                document.getElementById("message").innerHTML = "Sua conta foi registrada com sucesso! Aguarde 5 segundos para poder se logar.";
//                $("#message").css("color", "green");
//                $("#message").css("display", "flex");
//            }, 1)

//            setTimeout(() => {
//                window.location.href = '/Home/Login';
//            }, 5000)
//        }

//        if (response.status == 409) {
//            setTimeout(() => {
//                document.getElementById("message").innerHTML = "Não foi possível criar a sua conta, tente novamente mais tarde.";
//                $("#message").css("color", "red");
//                $("#message").css("display", "flex");
//            }, 1)

//            setTimeout(() => {
//                $("#message").css("display", "none");
//            }, 10000)
//        }

//        else {
//            setTimeout(() => {
//                document.getElementById("message").innerHTML = "Erro desconhecido pelo desenvolvedor.";
//                $("#message").css("color", "red");
//                $("message").css("display", "flex");
//            })

//            setTimeout(() => {
//                $("message").css("display", "none");
//            })
//        }

//    } catch (error) {
//        console.error("Error: \n" + error);
//    }
//}