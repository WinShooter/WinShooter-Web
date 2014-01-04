// The different JSON urls
var competitionsApiUrl = "/api/competitions";
var competitionApiUrl = "/api/competition";
var userApiUrl = "/api/user";

// Here's the viewModel for the header
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
    $.getJSON(userApiUrl, function (data) {
        self.isLoggedIn(data.IsLoggedIn);

        if (data.IsLoggedIn) {
            self.displayName(data.DisplayName);
        }
    }).fail(function () {
        alert("Failed to retrieve user info");
    });
};

