app.controller('MovieDetailController', ['$scope', '$http', 'backendUrl', '$location',
 function($scope, $http, backendUrl, $location) {
  $scope.movie = {};
  $scope.movieId = 0;
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
  // Function to fetch movie data from the backend API
  $scope.getMovieDetail = function() {
    $http.get(backendUrl + '/api/get-movie-by-id?id=' + $scope.movieId)
      .then(function(response) {
        console.log(response.data);
        $scope.movie = response.data;
      })
      .catch(function(error) {
        console.log('Error getting movie:', error);
      });
  };

  $scope.goBack = function() {
    // Go back to the users list
    window.location.href ="/movies";
  };

  $scope.addMovieTypePreference = function(){
    window.location.href ="/movieTypePreference?movieId="+$scope.movieId;
  }
  
  $scope.deleteMovieTypePreference = function(id) {
    // Add logic to handle deleting user type preference
    
    $http.delete(backendUrl + '/api/delete-movie-preference-by-id?id=' + id)
      .then(function(response) {
        window.location.href ="/movieDetail?userId=" + $scope.movieId;
      })
      .catch(function(error) {
        console.log('Error getting movie:', error);
      });  
  };
  
  // Call the getUserDetail function to fetch the user when the controller is loaded
  $scope.getMovieDetail();
}]);
