$.getJSON(currentUserApiUrl, function (data) {
    if (data.IsLoggedIn) {
        window.location = window.referrer;
    }
});
