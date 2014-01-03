// Here's my viewModel
var LoginViewModel = function () {
    var self = this;

    // Attributes
    self.displayName = ko.observable('');
    self.isLoggedIn = ko.observable(false);

    // calculated attributes
    this.shouldShowLoginLink = ko.computed(function () {
        if (self.isLoggedIn()) {
            return false;
        }
        return true;
    }, this);

    // Fetch competitions from api and bind with knockout.
    var userApi = "/api/user";
    $.getJSON(userApi, function (data) {
        self.isLoggedIn(data.IsLoggedIn);

        if (data.IsLoggedIn) {
            self.displayName(data.DisplayName);
        }
    }).fail(function () {
        alert("Failed to retrieve user info");
    });
};
