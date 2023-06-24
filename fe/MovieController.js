app.controller('MovieController', ['$scope', '$http', 'backendUrl','$location', '$window', 
function($scope, $http, backendUrl, $location, $window) {
  $scope.movies = [];
  
  // Function to fetch user data from the backend API
  $scope.getMovies = function() {
    $http.get(backendUrl + '/api/get-movies')
      .then(function(response) {
        $scope.movies = response.data;
      })
      .catch(function(error) {
        console.log('Error fetching movies:', error);
      });
  };
  
  $scope.goBack = function(){
    $window.location.href = '/';
  }

  // Call the getUsers function to fetch users when the controller is loaded
  $scope.getMovies();
}]);