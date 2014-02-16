///<reference path="~/Scripts/jasmine.js"/>
///<reference path="~/../WinShooter-Web/Scripts/Angular.js"/>
///<reference path="~/../WinShooter-Web/Scripts/angular-resource.js" />
///<reference path="~/../WinShooter-Web/Scripts/angular-sanitize.js"/>
///<reference path="~/../WinShooter-Web/Scripts/angular-mocks.js" />
///<reference path="~/../WinShooter-Web/Scripts/jquery-2.0.3.js" />
///<reference path="~/../WinShooter-Web/Scripts/App/common.js"/>
///<reference path="~/../WinShooter-Web/Scripts/App/AngularModules.js"/>
///<reference path="~/../WinShooter-Web/Scripts/App/Controllers/CompetitionController.js"/>

'use strict';

describe("AngularModules-CompetitionController", function () {
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
    it('There is no competition to show (error case)', function () {
        angular.mock.inject(function ($rootScope, $controller, $routeParams, $modal, competitionFactory, currentUserFactory) {
            //create an empty scope
            scope = $rootScope.$new();
            scope.sharedData = {};
            scope.sharedData.isLoggedIn = true;
            scope.sharedData.competitionId = "A6109CFD-C4D8-4003-A6E7-A2BB006A81EA";
            scope.sharedData.rights = [];
            scope.sharedData.userHasRight = function (right) {
                var toReturn = -1 !== $.inArray(right, scope.sharedData.rights);
                console.log("SharedData: User has right '" + right + "': " + toReturn);
                return toReturn;
            };
            scope.initCheckboxes = function () {
                // Don't do anything
            };

            // backend response
            $httpBackend.when('GET', '/api/competition?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA').respond({}, {});
            $httpBackend.expectGET('/api/competition?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA');

            // Set the current cometition
            window.competitionId = "";

            //declare the controller and inject our empty scope
            var myController = $controller('CompetitionController', {
                $scope: scope,
                $routeParams: $routeParams,
                $modal: $modal,
                competitionFactory: competitionFactory,
                currentUserFactory: currentUserFactory
            });

            expect(myController).toBeDefined();

            // Check the result before HTTP
            expect(scope.userCanUpdateCompetition).toEqual(false);
            expect(scope.userCanDeleteCompetition).toEqual(false);

            // Run the HTTP request
            $httpBackend.flush();
        });
    });

    it('There is one competition to show', function () {
        angular.mock.inject(function ($rootScope, $controller, $routeParams, $modal, competitionFactory, currentUserFactory) {
            //create an empty scope
            scope = $rootScope.$new();
            scope.sharedData = {};
            scope.sharedData.isLoggedIn = true;
            scope.sharedData.competitionId = "A6109CFD-C4D8-4003-A6E7-A2BB006A81EA";
            scope.sharedData.rights = [];
            scope.sharedData.userHasRight = function (right) {
                var toReturn = -1 !== $.inArray(right, scope.sharedData.rights);
                console.log("SharedData: User has right '" + right + "': " + toReturn);
                return toReturn;
            };
            scope.initCheckboxes = function () {
                // Don't do anything
            };

            // backend response
            $httpBackend.when('GET', '/api/competition?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA').respond({ "CompetitionId": "A6109CFD-C4D8-4003-A6E7-A2BB006A81EA", "CompetitionType": "Field", "IsPublic": true, "Name": "Name", "StartDate": "2014-01-31T20:41:00.000Z", "UseNorwegianCount": true }, {});
            $httpBackend.expectGET('/api/competition?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA');

            // Set the current competition
            window.competitionId = "A6109CFD-C4D8-4003-A6E7-A2BB006A81EA";
            $routeParams.competitionId = window.competitionId;

            //declare the controller and inject our empty scope
            var myController = $controller('CompetitionController', {
                $scope: scope,
                $routeParams: $routeParams,
                $modal: $modal,
                competitionFactory: competitionFactory,
                currentUserFactory: currentUserFactory
            });

            expect(myController).toBeDefined();

            // Check the result before HTTP
            expect(scope.competition.Name).toBeUndefined();
            expect(scope.competition.StartDate).toBeUndefined();
            expect(scope.competition.CompetitionId).toBeUndefined();
            expect(scope.competition.IsPublic).toBeUndefined();
            expect(scope.competition.UseNorwegianCount).toBeUndefined();

            // Run the HTTP request
            $httpBackend.flush();

            // Check the result after HTTP
            expect(scope.competition.Name).toEqual("Name");
            expect(scope.competition.StartDate).toEqual(new Date("2014-01-31T20:41:00.000Z"));
            expect(scope.competition.CompetitionId).toEqual("A6109CFD-C4D8-4003-A6E7-A2BB006A81EA");
            expect(scope.competition.IsPublic).toEqual(true);
            expect(scope.competition.UseNorwegianCount).toEqual(true);
        });
    });

    it('Update competition', function () {
        angular.mock.inject(function ($rootScope, $controller, $routeParams, $modal, competitionFactory, currentUserFactory) {
            //create an empty scope
            scope = $rootScope.$new();
            scope.sharedData = {};
            scope.sharedData.isLoggedIn = true;
            scope.sharedData.competitionId = "A6109CFD-C4D8-4003-A6E7-A2BB006A81EA";
            scope.sharedData.rights = [];
            scope.sharedData.userHasRight = function (right) {
                var toReturn = -1 !== $.inArray(right, scope.sharedData.rights);
                console.log("SharedData: User has right '" + right + "': " + toReturn);
                return toReturn;
            };
            scope.initCheckboxes = function () {
                // Don't do anything
            };

            // backend response
            $httpBackend.when('GET', '/api/competition?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA').respond({ "CompetitionId": "A6109CFD-C4D8-4003-A6E7-A2BB006A81EA", "CompetitionType": "Field", "IsPublic": true, "Name": "Name", "StartDate": "2014-01-31T20:41:00.000Z", "UseNorwegianCount": true }, {});
            $httpBackend.expectGET('/api/competition?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA');

            // Set the current competition
            window.competitionId = "A6109CFD-C4D8-4003-A6E7-A2BB006A81EA";
            $routeParams.competitionId = window.competitionId;

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
            var myController = $controller('CompetitionController', {
                $scope: scope,
                $routeParams: $routeParams,
                $modal: $modal,
                competitionFactory: competitionFactory,
                currentUserFactory: currentUserFactory
            });

            expect(myController).toBeDefined();

            // Check the result before HTTP
            expect(scope.competition.Name).toBeUndefined();
            expect(scope.competition.StartDate).toBeUndefined();
            expect(scope.competition.CompetitionId).toBeUndefined();
            expect(scope.competition.IsPublic).toBeUndefined();
            expect(scope.competition.UseNorwegianCount).toBeUndefined();

            // Run the HTTP request
            $httpBackend.flush();

            // Update the values
            scope.competition.Name = "updated";
            scope.competition.StartDate = new Date("2014-02-11T10:55:00.000Z");
            scope.competition.IsPublic = false;

            // Prepare for HTTP update
            $httpBackend.when('POST', '/api/competition?competitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA').respond({}, {});
            $httpBackend.expectPOST(
                '/api/competition?competitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA',
                '{"CompetitionId":"A6109CFD-C4D8-4003-A6E7-A2BB006A81EA","CompetitionType":"Field","IsPublic":false,"Name":"updated","StartDate":"2014-02-11T10:55:00.000Z","UseNorwegianCount":false}');

            // Tell controller to update
            scope.updateCompetition();

            // Run the HTTP request
            $httpBackend.flush();

            // Check the result after HTTP
            $httpBackend.verifyNoOutstandingExpectation();
        });
    });

    it('Delete competition', function () {
        angular.mock.inject(function ($rootScope, $controller, $routeParams, $modal, competitionFactory, currentUserFactory) {
            //create an empty scope
            scope = $rootScope.$new();
            scope.sharedData = {};
            scope.sharedData.isLoggedIn = true;
            scope.sharedData.competitionId = "A6109CFD-C4D8-4003-A6E7-A2BB006A81EA";
            scope.sharedData.rights = [];
            scope.sharedData.userHasRight = function (right) {
                var toReturn = -1 !== $.inArray(right, scope.sharedData.rights);
                console.log("SharedData: User has right '" + right + "': " + toReturn);
                return toReturn;
            };
            scope.initCheckboxes = function() {
                // Don't do anything
            };

            // backend response
            $httpBackend.when('GET', '/api/competition?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA').respond({ "CompetitionId": "A6109CFD-C4D8-4003-A6E7-A2BB006A81EA", "CompetitionType": "Field", "IsPublic": true, "Name": "Name", "StartDate": "2014-01-31T20:41:00.000Z", "UseNorwegianCount": true }, {});
            $httpBackend.expectGET('/api/competition?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA');

            // Set the current competition
            window.competitionId = "A6109CFD-C4D8-4003-A6E7-A2BB006A81EA";
            $routeParams.competitionId = window.competitionId;

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
            var myController = $controller('CompetitionController', {
                $scope: scope,
                $routeParams: $routeParams,
                $modal: $modal,
                competitionFactory: competitionFactory,
                currentUserFactory: currentUserFactory
            });

            expect(myController).toBeDefined();

            // Check the result before HTTP
            expect(scope.competition.Name).toBeUndefined();
            expect(scope.competition.StartDate).toBeUndefined();
            expect(scope.competition.CompetitionId).toBeUndefined();
            expect(scope.competition.IsPublic).toBeUndefined();
            expect(scope.competition.UseNorwegianCount).toBeUndefined();

            // Run the HTTP request
            $httpBackend.flush();

            // Update the values
            scope.competition.Name = "updated";
            scope.competition.StartDate = new Date("2014-02-11T10:55:00.000Z");
            scope.competition.IsPublic = false;

            // Prepare for HTTP update
            $httpBackend.when('DELETE', '/api/competition?competitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA').respond({}, {});
            $httpBackend.expectDELETE(
                '/api/competition?competitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA',
                { "Accept": "application/json, text/plain, */*" });

            // Tell controller to delete
            scope.deleteCompetition();

            // Press yes on modal
            fakeModal.close(fakeModal);

            // Run the HTTP request
            $httpBackend.flush();

            // Check the result after HTTP
            $httpBackend.verifyNoOutstandingExpectation();
        });
    });
});