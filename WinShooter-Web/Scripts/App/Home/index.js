// The different JSON urls
var competitionsApiUrl = "/api/competitions";
var competitionApiUrl = "/api/competition";

var winshooterModule = angular.module('winshooter', ['ngResource']);

winshooterModule.factory('competitionsFactory', [
    '$resource', function($resource) {
        return $resource(competitionsApiUrl, {}, {
            query: { method: 'GET', isArray: true }
        });
    }
]);

winshooterModule.controller('BodyController', function ($scope, competitionsFactory) {
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
        }, function() {
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
