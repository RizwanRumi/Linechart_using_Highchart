using S3LabTest2WebApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace S3LabTest2WebApp.BL
{
    static class FileManager
    {
        public static List<Graph> GetCSvValue()
        {
            List<Graph> dateList = new List<Graph>();


            string path = HttpContext.Current.Server.MapPath("~/UploadFiles/InputFile.csv");

            if (System.IO.File.Exists(path))
            {
                FileStream fs = new FileStream(path, FileMode.Open);
                StreamReader sr = new StreamReader(fs);

                string data;

                while (!sr.EndOfStream)
                {
                    data = sr.ReadLine();

                    string[] str = data.Split(',');

                    if (!str[0].Equals("Time"))
                    {
                        Graph grp = new Graph();
                        grp.Time = str[0];
                        grp.Value = str[1];

                        dateList.Add(grp);
                    }
                }

                fs.Close();
            }

            return dateList;
        }
    }
}