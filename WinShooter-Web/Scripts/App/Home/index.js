// The different JSON urls
var competitionsApiUrl = "/api/competitions";
var competitionApiUrl = "/api/competition";

// Here's my data model
var ViewModel = function (competitions) {
    this.loginViewModel = new LoginViewModel();

    // Attributes for showing existing competition
    this.competitions = ko.observableArray(competitions);
    this.selectedCompetition = ko.observable('');
    
    // For showing existing competitions data
    this.selectedCompetitionName = ko.computed(function () {
        if (this.selectedCompetition() === undefined) {
            return '';
        }
        return this.selectedCompetition().Name;
    }, this);

    this.selectedCompetitionGuid = ko.computed(function () {
        if (this.selectedCompetition() === undefined) {
            return '';
        }
        return this.selectedCompetition().CompetitionId;
    }, this);

    this.selectedCompetitionStartDate = ko.computed(function () {
        if (this.selectedCompetition() === undefined) {
            return '';
        }
        return this.selectedCompetition().StartDate;
    }, this);
    
    this.selectedCompetitionIsPublic = ko.computed(function () {
        if (this.selectedCompetition() === undefined) {
            return '';
        }
        return this.selectedCompetition().IsPublic;
    }, this);

    this.selectedCompetitionUseNorwegianCount = ko.computed(function () {
        if (this.selectedCompetition() === undefined) {
            return '';
        }
        return this.selectedCompetition().UseNorwegianCount;
    }, this);

    // Selects the current competition
    this.selectCompetitionOnServer = function () {
        var newLocation = "/home/competition/";
        if (this.selectedCompetition() !== undefined) {
            newLocation = newLocation + this.selectedCompetition().CompetitionId;
            window.location.href = newLocation;
        }
    };
    
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

        var data = JSON.stringify(this.selectedCompetition());

        var deleteRequest = {
            url: competitionApiUrl + "/" + this.selectedCompetition().CompetitionId,
            type: "delete",
            dataType: "json",
            success: function() {
                alert("Success!");
            }
        };

        $.ajax(deleteRequest).fail(function(data) {
            alert("Misslyckades med att radera tävlingen:\r\n" + data.responseJSON.ResponseStatus.Message);
        }).success(function() {
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
    this.addCompetition = function() {
        var competition = {
            CompetitionType: "Field",
            Name: this.newCompetitionName(),
            UseNorwegianCount: "False",
            StartDate: this.newCompetitionStartDate(),
        };

        $.post("/api/competition", competition, function(returnedData) {
            alert("success!");
        }).fail(function() {
            alert("fail!");
        });
    };
};

// Add binding and such when document is loaded.
$(function () {
    // Add datepicker to the newCompetitionStartDate field
    $("#newCompetitionStartDate").datepicker({ dateFormat: 'yy-mm-dd' });

    // Fetch competitions from api and bind with knockout.
    $.getJSON(competitionsApiUrl, function(data) {
        ko.applyBindings(new ViewModel(data));
    });
});