
(function () {
  'use strict';

  angular
    .module('app.order')
    .controller('OrderController', ['$scope', 'orderService', 'countryService', 'cartService', orderController]);

  function orderController($scope, orderService, countryService, cartService) {
    var vm = this;
    vm.orderService = orderService;
    vm.countryService = countryService;
    vm.cartService = cartService;

    vm.getCartTotal = function () {
      return cartService.cartTotal();
    }

    vm.setAddress = function (order) {
      order.cart = vm.cartService.cart;
      order.paymentMethod = orderService.selectedOption;
      return orderService.setOrder(order);
    }


  }
})();
