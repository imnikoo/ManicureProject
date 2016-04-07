export default function ClientController($state, $scope, $stateParams, $mdDialog, $mdMedia, ClientService) {
    'ngInject';

    var vm = $scope;
    vm.pageIsloaded = false;
    vm.client = {};
    vm.clientBefore = {};
    getClient($stateParams.clientId);
    vm.cities = [];
    getCities();
    vm.querySearch = querySearch;

    vm.saveClientAndBack = () => {
        vm.saveClient().then(value => $state.go('clients'));
    }

    vm.saveClient = () => {
        return ClientService.saveClient(vm.client);
    }

    vm.checkAndWarn = ($event) => {
        if(isClientChanged()) {
            vm.showConfirm($event);
        }
        else {
            $state.go('clients');
        }
    }

    function isClientChanged() {
        return !_.isEqual(vm.client, vm.clientBefore);
    }

    vm.showConfirm = function(ev) {
        var confirm = $mdDialog.confirm()
              .title('Э')
              .textContent('Вы изменили клиента и выходите. Выйти без сохранения?')
              .ariaLabel('Ди?')
              .targetEvent(ev)
              .ok('Выйти')
              .cancel('Отмена');

        $mdDialog.show(confirm).then(function() {
            $state.go('clients');
        }, function() {
            
        });
    }

    function getCities() {
        ClientService.getCities().then(value => {
            vm.cities=value;
        })
    }

    function getClient(clientId) {
        ClientService.getClient(clientId).then( value => {
            vm.client = value;
            vm.clientBefore = _.clone(value);
            vm.pageIsLoaded = true;
        })

    }
    function querySearch(query) {
        console.log(query);
        var results = query ? vm.cities.filter( createFilterFor(query) ) : vm.cities,
           deferred;
        return results;
    }

    function createFilterFor(query) {
        var lowercaseQuery = angular.lowercase(query);
        return function filterFn(category) {
            return (city.title.toLowerCase().indexOf(lowercaseQuery) === 0);
        };
    }

}
	