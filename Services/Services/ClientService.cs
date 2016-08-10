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
    public class ClientService
    {
        IUnitOfWork uow;
        public ClientService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public ClientDTO Get(int id)
        {
            var entity = uow.ClientRepository.GetByID(id);
            return DTOService.ToDTO<Client, ClientDTO>(entity);
        }

        public ClientDTO Add(ClientDTO ClientToAdd)
        {
            Client clientToAdd = new Client();
            clientToAdd.Update(ClientToAdd, uow);
            uow.ClientRepository.Insert(clientToAdd);
            uow.Commit();
            return DTOService.ToDTO<Client, ClientDTO>(clientToAdd);
        }

        public ClientDTO Update(ClientDTO entity)
        {
            Client _Client = uow.ClientRepository.GetByID(entity.Id);
            _Client.Update(entity, uow);
            uow.ClientRepository.Update(_Client);
            uow.Commit();
            return DTOService.ToDTO<Client, ClientDTO>(_Client);
        }

        public Tuple<IEnumerable<ClientDTO>, int> Get(int page, int perPage, string filterText)
        {
            List<Expression<Func<Client, bool>>> predicates = new List<Expression<Func<Client, bool>>>();

            if (!String.IsNullOrEmpty(filterText))
            {
                predicates.Add(x =>
                x.City.Title.StartsWith(filterText)
                || x.FirstName.StartsWith(filterText)
                || x.LastName.StartsWith(filterText)
                || x.PhoneNumber.StartsWith(filterText)
                || x.Source.StartsWith(filterText)
                || x.AdditionalInformation.Contains(filterText));
            }

            var clients = uow.ClientRepository.Get(predicates);
            var total = clients.Count();

            return new Tuple<IEnumerable<ClientDTO>, int>(
                clients.Skip(page * perPage).Take(perPage).Select(client => DTOService.ToDTO<Client, ClientDTO>(client)),
                total);
        }

        public bool Delete(int id)
        {
            bool deleteResult;
            var entityToDelete = uow.ClientRepository.GetByID(id);
            if (entityToDelete == null)
            {
                deleteResult = false;
            }
            else {
                uow.ClientRepository.Delete(id);
                uow.Commit();
                deleteResult = true;
            }
            return deleteResult;
        }
    }
}
