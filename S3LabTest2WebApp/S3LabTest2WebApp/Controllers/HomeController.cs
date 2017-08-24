using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using S3LabTest2WebApp.BL;
using S3LabTest2WebApp.IBLL;
using S3LabTest2WebApp.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace S3LabTest2WebApp.Controllers
{
    public class HomeController : Controller
    {
        private IChartsData _iChartsData;
        
        public ActionResult Index()
        {
            List<Graph> getDates = new List<Graph>();
                getDates = FileManager.GetCSvValue();

            int listLength = getDates.Count;

             string[] timeList = new string[listLength];
             double[] ValueList = new double[listLength];

             for (int i = 0; i< listLength; i++)
             {
                 timeList[i] = getDates[i].Time;
                 ValueList[i] = Convert.ToDouble(getDates[i].Value);
             }

             ChartsData.Times = timeList;

             IChartsData chm = new ChartsDataManager();

             ChartsData.Values = chm.GenerateData(getDates);
                
        


             Highcharts charts = new Highcharts("chart")
                .InitChart(new Chart { DefaultSeriesType = ChartTypes.Spline })
                .SetTitle(new Title { Text = "Cooling Data Information Per Hour" })
                .SetSubtitle(new Subtitle { Text = "Green Building" })
                .SetXAxis(new XAxis { Categories = ChartsData.Times })
                .SetYAxis(new YAxis
                {
                    Max = 2000,
                    Min = 0,
                    Title = new YAxisTitle { Text = "Value" }
                    //, Labels = new YAxisLabels { Formatter = "function() { return this.value +'°' }" }
                })
                .SetTooltip(new Tooltip
                {
                    Crosshairs = new Crosshairs(true),
                    Shared = true
                })
                .SetPlotOptions(new PlotOptions
                {
                    Spline = new PlotOptionsSpline
                    {
                        Marker = new PlotOptionsSplineMarker
                        {
                            Radius = 4,
                            LineColor = ColorTranslator.FromHtml("#666666"),
                            LineWidth = 1
                        }
                    }
                })
                .SetSeries(ChartsData.Values);
                
            return View(charts);
        }


        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file_Uploader)  
        {
            string fName = string.Empty;
            string fileName = string.Empty;
            string destinationPath = string.Empty;

            try  
            {
                if (file_Uploader.ContentLength > 0)  
                {
                    string prevFile = HttpContext.Server.MapPath("~/UploadFiles/InputFile.csv");
                    

                    fName = Path.GetFileName(file_Uploader.FileName);
                    var extension = Path.GetExtension(fName);

                    if (extension.ToLower() == ".csv")
                    {
                        if (System.IO.File.Exists(prevFile))
                        {
                            System.IO.File.Delete(prevFile);
                        }

                        fileName = "InputFile" + extension;

                        destinationPath = HttpContext.Server.MapPath("~/UploadFiles/" + fileName);

                        file_Uploader.SaveAs(destinationPath);

                        ViewBag.Message = "File Uploaded Successfully!!";

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Message = "Please select only CSV file";
                        return View("~/Views/Home/Index.cshtml");
                    }
                }

                else
                {
                    ViewBag.Message = "File upload failed!!";  
                } 
            }  
            catch  
            {  
                ViewBag.Message = "File upload failed!!";  
                
            }

            return null;
            
        }
    }
}