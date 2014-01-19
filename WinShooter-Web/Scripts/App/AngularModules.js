/// <reference path="/Scripts/App/common.js" />
/// <reference path="/Scripts/ui-bootstrap-0.9.0.js" />
/// <reference path="/Scripts/ui-bootstrap-tpls-0.9.0.js" />

// Create the angular module
var winshooterModule = angular.module('winshooter', ['ngResource', 'ui.bootstrap']);

// Create factory for competitions
winshooterModule.factory('competitionsFactory', [
    '$resource', function ($resource) {
        return $resource(competitionsApiUrl, {}, {
            query: { method: 'GET', isArray: true },
            search: { method: 'GET', isArray: false }
        });
    }
]);

// Create factory for competitions
winshooterModule.factory('competitionFactory', [
    '$resource', function ($resource) {
        return $resource(competitionApiUrl, {
            competitionId: '@CompetitionId'
        }, {
            query: { method: 'GET', isArray: true },
            search: { method: 'GET', isArray: false }
        });
    }
]);

// Create factory for current user
winshooterModule.factory('currentUserFactory', [
    '$resource', function ($resource) {
        return $resource(currentUserApiUrl, {
            competitionId: '@competitionId'
        }, {
            query: { method: 'GET', isArray: false },
            search: { method: 'GET', isArray: false },
            delete: { method: 'DELETE', isArray: false },
            update: { method: 'POST', isArray: false }
        });
    }
]);

// Here's the viewModel for the header
winshooterModule.controller('CurrentUserController', function ($scope, currentUserFactory) {
    // Attributes
    $scope.currentUser = { IsLoggedIn: false };
    $scope.displayName = "";
    $scope.isLoggedIn = false;
    $scope.rights = [];
    $scope.shouldShowLoginLink = false;
    $scope.shouldShowAddResultsLink = false;
    $scope.shouldShowEditRightsLink = false;
    $scope.shouldShowEditClubsLink = false;
    $scope.shouldShowEditWeaponsLink = false;

    init();

    function init() {
        $scope.currentUser = currentUserFactory.query(function () {
            // Get data
            $scope.shouldShowLoginLink = !$scope.currentUser.IsLoggedIn;
            $scope.isLoggedIn = $scope.currentUser.IsLoggedIn;

            if ($scope.currentUser.IsLoggedIn) {
                $scope.displayName = $scope.currentUser.DisplayName;
                $scope.rights = $scope.currentUser.CompetitionRights;

                $scope.shouldShowAddResultsLink = -1 !== $.inArray("ReadCompetitorResult", $scope.rights);
                $scope.shouldShowEditRightsLink = -1 !== $.inArray("ReadUserCompetitionRole", $scope.rights);
                $scope.shouldShowEditClubsLink = -1 !== $.inArray("UpdateClub", $scope.rights);
                $scope.shouldShowEditWeaponsLink = -1 !== $.inArray("UpdateWeapon", $scope.rights);
            }
        }, function () {
            alert("failed to retrieve current user.");
        });
    }
});

// Here the module for the index page
winshooterModule.controller('IndexController', function ($scope, competitionsFactory) {
    //self.loginViewModel = new LoginViewModel();

    // Attributes for showing existing competition
    $scope.competitions = [];
    $scope.selectedCompetition = {
        Name: "Laddar tävlingar. Var god vänta...",
        StartDate: "",
        CompetitionId: "",
        IsPublic: false,
        UseNorwegianCount: false
    };

    init();

    function init() {
        $scope.competitions = competitionsFactory.query(function () {
            if ($scope.competitions.length > 0) {
                // We got some competitions back. Select first one.
                $scope.selectedCompetition = $scope.competitions[0];
            } else {
                $scope.selectedCompetition.Name = "Inga tävlingar hittades.";
            }
        }, function () {
            alert("failed to retrieve competitions.");
        });
    }

    // Selects the current competition
    $scope.selectCompetitionOnServer = function () {
        var newLocation = "/home/competition/";
        if ($scope.selectedCompetition !== undefined) {
            newLocation = newLocation + $scope.selectedCompetition.CompetitionId;
            window.location.href = newLocation;
        }
    };
});

