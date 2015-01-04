/// <reference path="/Scripts/App/common.js" />
/// <reference path="/Scripts/angular-ui/ui-bootstrap.js" />
/// <reference path="/Scripts/angular-ui/ui-bootstrap-tpls.js" />
/// <reference path="/Scripts/angular.js" />
/// <reference path="/Scripts/angular-route.js" />
/// <reference path="/Scripts/angular-sanitize.js"/>

// Here the module for the competition page
angular.module('winshooter').controller('CompetitionController', function ($scope, $routeParams, $modal, competitionFactory, currentUserFactory) {
    console.log("CompetitionController: starting");
    if ($routeParams.competitionId != undefined) {
        $scope.sharedData.competitionId = $routeParams.competitionId;
    }

    // Attributes for showing existing competition
    $scope.competition = {};
    $scope.userCanUpdateCompetition = false;
    $scope.userCanDeleteCompetition = false;

    $scope.initRights = function() {
        console.log("CompetitionController: Initializing rights");

        $scope.userCanUpdateCompetition = $scope.sharedData.userHasRight("UpdateCompetition");
        $scope.userCanDeleteCompetition = $scope.sharedData.userHasRight("DeleteCompetition");

        console.log("CompetitionController: Initialized rights");
    };

    $scope.init = function() {
        console.log("CompetitionController: Initializing");

        $scope.competition = competitionFactory.search({ CompetitionId: $scope.sharedData.competitionId }, function() {
            // Nothing to do here. Carry on!
            $scope.competition.StartDate = new Date($scope.competition.StartDate);
            $scope.initCheckboxes();
        }, function(data) {
            var error = "Misslyckades med att hämta tävling";
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
                    items: function() {
                        return {
                            header: "Ett fel inträffade",
                            body: error
                        };
                    }
                }
            });
        });
    };

    $scope.$watch('sharedData.competitionId', function () {
        console.log("CompetitionController: competition has changed, re-initialize");
        $scope.init();
    });

    $scope.$watch('sharedData.rights', function () {
        console.log("CompetitionController: rights has changed, re-initialize");
        $scope.initRights();
    });

    $scope.updateCompetition = function () {
        if ($scope.competition === undefined) {
            // What? Should never happen.
            var error = "Du måste välja en tävling att uppdatera.";

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
            return false;
        }

        // For some reason angular cannot have a two way binding to a class.
        // Let's pick it out with jQuery instead.
        $scope.competition.IsPublic = $("label[for='selectedCompetitionIsPublic']").hasClass('checked');
        $scope.competition.UseNorwegianCount = $("label[for='selectedCompetitionUseNorwegianCount']").hasClass('checked');

        $scope.competition.$save(function () {
            // Show success dialog.
            var modal = $modal.open({
                templateUrl: 'informationModalContent',
                controller: DialogConfirmController,
                resolve: {
                    items: function () {
                        return {
                            header: "Sparad",
                            body: "Tävlingen sparades"
                        };
                    }
                }
            });
        }, function () {
            // Show error dialog.
            var modal = $modal.open({
                templateUrl: 'errorModalContent',
                controller: DialogConfirmController,
                resolve: {
                    items: function () {
                        return {
                            header: "Ett fel inträffade",
                            body: "Kunde inte spara tävlingen"
                        };
                    }
                }
            });
        });

        return false;
    };

    $scope.deleteCompetition = function () {
        if ($scope.competition === undefined) {
            // What? Should never happen.
            var error = "Du måste välja en tävling att radera.";
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
            return false;
        }

        // Show confirmation dialog first.
        var modal = $modal.open({
            templateUrl: 'confirmationModalContent',
            controller: DialogConfirmController,
            resolve: {
                items: function () {
                    return {
                        header: "Radera tävling",
                        body: "Är du säker på att du vill radera " + $scope.competition.Name + "?"
                    };
                }
            }
        });

        modal.result.then(function () {
            // In a production site we would submit the order via Ajax here.
            $scope.competition.$delete(function (data, status) {
                var newLocation = "/";
                window.location.href = newLocation;
            }, function (data, status) {
                var error = "Misslyckades med att radera tävling";
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
        }, function () {
            // Do nothing.
        });

        return false;
    };
});
