// Vanilla JavaScript: Show/Hide Password functionality
function togglePasswordVisibility() {
    var pwdInput = document.getElementById("Password");
    var confirmPwdInput = document.getElementById("ConfirmPassword");

    if (pwdInput.type === "password") {
        pwdInput.type = "text";
        if (confirmPwdInput) confirmPwdInput.type = "text";
    } else {
        pwdInput.type = "password";
        if (confirmPwdInput) confirmPwdInput.type = "password";
    }
}

function toggleLoginPassword() {
    var loginPwd = document.getElementById("LoginPassword");
    if (loginPwd.type === "password") {
        loginPwd.type = "text";
    } else {
        loginPwd.type = "password";
    }
}

// jQuery: Client-side validation & interaction handling
$(document).ready(function () {
    // Detect Registration Form Submission
    $("#registerForm").on("submit", function (e) {
        var password = $("#Password").val();
        var confirmPassword = $("#ConfirmPassword").val();
        var errorBox = $("#jsValidationError");

        errorBox.hide().text("");

        // Simple validation: Password Matching
        if (password !== confirmPassword) {
            e.preventDefault(); // Stop submission
            errorBox.text("JavaScript Alert: Password and Confirm Password do not match!").fadeIn();
            return false;
        }
    });

    // Detect Login Form Submission
    $("#loginForm").on("submit", function (e) {
        var email = $("#LoginEmail").val().trim();
        var password = $("#LoginPassword").val().trim();
        var errorBox = $("#jsLoginValidationError");

        if (email === "" || password === "") {
            e.preventDefault();
            errorBox.text("JavaScript Alert: All fields are required.").fadeIn();
            return false;
        }
    });

    // Auto-hide alert boxes after 4 seconds
    setTimeout(function () {
        $("#alertMessage").fadeOut("slow");
    }, 4000);
});