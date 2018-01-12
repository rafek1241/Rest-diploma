(function () {
    'use strict';

    angular
        .module('app.cart')
        .controller('CartController', ['$scope', '$timeout', 'cartService', cartController]);

    function cartController($scope, $timeout, cartService) {
        var vm = this;
        vm.cartService = cartService;

        vm.getCartTotal = function () {
            return cartService.cartTotal();
        }


        activate();

        function activate() { }
    }
})();