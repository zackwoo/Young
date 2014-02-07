var app = angular.module('WebmasterApp', [
  'ngRoute',
  'termControllers'
]);

app.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider.
          when('/login', {
              templateUrl: 'partials/login.html',
              controller: 'LoginCtrl'
          }).
          when('/term', {
              templateUrl: 'partials/term.html',
              controller: 'TermCtrl'
          }).
          otherwise({
              //redirectTo: '/'
          });
  }]);