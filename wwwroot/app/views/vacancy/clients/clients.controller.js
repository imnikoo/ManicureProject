export default function ClientsController($scope, ClientService) {
    'ngInject';
    var vm = $scope;

    vm.pageIsLoaded = false;
    vm.clients = [];
    getClients();

    vm.options = {
        autoSelect: false,
        boundaryLinks: false,
        largeEditDialog: false,
        pageSelector: false,
        rowSelection: false
    };

    vm.query = {
        order: 'id',
        limit: 15,
        page: 1
    };

    function getClients() {
        ClientService.getClients().then(value =>  {
            vm.clients = value;
            vm.pageIsLoaded = true;
        });
    }
}