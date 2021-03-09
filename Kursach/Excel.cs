using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace Kursach
{
    class ExcelWork
    {
        Excel.Application exselapp;
        public static void ExcelGraph(List<string> dev, List<int> devnumber)
        {
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook workBook;
            Excel.Worksheet workSheet;
            string outputPath = @"D:\graph.xls";
            workBook = excelApp.Workbooks.Add();
            workSheet = (Excel.Worksheet)workBook.Worksheets.get_Item(1);
            for (int i = 0; i < dev.Count; i++)
            {
                workSheet.Cells[i + 1, 1] = dev[i];
                workSheet.Cells[i + 1, 2] = devnumber[i];
            }

            Excel.Range rng = workSheet.Range[$"A{dev.Count + 1}"];
            Excel.Borders border = rng.Borders;
            border.LineStyle = Excel.XlLineStyle.xlContinuous;

            Excel.ChartObjects chartObjs = (Excel.ChartObjects)workSheet.ChartObjects();
            Excel.ChartObject chartObj = chartObjs.Add(100, 5, 600, 400);
            Excel.Chart xlChart = chartObj.Chart;
            Excel.Range rng2 = workSheet.Range[$"A1:A{dev.Count}"];
            Excel.Range rng3 = workSheet.Range[$"A{ dev.Count + 1}:A{ dev.Count}"];

            xlChart.ChartType = Excel.XlChartType.xl3DColumnStacked;

            Excel.SeriesCollection seriesCollection = (Excel.SeriesCollection)xlChart.SeriesCollection(System.Type.Missing);

            Excel.Series series = seriesCollection.NewSeries();
            series.XValues = workSheet.get_Range("A1", $"A{dev.Count}");
            series.Values = workSheet.get_Range("B1", $"B{dev.Count}");

            xlChart.HasTitle = true;
            xlChart.ChartTitle.Text = "Рейтинг жанров";

            excelApp.Visible = true;
            excelApp.UserControl = true;
            try
            {
                workBook.SaveAs(outputPath, Excel.XlFileFormat.xlWorkbookNormal);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            excelApp.Quit();
        }
    }
}
