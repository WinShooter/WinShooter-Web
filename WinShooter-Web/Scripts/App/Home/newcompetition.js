﻿/// <reference path="/Scripts/Flat-ui/flatui-checkbox.js" />
/// <reference path="/Scripts/App/common.js" />
/// <reference path="/Scripts/bootstrap.js" />
/// <reference path="/Scripts/knockout-3.0.0.js" />
// The different JSON urls
var competitionsApiUrl = "/api/competitions";
var competitionApiUrl = "/api/competition";

// Here's my data model
var ViewModel = function () {
    this.loginViewModel = new LoginViewModel();

    // Attributes for creating a new competition
    this.newCompetitionName = ko.observable('');
    this.newCompetitionStartDate = ko.observable('');
    this.newCompetitionStartTime = ko.observable('');

    // Function for adding competition
    this.addCompetition = function () {
        var competition = {
            CompetitionType: "Field",
            Name: this.newCompetitionName(),
            UseNorwegianCount: $('label[for="newCompetitionUseNorwegianCount"]').hasClass("checked"),
            IsPublic : $('label[for="newCompetitionIsPublic"]').hasClass("checked"),
            StartDate: this.newCompetitionStartDate() + " " + this.newCompetitionStartTime(),
        };

        $.post(competitionApiUrl, competition, function(returnedData) {
                var newLocation = "/home/competition/" + returnedData.CompetitionId;
                window.location.href = newLocation;
        }, "json").fail(function (data) {
            alert("Misslyckades med att lägga till tävlingen.\r\n" + data.responseJSON.ResponseStatus.Message);
        });
    };
};

// Add binding and such when document is loaded.
$(function () {
    // Add datepicker to the newCompetitionStartDate field
    $("#newCompetitionStartDate").datepicker({ dateFormat: 'yy-mm-dd' });

    // Fetch competitions from api and bind with knockout.
    ko.applyBindings(new ViewModel());
});