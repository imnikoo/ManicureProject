using ManicureDomain.DTOs;
using ManicureDomain.Entities;
using ManicureProject.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Services.Services;
using System.Web.Http;

namespace ManicureProject.Controllers
{
    public class AProjectController<DomainEntity, ViewModel> : ApiController
        where DomainEntity : Entity, new()
        where ViewModel : EntityDTO, new()
    {
        protected static JsonSerializerSettings PROJECT_SERIALIZER_SETTINGS = new JsonSerializerSettings()
        {
            ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        BaseService<DomainEntity, ViewModel> entityService;

        public AProjectController(BaseService<DomainEntity, ViewModel> service)
        {
            entityService = service;
        }

        public AProjectController()
        {
        }

        [HttpGet]
        public virtual IHttpActionResult Get()
        {
            var foundedEntities = entityService.Get();
            return Json(foundedEntities, PROJECT_SERIALIZER_SETTINGS);
        }

        [HttpGet]
        [Route("{id:int}")]
        public virtual IHttpActionResult Get(int id)
        {
            var foundedEntity = entityService.Get(id);
            if (foundedEntity != null)
            {
                return Json(foundedEntity, PROJECT_SERIALIZER_SETTINGS);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public virtual IHttpActionResult Remove(int id)
        {
            if (entityService.Delete(id))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        public virtual IHttpActionResult Add([FromBody]ViewModel entity)
        {
            if (!ModelState.IsValid)
            {
                return Json(ModelState.Errors(), PROJECT_SERIALIZER_SETTINGS);
            }
            var newEntity = entityService.Add(entity);
            return Json((newEntity), PROJECT_SERIALIZER_SETTINGS);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual IHttpActionResult Put(int id, [FromBody]ViewModel changedEntity)
        {
            if (!ModelState.IsValid)
            {
                return Json(ModelState.Errors(), PROJECT_SERIALIZER_SETTINGS);
            }
            var domainChangedEntity = entityService.Update(changedEntity);
            return Json(domainChangedEntity, PROJECT_SERIALIZER_SETTINGS);
        }
    }
}
