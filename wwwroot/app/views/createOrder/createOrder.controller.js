export default function CreateOrderController($state,
    $scope,
    $stateParams, 
    $mdDialog, 
    $mdMedia,
    ClientService,
    OrderService) {
    'ngInject';
    var vm = $scope;

     vm.getClientsFullName = function (client) {
        if (client) {
            return client.firstName + ' ' + client.lastName;
        }
        return 'Клиент';
    };

    vm.stage = parseInt($stateParams.stage ? $stateParams.stage : 1);
    vm.discount = "";
    vm.amountOfDiscount = 0;
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
    vm.calculateToPay = (calledFromAlreadyPaidContext) => {
        let discount = vm.order.discount;
        var percentPattern = new RegExp('.%');
        var digitPattern = new RegExp('[0-9]');
        var letterPattern = new RegExp('[a-zA-Z]');

        if (digitPattern.test(discount) && !letterPattern.test(discount)) {
            if (percentPattern.test(discount)) {
                vm.order.amountOfDiscount = _.round(vm.order.sum * discount.split('%')[0] / 100);
            } else {
                vm.order.amountOfDiscount = discount;
            }
            vm.order.toPay = _.round(vm.order.sum - vm.order.amountOfDiscount);
            if (vm.order.alreadyPaid && !calledFromAlreadyPaidContext) {
                vm.order.toPay =_.round(vm.order.toPay - vm.order.alreadyPaid);
            }
        }
        if (!discount.length) {
            vm.order.toPay = vm.order.sum;
        }
    };

     vm.calculateAlreadyPaid = () => {
        let alreadyPaid = vm.order.alreadyPaid;
        if (!alreadyPaid) {
            if (vm.order.sum !== vm.order.toPay) {
                vm.calculateToPay(false);
            } else {
                vm.order.toPay = vm.order.sum;
            }
        } else if (vm.order.sum !== vm.order.toPay) {
            vm.calculateToPay(true);
            vm.order.toPay = _.round(vm.order.toPay - alreadyPaid);
        } else {
            vm.order.toPay = _.round(vm.order.sum - alreadyPaid);
        }
    };
    init();
    function init() {
        switch (vm.stage) {
            case (1): {
                $state.go('items', { options: vm.orderOptions, isOrderCase: true });
                break;
            }
            case (2): {
                debugger;
                vm.order = $stateParams.order ? $stateParams.order : {}; 
                vm.order.sum = _.reduce(vm.order.items, (orderSum, orderItem) => {
                    return orderSum + orderItem.item.marginalPrice * orderItem.quantity;
                }, 0);
                vm.order.toPay = vm.order.sum;
                vm.calculateAlreadyPaid();
                vm.calculateToPay();
                break;
            }
            case (3): {
                debugger;
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

    vm.toOrder = (order) => {
        $state.go('createOrder', { stage: 2, order: vm.order });
    }

    vm.chooseClient = () => {
        $state.go('clients', { options: vm.orderOptions, order: vm.order });
    }
    vm.saveOrder = (order) => {
        OrderService.saveOrder(order).then(() => {
            $state.go('orders');
        });
    };

    vm.deleteOrder = (order) => {
        var confirm = $mdDialog.confirm()
          .title('Удалить заказ?')
          .ok('OK')
          .cancel('Отменить');
        $mdDialog.show(confirm).then(() => {
            OrderService.deleteOrder(order).then(() => {
                $state.go('orders');
            });
        });
    };

    vm.toChosenItems = () => {
        $state.go('items', { 
            options: vm.orderOptions, 
            order: vm.order, 
            isOrderCase: true 
        });
    };

    vm.calculateSumOf = (orderedItem) => {
        return orderedItem.item.marginalPrice * orderedItem.quantity;
    };

    vm.goNext = () => {
        if (vm.stage === 2) {
            $state.go('createOrder', { stage: vm.stage + 1, order: vm.order });
        }
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