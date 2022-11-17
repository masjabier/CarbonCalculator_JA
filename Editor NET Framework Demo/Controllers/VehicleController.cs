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
    /// Vehicle Controller using VehicleModel
    /// </summary>
    public class VehicleController : ApiController
    {
        [Route("api/vehicle")]
        [HttpGet]
        [HttpPost]
        public IHttpActionResult House()
        {
            var request = HttpContext.Current.Request;
            var settings = Properties.Settings.Default;

            using (var db = new Database(settings.DbType, settings.DbConnection))
            {
                var response = new Editor(db, "TblT_VehicleEmision")
                    .Model<VehicleModel>()                                       
                    .Field(new Field("VehicleId"))
                    .Field(new Field("FuelId")
                        .Validator(Validation.Numeric())
                    )
                    .Field(new Field("TransportationId")
                        .Validator(Validation.Numeric())
                        .SetFormatter(Format.IfEmpty(null))
                    )
                    .Field(new Field("CapacityId")
                        .Validator(Validation.Numeric())
                        .SetFormatter(Format.IfEmpty(null))
                    )
                    .Field(new Field("PeriodeId")
                        .Validator(Validation.Numeric())
                        .SetFormatter(Format.IfEmpty(null))
                    )
                    .Field(new Field("UserId")
                        .Validator(Validation.Numeric())
                        .SetFormatter(Format.IfEmpty(null))
                    )
                    .Field(new Field("TravelFrequency")
                        .Validator(Validation.Numeric())
                        .SetFormatter(Format.IfEmpty(null))
                    )
                    .Field(new Field("Per")
                        .Validator(Validation.DateFormat(
                            Format.DATE_ISO_8601,
                            new ValidationOpts { Message = "Please enter a date in the format yyyy-mm-dd" }
                        ))
                        .GetFormatter(Format.DateSqlToFormat(Format.DATE_ISO_8601))
                        .SetFormatter(Format.DateFormatToSql(Format.DATE_ISO_8601))
                    )
                    .Field(new Field("AmountPeople")
                        .Validator(Validation.Numeric())
                        .SetFormatter(Format.IfEmpty(null))
                    )
                    .Field(new Field("Mileage")
                        .Validator(Validation.Numeric())
                        .SetFormatter(Format.IfEmpty(null))
                    )
                    .Field(new Field("CreatedDate") 
                        .Validator(Validation.DateFormat(
                            Format.DATE_ISO_8601,
                            new ValidationOpts { Message = "Please enter a date in the format yyyy-mm-dd" }
                        ))
                        .GetFormatter(Format.DateSqlToFormat(Format.DATE_ISO_8601))
                        .SetFormatter(Format.DateFormatToSql(Format.DATE_ISO_8601))
                    )

                    .Process(request)
                    .Data();

                return Json(response);
            }
        }
    }
}
