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
    /// Users controller
    /// </summary>
    public class UsersController : ApiController
    {
        [Route("api/users")]
        [HttpPost]
        public IHttpActionResult Users()
        {
            var request = HttpContext.Current.Request;
            var settings = Properties.Settings.Default;

            using (var db = new Database(settings.DbType, settings.DbConnection))
            {
				var site = "-1";
				if (request.Form.AllKeys.Contains("site"))
				{
					site = request.Form["site"];
				}

                var response = new Editor(db, "users")
                    .Model<JoinModelUsers>("users")
                    .Model<JoinModelSites>("sites")
                    .Field(new Field("users.site")
                        .Options(new Options()
                            .Table("sites")
                            .Value("id")
                            .Label("name")
                        )
                        .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                    )
                    .LeftJoin("sites", "sites.id", "=", "users.site")
					.Where("site", site)
                    .Process(Request)
                    .Data();

                return Json(response);
            }
        }
    }
}
