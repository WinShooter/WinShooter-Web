// The different JSON urls
var competitionsApiUrl = "/api/competitions";
var competitionApiUrl = "/api/competition";
var userApiUrl = "/api/user";
var currentUserApiUrl = "/api/currentuser";

// Here's the viewModel for the header
var LoginViewModel = function () {
    var self = this;

    // Attributes
    self.displayName = ko.observable('');
    self.isLoggedIn = ko.observable(false);
    self.rights = ko.observableArray();

    // calculated attributes
    this.shouldShowLoginLink = ko.computed(function () {
        return !self.isLoggedIn();
    }, this);

    this.shouldShowEditRightsLink = ko.computed(function () {
        if (!self.isLoggedIn()) {
            return false;
        }

        return $.inArray("ReadUserCompetitionRole", self.rights());
    }, this);

    // Fetch competitions from api and bind with knockout.
    var jsonUrl = currentUserApiUrl;
    if (window.competitionId !== undefined) {
        jsonUrl += "/" + window.competitionId;
    }
    $.getJSON(jsonUrl, function (data) {
        self.isLoggedIn(data.IsLoggedIn);

        if (data.IsLoggedIn) {
            self.displayName(data.DisplayName);
            self.rights(data.CompetitionRights);
        }
    }).fail(function (data) {
        alert("Failed to retrieve user info:" + data);
    });
};

