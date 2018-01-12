
(function () {
    'use strict';

    angular
        .module('app.order')
        .factory('orderService', ['$http', '$q', '$location', orderService]);

    function orderService($http, $q, $location) {
        var service = {
            selectedOption: "",
            setupPayment: setupPayment,
            setOrder: setOrder
        };

        function setOrder(order) {
            return $http.post('api/order', order);
        }

        function setupPayment(option) {
            service.selectedOption = option;
        }

        return service;
    }
})();
