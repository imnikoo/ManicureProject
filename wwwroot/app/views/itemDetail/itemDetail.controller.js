
export default function ItemDetailController($state, $scope, $stateParams, $mdDialog, $mdMedia, ItemService) {
    'ngInject';
    var vm = $scope;

    vm.pageIsLoaded = false;
    vm.showPurchases = false;
    vm.showItemOrder = false;
    vm.item = {
        purchases: [],
    };
    vm.itemBefore = {};
    getItem($stateParams.itemId);   
    vm.categories = [];
    getCategories();
    vm.purchasePlaces = [];
    getPurchasePlaces();
    vm.querySearch = querySearch;  
    vm.saveItem = saveItem;
    vm.saveItemAndBack = saveItemAndBack;
    vm.calculateAndPasteMarginalPrice = calculateAndPasteMarginalPrice;
    vm.markUp = markUp;
    vm.tooglePurchases = tooglePurchases;
    vm.confirmOrder = confirmOrder; 
    vm.orderPlaceTitle = {};

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

    vm.orderPlace = {};
    vm.purchase={};

    function confirmOrder() {
        console.log(vm.purchase)
        if(vm.purchase.pricePerPiece && vm.purchase.amount  && vm.purchase.place && vm.purchase.orderDate) {
            vm.purchase.approximateArrivalDate = null;
            vm.purchase.place = JSON.parse(vm.purchase.place);
            vm.purchase.arrivalDate = null;
            vm.purchase.isArrived = false;
            vm.item.purchases.push(vm.purchase);
            saveItem().then((updatedItem) => { 
                vm.item = updatedItem;
                vm.purchase = {}; 
                vm.showItemOrder=false; 

            });
        }
    }

    function tooglePurchases() {
        if(vm.showItemOrder) {
            vm.showItemOrder = false;
        }
        vm.showPurchases=!vm.showPurchases;
    }

    function markUp () {
        return _.round((vm.item.marginalPrice*100/vm.item.originalPrice)-100);
    }

    function calculateAndPasteMarginalPrice() {
        console.log(vm.item.originalPrice*1.5);
        if(vm.item.originalPrice) {
            vm.item.marginalPrice=parseFloat((vm.item.originalPrice*1.5).toFixed(2));
        }
    }

    function isItemChanged() {
        return !_.isEqual(vm.item, vm.itemBefore);
    }

    vm.toogleOrder = () => {
        if(vm.showPurchases) {
            vm.showPurchases=false;
        }
        vm.showItemOrder=!vm.showItemOrder;
    }
    vm.checkAndWarn = ($event) => {
        if(isItemChanged()) {
            vm.showConfirm($event);
        }
        else {
            $state.go('items');
        }
    }
    vm.showConfirm = function(ev) {
        var confirm = $mdDialog.confirm()
              .title('Э')
              .textContent('Вы изменили содержимое товара и выходите. Выйти без сохранения?')
              .ariaLabel('Ди?')
              .targetEvent(ev)
              .ok('Выйти')
              .cancel('Отмена');

        $mdDialog.show(confirm).then(function() {
            $state.go('items');
        }, function() {
            
        });
    }

    function saveItemAndBack() {
        saveItem().then(value => $state.go('items'));
    }

    function saveItem() {
        return ItemService.saveItem(vm.item);
    }

    function getItem(itemId) {
        if(itemId) {
            ItemService.getItem(itemId).then(value => {
                vm.item = value;
                vm.itemBefore = _.clone(value);
                vm.pageIsLoaded = true;
            });
        }
	}

	function getCategories() {
	    ItemService.getCategories().then(value => {
	        vm.categories = value;    
	    });
	}

	function getPurchasePlaces() {
        ItemService.getPurchasePlaces().then(value => {
            vm.purchasePlaces = (value);
        })
	}

	function querySearch(query) {
	    console.log(query);
	    var results = query ? vm.categories.filter( createFilterFor(query) ) : vm.categories,
           deferred;
	    return results;
	}

	function createFilterFor(query) {
	    var lowercaseQuery = angular.lowercase(query);

	    return function filterFn(category) {
	        return (category.title.toLowerCase().indexOf(lowercaseQuery) === 0);
	    };
	}
}