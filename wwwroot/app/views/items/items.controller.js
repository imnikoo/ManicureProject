﻿export default function ItemsController($rootScope, $scope, $state, $stateParams, $mdDialog, $mdMedia, ItemService) {
	'ngInject';
	var vm = $scope;
	vm.pageIsLoaded = false;
    vm.items = [];
	vm.selectedItems = [];
	vm.itemCount = $stateParams.order ? $stateParams.order.items : [];
	vm.isOrderCase = $stateParams.isOrderCase ? true : false;
	

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
		page: $stateParams.page ? $stateParams.page : 1,
		filterText: $rootScope.prevState == 'item' ? $rootScope.searchText : ''
    };

	vm.deselect = (item) => {
		vm.itemCount = _.filter(vm.itemCount, (selectedItem) => {
			if (selectedItem.item.id === item.id && selectedItem.id) {
				selectedItem.removed = true;
				return true;
			}
			return selectedItem.item.id !== item.id;
		});
	}

	vm.select = (item) => {
		var confirm = $mdDialog.prompt()
			.title('Выбор количества товара')
			.placeholder('Количество')
			.initialValue('1')
			.ok('ОК')
			.cancel('Отменить');
		$mdDialog.show(confirm).then((result) => {
			let parsedResult = parseInt(result);
			if (!isNaN(parsedResult)) {
				vm.itemCount.push({
					item: item,
					quantity: parsedResult
				});
			} else {
				vm.selectedItems = _.filter(vm.selectedItems, (selectedItem) => {
					return selectedItem.id !== item.id;
				});
				$mdDialog.show(
					$mdDialog.alert()
						.clickOutsideToClose(true)
						.title('Количество')
						.textContent('Товар исчисляется в количестве, а не в непонятности.')
						.ok('Окей.')
				);
			}
		})
		.catch(() => { 
			vm.selectedItems = _.filter(vm.selectedItems, (selectedItem) => {
					return selectedItem.id !== item.id;
				});
		});
	};

	vm.getItems = (page, limit) => {
		let query = {
			'limit': limit,
			'page': page,
			'filterText': vm.query.filterText
		};
		if (!page) {
            query = vm.query;
        }
		vm.performSearch(query);
	};

	vm.performSearch = (query, isSearchFilter) => {
		if(isSearchFilter) {
			query.page = 1;
		}
		vm.promise = ItemService.getItems(query).then(queryResult => {
			vm.items = queryResult.item;
			vm.queryResult = {
				'total': queryResult.total,
			};
			vm.pageIsLoaded = true;
		}).then(() => {
			if(vm.itemCount.length) {
				_.forEach(vm.items, item => {
					if(_.some(vm.itemCount, { itemId: item.id})) {
						vm.selectedItems.push(item);
					}
				});
			}	
		});
		return vm.promise;
	};

    vm.goToItem = (itemId) => {
		if (!vm.isOrderCase) {
			$state.go('item', { itemId, page: vm.query.page });
		}
    };

	vm.goToNextStage = () => {
		let orderedItems = _.map(vm.itemCount, (itemQuantityPair) => {
			return {
				id: itemQuantityPair.id,
				item: itemQuantityPair.item,
				itemId: itemQuantityPair.item.id,
				quantity: itemQuantityPair.quantity,
				removed: itemQuantityPair.removed
			};
		});
		let order = {};
		if($stateParams.order) {
			order = $stateParams.order;
			order.items = orderedItems;
		} else {
			order = {
				sum: 0,
				discount: 0,
				alreadyPaid: 0,
				toPay: 0,
				mailNumber: null,
				additionalInformation: null,
				cityId: null,
				reciever: null,
				phoneNumber: null,
				clientId: null,
				items: orderedItems
			};
		}
		$state.go('createOrder', { stage: 2, order: order });
	};

    vm.getItems();

}