using System;
using Excel = Microsoft.Office.Interop.Excel;

namespace RestSampleA16
{
    public static class ExcelUtil
    {

        public static string ReadData(int myRow, int myColumn)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;

            string str = null;
            int rCnt = 0;
            int cCnt = 0;

            xlApp = new Excel.Application();
            string excelFileLocation = "c:/Groupon.xlsx"; //Drive from Config
            xlWorkBook = xlApp.Workbooks.Open(excelFileLocation, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(2);

            range = xlWorkSheet.UsedRange;

            //range.Rows.Count

            for (rCnt = myRow; rCnt <= myRow ; rCnt++)
            {
                for (cCnt = myColumn; cCnt <= myColumn; cCnt++)
                {
                    str = Convert.ToString((range.Cells[rCnt, cCnt] as Excel.Range).Value2);
                    
                }
            }

            xlWorkBook.Close(true, null, null);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);

            return str;
        }

        private static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                Console.Write(ex.Message.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

    }
}