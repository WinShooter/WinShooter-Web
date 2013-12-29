// Here's my data model
var LoginViewModel = function (loginData) {
    // Attributes
    this.userName = ko.observable(loginData.DisplayName);

    // calculated attributes
    this.shouldShowLoginLink = ko.computed(function () {
        if (this.userName()) {
            return false;
        }
        return true;
    }, this);

    this.shouldShowLogoutLink = ko.computed(function () {
        if (this.userName()) {
            return true;
        }
        return false;
    }, this);
};

// Fetch competitions from api and bind with knockout.
var userApi = "/api/user";
$.getJSON(userApi, function (data) {
    ko.applyBindings(new LoginViewModel(data), document.getElementById("loginInfo"));
}).fail(function() {
    alert("Failed to retrieve user info");
});
