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
    /// This is controller is used by the majority of Editor examples as it
    /// provides a nice rounded set of information for the client-side Editor
    /// Javascript library to show its capabilities.
    ///
    /// In the code here, note that the `StaffModel` is used as the model for
    /// the Editor, which automatically defines the database fields to be read.
    /// Additional instructions can be given for each field by creating a `Field`
    /// instance for it - many of the fields have validation methods applied here
    /// and the date field has a formatter to make it readable to users looking
    /// at the table!
    /// </summary>
    public class UploadManyController : ApiController
    {
        [HttpGet, HttpPost, Route("api/upload-many")]
        public IHttpActionResult Staff()
        {
            var request = HttpContext.Current.Request;
            var settings = Properties.Settings.Default;

            using (var db = new Database(settings.DbType, settings.DbConnection))
            {
                var response = new Editor(db, "users")
                    .Model<UploadManyModel>()
                    .Field(new Field("users.site")
                        .Options(new Options()
                            .Table("sites")
                            .Value("id")
                            .Label("name")
                        )
                    )
                    .Where("users.site", "0")
                    .LeftJoin("sites", "sites.id", "=", "users.site")
                    .MJoin(new MJoin("files")
                        .Link("users.id", "users_files.user_id")
                        .Link("files.id", "users_files.file_id")
                        .Field(
                            new Field("id")
                                .Upload(new Upload(request.PhysicalApplicationPath + @"uploads\__ID____EXTN__")
                                    .Db("files", "id", new Dictionary<string, object>
                                    {
                                        {"web_path", Upload.DbType.WebPath},
                                        {"system_path", Upload.DbType.SystemPath},
                                        {"filename", Upload.DbType.FileName},
                                        {"filesize", Upload.DbType.FileSize}
                                    })
                                    .Validator(Validation.FileSize(500000, "Max file size is 500K."))
                                    .Validator(Validation.FileExtensions(new[] { "jpg", "png", "gif" }, "Please upload an image."))
                                )
                        )
                    )
                    .TryCatch(false)
                    .Debug(true)
                    .Process(request)
                    .Data();

                return Json(response);
            }
        }
    }
}
