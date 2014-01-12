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

        return "";
    };

    console.log("setting addNewStation");
    self.addNewStation = function () {
        console.log("addNewStation");
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
        }).fail(function() {
            alert("Could not fetch stations from server");
        });
    };

    console.log("setting updateStation");
    self.updateStation = function () {
        var station = this;
        console.log("Update station with number " + station.StationNumber);
        console.log("Station: " + JSON.stringify(this));

        //station.Points = $("label[for='#station" + stationNumber + "IsPoints']").hasClass("checked");
        //station.Distinguish = $("label[for='#station" + stationNumber + "IsDistinguish']").hasClass("checked");

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
        var station = this;
        console.log("Station: " + JSON.stringify(station));

        if (confirm("Vill du verkligen radera stationen?") === false) {
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
