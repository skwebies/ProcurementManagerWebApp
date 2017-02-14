function RegisterUser() {
    var model = {
        Email: $('#Email').val(),
        Password: $('#Password').val(),
        ConfirmPassword:$('#ConfirmPassword').val(),
    }
    $.ajax({
        url: '/Account/Register',
        data: JSON.stringify(model),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#UserRegister').modal('hide');
        },
        error: function (errorMessage) {
            alert(errorMessage.responseText);
        }
    });
}

//$('#CreateAccount').on('click', function () {
//    $.get('/Account/Register', function (data) {
//        $('#modalContainer').html(data);
//        $('#UserRegister').modal({});
//    });
//});