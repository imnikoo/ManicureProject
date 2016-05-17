using Data.EntityFramework.Infrastructure;
using ManicureDomain.Abstract;
using ManicureDomain.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ManicureProject.Controllers
{
    public class ClientsController : AProjectController<Client>
    {
        public ClientsController(IDataRepositoryFactory factory) : base(factory)
        {

        }

        public override IHttpActionResult All(HttpRequestMessage request)
        {
            return base.All(request);
        }

        // GET: api/Clients/5
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            var _currentRepository = _repositoryFactory.GetDataRepository<Client>(request);

            HttpResponseMessage response = new HttpResponseMessage();
            var foundedClient = _currentRepository.Get(id);
            if (foundedClient != null)
            {
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new StringContent(JsonConvert.SerializeObject(_currentRepository.Get(id), Formatting.Indented, new JsonSerializerSettings
                {
                    DateFormatString = "yyyy-MM-dd",
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }));
            }
            else
            {
                response.StatusCode = HttpStatusCode.NotFound;
            }
            return response;
        }

        // POST: api/Clients
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, [FromBody]JObject entity)
        {
            var _currentRepository = _repositoryFactory.GetDataRepository<Client>(request);

            HttpResponseMessage response = new HttpResponseMessage();
            var newCandidate = entity.ToObject<Client>();
            _currentRepository.Add(newCandidate);
            response.Content = new StringContent(JsonConvert.SerializeObject(_currentRepository.GetAll().Last(), Formatting.Indented, new JsonSerializerSettings
            {
                DateFormatString = "dd/mm/yyyy",
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }));
            response.StatusCode = HttpStatusCode.Created;
            return response;
        }

        // PUT: api/Clients/5
        [HttpPut]
        public HttpResponseMessage Put(HttpRequestMessage request, int id, [FromBody]JObject entity)
        {
            var _currentRepository = _repositoryFactory.GetDataRepository<Client>(request);

            HttpResponseMessage response = new HttpResponseMessage();
            var updatedClient = entity.ToObject<Client>();
            _currentRepository.Update(updatedClient);
            response.Content = new StringContent(JsonConvert.SerializeObject(_currentRepository.Get(updatedClient.Id), Formatting.Indented, new JsonSerializerSettings
            {
                DateFormatString = "dd/mm/yyyy",
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }));
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }

        // DELETE: api/Clients/5
        [HttpDelete]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            var _currentRepository = _repositoryFactory.GetDataRepository<Client>(request);

            HttpResponseMessage response = new HttpResponseMessage();
            try {
                _currentRepository.Remove(id);
            }
            catch (Exception e)
            {
                response = new HttpResponseMessage(HttpStatusCode.NoContent);
                response.Content = new StringContent(e.Message);
            }
            if (_currentRepository.Get(id)==null)
            {
                response = new HttpResponseMessage(HttpStatusCode.OK);
            }
            return response;
        }
    }
}
