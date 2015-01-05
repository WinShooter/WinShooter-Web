///<reference path="~/Scripts/jasmine.js"/>
///<reference path="~/../WinShooter-Web/Scripts/Angular.js"/>
///<reference path="~/../WinShooter-Web/Scripts/angular-resource.js" />
///<reference path="~/../WinShooter-Web/Scripts/angular-sanitize.js"/>
///<reference path="~/../WinShooter-Web/Scripts/angular-mocks.js" />
///<reference path="~/../WinShooter-Web/Scripts/jquery-2.1.1.js" />
///<reference path="~/../WinShooter-Web/Scripts/App/common.js"/>
///<reference path="~/../WinShooter-Web/Scripts/App/AngularModules.js"/>
///<reference path="~/../WinShooter-Web/Scripts/App/Controllers/UserLoggedInController.js"/>

'use strict';

describe("AngularModules-AccountLoggedInController", function () {
    // For now, disable resharper callback
    //jasmine.getEnv().currentRunner_.finishCallback = function () { };

    //we'll use this scope in our tests
    var scope;
    debugger;

    //mock Application to allow us to inject our own dependencies
    beforeEach(angular.mock.module('winshooter'));

    // backend definition common for all tests
    var $httpBackend;
    beforeEach(angular.mock.inject(function ($injector) {
        // backend definition common for all tests
        $httpBackend = $injector.get('$httpBackend');
    }));

    // Check the httpBackend for all operations
    afterEach(function() {
        $httpBackend.verifyNoOutstandingExpectation();
    });

    // tests start here
    it('User isnt authenticated', function () {
        angular.mock.inject(function ($rootScope, $controller, $location, currentUserFactory) {
            // Init $location
            $location.url("/Account/LoggedIn");

            //create an empty scope
            scope = $rootScope.$new();
            scope.sharedData = {};
            scope.sharedData.currentUser = {};
            scope.sharedData.currentUser.HasAcceptedTerms = 0;
            scope.sharedData.currentUser.$resolved = "not resolved";
            scope.isLoggedIn = false;

            //declare the controller and inject our empty scope
            var myController = $controller('AccountLoggedInController', {
                $scope: scope,
                $location: $location,
                currentUserFactory: currentUserFactory
            });

            expect(myController).toBeDefined();

            // Simulate resolving user data
            scope.sharedData.currentUser.$resolved = "resolved";
            scope.$apply();

            // Check the redirection
            expect($location.url()).toEqual('/Home/Index');
        });
    });

    // tests start here
    it('User has not accepted the terms', function () {
        angular.mock.inject(function ($rootScope, $controller, $location) {
            // Init $location
            $location.url("/Account/LoggedIn");

            //create an empty scope
            scope = $rootScope.$new();
            scope.sharedData = {};
            scope.sharedData.isLoggedIn = true;
            scope.sharedData.currentUser = {};
            scope.sharedData.currentUser.HasAcceptedTerms = 0;
            scope.sharedData.currentUser.hasBeenSaved = false;
            scope.sharedData.currentUser.$save = function(callback) {
                // Fake the storing
                scope.sharedData.currentUser.hasBeenSaved = true;
                callback();
            };

            //declare the controller and inject our empty scope
            var myController = $controller('AccountLoggedInController', {
                $scope: scope,
                $location: $location
            });

            expect(myController).toBeDefined();

            // Check the result before HTTP
            expect(scope.notInitialized).toEqual(true);
            expect(scope.firstTimeWinShooter).toEqual(false);

            // Simulate resolving user data
            scope.sharedData.currentUser.$resolved = "resolved";
            scope.$apply();

            // Check the result after HTTP
            expect(scope.notInitialized).toEqual(false);
            expect(scope.firstTimeWinShooter).toEqual(true);

            // Let user click accept
            scope.accept();
            scope.$apply();

            // Run the HTTP request
            expect(scope.sharedData.currentUser.hasBeenSaved).toEqual(true);
            expect($location.url()).toEqual('/Home/Index');
        });
    });

    // tests start here
    it('User has authenticated before', function () {
        angular.mock.inject(function ($rootScope, $controller, $location, currentUserFactory) {
            // Init $location
            $location.url("/Account/LoggedIn");

            //create an empty scope
            scope = $rootScope.$new();
            scope.sharedData = {};
            scope.sharedData.currentUser = {};
            scope.sharedData.currentUser.HasAcceptedTerms = 1;
            scope.isLoggedIn = true;

            //declare the controller and inject our empty scope
            var myController = $controller('AccountLoggedInController', {
                $scope: scope,
                $location: $location,
                currentUserFactory: currentUserFactory
            });

            expect(myController).toBeDefined();

            // Simulate resolving user data
            scope.sharedData.currentUser.$resolved = "resolved";
            scope.$apply();

            // Check the redirection
            expect($location.url()).toEqual('/Home/Index');
        });
    });
});