using ManicureDomain.DTOs;
using ManicureProject.Extensions;
using ManicureProject.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Services.Services;
using System;
using System.Web.Http;

namespace ManicureProject.Controllers
{
    [RoutePrefix("api/Orders")]
    public class OrdersController : ApiController
    {
        OrderService _service;
        protected static JsonSerializerSettings PROJECT_SERIALIZER_SETTINGS = new JsonSerializerSettings()
        {
            ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public OrdersController(OrderService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("query")]
        public IHttpActionResult Get([FromBody]OrderQueryModel query)
        {
            if (query.Page != 0)
            {
                query.Page = query.Page - 1;
            }
            var tupleResult = _service.Get(query.Page, query.PerPage, query.FilterText);
            var itemQueryResult = tupleResult.Item1;
            var total = tupleResult.Item2;
            var ret = new { Order = itemQueryResult, Current = query.Page, Size = query.PerPage, Total = total };
            return Json(ret, PROJECT_SERIALIZER_SETTINGS);
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            var foundedEntity = _service.Get(id);
            if (foundedEntity != null)
            {
                return Json(foundedEntity, PROJECT_SERIALIZER_SETTINGS);
            }
            return BadRequest();
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]OrderDTO newOrder)
        {
            if (!ModelState.IsValid)
            {
                return Json(ModelState.Errors(), PROJECT_SERIALIZER_SETTINGS);
            }
            var addedOrder = _service.Add(newOrder);
            return Json(addedOrder, PROJECT_SERIALIZER_SETTINGS);
        }

        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Put(int id, [FromBody]OrderDTO changedOrder)
        {
            if (!ModelState.IsValid)
            {
                return Json(ModelState.Errors(), PROJECT_SERIALIZER_SETTINGS);
            }
            var updOrder = _service.Update(changedOrder);
            return Json(updOrder, PROJECT_SERIALIZER_SETTINGS);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
