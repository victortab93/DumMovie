app.controller('UserTypePreferenceController', ['$scope', '$http', 'backendUrl', '$location'
, function($scope, $http, backendUrl, $location) {
  $scope.user = {};
  $scope.userId = 0;
  $scope.newPreference = '';
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
    
  };

  $scope.add = function() {
    // Perform the user creation process here
    // You can access the user data from $scope.user

    // Example: Making a POST request to the backend API

    let userPreference = {
      userId: $scope.userId,
      preferenceId: $scope.newPreference
    };

    console.log($scope.newPreference);
    $http.post(backendUrl + '/api/add-preferences-to-user', userPreference)
      .then(function(response) {
        // Handle the success response
        window.location.href ="/userDetail?userId=" + $scope.userId;
      })
      .catch(function(error) {
        // Handle the error response
        // You can display an error message or perform any necessary actions

        console.log('Error creating user:', error);
      });
  };

  $scope.getPreferenceOptions = function() {
    $http.get(backendUrl + '/api/preference-options')
      .then(function(response) {
        $scope.preferenceOptions = response.data;
      })
      .catch(function(error) {
        console.log('Error getting preference options:', error);
      });
  };

  $scope.goBack = function() {
    // Go back to the users list
    window.location.href ="/userDetail?userId=" + $scope.userId;
  };

  // Call the getUserDetail function to fetch the user when the controller is loaded
  $scope.getUserToEdit();
  $scope.getPreferenceOptions();
}]);
