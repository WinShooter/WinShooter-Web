// The different JSON urls
var competitionsApiUrl = "/api/competitions";
var competitionApiUrl = "/api/competition";

// Here's my data model
var ViewModel = function (competitions) {
    var self = this;

    self.loginViewModel = new LoginViewModel();

    // Attributes for showing existing competition
    self.competitions = ko.observableArray(competitions);
    self.selectedCompetition = ko.observable('');
    
    // For showing existing competitions data
    self.selectedCompetitionName = ko.computed(function () {
        if (self.selectedCompetition() === undefined) {
            return '';
        }
        return self.selectedCompetition().Name;
    }, self);

    self.selectedCompetitionGuid = ko.computed(function () {
        if (self.selectedCompetition() === undefined) {
            return '';
        }
        return self.selectedCompetition().CompetitionId;
    }, self);

    self.selectedCompetitionStartDate = ko.computed(function () {
        if (self.selectedCompetition() === undefined) {
            return '';
        }
        return self.selectedCompetition().StartDate;
    }, self);

    self.selectedCompetition.subscribe(function (newCompetition) {
        if (newCompetition === undefined) {
            $('#selectedCompetitionUseNorwegianCount').checkbox('');
            $('#selectedCompetitionIsPublic').checkbox('');
            return;
        }

        if (newCompetition.UseNorwegianCount) {
            $('#selectedCompetitionUseNorwegianCount').checkbox('check');
        } else {
            $('#selectedCompetitionUseNorwegianCount').checkbox('');
        }

        if (newCompetition.IsPublic) {
            $('#selectedCompetitionIsPublic').checkbox('check');
        } else {
            $('#selectedCompetitionIsPublic').checkbox('');
        }
    });

    // Selects the current competition
    self.selectCompetitionOnServer = function () {
        var newLocation = "/home/competition/";
        if (self.selectedCompetition() !== undefined) {
            newLocation = newLocation + self.selectedCompetition().CompetitionId;
            window.location.href = newLocation;
        }
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