const login = () => {
    const UserName = document.getElementById('userName');
    const Password = document.getElementById('userPassword');
    console.log("we are trying to login!!!");
    const user = {
        UserName: UserName.value.trim(),
        Password: Password.value.trim()
    };
    // const dt = DateTime.Now;
    fetch(`/user/Login`, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(user)
    })
        .then(response => response.json())
        .then((data) => {
            if (data.status > 400) {
                console.log("unotherize!")
                //     alert("no good!")
            }
            else {
                localStorage.setItem("token", data);
                alert("hello!!");
                if (user.UserName === "Miri&Shani" && user.Password === `MSMB!`)
                    location.href = "/admin.html";
                else
                    location.href = "/main.html";

            }
            //not coming here... relocating
            user.UserName.value = "";
            user.Password.value = "";
        })
        .catch(() => {
            console.error("unable to login");
        });

}


