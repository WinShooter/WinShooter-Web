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

    self.stations = ko.observableArray(stations);

    self.addNewStation = function () {
        if (self.stations == undefined) {
            // What? Should never happen
            return false;
        }

        var stationToAdd = {
            CompetitionId: window.competitionId,
            Distinguish: false,
            NumberOfShots: 6,
            NumberOfTargets: 3,
            Points: 0,
            StationNumber: -1
        };

        $.post(stationsApiUrl, stationToAdd, function (returnedData) {
            self.updateStationsFromServer();
        }, "json").fail(function (data) {
            alert("Misslyckades med att lägga till stationen.\r\n" + data.responseJSON.ResponseStatus.Message);
        });

        return false;
    };

    self.updateStationsFromServer = function () {
        $.getJSON(stationsApiUrl + "?CompetitionId=" + window.competitionId, function (data) {
            self.stations(data);
            self.updateStationView();
        }).fail(function () {
            alert("Could not fetch stations from server");
        });
    }

    self.updateStationView = function () {
        alert("updating view started");

        if (self.stations == undefined) {
            // What? Should never happen
            return;
        }

        // Remove all earlier stations
        $('#stationsContainer').empty();

        // Add each station to the container
        self.stations().forEach(function (station) {
            alert(JSON.stringify(station));
        });
    };
};

// Add binding and such when document is loaded.
$(function () {
    // Fetch stations from api and bind with knockout.
    $.getJSON(stationsApiUrl + "?CompetitionId=" + window.competitionId, function (data) {
        ko.applyBindings(new ViewModel(data));
    }).fail(function() {
        alert("Could not fetch stations from server");
    });
});
