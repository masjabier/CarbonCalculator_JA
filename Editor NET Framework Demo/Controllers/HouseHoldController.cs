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
    /// Household Controller
    /// This controller used for CRUD operation of Household type in Carbon Calculator
    /// </summary>
    public class HouseHoldController : ApiController
    {
        [Route("api/household")]
        [HttpGet]
        [HttpPost]
        public IHttpActionResult House()
        {
            var request = HttpContext.Current.Request;
            var settings = Properties.Settings.Default;

            using (var db = new Database(settings.DbType, settings.DbConnection))
            {
                var response = new Editor(db, "TblT_Household")
                    .Model<HouseHoldModel>()                   
                    
                    .Field(new Field("AmountPeople"))
                    .Field(new Field("StandMeter")
                        .Validator(Validation.Numeric())
                    )
                    .Field(new Field("LpgConsumption")
                        .Validator(Validation.Numeric())
                        .SetFormatter(Format.IfEmpty(null))
                    )
                    .Field(new Field("CityGasConsumption")
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

                     .Field(new Field("UserId")
                        .Validator(Validation.NotEmpty())
                    )

                    // Setup for PeriodeID column
                    //.Field(new Field("PeriodeId")
                    //   .Validator(Validation.Numeric())

                    .Process(request)
                    .Data();

                return Json(response);
            }
        }
    }
}
