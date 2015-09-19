(function ()
{
    'use strict';

    angular
        .module('moxtraService', ['ngResource'])
        .factory('moxtraDataService', moxtraDataService)
        .factory('transformRequestAsFormPost', transformRequestAsFormPost);

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
            authenticate: authenticate1
            //authenticate2 : authenticate2,
            //authenticate3 : authenticate3
        };
        return moxtraServiceInterface;


        ////////////////

        /***
         * authenticate attempt 1
         * @param rtUserId
         * @returns {deferred.promise|{then}}
         */
        function authenticate1(rtUserId)
        {
            /***
             * POST data payload
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

            $http.defaults.headers.post["Content-Type"] = 'application/x-www-form-urlencoded';

            /*
             $http.post(MOXTRA_CONFIG.SANDBOX_URL, moxtraAuthenticationData, { headers: {'Content-Type': 'application/x-www-form-urlencoded;charset=utf-8' } })
             .then(
             function(success) { deferred.resolve(success); },
             function(failure) { deferred.reject(failure); }
             );
             */
            $http({
                method: "POST",
                url: MOXTRA_CONFIG.SANDBOX_URL,
                data: $.param(moxtraAuthenticationData)
            })
                .then(
                function(success) { deferred.resolve(success); },
                function(failure) { deferred.reject(failure); }
            );
            // Store the data-dump of the FORM scope.
        //    request.success(
        //        deferred.resolve(success)
        //)
        //    ;
        //
        //    request.failure(
        //        deferred.reject(failure)
        //)
        //    ;

            return deferred.promise;
        }

        /***
         * authenticate attempt 2
         * @param rtUserId
         * @returns {deferred.promise|{then}}
         */
        function authenticate2(rtUserId)
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
                url: MOXTRA_CONFIG.SANDBOX_URL,
                headers: {'Content-Type': 'application/x-www-form-urlencoded'},
                transformRequest: function (obj)
                {
                    // convert data into a string
                    var str = []
                    for (var p in obj)
                        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                    return str.join("&");
                },
                data: $.param(moxtraAuthenticationData)
            })
                .then(
                function (success)
                {
                    deferred.resolve(success);
                },
                function (failure)
                {
                    deferred.reject(failure);
                }
            );
            return deferred.promise;
        }

        /***
         * authenticate attempt 3
         * @param rtUserId
         * @returns {deferred.promise|{then}}
         */
        function authenticate3(rtUserId)
        {
            var moxtraAuthenticationData =
                "client_id=" + MOXTRA_CONFIG.CLIENT_ID + "&" +
                "client_secret=" + MOXTRA_CONFIG.SECRET + "&" +
                "grant_type=" + MOXTRA_CONFIG.GRANT_TYPE + "&" +
                "uniqueid=" + rtUserId + "&" +
                "timestamp=" + new Date().getTime();

            var deferred = $q.defer();

            $http.post(MOXTRA_CONFIG.SANDBOX_URL, moxtraAuthenticationData, {headers: {'Content-Type': 'application/x-www-form-urlencoded;charset=utf-8'}})
                .then(
                function (success)
                {
                    deferred.resolve(success);
                },
                function (failure)
                {
                    deferred.reject(failure);
                }
            );
            return deferred.promise;
        }
    }


    // I provide a request-transformation method that is used to prepare the outgoing
    // request as a FORM post instead of a JSON packet.
    //app.factory(
    //    "transformRequestAsFormPost",
    function transformRequestAsFormPost()
    {
        // I prepare the request data for the form post.
        function transformRequest(data, getHeaders)
        {
            var headers = getHeaders();
            headers["Content-type"] = "application/x-www-form-urlencoded; charset=utf-8";
            return ( serializeData(data) );
        }

        // Return the factory value.
        return ( transformRequest );
        // ---
        // PRVIATE METHODS.
        // ---
        // I serialize the given Object into a key-value pair string. This
        // method expects an object and will default to the toString() method.
        // --
        // NOTE: This is an atered version of the jQuery.param() method which
        // will serialize a data collection for Form posting.
        // --
        // https://github.com/jquery/jquery/blob/master/src/serialize.js#L45
        function serializeData(data)
        {
            // If this is not an object, defer to native stringification.
            if (!angular.isObject(data))
            {
                return ( ( data == null ) ? "" : data.toString() );
            }
            var buffer = [];
            // Serialize each key in the object.
            for (var name in data)
            {
                if (!data.hasOwnProperty(name))
                {
                    continue;
                }
                var value = data[name];
                buffer.push(
                    encodeURIComponent(name) +
                    "=" +
                    encodeURIComponent(( value == null ) ? "" : value)
                );
            }
            // Serialize the buffer and clean it up for transportation.
            var source = buffer
                    .join("&")
                    .replace(/%20/g, "+")
                ;
            return ( source );
        }
    }

    //);


}());

