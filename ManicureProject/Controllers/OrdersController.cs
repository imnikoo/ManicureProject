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
    public class OrdersController : AProjectController<Order>
    {
        public OrdersController(IDataRepositoryFactory factory) : base(factory)
        {

        }

        public override IHttpActionResult All(HttpRequestMessage request)
        {
            return base.All(request);
        }

        // GET: api/Orders/5
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            var _currentRepository = _repositoryFactory.GetDataRepository<Order>(request);

            HttpResponseMessage response = new HttpResponseMessage();
            var foundedOrder = _currentRepository.Get(id);
            if (foundedOrder != null)
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

        // POST: api/Orders
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, [FromBody]JObject entity)
        {
            var _currentRepository = _repositoryFactory.GetDataRepository<Order>(request);

            HttpResponseMessage response = new HttpResponseMessage();
            var newCandidate = entity.ToObject<Order>();
            _currentRepository.Add(newCandidate);
            response.Content = new StringContent(JsonConvert.SerializeObject(_currentRepository.GetAll().Last(), Formatting.Indented, new JsonSerializerSettings
            {
                DateFormatString = "dd-MM-yyyy",
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }));
            response.StatusCode = HttpStatusCode.Created;
            return response;
        }

        // PUT: api/Orders/5
        [HttpPut]
        public HttpResponseMessage Put(HttpRequestMessage request, int id, [FromBody]JObject entity)
        {
            var _currentRepository = _repositoryFactory.GetDataRepository<Order>(request);

            HttpResponseMessage response = new HttpResponseMessage();
            var updatedOrder = entity.ToObject<Order>();
            _currentRepository.Update(updatedOrder);
            response.Content = new StringContent(JsonConvert.SerializeObject(_currentRepository.Get(updatedOrder.Id), Formatting.Indented, new JsonSerializerSettings
            {
                DateFormatString = "dd-MM-yyyy",
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }));
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }

        // DELETE: api/Orders/5
        [HttpDelete]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            var _currentRepository = _repositoryFactory.GetDataRepository<Order>(request);

            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                _currentRepository.Remove(id);
            }
            catch (Exception e)
            {
                response = new HttpResponseMessage(HttpStatusCode.NoContent);
                response.Content = new StringContent(e.Message);
            }
            if (_currentRepository.Get(id) == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.OK);
            }
            return response;
        }
    }
}
