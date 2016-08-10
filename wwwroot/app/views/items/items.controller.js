export default function ItemsController($scope, $state, $stateParams, $mdDialog, $mdMedia, ItemService) {
	'ngInject';
	var vm = $scope;
	vm.pageIsLoaded = false;
    vm.title = "Загрузка.."
    vm.items = [];
	vm.itemCount = $stateParams.orderedItems ? $stateParams.orderedItems : [];
	vm.selectedItems = [];
	console.log(vm.selectedItems, 'popali');

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
		filterText: ''
    };

	vm.deselect = (item) => {
		vm.itemCount = _.filter(vm.itemCount, (selectedItem) => {
			return selectedItem.item.id !== item.id;
		});
	}

	vm.select = (item) => {
		var confirm = $mdDialog.prompt()
			.title('Выбор количества товара')
			.placeholder('Количество')
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
		});
		console.log(vm.selectedItems, 'dobavili');
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

	vm.performSearch = (query) => {
		vm.promise = ItemService.getItems(query).then(value => {
			vm.items = value.item;
			vm.queryResult = {
				'total': value.total,
			};
			vm.pageIsLoaded = true;
			vm.selectedItems = vm.itemCount.length ? _.filter(vm.items, (item) => {
				return _.some(_.map(vm.itemCount, (itemCountPair) => {
					return itemCountPair.item;
				}), { id: item.id });
			}) : [];
		});
	};

    vm.goToItem = (itemId) => {
        $state.go('item', { itemId, page: vm.query.page });
    };

	vm.goToNextStage = () => {
		$state.go('createOrder', { stage: 2, orderedItems: vm.itemCount });
	};

    vm.getItems();

}