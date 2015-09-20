(function ()
{
    'use strict';

    angular
        .module('moxtraService', ['ngResource'])
        .factory('moxtraDataService', moxtraDataService);

    moxtraDataService.$inject = ['$http', '$q'];

    /* @ngInject */
    function moxtraDataService($http, $q)
    {
        /***
         * static moxtra config object
         * @type {{SANDBOX_URL: string, PROD_URL: string, CLIENT_ID: string, SECRET: string, GRANT_TYPE: string}}
         */
        var MOXTRA_CONFIG = {
            SANDBOX_URL: 'https://apisandbox.moxtra.com/oauth/token',
            PROD_URL: 'https://api.moxtra.com/oauth/token',
            CLIENT_ID: 'i6tzmTHd1ss',
            SECRET: 's0-8rnq0wBU',
            GRANT_TYPE: 'http://www.moxtra.com/auth_uniqueid'
        };

        /***
         * moxtra data service interface
         * @type {{authenticate1: authenticate1, authenticate2: authenticate2}}
         */
        var moxtraServiceInterface = {
            authenticate: authenticate,
            initialize: initialize
        };
        return moxtraServiceInterface;

        ////////////////

        /***
         * authenticate to moxtra
         * @param rtUserId
         * @returns {deferred.promise|{then}}
         */
        function authenticate(rtUserId)
        {
            /***
             * POST data packet
             * @type {{client_id: string, client_secret: string, grant_type: string, uniqueid: *, timestamp: number}}
             */
            var moxtraAuthenticationData = {
                client_id: MOXTRA_CONFIG.CLIENT_ID,
                client_secret: MOXTRA_CONFIG.SECRET,
                grant_type: MOXTRA_CONFIG.GRANT_TYPE,
                uniqueid: rtUserId,
                timestamp: new Date().getTime()
            };

            var deferred = $q.defer();

            $http({
                method: 'POST',
                headers: {'Content-Type': 'application/x-www-form-urlencoded'},
                url: MOXTRA_CONFIG.SANDBOX_URL,
                data: $.param(moxtraAuthenticationData)
            }).then(
                function (success)
                {
                    success.moxtraUserId = rtUserId;
                    deferred.resolve(success);
                },
                function (failure)
                {
                    deferred.reject(failure);
                }
            );
            return deferred.promise;
        }

        function initialize(moxtraAccessData)
        {
            var options = {
                mode: "sandbox", //for production environment change to "production"
                client_id: MOXTRA_CONFIG.CLIENT_ID,
                access_token: moxtraAccessData.access_token, //valid access token from user authentication
                invalid_token: function (event)
                {
                    alert("Access Token expired for session id: " + event.session_id);
                }
            };

            Moxtra.init(options);
        }
    }

}());

