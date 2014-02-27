///<reference path="~/Scripts/jasmine.js"/>
///<reference path="~/../WinShooter-Web/Scripts/Angular.js"/>
///<reference path="~/../WinShooter-Web/Scripts/angular-resource.js" />
///<reference path="~/../WinShooter-Web/Scripts/angular-sanitize.js"/>
///<reference path="~/../WinShooter-Web/Scripts/angular-mocks.js" />
///<reference path="~/../WinShooter-Web/Scripts/jquery-2.0.3.js" />
///<reference path="~/../WinShooter-Web/Scripts/App/common.js"/>
///<reference path="~/../WinShooter-Web/Scripts/App/AngularModules.js"/>
///<reference path="~/../WinShooter-Web/Scripts/App/Controllers/PatrolsController.js"/>

'use strict';

describe("AngularModules-PatrolsController", function () {
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
    it('No logged in user, no patrols', function () {
        angular.mock.inject(function ($rootScope, $controller, $routeParams, $modal, patrolsFactory) {
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

            // backend response
            $httpBackend.when('GET', '/api/patrols?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA').respond([], {});
            $httpBackend.expectGET('/api/patrols?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA');

            // Set the current cometition
            $routeParams.competitionId = scope.sharedData.competitionId;

            //declare the controller and inject our empty scope
            var myController = $controller('PatrolsController', {
                $scope: scope,
                $routeParams: $routeParams,
                $modal: $modal,
                patrolsFactory: patrolsFactory
            });

            expect(myController).toBeDefined();

            // Check the result before HTTP
            expect(scope.userCanCreatePatrol).toEqual(false);
            expect(scope.userCanUpdatePatrol).toEqual(false);
            expect(scope.userCanDeletePatrol).toEqual(false);

            // Run the HTTP request
            $httpBackend.flush();

            // Check the result after HTTP
            expect(scope.userCanCreatePatrol).toEqual(false);
            expect(scope.userCanUpdatePatrol).toEqual(false);
            expect(scope.userCanDeletePatrol).toEqual(false);
            expect(scope.patrols.length).toEqual(0);
        });
    });

    // tests start here
    it('Logged in user with rights, no patrols', function () {
        angular.mock.inject(function ($rootScope, $controller, $routeParams, $modal, patrolsFactory) {
            //create an empty scope
            scope = $rootScope.$new();
            scope.sharedData = {};
            scope.sharedData.isLoggedIn = true;
            scope.sharedData.competitionId = "A6109CFD-C4D8-4003-A6E7-A2BB006A81EA";
            scope.sharedData.rights = ['CreatePatrol', 'UpdatePatrol', 'DeletePatrol'];
            scope.sharedData.userHasRight = function (right) {
                var toReturn = -1 !== $.inArray(right, scope.sharedData.rights);
                console.log("SharedData: User has right '" + right + "': " + toReturn);
                return toReturn;
            };

            // backend response
            $httpBackend.when('GET', '/api/patrols?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA').respond([], {});
            $httpBackend.expectGET('/api/patrols?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA');

            // Set the current cometition
            $routeParams.competitionId = scope.sharedData.competitionId;

            //declare the controller and inject our empty scope
            var myController = $controller('PatrolsController', {
                $scope: scope,
                $routeParams: $routeParams,
                $modal: $modal,
                patrolsFactory: patrolsFactory
            });

            expect(myController).toBeDefined();

            // Check the result before HTTP
            expect(scope.userCanCreatePatrol).toEqual(false);
            expect(scope.userCanUpdatePatrol).toEqual(false);
            expect(scope.userCanDeletePatrol).toEqual(false);

            // Run the HTTP request
            $httpBackend.flush();

            // Check the result after HTTP
            expect(scope.userCanCreatePatrol).toEqual(true);
            expect(scope.userCanUpdatePatrol).toEqual(true);
            expect(scope.userCanDeletePatrol).toEqual(true);
            expect(scope.patrols.length).toEqual(0);
        });
    });

    // tests start here
    it('Logged in user with rights, one patrol', function () {
        angular.mock.inject(function ($rootScope, $controller, $routeParams, $modal, patrolsFactory) {
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

            // backend response
            $httpBackend.when('GET', '/api/patrols?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA').respond([{ "CompetitionId": "74ec4f92-4b72-4c40-927a-de308269e074", "PatrolId": "74ec4f92-4b72-4c40-927a-de308269e074", "PatrolNumber": 2, "StartTime": "2014-02-16T10:27:00.000Z" }], {});
            $httpBackend.expectGET('/api/patrols?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA');

            // Set the current cometition
            $routeParams.competitionId = scope.sharedData.competitionId;

            //declare the controller and inject our empty scope
            var myController = $controller('PatrolsController', {
                $scope: scope,
                $routeParams: $routeParams,
                $modal: $modal,
                patrolsFactory: patrolsFactory
            });

            expect(myController).toBeDefined();

            // Run the HTTP request
            $httpBackend.flush();

            // Check the result after HTTP
            expect(scope.patrols.length).toEqual(1);
            expect(scope.patrols[0].PatrolId).toEqual("74ec4f92-4b72-4c40-927a-de308269e074");
            expect(scope.patrols[0].StartTime).toEqual(new Date("2014-02-16T10:27:00.000Z"));
        });
    });

    // tests start here
    it('Logged in user with rights, no patrols, add patrol', function () {
        angular.mock.inject(function ($rootScope, $controller, $routeParams, $modal, $http, patrolsFactory) {
            //create an empty scope
            scope = $rootScope.$new();
            scope.sharedData = {};
            scope.sharedData.isLoggedIn = true;
            scope.sharedData.competitionId = "A6109CFD-C4D8-4003-A6E7-A2BB006A81EA";
            scope.sharedData.rights = ['CreateStation', 'UpdateStation', 'DeleteStation'];
            scope.sharedData.userHasRight = function (right) {
                var toReturn = -1 !== $.inArray(right, scope.sharedData.rights);
                console.log("SharedData: User has right '" + right + "': " + toReturn);
                return toReturn;
            };

            // backend response
            $httpBackend.when('GET', '/api/patrols?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA').respond([], {});
            $httpBackend.expectGET('/api/patrols?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA');

            // Set the current cometition
            $routeParams.competitionId = scope.sharedData.competitionId;

            //declare the controller and inject our empty scope
            var myController = $controller('PatrolsController', {
                $rootScope: scope,
                $scope: scope,
                $routeParams: $routeParams,
                $modal: $modal,
                $http: $http,
                patrolsFactory: patrolsFactory
            });

            expect(myController).toBeDefined();

            // Run the HTTP request
            $httpBackend.flush();

            // Check the result after HTTP
            expect(scope.patrols.length).toEqual(0);

            // Prepare to add station
            $httpBackend.when('POST', '/api/patrols').respond({ 'CompetitionId': 'A6109CFD-C4D8-4003-A6E7-A2BB006A81EA' }, {});
            $httpBackend.expectPOST('/api/patrols', '{"CompetitionId":"A6109CFD-C4D8-4003-A6E7-A2BB006A81EA","PatrolId":"","PatrolNumber":-1,"StartTime":"","PatrolClass":0}');

            $httpBackend.when('GET', '/api/patrols?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA').respond([], {});
            $httpBackend.expectGET('/api/patrols?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA');

            // Add station
            scope.addNewPatrol();

            // Run the HTTP request
            $httpBackend.flush();
        });
    });

    // tests start here
    it('Logged in user with rights, one patrol, edit patrol', function () {
        angular.mock.inject(function ($rootScope, $controller, $routeParams, $modal, patrolsFactory) {
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

            // backend response
            $httpBackend.when('GET', '/api/patrols?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA').respond([{ "CompetitionId": "74ec4f92-4b72-4c40-927a-de308269e074", "PatrolId": "74ec4f92-4b72-4c40-927a-de308269e074", "PatrolNumber": 2, "StartTime": "2014-02-16T10:27:00.000Z", "PatrolClass": 2, "NumberOfCompetitors": 0 }], {});
            $httpBackend.expectGET('/api/patrols?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA');

            // Set the current cometition
            $routeParams.competitionId = scope.sharedData.competitionId;

            //declare the controller and inject our empty scope
            var myController = $controller('PatrolsController', {
                $scope: scope,
                $routeParams: $routeParams,
                $modal: $modal,
                patrolsFactory: patrolsFactory
            });

            expect(myController).toBeDefined();
            expect(scope.isEditing).toEqual(false);

            // Run the HTTP request
            $httpBackend.flush();
            $httpBackend.verifyNoOutstandingExpectation();

            // Check the result after HTTP
            expect(scope.patrols.length).toEqual(1);
            expect(scope.patrols[0].PatrolId).toEqual("74ec4f92-4b72-4c40-927a-de308269e074");

            // Start editing station
            scope.startEdit(scope.patrols[0]);

            // Check we are editing
            expect(scope.isEditing).toEqual(true);
            expect(scope.patrolToEdit).toBeDefined();
            expect(scope.patrolToEdit).toEqual(scope.patrols[0]);

            // Update values
            scope.patrolToEdit.PatrolClass = 3;

            // Prepare backend responses
            $httpBackend.when('POST', '/api/patrols?competitionId=74ec4f92-4b72-4c40-927a-de308269e074&patrolId=74ec4f92-4b72-4c40-927a-de308269e074').respond({ "CompetitionId": "74ec4f92-4b72-4c40-927a-de308269e074", "PatrolId": "74ec4f92-4b72-4c40-927a-de308269e074", "PatrolNumber": 2, "NumberOfCompetitors": 0 }, {});
            $httpBackend.expectPOST('/api/patrols?competitionId=74ec4f92-4b72-4c40-927a-de308269e074&patrolId=74ec4f92-4b72-4c40-927a-de308269e074', { "CompetitionId": "74ec4f92-4b72-4c40-927a-de308269e074", "PatrolId": "74ec4f92-4b72-4c40-927a-de308269e074", "PatrolNumber": 2, "StartTime": "2014-02-16T10:27:00.000Z", "PatrolClass": 2, "NumberOfCompetitors": 0 });
            $httpBackend.when('GET', '/api/patrols?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA').respond([{ "CompetitionId": "74ec4f92-4b72-4c40-927a-de308269e074", "PatrolId": "74ec4f92-4b72-4c40-927a-de308269e074", "PatrolNumber": 2, "NumberOfCompetitors": 0 }], {});
            $httpBackend.expectGET('/api/patrols?CompetitionId=A6109CFD-C4D8-4003-A6E7-A2BB006A81EA');

            // Save values
            scope.saveEdit();

            // Run the HTTP request
            $httpBackend.flush();
            $httpBackend.verifyNoOutstandingExpectation();

            // Verify values
            expect(scope.isEditing).toEqual(false);
        });
    });
});