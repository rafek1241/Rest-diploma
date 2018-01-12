(function () {
    'use strict';

    angular
        .module('app.cart')
        .factory('cartService', ['$http', '$q', '$cookies', '$location', cartService]);

    function cartService($http, $q, $cookies, $location) {
        var entityUrl = "api/cart";
        var token = $cookies.get('cartToken') || "00000000-0000-0000-0000-000000000000";

        var service = {
            cart: {
                products: []
            },
            getData: getData,
            clearCart: clearCart,
            addToCart: addToCart,
            removeFromCart: removeFromCart,
            increaseCount: increaseCount,
            decreaseCount: decreaseCount,
            goToPayment: goToPayment,
            cartTotal:cartTotal
        };

        function cartTotal() {
            var count = 0;
            service.cart.products.forEach(function (item) {
                count += item.product.price * item.count;
            });
            return count;
        }

        function goToPayment(parameters) {
            $location.path("order");
        }

        function increaseCount(item) {
            return $http.patch(entityUrl + "/" + token + "/product", { product: item.product, option: "increase" }).then(
                function (response) {
                    service.cart = response.data;
                });
        }

        function decreaseCount(item) {
            return $http.patch(entityUrl + "/" + token + "/product", { product: item.product, option: "decrease" }).then(
                function (response) {
                    service.cart = response.data;
                });
        }

        function clearCart() {
            var defer = $q.defer();

            $http.delete(entityUrl + "/" + token).then(function () {
                $cookies.put('cartToken', "00000000-0000-0000-0000-000000000000");
                getData().then(function () {
                    defer.resolve();
                    $location.path('products');
                });
            });

            return defer.promise;
        }

        function removeFromCart(productId) {
            return $http.delete(entityUrl + "/" + token + "/product/" + productId).then(function (response) {
                service.cart = response.data;
            });
        }

        //Check if product is inside cart - if true then send PUT/PATCH and edit existing count of product.
        function addToCart(product) {
            var defer = $q.defer();
            var isItemInProducts = false;

            service.cart.products.forEach(function (item) {
                if (item.product.productId === product.productId) {
                    isItemInProducts = true;
                }
            });

            if (isItemInProducts) {
                increaseCount({ product: product }).then(function () {
                    defer.resolve();
                });
            } else {
                $http.post(entityUrl + "/" + token + "/product", product).then(function (response) {
                    service.cart = response.data;
                    defer.resolve();
                });
            }

            return defer.promise;
        }

        function getData() {

            return $http.get(entityUrl + "/" + token).then(function (response) {
                $cookies.put('cartToken', response.data.cookieToken);
                token = $cookies.get('cartToken') || "";

                service.cart = response.data;
            });
        }


        return service;
    }
})();