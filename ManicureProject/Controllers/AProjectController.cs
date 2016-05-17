using Data.EntityFramework.Infrastructure;
using ManicureDomain.Abstract;
using ManicureDomain.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace ManicureProject.Controllers
{
    public class AProjectController<DomainEntity> : ApiController
        where DomainEntity : Entity, new ()
    {
        protected static JsonSerializerSettings PROJECT_SERIALIZER_SETTINGS = new JsonSerializerSettings()
        {
            ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
        
        protected IDataRepositoryFactory _repositoryFactory;

        public AProjectController(IDataRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        [HttpGet]
        public virtual IHttpActionResult All(HttpRequestMessage request)
        {
            var _currentRepository = _repositoryFactory.GetDataRepository<DomainEntity>(request);

            var entitiesQuery = _currentRepository.GetAll().OrderBy(x => x.Id);
            var entities = entitiesQuery.ToList();
            return Json(entities, PROJECT_SERIALIZER_SETTINGS);
        }

        // GET: api/Clients/5
        [HttpGet]
        public virtual IHttpActionResult Get(HttpRequestMessage request, int id)
        {
            var _currentRepository = _repositoryFactory.GetDataRepository<DomainEntity>(request);

            var foundedEntity = _currentRepository.Get(id);
            if (foundedEntity != null)
            {
                return Json(foundedEntity, PROJECT_SERIALIZER_SETTINGS);
            }
            return NotFound();
        }

        // POST: api/Clients
        [HttpPost]
        public virtual IHttpActionResult Add(HttpRequestMessage request, [FromBody]DomainEntity entity)
        {
            var _currentRepository = _repositoryFactory.GetDataRepository<DomainEntity>(request);

            if (!ModelState.IsValid)
            {
                var errorList = ModelState.Keys.SelectMany(k => ModelState[k].Errors).Select(x => x.ErrorMessage);
                return Json(new
                {
                    summary = "Bad request",
                    errorList = errorList
                });
            }
            else
            {
                var newEntity = entity;
                _currentRepository.Add(newEntity);
                _currentRepository.Commit();
                return Json(newEntity, PROJECT_SERIALIZER_SETTINGS);
            }
        }

        // PUT: api/Clients/5
        [HttpPut]
        public virtual IHttpActionResult Put(HttpRequestMessage request, int id, [FromBody]DomainEntity entity)
        {
            var _currentRepository = _repositoryFactory.GetDataRepository<DomainEntity>(request);

            if (!ModelState.IsValid)
            {
                var errorList = ModelState.Keys.SelectMany(k => ModelState[k].Errors).Select(x => x.ErrorMessage);

                return Json(new
                {
                    summary = "Bad request",
                    errorList = errorList
                });
            }
            else
            {
                var changedDomainEntity = entity;
                _currentRepository.Update(changedDomainEntity);
                _currentRepository.Commit();
                return Json(changedDomainEntity, PROJECT_SERIALIZER_SETTINGS);
            }
        }

        // DELETE: api/Clients/5
        [HttpDelete]
        public virtual IHttpActionResult Remove(HttpRequestMessage request, int id)
        {
            var _currentRepository = _repositoryFactory.GetDataRepository<DomainEntity>(request);

            var entityToRemove = _currentRepository.Get(id);
            if (entityToRemove != null)
            {
                _currentRepository.Remove(entityToRemove);
                _currentRepository.Commit();
                return Ok();
            }
            return BadRequest();
        }

    }
}
