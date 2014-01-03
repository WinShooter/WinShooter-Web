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

    this.selectedCompetitionUserCanEditCompetition = ko.computed(function () {
        if (this.selectedCompetition() === undefined) {
            return false;
        }
        return this.selectedCompetition().UserCanEditCompetition;
    }, this);

    this.selectedCompetitionUserCannotEditCompetition = ko.computed(function () {
        if (this.selectedCompetition() === undefined) {
            return true;
        }
        if (this.selectedCompetition().UserCanEditCompetition) {
            return false;
        }
        return true;
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
    var competitionsApi = "/api/competitions";
    $.getJSON(competitionsApi, function(data) {
        ko.applyBindings(new ViewModel(data));
    });
});