// Here the module for the competition page
winshooterModule.controller('CompetitionController', function($scope, competitionFactory, currentUserFactory) {
    // Attributes for showing existing competition
    $scope.currentUser = { IsLoggedIn: false, Rights: [] };
    $scope.competition = {};
    $scope.userCanUpdateCompetition = false;
    $scope.userCanDeleteCompetition = false;

    init();

    function init() {
        $scope.currentUser = currentUserFactory.search({ CompetitionId: window.competitionId }, function() {
            $scope.userCanUpdateCompetition = -1 !== $.inArray("UpdateCompetition", $scope.currentUser.CompetitionRights);
            $scope.userCanDeleteCompetition = -1 !== $.inArray("DeleteCompetition", $scope.currentUser.CompetitionRights);
        }, function() {
            alert("failed to retrieve current user.");
        });

        $scope.competition = competitionFactory.search({ CompetitionId: window.competitionId }, function() {
            // Nothing to do here. Carry on!
        }, function () {
            alert("failed to retrieve competitions.");
        });
    }

    $scope.updateCompetition = function() {
        if ($scope.competition === undefined) {
            // What? Should never happen.
            alert("Du måste välja en tävling att uppdatera.");
            return false;
        }

        // For some reason angular cannot have a two way binding to a class.
        // Let's pick it out with jQuery instead.
        $scope.competition.IsPublic = $("label[for='selectedCompetitionIsPublic']").hasClass('checked');
        $scope.competition.UseNorwegianCount = $("label[for='selectedCompetitionUseNorwegianCount']").hasClass('checked');

        $scope.competition.$save();

        return false;
    };

    $scope.deleteCompetition = function() {
        if ($scope.competition === undefined) {
            // What? Should never happen.
            alert("Du måste välja en tävling att radera.");
            return false;
        }

        if (confirm("Vill du verkligen radera tävlingen?") === false) {
            return false;
        }

        $scope.competition.$delete();

        return false;
    };
});

// Here the module for the competition page
winshooterModule.controller('NewCompetitionController', function ($scope, competitionFactory, currentUserFactory) {
    $scope.currentUser = { IsLoggedIn: false, Rights: [] };

    // Attributes for adding new competition
    $scope.Name = "";
    $scope.StartDate = "";
    $scope.StartTime = "";
    $scope.IsPublic = false;
    $scope.UseNorwegianCount = false;

    // UI attributes
    $scope.datePickerOpen = true;
    $scope.datePickerShowWeeks = true;
    $scope.datePickerDateOptions = { 'year-format': 'yy', 'starting-day': 1 };

    // Init our page
    init();

    function init() {
        $scope.currentUser = currentUserFactory.search({ CompetitionId: window.competitionId }, function () {
            // Nothing to do here. Carry on!
        }, function () {
            alert("failed to retrieve current user.");
        });
    }

    $scope.addCompetition = function () {
        var competition = {
            CompetitionId: "",
            CompetitionType: "Field",
            Name: $scope.Name,
            UseNorwegianCount: $scope.UseNorwegianCount,
            IsPublic : $scope.IsPublic,
            StartDate: $scope.StartDate.getFullYear() + "-" + $scope.StartDate.getMonth() + "-" + $scope.StartDate.getDay() + " " + $scope.StartTime,
        };
        
        $.post(competitionApiUrl, competition, function(returnedData) {
            var newLocation = "/home/competition/" + returnedData.CompetitionId;
            window.location.href = newLocation;
        }, "json").fail(function (data) {
            alert("Misslyckades med att lägga till tävlingen.\r\n" + data.responseJSON.ResponseStatus.Message);
        });
    };
});