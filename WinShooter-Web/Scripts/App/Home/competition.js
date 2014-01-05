// The different JSON urls
var competitionsApiUrl = "/api/competitions";
var competitionApiUrl = "/api/competition";

// Here's my data model
var ViewModel = function (competition) {
    this.loginViewModel = new LoginViewModel();

    // Attributes for showing existing competition
    this.competition = ko.observable(competition);
    this.currentCompetition = ko.observable('');

    // For showing existing competitions data
    this.competitionName = ko.observable(competition.Name);
    this.competitionStartDate = ko.observable(competition.StartDate);
    this.competitionIsPublic = ko.observable(competition.IsPublic);
    this.competitionUseNorwegianCount = ko.observable(competition.UseNorwegianCount);
    this.userCanUpdateCompetition = ko.observable(competition.UserCanUpdateCompetition);
    this.userCanDeleteCompetition = ko.observable(competition.UserCanDeleteCompetition);

    // Attributes for creating a new competition
    this.newCompetitionName = ko.observable('');
    this.newCompetitionStartDate = ko.observable('');

    // Function for deleting competition
    this.deleteCompetition = function () {
        if (this.selectedCompetition() === undefined) {
            // What? Should never happen.
            alert("Du måste välja en tävling att radera.");
            return;
        }

        if (confirm("Vill du verkligen radera tävlingen?") === false) {
            return;
        }

        var deleteRequest = {
            url: competitionApiUrl + "/" + this.selectedCompetition().CompetitionId,
            type: "delete",
            dataType: "json",
            success: function () {
                alert("Success!");
            }
        };

        $.ajax(deleteRequest).fail(function (data) {
            alert("Misslyckades med att radera tävlingen:\r\n" + data.responseJSON.ResponseStatus.Message);
        }).success(function () {
            alert("Tävlingen raderades. (TODO: Här ska vi också uppdatera listan med tävlingar)");
        });
    };

    // Function for updating competition
    this.updateCompetition = function () {
        if (this.selectedCompetition() === undefined) {
            // What? Should never happen.
            alert("Du måste välja en tävling att uppdatera.");
            return;
        }

        alert("NotImplementedYet");
    };

    // Function for adding competition
    this.addCompetition = function () {
        var competition = {
            CompetitionType: "Field",
            Name: this.newCompetitionName(),
            UseNorwegianCount: "False",
            StartDate: this.newCompetitionStartDate(),
        };

        $.post("/api/competition", competition, function (returnedData) {
            alert("success!");
        }).fail(function () {
            alert("fail!");
        });
    };
};

// Add binding and such when document is loaded.
$(function () {
    // Add datepicker to the newCompetitionStartDate field
    $("#newCompetitionStartDate").datepicker({ dateFormat: 'yy-mm-dd' });

    // Fetch competitions from api and bind with knockout.
    $.getJSON(competitionsApiUrl, function (data) {
        ko.applyBindings(new ViewModel(data));
    });
});