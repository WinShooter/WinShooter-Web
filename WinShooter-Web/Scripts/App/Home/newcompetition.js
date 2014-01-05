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
            UseNorwegianCount: "False",
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