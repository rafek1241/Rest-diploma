(function () {
    'use strict';

    angular.module('app.cart',
        [
            'app.core'
        ])
        .run(run);

    run.$inject = ['cartService', '$rootScope'];

    function run(cartService, $rootScope) {
        cartService.getData();
    }

})();