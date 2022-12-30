function checkValid() {
    var pass = document.querySelectorAll('[type="password"]')
    var error = document.querySelector(".login-error");

    if (pass[0].value != "" && pass[1].value != "") {
        if (pass[0].value == pass[1].value) {
            error.classList.remove("login-error-visible");
        } else {
            error.innerHTML = "Пароли не совпадают!";
            error.classList.add("login-error-visible");
        }
    }
}