using Data.EntityFramework.Infrastructure;
using ManicureDomain.DTOs;
using ManicureDomain.Entities;
using Services.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

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

        public int? CheckName(string firstName, string lastName)
        {
            List<Expression<Func<Client, bool>>> predicates = new List<Expression<Func<Client, bool>>>();

            if (!String.IsNullOrEmpty(firstName) && !String.IsNullOrEmpty(lastName))
            {
                    predicates.Add(client =>
                        (client.FirstName.Contains(firstName) && client.LastName.Contains(lastName)
                    ));
                var clients = uow.ClientRepository.Get(predicates).ToList();
                if (clients.Any())
                {
                    return clients.First().Id;
                }
            }
            return null;
        }

        public Tuple<IEnumerable<ClientDTO>, int> Get(int page, int perPage, string filterText)
        {
            List<Expression<Func<Client, bool>>> predicates = new List<Expression<Func<Client, bool>>>();
            
            var clients = uow.ClientRepository.Get().Where(x => Regex.IsMatch(x.City.Title, filterText, RegexOptions.IgnoreCase)
            || Regex.IsMatch(x.City.Title, filterText, RegexOptions.IgnoreCase)
            || Regex.IsMatch(x.FirstName ?? String.Empty, filterText, RegexOptions.IgnoreCase)
            || Regex.IsMatch(x.LastName ?? String.Empty, filterText, RegexOptions.IgnoreCase)
            || Regex.IsMatch(x.PhoneNumber ?? String.Empty, filterText, RegexOptions.IgnoreCase)
            || Regex.IsMatch(x.Source ?? String.Empty, filterText, RegexOptions.IgnoreCase)
            || Regex.IsMatch(x.AdditionalInformation ?? String.Empty, filterText, RegexOptions.IgnoreCase));
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
