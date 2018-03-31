angular.module('app.services', [])
    .factory('AppService', ['$rootScope', '$http', '$q', '$state', "$cookieStore", function ($rootScope, $http, $q, $state, $cookieStore) {

        var serverBaseUrl = "http://localhost:59181/v1api/";
        var token = "72f303a7-f1f0-45a0-ad2b-e6db29328b1a";

        return {
            login: login,
            getHeaders: getHeaders,
            getUsers: getUsers
        }

        function getHeaders() {
            var ContentType = 'application/x-www-form-urlencoded; charset=UTF-8';
            var AppToken = "AppToken";
            return { "Content-Type": ContentType };
        }

        function login(model) {
            console.log(model);
            var url = serverBaseUrl + "Auth/Authenticate";
            var deferred = $q.defer();
            $http({
                method: 'POST',
                url: url,
                data: model,
                headers: {
                    "Content-Type": 'application/json; charset=utf-8',
                    "AppToken": token,
                },
            }).success(function (data, status, cfg) {
                deferred.resolve(data);
                if (data.result != null) {
                }
            }).error(function (err, status) {
                deferred.reject(status);
            });

            return deferred.promise;
        }


        function getUsers() {

            var url = serverBaseUrl + "User";
            var deferred = $q.defer();
            $http({
                method: 'Get',
                url: url,
                headers: {
                    "Content-Type": 'application/json; charset=utf-8',
                    "AppToken": token,
                },
            }).success(function (data, status, cfg) {
                deferred.resolve(data);
                if (data.result != null) {
                }
            }).error(function (err, status) {
                deferred.reject(status);
            });

            return deferred.promise;
        }

    }]);


