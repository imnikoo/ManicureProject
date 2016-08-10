export default function ClientsController($state, $mdDialog, $stateParams, $scope, ClientService) {
    'ngInject';
    var vm = $scope;
    vm.clients = [];
    vm.selectedClients = [];

    vm.orderToGoBack = $stateParams.order ? $stateParams.order : null;
    vm.options = $stateParams.options ? $stateParams.options : {
        autoSelect: false,
        boundaryLinks: false,
        largeEditDialog: false,
        pageSelector: false,
        rowSelection: false
    };

    vm.query = {
        order: '-id',
        limit: 10,
        page: 1,
        filterText: ''
    };

    vm.select = (selectedClient) => {
        vm.selectedClients = [];
        vm.selectedClients.push(selectedClient);
        vm.showConfirm()
        .then(() => {
            vm.orderToGoBack.clientId = selectedClient.id;
            vm.orderToGoBack.client = selectedClient;
            $state.go('createOrder', { stage: 3, order: vm.orderToGoBack });
        })
        .catch(() => {
            vm.selectedClients = [];
        });
    };

    vm.showConfirm = () => {
        var confirm = $mdDialog.confirm()
            .title('Выбор клиента')
            .textContent('Выбрать клиента и продолжить обрабатывать заказ?')
            .ok('Да')
            .cancel('Нет');
        return $mdDialog.show(confirm);
    }

    vm.pageIsLoaded = false;
    vm.performSearch = (query) => {
        vm.promise = ClientService.getClients(query).then(value => {
            vm.clients = value.client;
            vm.queryResult = {
                'total': value.total,
            };
            vm.pageIsLoaded = true;
        });
    }

    vm.getClients = (page, limit) => {
        let query = {
            limit: limit,
            page: page,
            filterText: vm.query.filterText
        };
        if (!page) {
            query = vm.query;
        }
        vm.performSearch(query);
    }

    vm.getClients();
}