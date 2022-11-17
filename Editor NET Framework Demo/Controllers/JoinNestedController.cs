using System;
using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using DataTables;
using Editor_NET_Framework_Demo.Models;

namespace Editor_NET_Framework_Demo.Controllers
{
    /// <summary>
    /// This example shows a very simple join using the `LeftJoin` method.
    /// Of particular note in this example is that the `JoinModel` defines two
    /// nested classes that obtain the data required from the two tables, where
    /// the nested class name defines the database table and the properties of
    /// the nested classes define the columns in each table.
    ///
    /// Note also the use of the `Options()` method for the `users.site` method
    /// which is used to retrieve the information for the `Site` drop down list
    /// on the client-side, which is automatically populated when the data is
    /// loaded.
    /// </summary>
    public class JoinNestedController : ApiController
    {
        [Route("api/joinNested")]
        [HttpGet]
        [HttpPost]
        public IHttpActionResult Join()
        {
            var request = HttpContext.Current.Request;
            var settings = Properties.Settings.Default;

            using (var db = new Database(settings.DbType, settings.DbConnection))
            {
                var response = new Editor(db, "users")
                    .Model<JoinModelUsers>("users")
                    .Model<JoinModelSites>("sites")
                    .Field(new Field("users.site")
                        .Validator(Validation.DbValues(new ValidationOpts { Empty = false }, "id", "sites", db))
                    )
                    .LeftJoin("sites", "sites.id", "=", "users.site")
                    .Process(request)
                    .Data();

                return Json(response);
            }
        }
    }
}
