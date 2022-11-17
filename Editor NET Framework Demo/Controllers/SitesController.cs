using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using DataTables;
using Editor_NET_Framework_Demo.Models;

namespace Editor_NET_Framework_Demo.Controllers
{
    /// <summary>
    /// Sites controller
    /// </summary>
    public class SitesController : ApiController
    {
        [Route("api/sites")]
        [HttpGet]
        [HttpPost]
        public IHttpActionResult Sites()
        {
            var request = HttpContext.Current.Request;
            var settings = Properties.Settings.Default;

            using (var db = new Database(settings.DbType, settings.DbConnection))
            {
                var response = new Editor(db, "sites")
                    .Field(new Field("id")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("name")
                        .Validator(Validation.NotEmpty())
                    )
                    .MJoin(new MJoin("users")
                        .Link("sites.id", "users.site")
                        .Field(new Field("id"))
                    )
                    .Process(Request)
                    .Data();

                return Json(response);
            }
        }
    }
}
