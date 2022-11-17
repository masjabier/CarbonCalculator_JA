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
    /// This example shows how multiple `LeftJoin()` statements can be used to
    /// reference data from multiple database tables. In this case the `user_dept`
    /// table is a "link table" that references both the user and dept tables
    /// to create a reference (a link) between them.
    /// </summary>
    public class JoinLinkTableController : ApiController
    {
        [Route("api/joinLinkTable")]
        [HttpGet]
        [HttpPost]
        public IHttpActionResult JoinLinkTable()
        {
            var request = HttpContext.Current.Request;
            var settings = Properties.Settings.Default;

            using (var db = new Database(settings.DbType, settings.DbConnection))
            {
                var response = new Editor(db, "users")
                    .Model<JoinLinkTableModel>()
                    .Field(new Field("users.site")
                        .Options(new Options()
                            .Table("sites")
                            .Value("id")
                            .Label("name")
                        )
                    )
                    .Field(new Field("user_dept.dept_id")
                        .Options(new Options()
                            .Table("dept")
                            .Value("id")
                            .Label("name")
                        )
                    )
                    .LeftJoin("sites", "sites.id", "=", "users.site")
                    .LeftJoin("user_dept", "users.id", "=", "user_dept.user_id")
                    .LeftJoin("dept", "user_dept.dept_id", "=", "dept.id")
                    .Process(request)
                    .Data();

                return Json(response);
            }
        }
    }
}
