using ManicureDomain.Abstract;
using ManicureDomain.DummyRepos;
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
    public class ClientsController : ApiController
    {
        IClientRepository _clientsRepo = new DummyClientRepository();

        // GET: api/Clients
        [HttpGet]
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(_clientsRepo.GetAll(), Formatting.Indented, new JsonSerializerSettings
                {
                    DateFormatString = "yyyy-MM-dd",
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                })),
            };
            
        }

        // GET: api/Clients/5
        public HttpResponseMessage Get(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var foundedClient = _clientsRepo.Read(id);
            if (foundedClient != null)
            {
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new StringContent(JsonConvert.SerializeObject(_clientsRepo.Read(id), Formatting.Indented, new JsonSerializerSettings
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
        public HttpResponseMessage Post([FromBody]JObject entity)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var newCandidate = entity.ToObject<Client>();
            _clientsRepo.Create(newCandidate);
            response.Content = new StringContent(JsonConvert.SerializeObject(_clientsRepo.GetAll().Last(), Formatting.Indented, new JsonSerializerSettings
            {
                DateFormatString = "dd/mm/yyyy",
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }));
            response.StatusCode = HttpStatusCode.Created;
            return response;
        }

        // PUT: api/Clients/5
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody]JObject entity)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var updatedClient = entity.ToObject<Client>();
            _clientsRepo.Update(updatedClient);
            response.Content = new StringContent(JsonConvert.SerializeObject(_clientsRepo.Read(updatedClient.Id), Formatting.Indented, new JsonSerializerSettings
            {
                DateFormatString = "dd/mm/yyyy",
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }));
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }

        // DELETE: api/Clients/5
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try {
                _clientsRepo.Delete(id);
            }
            catch (Exception e)
            {
                response = new HttpResponseMessage(HttpStatusCode.NoContent);
                response.Content = new StringContent(e.Message);
            }
            if (_clientsRepo.Read(id)==null)
            {
                response = new HttpResponseMessage(HttpStatusCode.OK);
            }
            return response;
        }
    }
}
