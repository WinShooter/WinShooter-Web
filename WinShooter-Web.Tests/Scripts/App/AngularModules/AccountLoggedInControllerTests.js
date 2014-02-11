///<reference path="~/Scripts/jasmine.js"/>
///<reference path="~/../WinShooter-Web/Scripts/Angular.js"/>
///<reference path="~/../WinShooter-Web/Scripts/angular-resource.js" />
///<reference path="~/../WinShooter-Web/Scripts/angular-sanitize.js"/>
///<reference path="~/../WinShooter-Web/Scripts/angular-mocks.js" />
///<reference path="~/../WinShooter-Web/Scripts/jquery-2.0.3.js" />
///<reference path="~/../WinShooter-Web/Scripts/App/common.js"/>
///<reference path="~/../WinShooter-Web/Scripts/App/AngularModules.js"/>

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
    it('User has never authenticated before', function () {
        angular.mock.inject(function ($rootScope, $controller, $location, currentUserFactory) {
            //create an empty scope
            scope = $rootScope.$new();

            // backend response
            $httpBackend.when('GET', '/api/currentuser?').respond({ IsLoggedIn: true, DisplayName: 'John Smith', Email: 'email@example.com', HasAcceptedTerms: 0 }, {});
            $httpBackend.expectGET('/api/currentuser?');
        
            //declare the controller and inject our empty scope
            var myController = $controller('AccountLoggedInController', {
                $scope: scope,
                $location: $location,
                currentUserFactory: currentUserFactory
            });

            expect(myController).toBeDefined();

            // Check the result before HTTP
            expect(scope.notInitialized).toEqual(true);
            expect(scope.firstTimeWinShooter).toEqual(false);

            // Run the HTTP request
            $httpBackend.flush();

            // Check the result after HTTP
            expect(scope.notInitialized).toEqual(false);
            expect(scope.firstTimeWinShooter).toEqual(true);

            // Prepare for acceptance
            $httpBackend.when('POST', '/api/currentuser?').respond({ IsLoggedIn: true, DisplayName: 'John Smith', Email: 'email@example.com', HasAcceptedTerms: 1 }, {});
            $httpBackend.expectPOST('/api/currentuser?', { "IsLoggedIn": true, "DisplayName": "John Smith", "Email": "email@example.com", "HasAcceptedTerms": 1 });

            // Let user click accept
            scope.accept();

            // Run the HTTP request
            $httpBackend.flush();
        });
    });
});