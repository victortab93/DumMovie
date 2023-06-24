app.controller('UserEditController', ['$scope', '$http', 'backendUrl', '$location'
, function($scope, $http, backendUrl, $location) {
  $scope.user = {};
  $scope.userId = 0;
  var url = $location.absUrl();
  
  // Extract the query parameter from the URL
  var params = url.split('?')[1];
  var userId = null;
  console.log(url);
  if (params) {
    var queryParams = params.split('&');
    
    for (var i = 0; i < queryParams.length; i++) {
      var param = queryParams[i].split('=');
      
      if (param[0] === 'userId') {
        userId = param[1];        
        userId = userId.replace(/\D/g,'');
        break;
      }
    }
  }
  $scope.userId = userId;
  // Function to fetch user data from the backend API
  $scope.getUserToEdit = function() {
    if($scope.userId > 0){
      $http.get(backendUrl + '/api/get-user-by-id?id=' + $scope.userId)
      .then(function(response) {
        console.log(response.data);
        $scope.user = response.data;
      })
      .catch(function(error) {
        console.log('Error getting user:', error);
      });
    }

  };

  $scope.save = function() {
    // Perform the user creation process here
    // You can access the user data from $scope.user

    // Example: Making a POST request to the backend API

    $http.post(backendUrl + '/api/create-user', $scope.user)
      .then(function(response) {
        // Handle the success response
        window.location.href ="/users"
      })
      .catch(function(error) {
        // Handle the error response
        // You can display an error message or perform any necessary actions

        console.log('Error creating user:', error);
      });
  };

  $scope.goBack = function() {
    // Go back to the users list
    window.history.back();
  };

  // Call the getUserDetail function to fetch the user when the controller is loaded
  $scope.getUserToEdit();
}]);
