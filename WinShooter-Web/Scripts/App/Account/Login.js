var userApiUrl = "/api/user";
$.getJSON(userApiUrl, function (data) {
    if (data.DisplayName) {
        window.location = "/";
    }
});
