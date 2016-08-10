export default function ClientController(
    $state,
    $scope,
    $stateParams,
    $mdDialog,
    $mdMedia,
    ClientService) {
    'ngInject';

    var vm = $scope;
    vm.pageIsloaded;
    _init();
    vm.client = { city: null };
    vm.clientBefore = { city: null };
    vm.cities = [];
    vm.querySearch = querySearch;

    function _init() {
        if ($stateParams.clientId) {
            vm.pageIsLoaded = false;
            getClient($stateParams.clientId);
        }
        else {
            vm.pageIsLoaded = true;
        }
        getCities();
    }

    vm.saveClientAndBack = () => {
        vm.saveClient().then(value => $state.go('clients'));
        //TODO: handle back to the page where user was
    }

    vm.saveClient = () => {
        return ClientService.saveClient(vm.client);
    }

    vm.checkAndWarn = ($event) => {
        if (isClientChanged()) {
            vm.showConfirm($event);
        }
        else {
            $state.go('clients');
        }
    }

    function isClientChanged() {
        return !_.isEqual(vm.client, vm.clientBefore);
    }

    vm.showConfirm = function (ev) {
        var confirm = $mdDialog.confirm()
            .title('Прет')
            .textContent('Вы изменили клиента и выходите. Выйти?')
            .ariaLabel('Оооо..')
            .targetEvent(ev)
            .ok('Выйти')
            .cancel('Отмена');

        $mdDialog.show(confirm).then(function () {
            $state.go('clients');
        }, function () {

        });
    }

    function getCities() {
        ClientService.getCities().then(value => {
            vm.cities = value;
        })
    }

    function getClient(clientId) {
        ClientService.getClient(clientId).then(value => {
            vm.client = value;
            vm.clientBefore = _.cloneDeep(value);
            vm.pageIsLoaded = true;
        })

    }
    function querySearch(query) {
        console.log(vm.client);
        console.log(query);
        var results = query ? vm.cities.filter(createFilterFor(query)) : vm.cities,
            deferred;
        return results;
    }

    function createFilterFor(query) {
        var lowercaseQuery = angular.lowercase(query);
        return function filterFn(category) {
            return (vm.client.city.title.toLowerCase().indexOf(lowercaseQuery) === 0);
        };
    }

}
