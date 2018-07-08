(function () {
    $(function () {
        var basicAuthUI =
            '<div class="input basicauth"><input placeholder="client username" id="input_username" name="username" type="text" size="10"></div>' +
            '<div class="input basicauth"><input placeholder="client password" id="input_password" name="password" type="password" size="10"></div>';

        if ($('#api_selector .basicauth').length == 0) {
            $(basicAuthUI).insertBefore('#api_selector div.input:last-child');
        }
        $("#input_apiKey").hide();
       
        $('#input_username').change(addAuthorization);
        $('#input_password').change(addAuthorization);
    });
   
    function addAuthorization() {
        var username = $('#input_username').val();
        var password = $('#input_password').val();
        if (username && username.trim() != "" && password && password.trim() != "") {
            var basicAuth = new SwaggerClient.PasswordAuthorization('basic', username, password);
            window.swaggerUi.api.clientAuthorizations.add("basicAuth", basicAuth);
            console.log("authorization added: username = " + username + ", password = " + password);
        }
    }
})();