/// <reference path="/Scripts/Flat-ui/flatui-checkbox.js" />
// The different JSON urls
var competitionsApiUrl = "/api/competitions";
var competitionApiUrl = "/api/competition";

// Here's my data model
var ViewModel = function (competition) {
    var self = this;
    self.loginViewModel = new LoginViewModel();

    // Attributes for showing existing competition
    self.competition = ko.observable(competition);
    self.currentCompetition = ko.observable('');

    self.competitionName = ko.observable(competition.Name);
    self.competitionStartDate = ko.observable(competition.StartDate);

    // Cannot use normal knockout bindings because bootstrap competes with knockout
    if (competition.IsPublic) {
        console.log('IsPublic:' + competition.IsPublic);
        $('#selectedCompetitionIsPublic').checkbox('check');
    }

    // Cannot use normal knockout bindings because bootstrap competes with knockout
    if (competition.UseNorwegianCount) {
        console.log('UseNorwegianCount:' + competition.UseNorwegianCount);
        $('#selectedCompetitionUseNorwegianCount').checkbox('check');
    }

    self.userCanUpdateCompetition = ko.computed(function () {
        if (!self.loginViewModel.isLoggedIn()) {
            return false;
        }

        return -1 !== $.inArray("UpdateCompetition", self.loginViewModel.rights());
    }, this);

    self.userCanDeleteCompetition = ko.computed(function () {
        if (!self.loginViewModel.isLoggedIn()) {
            return false;
        }

        return -1 !== $.inArray("DeleteCompetition", self.loginViewModel.rights());
    }, this);

    // Function for deleting competition
    self.deleteCompetition = function () {
        if (self.competition() === undefined) {
            // What? Should never happen.
            alert("Du måste välja en tävling att radera.");
            return;
        }

        if (confirm("Vill du verkligen radera tävlingen?") === false) {
            return;
        }

        var deleteRequest = {
            url: competitionApiUrl + "/" + self.competition().CompetitionId,
            type: "delete",
            dataType: "json"
        };

        $.ajax(deleteRequest).fail(function (data) {
            if (data !== null && data !== undefined && data.responseJSON !== undefined && data.responseJSON.ResponseStatus !== undefined && data.responseJSON.ResponseStatus.Message !== undefined) {
                alert("Misslyckades med att radera tävlingen:\r\n" + data.responseJSON.ResponseStatus.Message);
            } else {
                alert("Misslyckades med att radera tävlingen!");
            }
        }).success(function () {
            alert("Tävlingen raderades.");
            window.location.href = "/";
        });
    };

    // Function for updating competition
    self.updateCompetition = function () {
        if (self.competition() === undefined) {
            // What? Should never happen.
            alert("Du måste välja en tävling att uppdatera.");
            return;
        }

        var updateCompetition = self.competition();

        updateCompetition.Name = self.competitionName();
        updateCompetition.CompetitionType = "Field";
        updateCompetition.StartDate = self.competitionStartDate();
        updateCompetition.UseNorwegianCount = $('#selectedCompetitionUseNorwegianCount').prop('checked', true);
        updateCompetition.IsPublic = $('#selectedCompetitionIsPublic').prop('checked', true);

        var updateRequest = {
            url: competitionApiUrl + "/" + updateCompetition.CompetitionId,
            data: updateCompetition,
            type: "post",
            dataType: "json"
        };

        $.ajax(updateRequest).fail(function (data) {
            if (data !== null && data !== undefined && data.responseJSON !== undefined && data.responseJSON.ResponseStatus !== undefined && data.responseJSON.ResponseStatus.Message !== undefined) {
                alert("Misslyckades med att uppdatera tävlingen:\r\n" + data.responseJSON.ResponseStatus.Message);
            } else {
                alert("Misslyckades med att uppdatera tävlingen!");
            }
        }).success(function () {
            alert("Tävlingen uppdaterades.");
        });
    };
};

// Add binding and such when document is loaded.
$(function () {
    // Add datepicker to the newCompetitionStartDate field
    $("#newCompetitionStartDate").datepicker({ dateFormat: 'yy-mm-dd' });

    // Fetch competitions from api and bind with knockout.
    $.getJSON(competitionApiUrl + "/" + window.competitionId, function (data) {
        ko.applyBindings(new ViewModel(data));
    });
});
