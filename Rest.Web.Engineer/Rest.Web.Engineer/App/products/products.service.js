(function () {
  'use strict';

  angular.module('app.products')
    .factory("productsService", productsService);

  productsService.$inject = ['$http', '$route','$location'];

  function productsService($http, $route, $location) {

    var entityUrl = "api/product";

    return {
      getList: getList,
      getOne: getOne,
      set: set,
      update: update,
      remove: remove,
      removeImageFromProduct: removeImageFromProduct
    };

    function removeImageFromProduct(productId, imageId) {
      var a = entityUrl + "/" + productId + '/images/' + imageId;
      return $http.delete(a);
    }

    function getList(page, perPage) {
      return $http.get(entityUrl,
        {
          params: {
            page: page,
            perPage: perPage
          }
        });
    }

    function getOne(id) {
      return $http.get(entityUrl + "/" + id);
    }

    function set(model) {
      return $http.post(entityUrl, model).then(function () {
        $route.reload();
        $(".modal-backdrop").remove();
      });

    }

    function update(id, model) {
      return $http.put(entityUrl + "/" + id, model).then(function (response) {
        $location.path('/products');
      });
    }

    function remove(id) {
      return $http.delete(entityUrl + "/" + id);
    }

  }

})();