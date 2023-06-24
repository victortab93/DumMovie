app.controller('UserDetailController', ['$scope', '$http', 'backendUrl', '$location',
 function($scope, $http, backendUrl, $location) {
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
  $scope.getUserDetail = function() {
    $http.get(backendUrl + '/api/get-user-by-id?id=' + $scope.userId)
      .then(function(response) {
        console.log(response.data);
        $scope.user = response.data;
      })
      .catch(function(error) {
        console.log('Error getting user:', error);
      });
  };

  $scope.goBack = function() {
    // Go back to the users list
    window.location.href ="/movies";
  };

  $scope.addUserTypePreference = function(){
    window.location.href ="/userTypePreference?userId="+$scope.userId;
  }
  
  $scope.deleteUserTypePreference = function(id) {
    // Add logic to handle deleting user type preference
    
    $http.delete(backendUrl + '/api/delete-user-preference-by-id?id=' + id)
      .then(function(response) {
        window.location.href ="/userDetail?userId=" + $scope.userId;
      })
      .catch(function(error) {
        console.log('Error getting user:', error);
      });  
  };
  
  // Call the getUserDetail function to fetch the user when the controller is loaded
  $scope.getUserDetail();
}]);
