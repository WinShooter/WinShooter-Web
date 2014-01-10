/// <reference path="/Scripts/Flat-ui/flatui-checkbox.js" />
/// <reference path="/Scripts/App/common.js" />
/// <reference path="/Scripts/bootstrap.js" />
/// <reference path="/Scripts/knockout-3.0.0.js" />
// The different JSON urls
var stationsApiUrl = "/api/stations";

// Here's my data model
var ViewModel = function (stations) {
    var self = this;
    self.loginViewModel = new LoginViewModel();

    alert(stations);
};

// Add binding and such when document is loaded.
$(function () {
    // Fetch stations from api and bind with knockout.
    $.getJSON(stationsApiUrl + "?CompetitionId=" + window.competitionId, function (data) {
        ko.applyBindings(new ViewModel(data));
    }).fail(function() {
        alert("Could not fetch station");
    });
});
