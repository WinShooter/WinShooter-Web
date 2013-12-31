// Here's my data model
var ViewModel = function (competitions) {
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
        return this.selectedCompetition().Guid;
    }, this);

    // Selects the current competition
    this.selectCompetitionOnServer = function () {
        var newLocation = "/home/index/";
        if (this.selectedCompetition() !== undefined) {
            newLocation = newLocation + this.selectedCompetition().Guid;
        }
        window.location.href = newLocation;
    };
    
    // Attributes for creating a new competition
    this.newCompetitionName = ko.observable('');
    this.newCompetitionStartDate = ko.observable('');

    // Function for adding competition
    this.addCompetition = function() {
        var competition = {
            CompetitionType: "Field",
            Name: this.newCompetitionName(),
            UseNorwegianCount: "False",
            StartDate: "2013-01-01",
        };

        $.post("/api/competition", competition, function (returnedData) {
            alert("success!");
        }).fail(function () {
            alert("fail!");
        });
    }
};

// Add datepicker to the newCompetitionStartDate field
$(function () {
    $("#newCompetitionStartDate").datepicker({ dateFormat: 'yy-mm-dd' });
});

// Fetch competitions from api and bind with knockout.
var competitionsApi = "/api/competitions";
$.getJSON(competitionsApi, function (data) {
    ko.applyBindings(new ViewModel(data), document.getElementById("body"));
});
