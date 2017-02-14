//questionsController
(function () {

    "use strict";

    //Getting existing module
    angular.module("app-questions")
    .controller("questionsController", questionsController);

    function questionsController($http) {

        var vm = this;

        vm.questions = [];

        vm.newQuestion = {};

        $http.get("/api/quesions")
            .then(function (response) {
                //success
                angular.copy(response.data, vm.questions);
            }, function () {
                //failure
            });


        vm.addQuestion = function () {
            //alert(vm.newQuestion.name);
            vm.questions.push({ name: vm.newQuestion.name, created: new Date() });
            vm.newQuestion = {};
        };
    }
})();
