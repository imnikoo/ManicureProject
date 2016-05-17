using Data.EntityFramework.Infrastructure;
using ManicureDomain.Abstract;
using ManicureDomain.Entities;
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
    public class PurchasePlacesController : AProjectController<PurchasePlace>
    {
        public PurchasePlacesController(IDataRepositoryFactory factory) : base(factory)
        {

        }
    }
}