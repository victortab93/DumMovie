app.controller('MovieTypePreferenceController', ['$scope', '$http', 'backendUrl', '$location'
, function($scope, $http, backendUrl, $location) {
  $scope.movie = {};
  $scope.movieId = 0;
  $scope.newPreference = '';
  var url = $location.absUrl();
  
  // Extract the query parameter from the URL
  var params = url.split('?')[1];
  var movieId = null;
  console.log(url);
  if (params) {
    var queryParams = params.split('&');
    
    for (var i = 0; i < queryParams.length; i++) {
      var param = queryParams[i].split('=');
      
      if (param[0] === 'movieId') {
        movieId = param[1];
        movieId = movieId.replace(/\D/g,'');
        break;
      }
    }
  }
  $scope.movieId = movieId;
  // Function to fetch user data from the backend API
  $scope.getMovieToEdit = function() {
    
  };

  $scope.add = function() {
    // Perform the user creation process here
    // You can access the user data from $scope.user

    // Example: Making a POST request to the backend API

    let moviePreference = {
      movieId: $scope.movieId,
      preferenceId: $scope.newPreference
    };

    console.log($scope.newPreference);
    $http.post(backendUrl + '/api/add-preferences-to-movie', moviePreference)
      .then(function(response) {
        // Handle the success response
        window.location.href ="/movieDetail?movieId=" + $scope.movieId;
      })
      .catch(function(error) {
        // Handle the error response
        // You can display an error message or perform any necessary actions

        console.log('Error creating movie:', error);
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
    window.location.href ="/movieDetail?movieId=" + $scope.movieId;
  };

  // Call the getUserDetail function to fetch the user when the controller is loaded
  $scope.getMovieToEdit();
  $scope.getPreferenceOptions();
}]);
