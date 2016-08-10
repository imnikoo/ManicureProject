
export default function ItemDetailController($state, $scope, $q, $stateParams, 
$mdDialog, $mdMedia,
    ItemService) {
    'ngInject';
    var vm = $scope;
    vm.pageIsLoaded;
    vm.showPurchases = false;
    vm.showItemOrder = false;
    vm.item = {
        purchases: [],
    };
    vm.itemBefore = {
        purchases: [],
    };
    vm.categories = [];
    vm.purchasePlaces = [];
    vm.orderPlaceTitle = {};

    vm.pageOfList = _.clone($stateParams.page);
    console.log($stateParams, 'params');
    console.log(vm.pageOfList);

    vm.options = {
        autoSelect: false,
        boundaryLinks: false,
        largeEditDialog: false,
        pageSelector: false,
        rowSelection: false
    };
    vm.query = {
        order: '-orderDate',
        limit: 15,
        page: 1
    };

    vm.orderPlace = {};
    vm.purchase = {};

    function _init() {
        if ($stateParams.itemId) {
            vm.getItem($stateParams.itemId);
            vm.pageIsLoaded = false;
        }
        else {
            vm.pageIsLoaded = true;
        }
        vm.getCategories();
        vm.getPurchasePlaces();
    }

    function _isPurchaseValid() {
        let deffered = $q.defer();
        let comparingResult =  vm.purchase.pricePerPiece && vm.purchase.amount && vm.purchase.place && vm.purchase.orderDate;
        deffered.resolve(comparingResult);
        return deffered.promise;
    }

    vm.confirmOrder = () => {
        _isPurchaseValid().then(() => {
            let approxArriveDate = new Date(vm.purchase.orderDate).setDate(approxArriveDate.getDate() + 30);
            let newPurchase = {
                pricePerPiece: vm.purchase.pricePerPiece,
                amount: vm.purchase.amount,
                isArrived: false,
                orderDate: vm.purchase.orderDate,
                purchasePlace: JSON.parse(vm.purchase.place),
                approximateArrivalDate: approxArriveDate,
                trackNumber: vm.purchase.trackNumber
            };
            vm.item.purchases.push(newPurchase);
            vm.purchase = {};
            vm.showItemOrder = false;
        });
    }

    vm.tooglePurchases = () => {
        if (vm.showItemOrder) {
            vm.showItemOrder = false;
        }
        vm.showPurchases = !vm.showPurchases;
    }

    vm.markUp = () => {
        return _.round((vm.item.marginalPrice * 100 / vm.item.originalPrice) - 100);
    }

    vm.calculateAndPasteMarginalPrice = () => {
        if (vm.item.originalPrice) {
            vm.item.marginalPrice = parseFloat((vm.item.originalPrice * 1.5).toFixed(2));
        }
    }

    function isItemChanged() {
        return !_.isEqual(vm.item, vm.itemBefore);
    }

    vm.toogleOrder = () => {
        if (vm.showPurchases) {
            vm.showPurchases = false;
        }
        vm.showItemOrder = !vm.showItemOrder;
    }
    vm.checkAndWarn = ($event) => {
        if (isItemChanged()) {
            vm.showConfirm($event);
        }
        else {
            goBack();
        }
    }
    vm.showConfirm = function (ev) {
        var confirm = $mdDialog.confirm()
            .title('Внимание!')
            .textContent('Вы изменили содержимое товара и выходите. Выйти без сохранения?')
            .ariaLabel('Ди?')
            .targetEvent(ev)
            .ok('Выйти')
            .cancel('Отмена');

        $mdDialog.show(confirm).then(function () {
            goBack();
        });
    }

    vm.saveItemAndBack = () => {
        saveItem().then(() => {
            goBack();
        });
    }

    function goBack() {
        $state.go('items', { page: vm.pageOfList });
    }

    vm.saveItem = () => {
        return ItemService.saveItem(vm.item);
    }

    vm.getItem = (itemId) => {
        if (itemId) {
            ItemService.getItem(itemId).then(value => {
                vm.item = value;
                vm.itemBefore = _.clone(value);
                vm.pageIsLoaded = true;
            });
        }
    }

    vm.getCategories = () => {
        ItemService.getCategories().then(value => {
            vm.categories = value;
        });
    }

    vm.getPurchasePlaces = () => {
        ItemService.getPurchasePlaces().then(value => {
            vm.purchasePlaces = (value);
        })
    }

    vm.querySearch = (query) => {
        var results = query ? vm.categories.filter(createFilterFor(query)) : vm.categories,
            deferred;
        return results;
    }

    vm.createFilterFor = (query) => {
        var lowercaseQuery = angular.lowercase(query);
        return function filterFn(category) {
            return (category.title.toLowerCase().indexOf(lowercaseQuery) === 0);
        };
    }
    _init();
}