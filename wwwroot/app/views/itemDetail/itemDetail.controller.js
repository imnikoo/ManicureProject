
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
        let comparingResult = vm.purchase.pricePerPiece && vm.purchase.amount && vm.purchase.place && vm.purchase.orderDate;
        deffered.resolve(comparingResult);
        return deffered.promise;
    }
    vm.checkName = (name) => {
        ItemService.checkName(name).then(itemId => {
            if (itemId) {
                let confirm = $mdDialog.confirm()
                    .title('Внимание')
                    .textContent('Товар с таким именем уже есть. Перейти на него?')
                    .ok('ОК')
                    .cancel('Отменить');
                $mdDialog.show(confirm).then(() => {
                    $state.go('item', { itemId });
                });
            }
        })
    }

    vm.confirmOrder = () => {
        _isPurchaseValid().then(() => {
            let approxArriveDate = new Date();
            approxArriveDate.setDate(vm.purchase.orderDate.getDate() + 30);
            let newPurchase = {
                pricePerPiece: vm.purchase.pricePerPiece,
                amount: vm.purchase.amount,
                isArrived: false,
                orderDate: vm.purchase.orderDate,
                purchasePlace: JSON.parse(vm.purchase.place),
                approximateArrivalDate: approxArriveDate,
                trackNumber: vm.purchase.trackNumber,
                itemId: vm.item.id
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

    vm.saveAndDuplicate = () => {
        var confirm = $mdDialog.prompt()
            .title('Сколько сделать дубликатов?')
            .placeholder('Количество')
            .ok('ОК')
            .cancel('Отменить');
        $mdDialog.show(confirm).then((result) => {
            let parsedResult = parseInt(result);
            if (!isNaN(parsedResult)) {
                $q.all(_.map(_.times(parsedResult), vm.saveItem))
                    .then(() => {
                        goBack();
                    });
            } else {
                $mdDialog.show(
                    $mdDialog.alert()
                        .clickOutsideToClose(true)
                        .title('Количество')
                        .textContent('Надо ввести цифры')
                        .ok('Окей')
                );
            }
        });
    };

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

        $mdDialog.show(confirm).then(() => {
            goBack();
        });
    }

    vm.saveItemAndBack = () => {
        vm.saveItem().then(() => {
            goBack();
        });
    }

    function goBack() {
        $state.go('items', { page: vm.pageOfList });
    }

    vm.saveItem = () => {
        return (() => {
            if (!vm.item.category) {
                var chosenCategory = _.find(vm.categories, ['title', vm.searchCategory]);
                if (chosenCategory) {
                    vm.item.category = chosenCategory;
                } else {
                    return ItemService.saveCategory({ title: vm.searchCategory }).then((value) => {
                        vm.item.category = value;
                        vm.item.categoryId = value.id;
                    })
                }
            }
            return $q.when();
        })().then(() => {
            return ItemService.saveItem(vm.item);
        });
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

    function createFilterFor(query) {
        var lowercaseQuery = angular.lowercase(query);
        return function filterFn(category) {
            return (category.title.toLowerCase().indexOf(lowercaseQuery) === 0);
        };
    }
    _init();
}