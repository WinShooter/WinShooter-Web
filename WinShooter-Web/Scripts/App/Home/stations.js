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

    alert(JSON.stringify(stations));

    self.stations = ko.observableArray(stations);

    self.userCanUpdateStation = ko.computed(function () {
        if (!self.loginViewModel.isLoggedIn()) {
            return false;
        }

        return -1 !== $.inArray("UpdateStation", self.loginViewModel.rights());
    }, this);

    self.userCanDeleteStation = ko.computed(function () {
        if (!self.loginViewModel.isLoggedIn()) {
            return false;
        }

        return -1 !== $.inArray("DeleteStation", self.loginViewModel.rights());
    }, this);

    self.stationNumberOfTargets = function (input) {
        for (var i = 0; i < self.stations().length; i++) {
            if (self.stations()[i].StationNumber === input) {
                return self.stations()[i].NumberOfTargets;
            }
        }
        return 'NumberOfTargets failed';
    };

    self.stationNumberOfShots = function (input) {
        for (var i = 0; i < self.stations().length; i++) {
            if (self.stations()[i].StationNumber === input) {
                return self.stations()[i].NumberOfShots;
            }
        }
        return 'NumberOfTargets failed';
    };

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

    self.updateStationsFromServer = function() {
        $.getJSON(stationsApiUrl + "?CompetitionId=" + window.competitionId, function(data) {
            self.stations(data);
            self.updateStationView();
        }).fail(function() {
            alert("Could not fetch stations from server");
        });
    };

    self.updateStationView = function () {
        if (self.stations == undefined) {
            // What? Should never happen
            return;
        }

        // Remove all earlier stations
        $('#stationsContainer').empty();

        // Add each station to the container
        // This is supposed to be very fast
        // http://www.learningjquery.com/2009/03/43439-reasons-to-use-append-correctly/
        var textToInsert = [];
        var i = 0;
        self.stations().forEach(function (station) {
            textToInsert[i++] = '<div class="col-md-3">';
            textToInsert[i++] = '<fieldset class="station-border">';
            textToInsert[i++] = '<legend class="station-border">Station ' + station.StationNumber + '</legend>';

            textToInsert[i++] = '<div class="form-group">';
            textToInsert[i++] = '<label for="station' + station.StationNumber + 'NumberOfTargets">Antal figurer:</label>';
            textToInsert[i++] = '<input class="form-control" id="station' + station.StationNumber + 'NumberOfTargets" data-bind="value: stationNumberOfTargets(' + station.StationNumber + '), visible:userCanUpdateStation()">';
            textToInsert[i++] = '<p data-bind="text: stationNumberOfTargets(' + station.StationNumber + '), visible:!userCanUpdateStation()"></p>';
            textToInsert[i++] = '</div>';

            textToInsert[i++] = '<div class="form-group">';
            textToInsert[i++] = '<label for="station' + station.StationNumber + 'NumberOfShots">Antal skott:</label>';
            textToInsert[i++] = '<input class="form-control" id="station' + station.StationNumber + 'NumberOfShots" data-bind="value: stationNumberOfShots(' + station.StationNumber + '), visible:userCanUpdateStation()">';
            textToInsert[i++] = '<p data-bind="text: stationNumberOfShots(' + station.StationNumber + '), visible:!userCanUpdateStation()"></p>';
            textToInsert[i++] = '</div>';

            textToInsert[i++] = '</fieldset>';
            textToInsert[i++] = '</div>';
        });
        $('#stationsContainer').append(textToInsert.join(''));
    };

    self.updateStationView();
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
