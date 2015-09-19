(function () {
    'use strict';

    angular
        .module('rtApp')
        .controller('activitiesController', activitiesController);

    activitiesController.$inject = ['$scope', 'Activities', 'moxtraDataService', '$log'];

    function activitiesController($scope, Activities, moxtraDataService, $log) {
        $scope.title = 'activitiesController';

        moxtraDataService.authenticate('hello').then(
            function(success) {
                $log.warn(success);
            },
            function(failure) {
                $log.warn(failure);
            }
        );

        $scope.activities = Activities.query();
    }
})();
