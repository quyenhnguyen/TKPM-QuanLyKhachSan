﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;
namespace QuanLiKhachSan.Model
{
    public abstract class Item
    {
        protected Excel.Application xlApp;
        protected Excel.Workbook xlWorkBook;
        protected Excel.Worksheet xlWorkSheet;
        private object misValue = System.Reflection.Missing.Value;
        protected string dbName;
        protected object type;
        protected List<List<string>> listData;
        public object Type { get => type; set => type = value; }
        public string DbName { get => dbName; set => dbName = value; }
        public List<List<string>> ListData { get => listData; set => listData = value; }
        public void createExcelFile()
        {
            string[] columns = DatabaseQueryTN.getColumnName(this.DbName);
            try
            {
                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = xlWorkBook.Worksheets.get_Item(1);
                xlWorkSheet.Cells.Style.WrapText = true;
                for (int i = 0; i < columns.Length; i++)
                {
                    xlWorkSheet.Columns[i + 1].ColumnWidth = 50;
                    xlWorkSheet.Columns[i + 1].Style.VerticalAlignment =
                          Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    xlWorkSheet.Cells[1, i + 1] = columns[i];
                }
                //xlWorkSheet.Range["A1:D1"].Interior.Color = Excel.XlRgbColor.rgbSkyBlue;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
            }
        }
        public void saveExcelFile()
        {
            //saveExcelData(xlWorkBook, xlWorkSheet);
            // Save and close
            try
            {

                xlWorkBook.SaveAs(System.AppDomain.CurrentDomain.BaseDirectory + "Dictionary.xlsx", Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue,
                    Excel.XlSaveAsAccessMode.xlShared, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close();
            }
            catch (Exception)
            {
            }
            finally
            {
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlApp);
            }
        }
        public void exportExcel()
        {
            for (int i = 0; i < this.listData.Count; i++)
            {
                List<string> items = this.listData[i];
                for (int j = 0; j < items.Count; j++)
                {
                    this.xlWorkSheet.Cells[i + 2, j + 1] = items[j];
                }
            }
        }
        public abstract void Accept(IAnimalVisitor visitor);
        public abstract void getListData();
    }
    public class LoaiDichVu : Item
    {
        public LoaiDichVu()
        {
            this.DbName = "LOAIDV";
        }
        public void Bark()
        {
            Console.WriteLine("Dog barking");
        }
        public override void Accept(IAnimalVisitor visitor)
        {
            visitor.Visit(this);
        }
        public override void getListData()
        {
            listData = null;
            listData = new List<List<string>>();
            List<LOAIDV> dsLoaiDV = DatabaseQueryTN.danhsachAllLoaDV();
            foreach (LOAIDV item in dsLoaiDV)
            {
                List<string> temp = new List<string>();
                temp.Add(item.LoaiDVID);
                temp.Add(item.NgayTao.ToString());
                temp.Add(item.TinhTrang.ToString());
                temp.Add(item.TenLoai);
                this.listData.Add(temp);
            }
        }
    }
    public class DichVu : Item
    {
        private DICHVU dv;
        public DICHVU Dv { get => dv; set => dv = value; }
        public void Meow()
        {
            Console.WriteLine("Cat meowing");
        }
        public override void Accept(IAnimalVisitor visitor)
        {
            visitor.Visit(this);
        }
        public override void getListData()
        {

        }
    }
    public interface IAnimalVisitor
    {
        void Visit(LoaiDichVu animal);
        void Visit(DichVu animal);
    }
    public class StartExcelExport : IAnimalVisitor
    {
        // Visitor
        // Template
        public void Visit(LoaiDichVu animal)
        {
            animal.createExcelFile();
            animal.getListData();
            animal.exportExcel();
            animal.saveExcelFile();
        }
        public void Visit(DichVu animal)
        {

        }
    }
    public class ExcelService
    {
        public ExcelService()
        {
        }
        public static void exportDichVu()
        {

        }
    }
    // Factory Method
    public interface IModelName
    {
        void exportExcel();
    }
    class LoaiDVModel : IModelName
    {
        public void exportExcel()
        {
            var animal = new LoaiDichVu();
            var visitor = new StartExcelExport();
            visitor.Visit(animal);
        }
    }
    class DichVuModel : IModelName
    {
        public void exportExcel()
        {

        }
    }
    abstract class ModelFactory
    {
        public abstract IModelName Factory(string ModelType);
    }
    class ConcreteModelFactory : ModelFactory
    {
        public override IModelName Factory(string ModelType)
        {
            switch (ModelType)
            {
                case "LOAIDV":
                    return new LoaiDVModel();
                case "DICHVU":
                    return new DichVuModel();
                default:
                    return null;
            }
        }
    }

}