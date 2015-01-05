/// <reference path="/Scripts/App/common.js" />
/// <reference path="/Scripts/angular-ui/ui-bootstrap.js" />
/// <reference path="/Scripts/angular-ui/ui-bootstrap-tpls.js" />
/// <reference path="/Scripts/angular.js" />
/// <reference path="/Scripts/angular-route.js" />
/// <reference path="/Scripts/angular-sanitize.js"/>

// Create the angular module
var winshooterModule = angular.module('winshooter', ['ngRoute', 'ngResource', 'ngSanitize', 'ui.bootstrap'])
    .config(function ($routeProvider, $locationProvider, $logProvider) {
    $routeProvider
        .when("/Account/Login", { templateUrl: "/partials/accountlogin.html" })
        .when("/Home/NewCompetition", { templateUrl: "/partials/newcompetition.html", controller: "NewCompetitionController" })
        .when("/Home/Competition/:competitionId", { templateUrl: "/partials/competition.html", controller: "CompetitionController" })
        .when("/Home/Stations/:competitionId", { templateUrl: "/partials/stations.html", controller: "StationsController" })
        .when("/Home/Patrols/:competitionId", { templateUrl: "/partials/patrols.html", controller: "PatrolsController" })
        .when("/Home/About", { templateUrl: "/partials/about.html" })
        .when("/Home/Privacy", { templateUrl: "/partials/privacy.html" })
        .when("/User/LoggedIn", { templateUrl: "/partials/userloggedin.html", controller: "UserLoggedInController" })
        .otherwise({ templateUrl: "/partials/index.html", controller: "IndexController" });

    $locationProvider.html5Mode(true);
    $logProvider.debugEnabled(true);
});

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
            competitionId: '@CompetitionId'
        }, {
            query: { method: 'GET', isArray: false },
            search: { method: 'GET', isArray: false },
            update: { method: 'POST', isArray: false }
        });
    }
]);

// Create factory for stations
winshooterModule.factory('stationsFactory', [
    '$resource', function ($resource) {
        return $resource(stationsApiUrl, {
            competitionId: '@CompetitionId',
            stationId: '@StationId'
        }, {
            query: { method: 'GET', isArray: true },
            search: { method: 'GET', isArray: false }
        });
    }
]);

// Create factory for patrols
winshooterModule.factory('patrolsFactory', [
    '$resource', function ($resource) {
        return $resource(patrolsApiUrl, {
            competitionId: '@CompetitionId',
            patrolId: '@PatrolId'
        }, {
            query: { method: 'GET', isArray: true },
            search: { method: 'GET', isArray: false }
        });
    }
]);

winshooterModule.directive('repeatDone', function() {
    return function(scope, element, attrs) {
        if (scope.$last) { // all are rendered
            scope.$eval(attrs.repeatDone);
        }
    };
});

// Controller for dialog modal instances.
var DialogConfirmController = function ($scope, $modalInstance, items) {
    $scope.header = items.header;
    $scope.body = items.body;

    $scope.ok = function () {
        $modalInstance.close();
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
};