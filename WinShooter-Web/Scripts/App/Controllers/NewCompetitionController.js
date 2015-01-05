/// <reference path="/Scripts/App/common.js" />
/// <reference path="/Scripts/angular-ui/ui-bootstrap.js" />
/// <reference path="/Scripts/angular-ui/ui-bootstrap-tpls.js" />
/// <reference path="/Scripts/angular.js" />
/// <reference path="/Scripts/angular-route.js" />
/// <reference path="/Scripts/angular-sanitize.js"/>

// Here the module for the competition page
angular.module('winshooter').controller('NewCompetitionController', function ($rootScope, $scope, $modal, $http, $location, competitionFactory, currentUserFactory) {
    $scope.currentUser = { IsLoggedIn: false, Rights: [] };

    // Attributes for adding new competition
    $scope.Name = "";
    $scope.StartDate = new Date();
    $scope.StartDate.setHours(9);
    $scope.StartDate.setMinutes(0);
    $scope.StartDate.setSeconds(0);
    $scope.StartDate.setMilliseconds(0);

    $scope.IsPublic = false;
    $scope.UseNorwegianCount = false;

    // UI attributes
    $scope.datePickerFormat = 'yyyy-MM-dd';
    $scope.datePickerShowWeeks = true;
    $scope.datePickerDateOptions = { 'year-format': "'yy'", 'starting-day': 1 };

    // Init our page
    init();

    function init() {
        $scope.currentUser = currentUserFactory.search({ CompetitionId: window.competitionId }, function (currentUserData) {
            // Nothing to do here. Carry on!
        }, function () {
            var error = "Misslyckades med att hämta användaruppgifter";
            if (data !== undefined && data.data !== undefined && data.data.ResponseStatus !== undefined && data.data.ResponseStatus.Message !== undefined) {
                error += ":<br />" + JSON.stringify(data.data.ResponseStatus.Message);
            } else {
                error += ".";
            }
            // Show error dialog.
            var modal = $modal.open({
                templateUrl: 'errorModalContent',
                controller: DialogConfirmController,
                resolve: {
                    items: function () {
                        return {
                            header: "Ett fel inträffade",
                            body: error
                        };
                    }
                }
            });
        });
    }

    $scope.addCompetition = function () {
        var competition = {
            CompetitionId: "",
            CompetitionType: "Field",
            Name: $scope.Name,
            UseNorwegianCount: $scope.UseNorwegianCount,
            IsPublic: $scope.IsPublic,
            StartDate: $scope.StartDate.toISOString()
        };

        $http.post(competitionApiUrl, competition)
            .success(function (data, status) {
                window.competitionId = data.CompetitionId;
                var newLocation = "/Home/Competition/" + data.CompetitionId;
                $location.url(newLocation);
            }).error(function (data, status) {
                var error = "Misslyckades med att lägga till tävlingen";
                if (data !== undefined && data.data !== undefined && data.data.ResponseStatus !== undefined && data.data.ResponseStatus.Message !== undefined) {
                    error += ":<br />" + JSON.stringify(data.data.ResponseStatus.Message);
                } else {
                    error += ".";
                }
                // Show error dialog.
                var modal = $modal.open({
                    templateUrl: 'errorModalContent',
                    controller: DialogConfirmController,
                    resolve: {
                        items: function () {
                            return {
                                header: "Ett fel inträffade",
                                body: error
                            };
                        }
                    }
                });
            });
    };
});