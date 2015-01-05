/// <reference path="/Scripts/App/common.js" />
/// <reference path="/Scripts/angular-ui/ui-bootstrap.js" />
/// <reference path="/Scripts/angular-ui/ui-bootstrap-tpls.js" />
/// <reference path="/Scripts/angular.js" />
/// <reference path="/Scripts/angular-route.js" />
/// <reference path="/Scripts/angular-sanitize.js"/>

// Here the module for the index page
angular.module('winshooter').controller('IndexController', function ($rootScope, $scope, $modal, $location, competitionsFactory) {
    window.competitionId = '';

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
        console.log("IndexController: Initalizating. Running competitions query");
        $scope.competitions = competitionsFactory.query(function () {
            console.log("IndexController: Got " + $scope.competitions.length + " competitions.");
            if ($scope.competitions.length > 0) {
                // We got some competitions back. Select first one.
                $scope.selectedCompetition = $scope.competitions[0];
            } else {
                $scope.selectedCompetition.Name = "Inga tävlingar hittades.";
            }

            $scope.competitions.forEach(function (competition) {
                competition.StartDate = new Date(competition.StartDate);
            });

            $scope.initCheckboxes();
        }, function (data) {
            console.log("IndexController: Failed to retrive competitions:" + JSON.stringify(data));
            var error = "Misslyckades med att hämta tävlingar";
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

    // Selects the current competition
    $scope.selectCompetitionOnServer = function () {
        var newLocation = "/Home/Competition/";
        if ($scope.selectedCompetition !== undefined) {
            console.log("IndexController: Setting new competitionId: " + $scope.selectedCompetition.CompetitionId);
            $scope.sharedData.competitionId = $scope.selectedCompetition.CompetitionId;
            newLocation = newLocation + $scope.selectedCompetition.CompetitionId;
            $location.path(newLocation);
        }
    };
});
