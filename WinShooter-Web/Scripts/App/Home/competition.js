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
    self.competitionIsPublic = ko.observable(competition.IsPublic);
    self.competitionUseNorwegianCount = ko.observable(competition.UseNorwegianCount);

    this.userCanUpdateCompetition = ko.computed(function () {
        if (!self.loginViewModel.isLoggedIn()) {
            return false;
        }

        return -1 !== $.inArray("UpdateCompetition", self.loginViewModel.rights());
    }, this);

    this.userCanDeleteCompetition = ko.computed(function () {
        if (!self.loginViewModel.isLoggedIn()) {
            return false;
        }

        return -1 !== $.inArray("DeleteCompetition", self.loginViewModel.rights());
    }, this);

    // Function for deleting competition
    this.deleteCompetition = function () {
        if (this.competition() === undefined) {
            // What? Should never happen.
            alert("Du måste välja en tävling att radera.");
            return;
        }

        if (confirm("Vill du verkligen radera tävlingen?") === false) {
            return;
        }

        var deleteRequest = {
            url: competitionApiUrl + "/" + this.competition().CompetitionId,
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
    this.updateCompetition = function () {
        if (this.competition() === undefined) {
            // What? Should never happen.
            alert("Du måste välja en tävling att uppdatera.");
            return;
        }

        self.competition().Name = self.competitionName();
        self.competition().StartDate = self.competitionStartDate();
        self.competition().UseNorwegianCount = self.competitionUseNorwegianCount();

        var updateRequest = {
            url: competitionApiUrl + "/" + this.competition().CompetitionId,
            data: self.competition(),
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
