using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace S3LabTest2WebApp.Controllers
{
    public class CsvFileController : ApiController
    {
        public HttpResponseMessage Post()
        {

            HttpResponseMessage result = null;

            var httpRequest = HttpContext.Current.Request;

            if (httpRequest.Files.Count > 0)
            {

                var csvfiles = new List<string>();

                foreach (string file in httpRequest.Files)
                {

                    var postedFile = httpRequest.Files[file];

                    var filePath = HttpContext.Current.Server.MapPath("~/UploadFiles/" + postedFile.FileName);

                    postedFile.SaveAs(filePath);



                    csvfiles.Add(filePath);

                }

                result = Request.CreateResponse(HttpStatusCode.Created, csvfiles);

            }

            else
            {

                result = Request.CreateResponse(HttpStatusCode.BadRequest);

            }

            return result;

        }
    }
}
