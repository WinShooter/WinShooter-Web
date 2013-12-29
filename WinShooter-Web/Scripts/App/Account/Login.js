var userApi = "/api/user";
$.getJSON(userApi, function (data) {
    if (data.DisplayName) {
        window.location = "/";
    }
});
