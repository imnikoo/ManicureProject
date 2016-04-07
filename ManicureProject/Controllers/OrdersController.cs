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
    public class OrdersController : ApiController
    {
        IOrderRepository _ordersRepo = new DummyOrderRepository();

        // GET: api/Orders
        [HttpGet]
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(_ordersRepo.GetAll(), Formatting.Indented, new JsonSerializerSettings
                {
                    DateFormatString = "yyyy-MM-dd",
                    ContractResolver = new CamelCasePropertyNamesContractResolver()

                })),
            };

        }

        // GET: api/Orders/5
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var foundedOrder = _ordersRepo.Read(id);
            if (foundedOrder != null)
            {
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new StringContent(JsonConvert.SerializeObject(_ordersRepo.Read(id), Formatting.Indented, new JsonSerializerSettings
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
        public HttpResponseMessage Post([FromBody]JObject entity)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var newCandidate = entity.ToObject<Order>();
            _ordersRepo.Create(newCandidate);
            response.Content = new StringContent(JsonConvert.SerializeObject(_ordersRepo.GetAll().Last(), Formatting.Indented, new JsonSerializerSettings
            {
                DateFormatString = "dd-MM-yyyy",
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }));
            response.StatusCode = HttpStatusCode.Created;
            return response;
        }

        // PUT: api/Orders/5
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody]JObject entity)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var updatedOrder = entity.ToObject<Order>();
            _ordersRepo.Update(updatedOrder);
            response.Content = new StringContent(JsonConvert.SerializeObject(_ordersRepo.Read(updatedOrder.Id), Formatting.Indented, new JsonSerializerSettings
            {
                DateFormatString = "dd-MM-yyyy",
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }));
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }

        // DELETE: api/Orders/5
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                _ordersRepo.Delete(id);
            }
            catch (Exception e)
            {
                response = new HttpResponseMessage(HttpStatusCode.NoContent);
                response.Content = new StringContent(e.Message);
            }
            if (_ordersRepo.Read(id) == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.OK);
            }
            return response;
        }
    }
}
