export default function CreateOrderController($state,
    $scope,
    $stateParams,
    ClientService,
    OrderService) {
    'ngInject';
    var vm = $scope;

    vm.stage = parseInt($stateParams.stage ? $stateParams.stage : 1);
    debugger;
    vm.sum = 0;
    vm.discount = 0;
    vm.amountOfDiscount = 0;
    vm.toPay = 0;
    vm.alreadyPaid = 0;
    vm.orderState = 1;

    vm.cities = [];

    vm.query = {
        order: '-id',
        limit: 10,
        page: 1,
    };

    vm.orderOptions = {
        autoSelect: true,
        boundaryLinks: false,
        largeEditDialog: false,
        pageSelector: false,
        rowSelection: true,
        multiSelect: true,
    };
    init();

    vm.getClientsFullName = (client) => {
        if (client) {
            return client.firstName + ' ' + client.lastName;
        }
        return 'Клиент';
    };

    function init() {
        switch (vm.stage) {
            case (1): {
                $state.go('items', { options: vm.orderOptions });
                break;
            }
            case (2): {
                debugger;
                vm.orderedItems = $stateParams.orderedItems;
                vm.sum = _.reduce($stateParams.orderedItems, (orderSum, orderItem) => {
                    return orderSum + orderItem.item.marginalPrice * orderItem.quantity;
                }, 0);
                vm.toPay = vm.sum;
                break;
            }
            case (3): {
                getCities();
                vm.order = $stateParams.order;
                if (vm.order.client) {
                    vm.order.city = _.cloneDeep(vm.order.client.city);
                    vm.order.cityId = vm.order.city.id;
                    vm.order.phoneNumber = vm.order.client.phoneNumber;
                    vm.order.reciever = vm.getClientsFullName(vm.order.client);
                }
                break;
            }
        }
    }

    function getCities() {
        OrderService.getCities().then((cities) => {
            vm.cities = cities;
        });
    }

    vm.updateCityId = () => {
        if (vm.order.city) {
            vm.order.cityId = vm.order.city.id;
        }
    };

    vm.calculateToPay = (discount, calledFromAlreadyPaidContext) => {
        vm.discount = discount;
        var percentPattern = new RegExp('.%');
        var digitPattern = new RegExp('[0-9]');
        var letterPattern = new RegExp('[a-zA-Z]');

        if (digitPattern.test(discount) && !letterPattern.test(discount)) {
            if (percentPattern.test(discount)) {
                vm.amountOfDiscount = vm.sum * discount.split('%')[0] / 100;
            } else {
                vm.amountOfDiscount = discount;
            }
            vm.toPay = vm.sum - vm.amountOfDiscount;
            if (vm.alreadyPaid && !calledFromAlreadyPaidContext) {
                vm.toPay = vm.toPay - vm.alreadyPaid;
            }
        }
        if (!discount.length) {
            vm.toPay = vm.sum;
        }
    };

    vm.chooseClient = () => {
        $state.go('clients', { options: vm.orderOptions, order: vm.order });
    }

    vm.calculateAlreadyPaid = (alreadyPaid) => {
        vm.alreadyPaid = alreadyPaid;
        if (!alreadyPaid) {
            if (vm.sum !== vm.toPay) {
                vm.calculateToPay(vm.discount, false);
            } else {
                vm.toPay = vm.sum;
            }
        } else if (vm.sum !== vm.toPay) {
            vm.calculateToPay(vm.discount, true);
            vm.toPay = vm.toPay - alreadyPaid;
        } else {
            vm.toPay = vm.sum - alreadyPaid;
        }

    };

    vm.toChosenItems = () => {
        $state.go('items', { options: vm.orderOptions, orderedItems: vm.orderedItems });
    };

    vm.calculateSumOf = (orderedItem) => {
        return orderedItem.item.marginalPrice * orderedItem.quantity;
    };

    vm.goNext = () => {
        if (vm.stage === 2) {
            vm.order = {
                sum: vm.sum,
                discount: vm.discount,
                alreadyPaid: vm.alreadyPaid,
                toPay: vm.toPay,
                mailNumber: null,
                additionalInformation: null,
                cityId: null,
                reciever: null,
                phoneNumber: null,
                clientId: null,
                items: vm.orderedItems
            }
        }
        $state.go('createOrder', { stage: vm.stage + 1, order: vm.order });
    }

    vm.querySearch = (query) => {
        var results = query ? vm.cities.filter(vm.createFilterFor(query)) : vm.cities,
            deferred;
        return results;
    }

    vm.createFilterFor = (query) => {
        var lowercaseQuery = angular.lowercase(query);
        return function filterFn(city) {
            return (city.title.toLowerCase().indexOf(lowercaseQuery) === 0);
        };
    }
}