///<reference path="~/Scripts/jasmine.js"/>
///<reference path="~/../WinShooter-Web/Scripts/Angular.js"/>
///<reference path="~/../WinShooter-Web/Scripts/angular-resource.js" />
///<reference path="~/../WinShooter-Web/Scripts/angular-sanitize.js"/>
///<reference path="~/../WinShooter-Web/Scripts/angular-mocks.js" />
///<reference path="~/../WinShooter-Web/Scripts/App/common.js"/>
///<reference path="~/../WinShooter-Web/Scripts/App/AngularModules.js"/>

'use strict';

describe("CurrentUserController", function () {
    // For now, disable resharper callback
    //jasmine.getEnv().currentRunner_.finishCallback = function () { };

    //we'll use this scope in our tests
    var scope;

    //mock Application to allow us to inject our own dependencies
    beforeEach(angular.mock.module('winshooter'));

    // tests start here
    it('Check that the user isnt logged in', function () {
        angular.mock.inject(function ($rootScope, $controller, $location, $modal) {
            //create an empty scope
            scope = $rootScope.$new();

            debugger;

            var currentUserFactory = jasmine.createSpyObj('currentUserFactoryStub', ['query', 'get']);
            currentUserFactory.query.andCallFake(function() {
                return { IsLoggedIn: false };
            });

            //declare the controller and inject our empty scope
            var myController = $controller('CurrentUserController', {
                $rootScope: scope,
                $scope: scope,
                $location: $location,
                $modal: $modal,
                currentUserFactory: currentUserFactory
            });
            expect(myController).toBeDefined();

            expect(scope.currentUser.IsLoggedIn).toBe(false);
        });
    });
});