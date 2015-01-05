/// <reference path="/Scripts/angular-ui/ui-bootstrap.js" />
/// <reference path="/Scripts/angular-ui/ui-bootstrap-tpls.js" />
/// <reference path="/Scripts/angular.js" />
/// <reference path="/Scripts/angular-route.js" />
/// <reference path="/Scripts/angular-sanitize.js"/>
/// <reference path="/Scripts/App/AngularModules.js" />
/// <reference path="/Scripts/App/common.js" />

angular.module('winshooter').controller('UserLoggedInController', function ($rootScope, $scope, $location, $modal) {
    $scope.currentTermsAcceptedLevel = 1;
    $scope.firstTimeWinShooter = false;
    $scope.notInitialized = true;

    $scope.init = function () {
        if ($scope.sharedData.currentUser.$resolved === false) {
            // Failsafe
            console.log("AccountLoggedInController: $scope.sharedData.currentUser isnt resolved yet.");
            return;
        }

        $scope.notInitialized = false;
        console.log("AccountLoggedInController: Initalizing. $scope.sharedData: " + JSON.stringify($scope.sharedData));
        if (!$scope.sharedData.isLoggedIn) {
            console.log("AccountLoggedInController: User isn't authenticated, redirect to home page");
            $location.url('/Home/Index');
            return;
        }

        if ($scope.sharedData.currentUser.HasAcceptedTerms === $scope.currentTermsAcceptedLevel) {
            console.log("AccountLoggedInController: User has already accepted the terms, redirect to first page");
            $location.url('/Home/Index');
            return;
        }

        console.log("User needs to accept our terms");
        $scope.firstTimeWinShooter = true;
    };

    $scope.$watch('sharedData.currentUser.$resolved', function () {
        console.log("AccountLoggedInController: AppController has changed sharedData.currentUser. Run our initialization");
        $scope.init();
    });

    $scope.accept = function () {
        $scope.sharedData.currentUser.HasAcceptedTerms = $scope.currentTermsAcceptedLevel;
        console.log("AccountLoggedInController: User accepted terms. Saving");
        $scope.sharedData.currentUser.$save(function () {
            console.log("AccountLoggedInController: Saved. Redirect to home page.");
            $location.url("/Home/Index");
        }, function (data) {
            // Show error dialog.
            var modal = $modal.open({
                templateUrl: 'errorModalContent',
                controller: DialogConfirmController,
                resolve: {
                    items: function () {
                        return {
                            header: "Ett fel inträffade",
                            body: "Kunde inte spara din acceptans"
                        };
                    }
                }
            });
        });
    };

    $scope.doIHaveAChoice = function () {
        // Show error dialog.
        var modal = $modal.open({
            templateUrl: 'informationModalContent',
            controller: DialogConfirmController,
            resolve: {
                items: function () {
                    return {
                        header: "Personuppgiftslagen",
                        body: "Nja, inte direkt. Du kan såklart fortsätta att titta på resultat och tävlingar, men vill du kunna se dina egna resultat eller hjälpa till i sekretariatet på en tävling, så måste du acceptera."
                    };
                }
            }
        });
    };
});