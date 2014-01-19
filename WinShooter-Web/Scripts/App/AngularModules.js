// Create the angular module
var winshooterModule = angular.module('winshooter', ['ngResource']);

// Create factory for competitions
winshooterModule.factory('competitionsFactory', [
    '$resource', function ($resource) {
        return $resource(competitionsApiUrl, {}, {
            query: { method: 'GET', isArray: true }
        });
    }
]);

// Create factory for current user
winshooterModule.factory('currentUserFactory', [
    '$resource', function ($resource) {
        return $resource(currentUserApiUrl, {}, {
            query: { method: 'GET', isArray: false }
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

    init();

    function init() {
        $scope.currentUser = currentUserFactory.query(function () {
            // Get data
            $scope.shouldShowLoginLink = !$scope.currentUser.IsLoggedIn;
            $scope.isLoggedIn = $scope.currentUser.IsLoggedIn;

            if ($scope.currentUser.IsLoggedIn) {
                $scope.displayName = $scope.currentUser.DisplayName;
                $scope.rights = $scope.currentUser.CompetitionRights;
            }
        }, function () {
            alert("failed to retrieve current user.");
        });
    }

    // calculated attributes
    $scope.shouldShowAddResultsLink = function () {
        if (!$scope.isLoggedIn()) {
            return false;
        }

        return -1 !== $.inArray("ReadCompetitorResult", $scope.rights());
    };

    $scope.shouldShowEditRightsLink = function () {
        if (!$scope.isLoggedIn()) {
            return false;
        }

        return -1 !== $.inArray("ReadUserCompetitionRole", $scope.rights());
    };

    $scope.shouldShowEditClubsLink = function () {
        if (!$scope.isLoggedIn()) {
            return false;
        }

        return -1 !== $.inArray("UpdateClub", $scope.rights());
    };

    $scope.shouldShowEditWeaponsLink = function () {
        if (!$scope.isLoggedIn()) {
            return false;
        }

        return -1 !== $.inArray("UpdateWeapon", $scope.rights());
    };

    // Fetch competitions from api and bind with knockout.
    //var jsonUrl = currentUserApiUrl;
    //if (window.competitionId !== undefined) {
    //    jsonUrl += "/" + window.competitionId;
    //}
    //$.getJSON(jsonUrl, function (data) {
    //    self.isLoggedIn(data.IsLoggedIn);

    //    if (data.IsLoggedIn) {
    //        self.displayName(data.DisplayName);
    //        self.rights(data.CompetitionRights);
    //    }
    //}).fail(function (data) {
    //    alert("Failed to retrieve user info:" + data);
    //});
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
