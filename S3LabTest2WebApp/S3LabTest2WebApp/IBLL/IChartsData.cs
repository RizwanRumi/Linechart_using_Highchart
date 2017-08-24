using DotNet.Highcharts.Options;
using S3LabTest2WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3LabTest2WebApp.IBLL
{
    public interface IChartsData
    {
        Series GenerateData(List<Graph> graphs);
    }
}
