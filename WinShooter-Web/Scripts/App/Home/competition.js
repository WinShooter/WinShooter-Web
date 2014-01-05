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
            dataType: "json",
            success: function () {
                alert("Success!");
            }
        };

        $.ajax(deleteRequest).fail(function (data) {
            alert("Misslyckades med att radera tävlingen:\r\n" + data.responseJSON.ResponseStatus.Message);
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

        alert("NotImplementedYet");
    };
};
