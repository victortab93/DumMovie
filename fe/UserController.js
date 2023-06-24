app.controller('UserController', ['$scope', '$http', 'backendUrl','$location', '$window', 
function($scope, $http, backendUrl, $location, $window) {
  $scope.users = [];
  $scope.suggestions = [];
  $scope.selectedUser = {};
  // Function to fetch user data from the backend API
  $scope.getUsers = function() {
    $http.get(backendUrl + '/api/get-users')
      .then(function(response) {
        $scope.users = response.data;
      })
      .catch(function(error) {
        console.log('Error fetching users:', error);
      });
  };
  
  $scope.getSuggestionForUser = function(){
    $http.get(backendUrl + '/api/get-suggestions-by-user?idUser=' + $scope.selectedUser)
      .then(function(response) {
        $scope.suggestions = response.data;
        console.log($scope.suggestions);
      })
      .catch(function(error) {
        console.log('Error fetching users:', error);
      });
  }

  $scope.goBack = function(){
    $window.location.href = '/';
  }

  // Call the getUsers function to fetch users when the controller is loaded
  $scope.getUsers();
}]);