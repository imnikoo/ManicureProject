export default function ClientController(
    $state,
    $scope,
    $stateParams,
    $mdDialog,
    $mdMedia,
    $q,
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
        return (() => {
            if (!vm.client.city) {
                var chosenCity = _.find(vm.cities, ['title', vm.searchCity]);
                if (chosenCity) {
                    vm.client.city = chosenCity;
                } else {
                    return ClientService.saveCity({
                        title: vm.searchCity
                    }).then((value) => {
                        vm.client.city = value;
                        vm.client.cityId = value.id;
                    })
                }
            }
            return $q.when();
        })().then(() => {
            return ClientService.saveClient(vm.client);
        });
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
    vm.checkName = () => {
        ClientService.checkName({
            firstName: vm.client.firstName,
            lastName: vm.client.lastName
        }).then(clientId => {
            if (clientId) {
                let confirm = $mdDialog.confirm()
                    .title('Внимание')
                    .textContent('Клиент с таким именем уже есть. Перейти на него?')
                    .ok('ОК')
                    .cancel('Отменить');
                $mdDialog.show(confirm).then(() => {
                    $state.go('client', { clientId });
                });
            }
        })
    }

    vm.showConfirm = function (ev) {
        var confirm = $mdDialog.confirm()
            .title('Прет')
            .textContent('Вы изменили клиента и выходите. Выйти?')
            .targetEvent(ev)
            .ok('Выйти')
            .cancel('Отмена');

        $mdDialog.show(confirm).then(() => {
            $state.go('clients');
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
        var results = query ? vm.cities.filter(createFilterFor(query)) : vm.cities,
            deferred;
        return results;
    }

    function createFilterFor(query) {
        var lowercaseQuery = angular.lowercase(query);
        return function filterFn(city) {
            return city.title.toLowerCase().indexOf(lowercaseQuery) === 0;
        };
    }

}
