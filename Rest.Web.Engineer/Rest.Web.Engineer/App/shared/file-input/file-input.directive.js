(function () {
    'use strict';

    angular.module('app.shared')
        .directive('fileInput', fileInput);

    fileInput.$inject = ['$rootScope', 'productsService', '$route'];

    function fileInput($rootScope, productsService, $route) {
        return {
            templateUrl: "app/shared/file-input/file-input.html",
            restrict: "E",
            require: 'ngModel',
            scope: {
                model: '=ngModel'
            },
            link: link
        };


        function link(scope, element, attr) {

            scope.removeImage = function (fileId) {
                productsService.removeImageFromProduct(parseInt($route.current.params.productId), fileId).then(function (response) {
                    scope.model = response.data.images;
                });
            }

            scope.productsService = productsService;

            scope.filesChanged = function () {
                scope.previews = [];

                scope.model.forEach(function (item, index) {

                    var reader = new FileReader();
                    reader.onload = function (event) {
                        return readFile(event, item, index);
                    };
                    reader.readAsDataURL(item);
                });

            };

            function readFile(event, item, index) {
                scope.$apply(function () {
                    scope.previews.push(event.target.result);
                    scope.model[index] = {
                        contentBase64: event.target.result.split("base64,").pop(),
                        type: item.type,
                        name: item.name
                    };
                });
            };
        }
    }

})();