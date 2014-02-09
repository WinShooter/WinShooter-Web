///<reference path="~/Scripts/jasmine.js"/>
///<reference path="~/../WinShooter-Web/Scripts/Angular.js"/>
///<reference path="~/../WinShooter-Web/Scripts/angular-route.js" />
///<reference path="~/../WinShooter-Web/Scripts/angular-mocks.js" />
///<reference path="~/../WinShooter-Web/Scripts/App/common.js"/>
///<reference path="~/../WinShooter-Web/Scripts/App/AngularModules.js"/>

'use strict';

angular.module('winshooter', [])
    .value('mode', 'app')
    .value('version', 'v1.0.1');

describe("CurrentUserController", function () {
    debugger;
    //jasmine.getEnv().currentRunner_.finishCallback = function () { };
    var scope;//we'll use this scope in our tests
 
    //mock Application to allow us to inject our own dependencies
    beforeEach(angular.mock.module('winshooter'));

    beforeEach(angular.module('ngRoute'));

    //mock the controller for the same reason and include $rootScope and $controller
    beforeEach(angular.mock.inject(function ($rootScope, $controller, _$httpBackend_) {
        //create an empty scope
        scope = $rootScope.$new();

        $httpBackend = _$httpBackend_;
        $httpBackend.when('GET', 'Users/users.json').respond([{ id: 1, name: 'Bob' }, { id: 2, name: 'Jane' }]);

        //declare the controller and inject our empty scope
        $controller('CurrentUserController', { $scope: scope });
    }));

    // tests start here
    it("This is a test place holder", function () {
        expect(true).toBe(false);
    });
});