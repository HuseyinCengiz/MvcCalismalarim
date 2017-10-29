// Contact Form
$(function () {
    $(".contact-form-box .form-control").tooltip({placement: 'top', trigger: 'manual'}).tooltip('hide');
    $('.contact-form-box .form-control').blur(function () {
        $(this).tooltip({placement: 'top', trigger: 'manual'}).tooltip('hide');
    });

    $(".contact-form-box #submitMessage").click(function () {
        // validate and process form
        $('.contact-form-box .error').hide();

        var name = $(".contact-form-box input#firstname").val();
        if (name == "") {
            $(".contact-form-box input#firstname").tooltip({placement: 'bottom', trigger: 'manual'}).tooltip('show');
            $(".contact-form-box input#firstname").focus();
            return false;
        }
        var email = $(".contact-form-box input#email").val();
        var filter = /^[a-zA-Z0-9]+[a-zA-Z0-9_.-]+[a-zA-Z0-9_-]+@[a-zA-Z0-9]+[a-zA-Z0-9.-]+[a-zA-Z0-9]+.[a-z]{2,4}$/;

        if (!filter.test(email)) {
            $(".contact-form-box input#email").tooltip({placement: 'bottom', trigger: 'manual'}).tooltip('show');
            $(".contact-form-box input#email").focus();
            return false;
        }
        var subject = 'E-mail from contact form';
        var message = $(".contact-form-box #message").val();
        if (message == "") {
            $(".contact-form-box #message").tooltip({placement: 'bottom', trigger: 'manual'}).tooltip('show');
            $(".contact-form-box #message").focus();
            return false;
        }

        var dataString = 'name=' + name + '&email=' + email + '&subject=' + subject + '&message=' + message;

        $.ajax({
            type:"POST",
            url:"form/contact-form.php",
            data:dataString,
            success:function () {
                $('.contact-form-box').prepend("<div class=\"alert alert-success\" style=\"margin-top: 20px\"><button class=\"close\" data-dismiss=\"alert\" type=\"button\">&times;</button><strong>Contact Form Submitted!</strong> We will be in touch soon.</div>");
                $('.contact-form-box')[0].reset();
            }
        });
        return false;
    });
});