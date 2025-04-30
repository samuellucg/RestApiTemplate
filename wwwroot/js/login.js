document.getElementById("form-login").addEventListener("submit", function (e) {
    e.preventDefault();
    var userData = {
        "Email": this.Email.value,
        "Password": this.Password.value
    }
    loginUser(userData);
});

function redirectToRegister()
{
    window.location.href = "/Home/Register"
}

async function loginUser(data) {
    const myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");

    try
    {
        const response = await fetch("/api/User/LoginUser", {
            method: 'POST',
            body: JSON.stringify(data),
            headers: myHeaders
        });

        if (response.ok) {
            const result = await response.json()
            localStorage.setItem("token", result.token)
            console.log("TOKEN:", localStorage.getItem("token"))
            //console.log("Storage:", localStorage)
            //console.log("Token:",token)
            console.log("Token salvo com sucesso!")

            await fetch("/api/Account/AuthenticationTest", {
                headers: {
                    "Authorization": "Bearer " += localStorage.getItem("token")
                }
            })
            
        }

        if (!response.ok) {
            setTimeout(() => {
                $("#warn-message").css("display", "flex")
            }, 1);

            setTimeout(() => {
                $("#warn-message".css("display", "none"))
            }, 10000);

            throw new Error(await response.text());
        }
    }

    catch (e) {
        console.error("Error: \n" + e);
    }
}

//async function loginUser(data) {
//    const myHeaders = new Headers();
//    myHeaders.append("Content-Type", "application/json");
//    try {
//        const response = await fetch("/api/User/LoginUser", {
//            method: 'POST',
//            body: JSON.stringify(data),
//            headers: myHeaders,
//            credentials: "include"
//        });
//        if (response.ok) {
//            console.warn(response);
//            const json = await response.json();
//            console.log("json dps do response:", json);
//            const token = json.token;
//            console.log("token:", token);
//            localStorage.setItem("jwt", token);
//            await fetch("/Home/Dashboard", {
//                method: 'GET'
//            });

//        }
//        if (!response.ok) {
//            setTimeout(() =>
//                $("#warn-message").css("display", "flex"), 1);
//            setTimeout(() =>
//                $("#warn-message").css("display", "none"), 10000);

//            throw new Error(await response.text());
//        }
//    }
//    catch (error) {
//        console.error("Error: \n" + error);
//    }
//}

