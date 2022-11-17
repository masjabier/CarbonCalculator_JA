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
    public class StaffViewController : ApiController
    {
        [Route("api/staff-view")]
        [HttpGet]
        [HttpPost]
        public IHttpActionResult Staff()
        {
            var request = HttpContext.Current.Request;
            var settings = Properties.Settings.Default;

            using (var db = new Database(settings.DbType, settings.DbConnection))
            {
                var response = new Editor(db, "users")
					.ReadTable("staff_newyork")
                    .Field(new Field("first_name")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("last_name"))
                    .Field(new Field("phone"))
                    .Field(new Field("city"))
                    .Field(new Field("site")
                        .Get(false)
                        .SetValue(4)
                    )
                    .Process(Request)
                    .Data();

                return Json(response);
            }
        }
    }
}
