using Data.EntityFramework.Infrastructure;
using ManicureDomain.DTOs;
using ManicureDomain.Entities;
using Services.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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

        public Tuple<IEnumerable<ItemDTO>, int> Get(int page, int perPage, string filterText)
        {
            List<Expression<Func<Item, bool>>> predicates = new List<Expression<Func<Item, bool>>>();

            if (!String.IsNullOrEmpty(filterText))
            {
                predicates.Add(x =>
                x.Category.Title.StartsWith(filterText)
                || x.Title.StartsWith(filterText)
                || x.MarginalPrice.ToString().Equals(filterText)
                || x.OriginalPrice.ToString().Equals(filterText)
                || x.Stock.ToString().StartsWith(filterText)
                || x.AdditionalInformation.Contains(filterText));
            }

            var items = uow.ItemRepository.Get(predicates);
            var total = items.Count();

            return new Tuple<IEnumerable<ItemDTO>, int>(
                items.Skip(page * perPage).Take(perPage).Select(item => DTOService.ToDTO<Item, ItemDTO>(item)),
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
