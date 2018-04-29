(function () {
  'use strict';

  angular.module('app.country')
    .factory("countryService", countryService);

  countryService.$inject = ['$http', '$route'];

  function countryService($http, $route) {

    var entityUrl = "api/country";

    return {
      getCountriesContainString: getCountriesContainString
    };

    function getCountriesContainString(query) {
      return $http.get(entityUrl,
        {
          params: {
            name: query
          }
        });
    }
  }

})();