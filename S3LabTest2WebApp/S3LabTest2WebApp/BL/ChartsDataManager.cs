using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using S3LabTest2WebApp.IBLL;
using S3LabTest2WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace S3LabTest2WebApp.BL
{
    public class ChartsDataManager : IChartsData
    {
        public Series GenerateData(List<Graph> graphs)
        {

            var series = new Series();
            series.Name = "Data Point";
                       
            int totalData = graphs.Count;

            Point[] seriesPoints = new Point[totalData];            



            for (int i = 0; i < totalData; i++)
            {
                double value = Convert.ToDouble(graphs[i].Value);
                string time = graphs[i].Time.ToString();

                if (CheckValueForSelectedTime(time, value) == true)
                {
                    seriesPoints[i] = new Point() { Marker = new PlotOptionsSeriesMarker { Symbol = "diamond" }, Y = Convert.ToDouble(graphs[i].Value) };
                }
                else
                {
                    seriesPoints[i] = new Point() { Marker = new PlotOptionsSeriesMarker { Symbol = "url(../Content/images/sun.png)" }, Y = Convert.ToDouble(graphs[i].Value) };
                }
               
            }

            series.Data = new Data(seriesPoints);

            return series;
        }

        private bool CheckValueForSelectedTime(string gtime, double val)
        {
            string getTime = gtime;
            double getVal = val;

            string[] str = getTime.Split(new char[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            int analogTime = Convert.ToInt32(str[0]);
            string format = str[2];

            int convTime = getDigitalTime(format, analogTime);

            bool result = GetResult(convTime, getVal);

            return result;
        }

        private bool GetResult(int digitalTime, double getVal)
        {
            bool res = false;
            if (digitalTime >= 0 && digitalTime <= 7)
            {
                if (getVal == 500.0)
                {
                    res = true;
                }
                else
                {
                    res = false;
                }
            }
            else if (digitalTime >= 8 && digitalTime <= 17)
            {
                if (getVal == 1500.0)
                {
                    res = true;
                }
                else
                {
                    res = false;
                }
            }
            else if (digitalTime >= 18 && digitalTime <= 23)
            {
                if (getVal == 500.0)
                {
                    res = true;
                }
                else
                {
                    res = false;
                }
            }

            return res;
        }

        private int getDigitalTime(string tFormat, int getTime)
        {
            if (tFormat == "AM")
            {
                if (getTime == 12)
                {
                    getTime = 0;
                }
            }
            else
            {
                if (getTime < 12)
                {
                    getTime += 12;
                }
            }

            return getTime;
        }
    }
}