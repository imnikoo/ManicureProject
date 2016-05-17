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

    }
}
