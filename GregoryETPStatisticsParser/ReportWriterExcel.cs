using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
/* To work eith EPPlus library */
using OfficeOpenXml;

namespace GregoryETPStatisticsParser
{
    internal class ReportWriterExcel
    {
        public FileInfo CreateFile(string fileName)
        {            
            var outputDir = @"C:\Reports\";
            Directory.CreateDirectory(outputDir); // in case it doesn't already exist; no harm done if it does
            var file = new FileInfo(outputDir + fileName);
            return file;
        }

        public void CreateReport()
        {
            var fileName = "TorgiETP_" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss") + ".xlsx";
            var file = CreateFile(fileName);
            using (var package = new ExcelPackage(file))
            { }
        }
    }
}
