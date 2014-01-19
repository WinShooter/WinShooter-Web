// The different JSON urls
var competitionsApiUrl = "/api/competitions";
var competitionApiUrl = "/api/competition";
var userApiUrl = "/api/user";
var currentUserApiUrl = "/api/currentuser";
var stationsApiUrl = "/api/stations";
var stationApiUrl = "/api/station";

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
    $scope.currentUser = {};
    $scope.displayName = "";
    $scope.isLoggedIn = false;
    $scope.rights = [];

    init();

    function init() {
        $scope.currentUser = currentUserFactory.query(function () {
            // Don't do anything
        }, function () {
            alert("failed to retrieve current user.");
        });
    }

    // calculated attributes
    $scope.shouldShowLoginLink = function () {
        return !self.isLoggedIn();
    };

    $scope.shouldShowAddResultsLink = function() {
        if (!self.isLoggedIn()) {
            return false;
        }

        return -1 !== $.inArray("ReadCompetitorResult", self.rights());
    };

    $scope.shouldShowEditRightsLink = function () {
        if (!self.isLoggedIn()) {
            return false;
        }

        return -1 !== $.inArray("ReadUserCompetitionRole", self.rights());
    };

    $scope.shouldShowEditClubsLink = function () {
        if (!self.isLoggedIn()) {
            return false;
        }

        return -1 !== $.inArray("UpdateClub", self.rights());
    };

    $scope.shouldShowEditWeaponsLink = function () {
        if (!self.isLoggedIn()) {
            return false;
        }

        return -1 !== $.inArray("UpdateWeapon", self.rights());
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
