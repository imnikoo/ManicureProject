using ManicureDomain.Abstract;
using ManicureDomain.DummyRepos;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ManicureProject.Controllers
{
    public class CategoriesController : ApiController
    {
        ICategoryRepository _categoryRepo = new DummyCategoryRepository();

        [HttpGet]
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(_categoryRepo.GetAll(), Formatting.Indented, new JsonSerializerSettings
                {
                    DateFormatString = "yyyy-MM-dd",
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                })),
            };

        }
    }
}
