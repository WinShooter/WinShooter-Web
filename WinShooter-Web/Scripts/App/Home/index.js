// The different JSON urls
var competitionsApiUrl = "/api/competitions";
var competitionApiUrl = "/api/competition";

var winshooterModule = angular.module('winshooter', []);

winshooterModule.factory('competitionsFactory', function() {
    var factory = {};
    var competitions = [
        { Name: "Välj tävling", StartDate: "", CompetitionId: "" },
        { Name: "Tävlingen", StartDate: "2014-01-17 15:17", CompetitionId: "123" }
    ];

    factory.getCompetitions = function() {
        return competitions;
    };

    return factory;
});

winshooterModule.controller('BodyController', function ($scope, competitionsFactory) {
    //self.loginViewModel = new LoginViewModel();

    // Attributes for showing existing competition
    $scope.competitions = [];

    init();

    function init() {
        $scope.competitions = competitionsFactory.getCompetitions();
        $scope.selectedCompetition = $scope.competitions[0];
    }

    $scope.selectedCompetitionName = function() {
        return $scope.selectedCompetition.Name;
    };

    $scope.selectedCompetitionStartDate = function() {
        return $scope.selectedCompetition.StartDate;
    };

    // For showing existing competitions data
    //self.selectedCompetitionName = ko.computed(function () {
    //    if (self.selectedCompetition() === undefined) {
    //        return '';
    //    }
    //    return self.selectedCompetition().Name;
    //}, self);

    //self.selectedCompetitionGuid = ko.computed(function () {
    //    if (self.selectedCompetition() === undefined) {
    //        return '';
    //    }
    //    return self.selectedCompetition().CompetitionId;
    //}, self);

    //self.selectedCompetitionStartDate = ko.computed(function () {
    //    if (self.selectedCompetition() === undefined) {
    //        return '';
    //    }
    //    return self.selectedCompetition().StartDate;
    //}, self);

    //self.selectedCompetition.subscribe(function (newCompetition) {
    //    if (newCompetition === undefined) {
    //        $('#selectedCompetitionUseNorwegianCount').checkbox('');
    //        $('#selectedCompetitionIsPublic').checkbox('');
    //        return;
    //    }

    //    if (newCompetition.UseNorwegianCount) {
    //        $('#selectedCompetitionUseNorwegianCount').checkbox('check');
    //    } else {
    //        $('#selectedCompetitionUseNorwegianCount').checkbox('');
    //    }

    //    if (newCompetition.IsPublic) {
    //        $('#selectedCompetitionIsPublic').checkbox('check');
    //    } else {
    //        $('#selectedCompetitionIsPublic').checkbox('');
    //    }
    //});

    // Selects the current competition
    $scope.selectCompetitionOnServer = function () {
        var newLocation = "/home/competition/";
        if ($scope.selectedCompetition !== undefined) {
            newLocation = newLocation + $scope.selectedCompetition.CompetitionId;
            window.location.href = newLocation;
        }
    };

    //// Fetch competitions from api and bind with knockout.
    //$.getJSON(competitionsApiUrl, function (data) {
    //    //ko.applyBindings(new ViewModel(data));
    //});
});

// Add binding and such when document is loaded.
$(function () {
    // Add datepicker to the newCompetitionStartDate field
    $("#newCompetitionStartDate").datepicker({ dateFormat: 'yy-mm-dd' });
});