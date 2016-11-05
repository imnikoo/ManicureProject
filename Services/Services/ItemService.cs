using Data.EntityFramework.Infrastructure;
using ManicureDomain.DTOs;
using ManicureDomain.Entities;
using ManicureDomain.Entities.Enums;
using Services.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Services.Services
{
    public class ItemService
    {
        IUnitOfWork uow;
        public ItemService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public ItemDTO Get(int id)
        {
            var entity = uow.ItemRepository.GetByID(id);
            return DTOService.ToDTO<Item, ItemDTO>(entity);
        }
        public int? CheckName(string itemName)
        {
            List<Expression<Func<Item, bool>>> predicates = new List<Expression<Func<Item, bool>>>();

            if (!String.IsNullOrEmpty(itemName))
            {
                var wordsOfName = itemName.Split(' ');
                wordsOfName.ToList().ForEach(word =>
                {
                    predicates.Add(item => item.Title.Contains(word));
                });
                var items = uow.ItemRepository.Get(predicates).ToList();
                if(items.Any())
                {
                    return items.First().Id;
                }
            }
            return null;
        }
        public Tuple<IEnumerable<ItemDTO>, int> Get(int page, int perPage, string filterText)
        {
            List<Expression<Func<Item, bool>>> predicates = new List<Expression<Func<Item, bool>>>();

            var items = uow.ItemRepository.Get().Where(x => Regex.IsMatch(x.Category.Title, filterText, RegexOptions.IgnoreCase) 
                || Regex.IsMatch(x.Title, filterText, RegexOptions.IgnoreCase)
                || x.MarginalPrice.ToString().Equals(filterText)
                || x.OriginalPrice.ToString().Equals(filterText)
                || x.Stock.ToString().Equals(filterText)
                || Regex.IsMatch(x.AdditionalInformation ?? String.Empty, filterText, RegexOptions.IgnoreCase)
                );
            var skipped = items.Skip(page * perPage).Take(perPage).Select(item => DTOService.ToDTO<Item, ItemDTO>(item)).ToList();
            List<Expression<Func<Order, bool>>> orderedItemsPredicates = new List<Expression<Func<Order, bool>>>();
            foreach (var item in skipped)
            {
                orderedItemsPredicates.Add(x => x.Items.Any(i => i.ItemId == item.Id));
                var orders = uow.OrderRepository.Get(orderedItemsPredicates).Where(x=> x.State != OrderState.Closed).ToList();
                item.OrdersOfItem  = orders.SelectMany(x => x.Items.Where(i => i.ItemId == item.Id))
                    .Aggregate(0, (s, ordItem) => s + ordItem.Quantity);
                orderedItemsPredicates.Clear();
            }
            
            var total = items.Count();

            return new Tuple<IEnumerable<ItemDTO>, int>(
                skipped,
                total);
        }

        public ItemDTO Add(ItemDTO ItemToAdd)
        {
            Item newItem = new Item();
            newItem.Update(ItemToAdd, uow);
            uow.ItemRepository.Insert(newItem);
            uow.Commit();
            return DTOService.ToDTO<Item, ItemDTO>(newItem);
        }
        
        public ItemDTO Update(ItemDTO entity)
        {
            Item _Item = uow.ItemRepository.GetByID(entity.Id);
            _Item.Update(entity, uow);
            uow.ItemRepository.Update(_Item);
            uow.Commit();
            return DTOService.ToDTO<Item, ItemDTO>(_Item);
        }

        public bool Delete(int id)
        {
            bool deleteResult;
            var entityToDelete = uow.ItemRepository.GetByID(id);
            if (entityToDelete == null)
            {
                deleteResult = false;
            }
            else {
                uow.ItemRepository.Delete(id);
                uow.Commit();
                deleteResult = true;
            }
            return deleteResult;
        }
    }
}
