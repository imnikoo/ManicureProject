export default function ItemsController($scope, ItemService) {
	'ngInject';
	var vm = $scope;
	vm.pageIsLoaded = false;
    vm.title = "Загрузка.."
    vm.items = [];
    vm.orders = [];
    getItems();
    getOrders();
    vm.getItems = getItems;

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

	vm.calculateOrderedByClients = calculateOrderedByClients;
	vm.calculateOrderedByUser = calculateOrderedByUser;

	function calculateOrderedByUser(item) {
		var orderedItemsCount = 0;
		item.purchases.forEach((purchase) => {
			if(!purchase.isArrived) {
				orderedItemsCount+=purchase.amount;
			}
		})
		if(orderedItemsCount==0) return '';
		return '+' + orderedItemsCount;
	}
	function calculateOrderedByClients(item) {
		var orderedItemsCount = 0;
		vm.orders.forEach((order) => {        
			order.items.forEach((orderedItem) => {
				if(orderedItem.item.id===item.id) {
					orderedItemsCount+=orderedItem.quantity;
				}
			})
		})
		if(orderedItemsCount==0) return '';
		return '-' + orderedItemsCount;
	}

	function getItems() {
	    ItemService.getItems().then(value => {
	        vm.items = value;
	        vm.pageIsLoaded = true;
	    });
	}

	function getOrders() {
		var orders = ItemService.getOrders();
		orders.then(value => vm.orders = value);
	}
}