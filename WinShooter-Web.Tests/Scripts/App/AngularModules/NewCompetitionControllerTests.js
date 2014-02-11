///<reference path="~/Scripts/jasmine.js"/>
///<reference path="~/../WinShooter-Web/Scripts/Angular.js"/>
///<reference path="~/../WinShooter-Web/Scripts/angular-resource.js" />
///<reference path="~/../WinShooter-Web/Scripts/angular-sanitize.js"/>
///<reference path="~/../WinShooter-Web/Scripts/angular-mocks.js" />
///<reference path="~/../WinShooter-Web/Scripts/jquery-2.0.3.js" />
///<reference path="~/../WinShooter-Web/Scripts/App/common.js"/>
///<reference path="~/../WinShooter-Web/Scripts/App/AngularModules.js"/>

'use strict';

describe("AngularModules-NewCompetitionController", function () {
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
    it('User isnt IsLoggedIn in', function () {
        angular.mock.inject(function ($rootScope, $controller, $routeParams, $modal, competitionFactory, currentUserFactory) {
            //create an empty scope
            scope = $rootScope.$new();

            // backend response
            $httpBackend.when('GET', '/api/currentuser?CompetitionId=').respond({ IsLoggedIn: false }, {});
            $httpBackend.expectGET('/api/currentuser?CompetitionId=');

            // Set the current cometition
            window.competitionId = "";

            //declare the controller and inject our empty scope
            var myController = $controller('NewCompetitionController', {
                $scope: scope,
                $routeParams: $routeParams,
                $modal: $modal,
                competitionFactory: competitionFactory,
                currentUserFactory: currentUserFactory
            });

            expect(myController).toBeDefined();

            // Check the result before HTTP
            expect(scope.Name).toEqual('');
            var expectedDate = new Date();
            expectedDate.setHours(9);
            expectedDate.setMinutes(0);
            expectedDate.setSeconds(0);
            expectedDate.setMilliseconds(0);
            expect(scope.StartDate).toEqual(expectedDate);
            expect(scope.IsPublic).toEqual(false);
            expect(scope.UseNorwegianCount).toEqual(false);

            // Run the HTTP request
            $httpBackend.flush();

            // Check some values
            expect(scope.currentUser.IsLoggedIn).toEqual(false);
        });
    });

    // tests start here
    it('Create new competition', function () {
        angular.mock.inject(function ($rootScope, $controller, $routeParams, $modal, $location, competitionFactory, currentUserFactory) {
            //create an empty scope
            scope = $rootScope.$new();

            // backend response
            $httpBackend.when('GET', '/api/currentuser?CompetitionId=').respond({ IsLoggedIn: true }, {});
            $httpBackend.expectGET('/api/currentuser?CompetitionId=');

            // Set the current cometition
            window.competitionId = "";

            // Prepare for catching the modal
            var fakeModal = {
                result: {
                    then: function (confirmCallback, cancelCallback) {
                        //Store the callbacks for later when the user clicks on the OK or Cancel button of the dialog
                        this.confirmCallBack = confirmCallback;
                        this.cancelCallback = cancelCallback;
                    }
                },
                close: function (item) {
                    //The user clicked OK on the modal dialog, call the stored confirm callback with the selected item
                    this.result.confirmCallBack(item);
                },
                dismiss: function (type) {
                    //The user clicked cancel on the modal dialog, call the stored cancel callback
                    this.result.cancelCallback(type);
                }
            };
            spyOn($modal, 'open').andReturn(fakeModal);

            //declare the controller and inject our empty scope
            var myController = $controller('NewCompetitionController', {
                $scope: scope,
                $routeParams: $routeParams,
                $modal: $modal,
                $location: $location,
                competitionFactory: competitionFactory,
                currentUserFactory: currentUserFactory
            });

            expect(myController).toBeDefined();

            // Check the result before HTTP
            expect(scope.Name).toEqual('');
            var expectedDate = new Date();
            expectedDate.setHours(9);
            expectedDate.setMinutes(0);
            expectedDate.setSeconds(0);
            expectedDate.setMilliseconds(0);
            expect(scope.StartDate).toEqual(expectedDate);
            expect(scope.IsPublic).toEqual(false);
            expect(scope.UseNorwegianCount).toEqual(false);

            // Run the HTTP request
            $httpBackend.flush();

            // Check some values
            expect(scope.currentUser.IsLoggedIn).toEqual(true);

            // Set user input
            scope.Name = "Some name";
            scope.IsPublic = true;
            scope.UseNorwegianCount = true;

            // Prepare for competition
            $httpBackend.when('POST', '/api/competition').respond({ 'CompetitionId': 'A6109CFD-C4D8-4003-A6E7-A2BB006A81EA' }, {});
            $httpBackend.expectPOST('/api/competition');

            // Create the competition
            scope.addCompetition();

            // Run the HTTP request
            $httpBackend.flush();

            // Check the redirect
            expect($location.path()).toEqual('/home/competition/A6109CFD-C4D8-4003-A6E7-A2BB006A81EA');
        });
    });
});