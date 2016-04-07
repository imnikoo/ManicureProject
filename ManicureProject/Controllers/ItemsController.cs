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
    public class ItemsController : ApiController
    {
        IItemRepository _itemsRepo = new DummyItemRepository();
        // GET: api/Items
        [HttpGet]
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(_itemsRepo.GetAll(), Formatting.Indented, new JsonSerializerSettings
                {
                    DateFormatString = "yyyy-MM-dd",
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                })),
            };

        }

        // GET: api/Items/5
        public HttpResponseMessage Get(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var foundedItem = _itemsRepo.Read(id);
            if (foundedItem != null)
            {
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new StringContent(JsonConvert.SerializeObject(_itemsRepo.Read(id), Formatting.Indented, new JsonSerializerSettings
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

        // POST: api/Items
        [HttpPost]
        public HttpResponseMessage Post([FromBody]JObject entity)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var newItem = entity.ToObject<Item>();
            _itemsRepo.Create(newItem);
            response.Content = new StringContent(JsonConvert.SerializeObject(_itemsRepo.GetAll().Last(), Formatting.Indented, new JsonSerializerSettings
            {
                DateFormatString = "dd-MM-yyyy",
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }));
            response.StatusCode = HttpStatusCode.Created;
            return response;
        }

        // PUT: api/Items/5
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody]JObject entity)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var updatedItem = entity.ToObject<Item>(new JsonSerializer()
            {
                DateFormatString = "dd-MM-yyyy"
            });
            updatedItem.EditDate = DateTime.Now;
            var purchase = updatedItem.Purchases.FirstOrDefault(x => x.ApproximateArrivalDate == null);
            if (purchase!= null)
            {
                purchase.ApproximateArrivalDate = purchase.OrderDate.AddDays(30);
            }
            _itemsRepo.Update(updatedItem);
            response.Content = new StringContent(JsonConvert.SerializeObject(_itemsRepo.Read(updatedItem.Id), Formatting.Indented, new JsonSerializerSettings
            {
                DateFormatString = "dd-MM-yyyy",
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }));
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }

        // DELETE: api/Items/5
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                _itemsRepo.Delete(id);
            }
            catch (Exception e)
            {
                response = new HttpResponseMessage(HttpStatusCode.NoContent);
                response.Content = new StringContent(e.Message);
            }
            if (_itemsRepo.Read(id) != null)
            {
                response = new HttpResponseMessage(HttpStatusCode.OK);
            }
            return response;
        }

        
    }
}
