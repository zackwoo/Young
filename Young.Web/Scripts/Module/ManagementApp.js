var mApp = angular.module('managementApp', [
  'ngRoute',
  'managementControllers'
]);

mApp.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider.
        when('/login', {
            templateUrl: 'partials/login.html',
            controller: 'LoginCtrl'
        }).
        otherwise({
            redirectTo: '/login'
        });
  }]);