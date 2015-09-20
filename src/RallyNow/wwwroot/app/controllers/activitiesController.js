(function () {
    'use strict';

    angular
        .module('rtApp')
        .controller('ActivitiesController', activitiesController);

    activitiesController.$inject = ['$scope', 'Activities', 'moxtraDataService', '$log', '$rootScope'];

    function activitiesController($scope, Activities, moxtraDataService, $log, $rootScope) {
        $scope.title = 'activitiesController';

        // HACK: need an unqiue user id to authenticate with moxtra here
        moxtraDataService.authenticate('someUniqueUserId').then(
            function(success) {
                $log.info("moxtra authenticated successfully: ", success);

                // save the moxtra access data
                $rootScope.moxtraAccessData = success.data;
                $rootScope.moxtraAccessData.moxtraUserId = success.moxtraUserId;

                // initialize the moxtra js sdk
                moxtraDataService.initialize($rootScope.moxtraAccessData);
            },
            function(failure) {
                $log.error("moxtra authentication failure! ", failure);
            }
        );

        $scope.activities = Activities.query();
    }
})();
