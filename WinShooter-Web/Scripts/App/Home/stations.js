/// <reference path="/Scripts/Flat-ui/flatui-checkbox.js" />
/// <reference path="/Scripts/App/common.js" />
/// <reference path="/Scripts/bootstrap.js" />
/// <reference path="/Scripts/knockout-3.0.0.js" />

// Here's my data model
var ViewModel = function (stations) {
    var self = this;
    self.loginViewModel = new LoginViewModel();

    console.log(JSON.stringify(stations));

    console.log("setting stations");
    self.stations = ko.observableArray(stations);

    console.log("setting userCanUpdateStation");
    self.userCanUpdateStation = ko.computed(function () {
        if (!self.loginViewModel.isLoggedIn()) {
            return false;
        }

        return -1 !== $.inArray("UpdateStation", self.loginViewModel.rights());
    }, this);

    console.log("setting userCanDeleteStation");
    self.userCanDeleteStation = ko.computed(function () {
        if (!self.loginViewModel.isLoggedIn()) {
            return false;
        }

        return -1 !== $.inArray("DeleteStation", self.loginViewModel.rights());
    }, this);

    self.getStationWithStationNumber = function(stationNumber) {
        for (var i = 0; i < self.stations().length; i++) {
            if (self.stations()[i].StationNumber === stationNumber) {
                return self.stations()[i];
            }
        }
    };

    console.log("setting stationNumberOfTargets");
    self.stationNumberOfTargets = function (stationNumber) {
        var station = self.getStationWithStationNumber(stationNumber);

        if (station === undefined || station === null) {
            return "failed";
        }

        return station.NumberOfTargets;
    };

    console.log("setting stationNumberOfShots");
    self.stationNumberOfShots = function (stationNumber) {
        var station = self.getStationWithStationNumber(stationNumber);

        if (station === undefined || station === null) {
            return "failed";
        }

        return station.NumberOfShots;
    };

    console.log("setting addNewStation");
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

    console.log("setting updateStationsFromServer");
    self.updateStationsFromServer = function() {
        $.getJSON(stationsApiUrl + "?CompetitionId=" + window.competitionId, function(data) {
            self.stations(data);
            self.updateStationView();
        }).fail(function() {
            alert("Could not fetch stations from server");
        });
    };

    console.log("setting updateStationView");
    self.updateStationView = function () {
        if (self.stations == undefined) {
            // What? Should never happen
            return false;
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

            textToInsert[i++] = '<div class="form-group">';
            textToInsert[i++] = '<label class="checkbox" for="station' + station.StationNumber + 'IsPoints">';
            textToInsert[i++] = '<input type="checkbox" id="station' + station.StationNumber + 'IsPoints" data-toggle="checkbox" />';
            textToInsert[i++] = 'Poängstation';
            textToInsert[i++] = '</label>';
            textToInsert[i++] = '</div>';

            textToInsert[i++] = '<div class="form-group">';
            textToInsert[i++] = '<label class="checkbox" for="station' + station.StationNumber + 'IsDistinguish">';
            textToInsert[i++] = '<input type="checkbox" id="station' + station.StationNumber + 'IsDistinguish" data-toggle="checkbox" />';
            textToInsert[i++] = 'Särskiljning';
            textToInsert[i++] = '</label>';
            textToInsert[i++] = '</div>';

            textToInsert[i++] = '<div class="form-group">';
            textToInsert[i++] = '<input type="button" class="btn btn-primary" value="Uppdatera" data-bind="visible:userCanUpdateStation, click: function() { updateStation(' + station.StationNumber + ') }" />';
            textToInsert[i++] = '<input type="button" class="btn btn-danger" value="Ta bort" data-bind="visible:userCanDeleteStation, click: function() { deleteStation(' + station.StationNumber + ')}" />';
            textToInsert[i++] = '</div>';

            textToInsert[i++] = '</fieldset>';
            textToInsert[i++] = '</div>';
        });
        $('#stationsContainer').append(textToInsert.join(''));

        return false;
    };

    console.log("setting updateStation");
    self.updateStation = function (stationNumber) {
        console.log("Update station with number " + stationNumber);
        var station = self.getStationWithStationNumber(stationNumber);
        console.log("Station: " + JSON.stringify(station));

        station.NumberOfTargets = $("#station" + stationNumber + "NumberOfTargets").val();
        station.NumberOfTargets = $("#station" + stationNumber + "NumberOfShots").val();
        station.Points = $("label[for='#station" + stationNumber + "IsPoints']").hasClass("checked");
        station.Distinguish = $("label[for='#station" + stationNumber + "IsDistinguish']").hasClass("checked");

        console.log("Station after retrieving values from DOM: " + JSON.stringify(station));

        var updateRequest = {
            url: stationsApiUrl + "/" + station.Id,
            data: station,
            type: "post",
            dataType: "json"
        };

        $.ajax(updateRequest).fail(function (data) {
            if (data !== null && data !== undefined && data.responseJSON !== undefined && data.responseJSON.ResponseStatus !== undefined && data.responseJSON.ResponseStatus.Message !== undefined) {
                alert("Misslyckades med att uppdatera stationen:\r\n" + data.responseJSON.ResponseStatus.Message);
            } else {
                alert("Misslyckades med att uppdatera stationen!");
            }
        }).success(function () {
            alert("Stationen uppdaterades.");
        });

        return false;
    };

    console.log("setting deleteStation");
    self.deleteStation = function (stationNumber) {
        var station = self.getStationWithStationNumber(stationNumber);
        if (station === undefined || station === null) {
            // What? Should never happen.
            alert("Du måste välja en station att radera.");
            return false;
        }
        console.log("Station: " + JSON.stringify(station));

        if (confirm("Vill du verkligen radera tävlingen?") === false) {
            return false;
        }

        var deleteRequest = {
            url: stationsApiUrl + "/" + station.Id,
            type: "delete",
            dataType: "json"
        };

        $.ajax(deleteRequest).fail(function (data) {
            if (data !== null && data !== undefined && data.responseJSON !== undefined && data.responseJSON.ResponseStatus !== undefined && data.responseJSON.ResponseStatus.Message !== undefined) {
                alert("Misslyckades med att radera stationen:\r\n" + data.responseJSON.ResponseStatus.Message);
            } else {
                alert("Misslyckades med att radera stationen!");
            }
        }).success(function () {
            alert("Stationen raderades.");
            self.updateStationsFromServer();
        });
        return false;
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
