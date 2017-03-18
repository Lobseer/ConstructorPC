using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;

using Excel = Microsoft.Office.Interop.Excel;

namespace ConstructorPC.service
{
    public class RepExcel : IDisposable
    {
        public Excel.Application excelapp;
        Excel.Workbooks excelappworkbooks;
        Excel.Workbook excelappworkbook;
        private Excel.Sheets excelsheets; // лист в екселе 
        private Excel.Worksheet excelworksheet; // ячейка 
        private Excel.Range excelcells; // диапазон ячеек

        // Конструктор
        public RepExcel()
        {
            excelapp = new Excel.Application();
            excelapp.Visible = false;
        }

        // Деструктор
        public void Dispose()
        {
            // Release COM objects (very important!)
            if (excelapp != null)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelapp);
            if (excelappworkbooks != null)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelappworkbooks);
            if (excelappworkbook != null)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelappworkbook);
            if (excelsheets != null)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelsheets);
            if (excelworksheet != null)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelworksheet);
            if (excelcells != null)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelcells);

            excelapp = null;
            excelappworkbooks = null;
            excelappworkbook = null;
            excelsheets = null;
            excelworksheet = null;
            excelcells = null;
            GC.Collect();

            ////ClassReportLog.error("RepExcel", "Dispose OK", 3, true);
            //  GC.GetTotalMemory(true);
        }

        /// <summary>
        /// Создание нового документа
        /// </summary>
        /// <param name="firstSheetName">Имя первого лиска в книге</param>
        public void CreateNewBook(string firstSheetName)
        {
            try
            {
                excelapp.SheetsInNewWorkbook = 1;
                excelapp.Workbooks.Add(Type.Missing);

                //Получаем набор ссылок на объекты Workbook (на созданные книги)
                excelappworkbooks = excelapp.Workbooks;

                //Получаем ссылку на книгу 1 - нумерация от 1
                excelappworkbook = excelappworkbooks[1];

                excelsheets = excelappworkbook.Worksheets;
                //Получаем ссылку на лист 1
                excelworksheet = (Excel.Worksheet)excelsheets.get_Item(1);
                excelworksheet.Name = firstSheetName;

                //отключаем подтверждение замены файла
                excelapp.DisplayAlerts = false;
            }
            catch (Exception ex)
            {
                //ClassReportLog.error("CreateNewBook  " + fullPathAndFilename, ex.Message, 3, true);
                excelapp.Quit();
                Dispose();
            }
        }

        /// <summary>
        /// Добавление нового листа в текущую книгу
        /// </summary>
        /// <param name="sheetName">Имя нового листа</param>
        public void AddNewSheet(string sheetName)
        {
            try
            {
                excelsheets.Add(Type.Missing);
                excelworksheet = (Excel.Worksheet)excelsheets.get_Item(1);
                excelworksheet.Name = sheetName;
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Открытие документа
        /// </summary>
        /// <param name="fullPathAndFilename">Полный путь к месту хранения документа</param>
        public void OpenBook(string fullPathAndFilename)
        {
            try
            {
                excelapp.Workbooks.Open(fullPathAndFilename,
                Type.Missing, false, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing);
                //ClassReportLog.info("OpenBook " + fullPathAndFilename, "OK", 3, true);

            }
            catch (Exception ex)
            {
                //ClassReportLog.error("OpenBook", ex.Message, 3, true); 
            }

        }

        /// <summary>
        /// Закрытие докумкента
        /// </summary>
        public void CloseBook()
        {
            try
            {
                excelapp.Workbooks.Close();
                excelapp.Quit();
                //ClassReportLog.error("CloseBook", "OK", 3, true);
            }
            catch (Exception ex)
            {
                //ClassReportLog.error("CloseBook", ex.Message, 3, true);
            }
        }

        /// <summary>
        /// Сохранение документа по заданому пути
        /// </summary>
        /// <param name="fullPathAndFilename">Полный путь к месту хранения документа</param>
        public void SaveAs(string fullPathAndFilename)
        {
            try
            {
                excelappworkbook = excelappworkbooks[1];

                excelappworkbook.SaveAs(
                fullPathAndFilename,                //object Filename
                Excel.XlFileFormat.xlExcel7,        //object FileFormat
                Type.Missing,                       //object Password 
                Type.Missing,                       //object WriteResPassword  
                false,                               //object ReadOnlyRecommended
                Type.Missing,                       //object CreateBackup
                Excel.XlSaveAsAccessMode.xlNoChange,//XlSaveAsAccessMode AccessMode
                Type.Missing,                       //object ConflictResolution
                Type.Missing,                       //object AddToMru 
                Type.Missing,                       //object TextCodepage
                Type.Missing,                       //object TextVisualLayout
                Type.Missing                        //object Local
                );
                //ClassReportLog.info("SaveAs " + fullPathAndFilename, "OK", 3, true);
            }
            catch (Exception ex)
            {
                //ClassReportLog.error("SaveAs " + fullPathAndFilename, ex.Message, 3, true);
                //MessageBox.Show("Возникла проблема при сохранении файла. " + ex.Message); 
            }

        }

        public void ChangeCurrentPage(string pageName)
        {
            try
            {
                excelworksheet = (Excel.Worksheet)excelsheets[pageName];
            }
            catch (Exception ex)
            {
                //ClassReportLog.error("SetValue page - " + pageName + "  address - " + address + " Value - " + StrValues, ex.Message, 3, true);
                // MessageBox.Show("Страница не найдена"); 
                AddNewSheet(pageName);
            }
        }

        public void SetValue(string address, string StrValues, string typeValue, bool isBold = false)
        {
            try
            {
                excelcells = excelworksheet.get_Range(address);
                if (typeValue == "double") excelcells.Value2 = Convert.ToDouble(StrValues, CultureInfo.GetCultureInfo("en-US").NumberFormat); //Convert.ToDouble(StrValues);
                if (typeValue == "string") excelcells.Value2 = StrValues;
                if (isBold) excelcells.EntireRow.Font.Bold = true;
                //ClassReportLog.info("SetValue page - " + pageName + "  address - " + address + " Value - " + StrValues, " OK", 3, true);
            }
            catch (Exception ex)
            {
                //ClassReportLog.error("SetValue page - " + pageName + "  address - " + address + " Value - " + StrValues, ex.Message, 3, true);
            }
        }

        public string GetValue(string address)
        {
            excelcells = excelworksheet.get_Range(address, address);
            return Convert.ToString(excelcells.Value2);
        }

        public void HidenRow(int indexRow)
        {
            excelworksheet.Range[$"A{indexRow}", $"A{indexRow}"].Rows.Hidden = true;
        }

        public void DisplayLine(string pageName, int indexRow)
        {
            excelworksheet.Range[$"A{indexRow}", $"A{indexRow}"].Rows.Hidden = false;
        }
    }
}

