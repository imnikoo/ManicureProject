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
    [RoutePrefix("api/Clients")]
    public class ClientsController : ApiController
    {
        ClientService _service;
        protected static JsonSerializerSettings PROJECT_SERIALIZER_SETTINGS = new JsonSerializerSettings()
        {
            ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public ClientsController(ClientService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("query")]
        public IHttpActionResult Get([FromBody]ClientQueryModel query)
        {
            if (query.Page != 0)
            {
                query.Page = query.Page - 1;
            }
            var tupleResult = _service.Get(query.Page, query.PerPage, query.FilterText);
            var clientQueryResult = tupleResult.Item1;
            var total = tupleResult.Item2;

            var ret = new { Client = clientQueryResult, Current = query.Page, Size = query.PerPage, Total = total };
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
        [Route("")]
        public IHttpActionResult Post([FromBody]ClientDTO newClient)
        {
            if (!ModelState.IsValid)
            {
                return Json(ModelState.Errors(), PROJECT_SERIALIZER_SETTINGS);
            }
            var addedClient = _service.Add(newClient);
            return Json(addedClient, PROJECT_SERIALIZER_SETTINGS);
        }

        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Put(int id, [FromBody]ClientDTO changedClient)
        {
            if (!ModelState.IsValid)
            {
                return Json(ModelState.Errors(), PROJECT_SERIALIZER_SETTINGS);
            }
            var updClient = _service.Update(changedClient);
            return Json(updClient, PROJECT_SERIALIZER_SETTINGS);
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
