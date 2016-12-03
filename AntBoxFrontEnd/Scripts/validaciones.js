var verifemail1 = true;
var verifemail2 = true;
$('#email').focusout(function () {
    var regex = /[\w-\.]{2,}@([\w-]{2,}\.)*([\w-]{2,}\.)[\w-]{2,4}/;
    if ($('#email').val() != "") {
        if (regex.test($('#email').val().trim())) {
            verifemail1 = true;
             $('#email').css('background', '');
            document.getElementById('alert_email').style.display = 'none';
        } else {
            verifemail1 = false;
            document.getElementById('alert_email').style.display = 'block';
           $('#email').css('background', '#ffdede');
        }
    }
});

$('#email2').focusout(function () {
    var regex = /[\w-\.]{2,}@([\w-]{2,}\.)*([\w-]{2,}\.)[\w-]{2,4}/;
    if ($('#email2').val() != "") {
        if (regex.test($('#email2').val().trim())) {
            verifemail2 = true;
            $('#email2').css('background', '');
            document.getElementById('alert_email2').style.display = 'none';
        } else {
            document.getElementById('alert_email2').style.display = 'block';
            verifemail2 = false;
             $('#email2').css('background', '#ffdede');
        }
    }
    if ((verifemail1 && verifemail2))
    {
        if (($('#email').val() !== $('#email2').val())) {
            document.getElementById('alert_email3').style.display = 'block';
            $('#email').css('background', '#ffdede');
            $('#email2').css('background', '#ffdede');
        } else {
            document.getElementById('alert_email3').style.display = 'none';
            $('#email').css('background', '');
            $('#email2').css('background', '');
        }
    }
    

});

var verifpassword1 = true;
var verifpassword2 = true;

$('#password').focusout(function () {
    var regex = /[A-Z]/;
    if ($('#password').val() != "") {
        if (regex.test($('#password').val().trim())) {
            document.getElementById('alert_password').style.display = 'none';
            verifpassword1 = true;
            $('#password').css('background', '');
        } else {
            document.getElementById('alert_password').style.display = 'block';
            verifpassword1 = false;
            $('#password').css('background', '#ffdede');
            //alert("Debe posee almenos una mayusculas");
           // $('#password').val("");
        }
    }
    var regex = /(?=.*\d)/;
    if ($('#password').val() != "") {
        if (regex.test($('#password').val().trim())) {
            document.getElementById('_alert_password').style.display = 'none';
            verifpassword1 = true;
            $('#password').css('background', '');
        } else {
            document.getElementById('alert_password').style.display = 'block';
            verifpassword1 = false;
            $('#password').css('background', '#ffdede');
            // alert("Debe posee almenos un Numero");
            //$('#password').val("");
        }
    }

});

$('#password2').focusout(function () {
    var regex = /[A-Z]/;
    var verif = true;
    if ($('#password2').val() != "") {
        if (regex.test($('#password2').val().trim())) {
            document.getElementById('alert_password2').style.display = 'none';
            verifpassword2 = true;
            $('#password2').css('background', '');
        } else {
            document.getElementById('alert_password2').style.display = 'block';
            verifpassword2 = false;
            verif = false;
            // alert("Debe posee almenos una mayusculas");
            $('#password2').css('background', '#ffdede');
           // $('#password2').val("");
        }
    }
    var regex = /(?=.*\d)/;
    if ($('#password2').val() != "") {
        if (regex.test($('#password2').val().trim())) {
            document.getElementById('alert_password2').style.display = 'none';
            verifpassword2 = true;
            $('#password2').css('background', '');
        } else {
            document.getElementById('alert_password2').style.display = 'block';
            verifpassword2 = true;
            verif = false;
            //alert("Debe posee almenos un Numero");
            $('#password2').css('background', '#ffdede');
          //  $('#password2').val("");
        }
    }

    if ((verifemail1 && verifemail2)) {
        if (($('#password').val() !== $('#password2').val()) && (verif)) {
            document.getElementById('alert_password3').style.display = 'block';
            $('#password').css('background', '#ffdede');
            $('#password2').css('background', '#ffdede');
        } else {
            document.getElementById('alert_password3').style.display = 'none';
            $('#password').css('background', '');
            $('#password2').css('background', '');
        }
    }

});




/*

 if ($('#password') !== $('#password1')) {
      document.getElementById('alert_password3').style.display = 'block';
        //alert("Debe posee almenos un Numero");
        $('#password2').css('background', '#ffdede');
    }

  if ($('#password').val() !== "") {
      document.getElementById('alert_password3').style.display = 'none';
        //alert("Debe posee almenos un Numero");
        $('#password2').css('background', '');
    } else

*/

