var app = angular.module('myApp', ['ngRoute']);

app
.constant('backendUrl', 'https://localhost:7201') // Replace with your backend URL
.config(function ($routeProvider, $locationProvider) {
  $routeProvider
    .when('/users', {
      templateUrl: 'users.html',
      controller: 'UserController'
    })
    .when('/userEdit', {
      templateUrl: 'userEdit.html',
      controller: 'UserEditController'
    })
    .when('/userTypePreference', {
      templateUrl: 'userTypePreference.html',
      controller: 'UserTypePreferenceController'
    })
    .when('/userDetail', {
      templateUrl: 'userdetail.html',
      controller: 'UserDetailController'
    })
    .when('/movies', {
      templateUrl: 'movies.html',
      controller: 'MovieController'
    })
    .when('/movieEdit', {
      templateUrl: 'movieEdit.html',
      controller: 'MovieEditController'
    })
    .when('/movieTypePreference', {
      templateUrl: 'movieTypePreference.html',
      controller: 'MovieTypePreferenceController'
    })
    .when('/movieDetail', {
      templateUrl: 'moviedetail.html',
      controller: 'MovieDetailController'
    })
    .otherwise({
      redirectTo: '/'
    });
});
