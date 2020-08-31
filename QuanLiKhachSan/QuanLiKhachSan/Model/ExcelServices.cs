using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using QuanLiKhachSan.View;
using QuanLiKhachSan.ViewModel;
using System;
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


        private string exportFileName;
        private bool isSave;
        private List<string> privelegeColumn = new List<string>() {"PHONGs", "DICHVUs", "LICHSUTHEMDICHVUs", "LOAIDV", "HOADONTHUEPHONGs", "LOAINHANVIEN", "NVKETOAN", "NVLETAN", "NVQUANLI","LOAIPHONG" };
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
        protected string ExportFileName { get => exportFileName; set => exportFileName = value; }
        protected bool IsSave { get => isSave; set => isSave = value; }

        // Export
        public void startExport()
        {
            try
            {
                this.createExcelFile();
                if (!isSave)
                {
                    return;
                }
                this.getListData();
                this.exportExcel();
                this.saveExcelFile();
                MessageBox.Show("Export Finish");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                SecurityModel.Log(e.ToString());
            }
        }
        protected void createExcelFile()
        {

            SaveFileDialog openFileDialog = new SaveFileDialog();
            openFileDialog.Filter = "Excel Files|*.xlsx;*.xlsm;*.xlsb;*.xls";
            if (openFileDialog.ShowDialog() == true)
            {
                this.ExportFileName = openFileDialog.FileName;
                isSave = true;
            }
            else
            {
                isSave = false;
                return;
            }
            string[] columns = DatabaseQueryTN.getColumnName(this.DbName);
            try
            {
                int len = 0;
                xlApp = new Excel.Application();
                xlApp.Visible = false;
                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = xlWorkBook.Worksheets.get_Item(1);
                xlWorkSheet.Cells.Style.WrapText = true;
                for (int i = 0; i < columns.Length; i++)
                {
                    if (!privelegeColumn.Contains(columns[i]))
                    {
                        len++;
                        xlWorkSheet.Columns[i + 1].Style.VerticalAlignment =
                              Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        xlWorkSheet.Cells[1, i + 1] = columns[i];

                    }
                }
                xlWorkSheet.Range[xlWorkSheet.Cells[1, 1], xlWorkSheet.Cells[1, len]].Interior.Color = Excel.XlRgbColor.rgbSkyBlue;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
            }
        }
        protected void closeExcel()
        {
            try
            {
                xlWorkBook.Close();
                xlApp.Quit();
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlApp);
            }
            catch (Exception)
            {

            }
            finally
            {
            }
        }
        protected void saveExcelFile()
        {
            //saveExcelData(xlWorkBook, xlWorkSheet);
            // Save and close
            try
            {
                // r chua
                if (isSave)
                {
                    xlWorkBook.SaveAs(ExportFileName, Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue,
                            Excel.XlSaveAsAccessMode.xlShared, misValue, misValue, misValue, misValue, misValue);
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                closeExcel();
            }
        }
        protected void exportExcel()
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
        protected abstract void getListData();

        // Import
        public void startImport()
        {
            openFile();
        }
        protected void openFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xlsx;*.xlsm;*.xlsb;*.xls";
            if (openFileDialog.ShowDialog() == true)
            {
                ExportFileName = openFileDialog.FileName;
                isSave = true;
            }
            else
            {
                isSave = false;
                return;
            }
            try
            {
                //System.AppDomain.CurrentDomain.BaseDirectory + "Dictionary.xlsx"
                xlApp = new Excel.Application();
                xlApp.Visible = false;
                // chỗ này t mún là open popup chọn file, lấy tên file, tạm gác
                xlWorkBook = xlApp.Workbooks.Open(ExportFileName);
                xlWorkSheet = xlWorkBook.Worksheets.get_Item(1);
                importFromExcel();
            }
            catch (Exception e)
            {
                MessageBox.Show("Kết thúc");
                SecurityModel.Log(e.ToString());
            }
            finally
            {
                closeExcel();
                MessageBox.Show("Hoàn thành");
            }
        }
        protected abstract void importFromExcel();

        //
        public abstract void Accept(IModelVisitor visitor);

    }
    public class LoaiDichVu : Item
    {
        public LoaiDichVu()
        {
            this.DbName = "LOAIDV";
        }
        public override void Accept(IModelVisitor visitor)
        {
            visitor.exportToExcel(this);
        }
        protected override void getListData()
        {
            listData = null;
            listData = new List<List<string>>();
            List<LOAIDV> dsLoaiDV = DatabaseQueryTN.danhsachAllLoaDV();
            foreach (LOAIDV item in dsLoaiDV)
            {
                List<string> temp = new List<string>();
                temp.Add(item.LoaiDVID);
                temp.Add(item.TenLoai);
                temp.Add(item.TinhTrang.ToString());
                temp.Add(item.NgayTao.ToString());
                this.listData.Add(temp);
            }
        }
        protected override void importFromExcel()
        {
            int realRow = xlWorkSheet.Cells.Find("*", System.Reflection.Missing.Value,
                               System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                               Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlPrevious,
                               false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Row;
            int type = -1;
            bool remember = false;
            for (int row = 2; row < realRow; row++)
            {
                LOAIDV newLP = new LOAIDV();
                newLP.LoaiDVID = (((Range)xlWorkSheet.Cells[row, 1]).Value2).ToString();
                newLP.TenLoai = (((Range)xlWorkSheet.Cells[row, 2]).Value2).ToString();
                newLP.TinhTrang = bool.Parse((((Range)xlWorkSheet.Cells[row, 3]).Value2).ToString());
                double dt = ((Range)xlWorkSheet.Cells[row, 4]).Value2;
                newLP.NgayTao = Convert.ToDateTime(DateTime.FromOADate(dt).ToString("MM/dd/yyyy hh:mm"));

                if (DatabaseQueryTN.kiemTraTonTaiLoaiDV(newLP.LoaiDVID))
                {
                    if (!remember)
                    {
                        PopUpImportExcel wd = new PopUpImportExcel();
                        if (wd.ShowDialog() == true)
                        {
                            type = ((PopUpImportExcelViewModel)wd.DataContext).index;
                        }
                        else
                        {
                            MessageBox.Show("Đã hủy");
                            return;
                        }

                    }
                    if (type == 0)
                    {
                        remember = true;
                    }
                    if (type == 1)
                    {
                        remember = false;
                    }
                    if (type == 2)
                    {
                        continue;
                    }
                    DatabaseQueryTN.capNhatLoaiDV(newLP);
                    if (newLP.TinhTrang==true)
                    {
                        DatabaseQueryTN.updateInsertLoaiDV(newLP);
                    }
                }
                else
                {
                    DatabaseQueryTN.themMoiLoaiDV(newLP);
                }
            }
        }
    }
    public class DichVu : Item
    {
        public DichVu()
        {
            this.DbName = "DICHVU";
        }
        public override void Accept(IModelVisitor visitor)
        {
            visitor.exportToExcel(this);
        }
        protected override void getListData()
        {
            listData = null;
            listData = new List<List<string>>();
            List<DICHVU> dsLoaiDV = DatabaseQueryTN.danhsachAllDV();
            foreach (DICHVU item in dsLoaiDV)
            {
                List<string> temp = new List<string>();
                temp.Add(item.DichVuID);
                temp.Add(item.TenDichVu.ToString());
                temp.Add(item.GiaBan.ToString());
                temp.Add(item.GiaCungCap.ToString());
                temp.Add(item.TinhTrangTonTai.ToString());
                temp.Add(item.LoaiDVID);
                temp.Add(item.DonVi);
                temp.Add(item.NgayTao.ToString());
                temp.Add(item.HinhAnh.ToString());
                this.listData.Add(temp);
            }
        }
        protected override void importFromExcel()
        {
            int realRow = xlWorkSheet.Cells.Find("*", System.Reflection.Missing.Value,
                               System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                               Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlPrevious,
                               false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Row;
            int type = -1;
            bool remember = false;
            for (int row = 2; row < realRow; row++)
            {
                DICHVU newLP = new DICHVU();
                newLP.DichVuID = (((Range)xlWorkSheet.Cells[row, 1]).Value2).ToString();
                newLP.TenDichVu = (((Range)xlWorkSheet.Cells[row, 2]).Value2).ToString();
                newLP.GiaBan = float.Parse((((Range)xlWorkSheet.Cells[row, 3]).Value2).ToString());
                newLP.GiaCungCap = float.Parse((((Range)xlWorkSheet.Cells[row, 4]).Value2).ToString());
                newLP.TinhTrangTonTai = bool.Parse((((Range)xlWorkSheet.Cells[row, 5]).Value2).ToString());
                newLP.LoaiDVID = (((Range)xlWorkSheet.Cells[row, 6]).Value2).ToString();
                newLP.DonVi = (((Range)xlWorkSheet.Cells[row, 7]).Value2).ToString();
                double dt = ((Range)xlWorkSheet.Cells[row, 8]).Value2;
                newLP.NgayTao = Convert.ToDateTime(DateTime.FromOADate(dt).ToString("MM/dd/yyyy hh:mm"));
                newLP.HinhAnh = (((Range)xlWorkSheet.Cells[row, 6]).Value2).ToString();
                if (DatabaseQueryTN.kiemTraTonTaiLoaiDV(newLP.DichVuID))
                {
                    if (!remember)
                    {
                        PopUpImportExcel wd = new PopUpImportExcel();
                        if (wd.ShowDialog() == true)
                        {
                            type = ((PopUpImportExcelViewModel)wd.DataContext).index;
                        }
                        else
                        {
                            MessageBox.Show("Đã hủy");
                            return;
                        }

                    }
                    if (type == 0)
                    {
                        remember = true;
                    }
                    if (type == 1)
                    {
                        remember = false;
                    }
                    if (type == 2)
                    {
                        continue;
                    }
                    DatabaseQueryTN.capNhatDV(newLP);
                }
                else
                {
                    DatabaseQueryTN.themMoiDichVu(newLP);
                }
            }
        }
    }
    public class NhanVien : Item
    {
        public NhanVien()
        {
            this.DbName = "NHANVIEN";
        }
        public override void Accept(IModelVisitor visitor)
        {
            visitor.exportToExcel(this);
        }
        protected override void getListData()
        {
            listData = null;
            listData = new List<List<string>>();
            List<NHANVIEN> dsLoaiDV = DatabaseQueryTN.danhsachAllNhanVien();
            foreach (NHANVIEN item in dsLoaiDV)
            {
                List<string> temp = new List<string>();
                temp.Add(item.NhanVienID.ToString());
                temp.Add(item.TenDangNhap);
                temp.Add(item.MatKhau);
                temp.Add(item.TinhTrang.ToString());
                temp.Add(item.HoTen);
                temp.Add(item.DiaChi);
                temp.Add(item.NgaySinh.ToString());
                temp.Add(item.SDT.ToString());
                temp.Add(item.CMND.ToString());
                temp.Add(item.Email);
                temp.Add(item.Loai.ToString());
                temp.Add(item.NgayTao.ToString());
                temp.Add(Convert.ToBase64String(item.AnhDaiDien).ToString());
                this.listData.Add(temp);
            }
        }
        protected override void importFromExcel()
        {
            int realRow = xlWorkSheet.Cells.Find("*", System.Reflection.Missing.Value,
                                  System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                                  Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlPrevious,
                                  false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Row;
            int type = -1;
            bool remember = false;
            for (int row = 2; row < realRow; row++)
            {
                try
                {

                    NHANVIEN newLP = new NHANVIEN();
                    newLP.NhanVienID = int.Parse((((Range)xlWorkSheet.Cells[row, 1]).Value2).ToString());
                    newLP.TenDangNhap = (((Range)xlWorkSheet.Cells[row, 2]).Value2).ToString();
                    newLP.MatKhau = (((Range)xlWorkSheet.Cells[row, 3]).Value2).ToString();
                    newLP.TinhTrang = bool.Parse((((Range)xlWorkSheet.Cells[row, 4]).Value2).ToString());
                    newLP.HoTen = (((Range)xlWorkSheet.Cells[row, 5]).Value2).ToString();
                    newLP.DiaChi = (((Range)xlWorkSheet.Cells[row, 6]).Value2).ToString();
                    double dt = ((Range)xlWorkSheet.Cells[row, 7]).Value2;
                    newLP.NgaySinh = Convert.ToDateTime(DateTime.FromOADate(dt).ToString("MM/dd/yyyy hh:mm"));

                    newLP.SDT = int.Parse((((Range)xlWorkSheet.Cells[row, 8]).Value2).ToString());
                    newLP.CMND = int.Parse((((Range)xlWorkSheet.Cells[row, 9]).Value2).ToString());
                    newLP.Email = (((Range)xlWorkSheet.Cells[row, 10]).Value2).ToString();
                    newLP.Loai = int.Parse((((Range)xlWorkSheet.Cells[row, 11]).Value2).ToString());
                    double dt2 = ((Range)xlWorkSheet.Cells[row, 12]).Value2;
                    newLP.NgayTao = Convert.ToDateTime(DateTime.FromOADate(dt2).ToString("MM/dd/yyyy hh:mm"));
                    newLP.AnhDaiDien = Convert.FromBase64String((((Range)xlWorkSheet.Cells[row, 13]).Value2).ToString());
                    if (DatabaseQueryTN.kiemtraTonTai(newLP.TenDangNhap, newLP.Email))
                    {
                        if (!remember)
                        {
                            PopUpImportExcel wd = new PopUpImportExcel();
                            if (wd.ShowDialog() == true)
                            {
                                type = ((PopUpImportExcelViewModel)wd.DataContext).index;
                            }
                            else
                            {
                                MessageBox.Show("Đã hủy");
                                return;
                            }

                        }
                        if (type == 0)
                        {
                            remember = true;
                        }
                        if (type == 1)
                        {
                            remember = false;
                        }
                        if (type == 2)
                        {
                            continue;
                        }
                        DatabaseQueryTN.capNhatNhanVien(newLP);
                    }
                    else
                    {
                        DatabaseQueryTN.themNhanVienMoi(newLP);
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
    }
    public class LoaiPhong : Item
    {
        public LoaiPhong()
        {
            this.DbName = "LOAIPHONG";
        }
        public override void Accept(IModelVisitor visitor)
        {
            visitor.exportToExcel(this);
        }
        protected override void getListData()
        {
            listData = null;
            listData = new List<List<string>>();
            List<LOAIPHONG> dsLoaiDV = DatabaseQueryTN.danhsachAllLoaiPhong();
            foreach (LOAIPHONG item in dsLoaiDV)
            {
                List<string> temp = new List<string>();
                temp.Add(item.LoaiPhongID);
                temp.Add(item.TenLoai);
                temp.Add(item.TinhTrang.ToString());
                temp.Add(item.NgayTao.ToString());
                this.listData.Add(temp);
            }
        }
        protected override void importFromExcel()
        {
            int realRow = xlWorkSheet.Cells.Find("*", System.Reflection.Missing.Value,
                                  System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                                  Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlPrevious,
                                  false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Row;
            int type = -1;
            bool remember = false;
            for (int row = 2; row < realRow; row++)
            {
                LOAIPHONG newLP = new LOAIPHONG();
                newLP.LoaiPhongID = (((Range)xlWorkSheet.Cells[row, 1]).Value2).ToString();
                newLP.TenLoai = (((Range)xlWorkSheet.Cells[row, 2]).Value2).ToString();
                newLP.TinhTrang = bool.Parse((((Range)xlWorkSheet.Cells[row, 3]).Value2).ToString());
                double dt2 = ((Range)xlWorkSheet.Cells[row, 4]).Value2;
                newLP.NgayTao = Convert.ToDateTime(DateTime.FromOADate(dt2).ToString("MM/dd/yyyy hh:mm"));

                if (DatabaseQueryTN.kiemTraTonTaiLoaiPhong(newLP.LoaiPhongID))
                {
                    if (DatabaseQueryTN.isUsedLoaiPhong(newLP.LoaiPhongID))
                    {
                        MessageBox.Show("Phòng đã thuê, không thể cập nhật");
                        continue;
                    }
                    if (!remember)
                    {
                        PopUpImportExcel wd = new PopUpImportExcel();
                        if (wd.ShowDialog() == true)
                        {
                            type = ((PopUpImportExcelViewModel)wd.DataContext).index;
                        }
                        else
                        {
                            MessageBox.Show("Đã hủy");
                            return;
                        }

                    }
                    if (type == 0)
                    {
                        remember = true;
                    }
                    if (type == 1)
                    {
                        remember = false;
                    }
                    if (type == 2)
                    {
                        continue;
                    }
                    DatabaseQueryTN.capNhatLoaiPhong(newLP);
                    if (newLP.TinhTrang == true)
                    {
                        DatabaseQueryTN.updateInsertLoaiPhong(newLP);
                    }
                }
                else
                {
                    DatabaseQueryTN.themMoiLoaiPhong(newLP);
                }
            }
        }
    }
    public class Phong : Item
    {
        public Phong()
        {
            this.DbName = "PHONG";
        }
        public override void Accept(IModelVisitor visitor)
        {
            visitor.exportToExcel(this);
        }
        protected override void getListData()
        {
            listData = null;
            listData = new List<List<string>>();
            List<PHONG> dsLoaiDV = DatabaseQueryTN.danhsachAllPhong();
            foreach (PHONG item in dsLoaiDV)
            {
                List<string> temp = new List<string>();
                temp.Add(item.PhongID);
                temp.Add(item.TenPhong);
                temp.Add(item.DonGia.ToString());
                temp.Add(item.TinhTrangTonTai.ToString());
                temp.Add(item.TinhTrangThue.ToString());
                temp.Add(item.LoaiPhongID);
                temp.Add(item.NgayTao.ToString());
                this.listData.Add(temp);
            }
        }
        protected override void importFromExcel()
        {
            int realRow = xlWorkSheet.Cells.Find("*", System.Reflection.Missing.Value,
                                  System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                                  Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlPrevious,
                                  false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Row;
            int type = -1;
            bool remember = false;
            for (int row = 2; row < realRow; row++)
            {
                PHONG newLP = new PHONG();
                newLP.PhongID = (((Range)xlWorkSheet.Cells[row, 1]).Value2).ToString();
                newLP.TenPhong = (((Range)xlWorkSheet.Cells[row, 2]).Value2).ToString();
                newLP.DonGia = float.Parse((((Range)xlWorkSheet.Cells[row, 3]).Value2).ToString());
                newLP.TinhTrangTonTai = bool.Parse((((Range)xlWorkSheet.Cells[row, 4]).Value2).ToString());
                newLP.TinhTrangThue = bool.Parse((((Range)xlWorkSheet.Cells[row, 5]).Value2).ToString());
                newLP.LoaiPhongID = (((Range)xlWorkSheet.Cells[row, 6]).Value2).ToString();
                double dt2 = ((Range)xlWorkSheet.Cells[row, 7]).Value2;
                newLP.NgayTao = Convert.ToDateTime(DateTime.FromOADate(dt2).ToString("MM/dd/yyyy hh:mm"));


                if (DatabaseQueryTN.kiemTraTonTaiPhong(newLP.PhongID))
                {
                    if (DatabaseQueryTN.isUsedPhong(newLP.PhongID))
                    {
                        MessageBox.Show("Phòng đã thuê, không thể cập nhật");
                        continue;
                    }
                    if (!remember)
                    {
                        PopUpImportExcel wd = new PopUpImportExcel();
                        if (wd.ShowDialog() == true)
                        {
                            type = ((PopUpImportExcelViewModel)wd.DataContext).index;
                        }
                        else
                        {
                            MessageBox.Show("Đã hủy");
                            return;
                        }

                    }
                    if (type == 0)
                    {
                        remember = true;
                    }
                    if (type == 1)
                    {
                        remember = false;
                    }
                    if (type == 2)
                    {
                        continue;
                    }
                    DatabaseQueryTN.capNhatPhong(newLP);
                }
                else
                {
                    DatabaseQueryTN.themMoiPhong(newLP);
                }
            }
        }
    }







    public interface IModelVisitor
    {
        // Export
        void exportToExcel(LoaiDichVu context);
        void exportToExcel(DichVu context);
        void exportToExcel(NhanVien context);
        void exportToExcel(LoaiPhong context);
        void exportToExcel(Phong context);
        // Import
        void importToExcel(LoaiDichVu context);
        void importToExcel(DichVu context);
        void importToExcel(NhanVien context);
        void importToExcel(LoaiPhong context);
        void importToExcel(Phong context);
    }
    public class StartExcelExport : IModelVisitor
    {
        // Visitor
        // Template
        public void exportToExcel(LoaiDichVu context)
        {
            context.startExport();
        }
        public void exportToExcel(DichVu context)
        {
            context.startExport();
        }
        public void exportToExcel(NhanVien context)
        {
            context.startExport();
        }
        public void exportToExcel(LoaiPhong context)
        {
            context.startExport();
        }
        public void exportToExcel(Phong context)
        {
            context.startExport();
        }

        //Import
        public void importToExcel(LoaiDichVu context)
        {
            context.startImport();
        }
        public void importToExcel(DichVu context)
        {
            context.startImport();
        }
        public void importToExcel(NhanVien context)
        {
            context.startImport();
        }
        public void importToExcel(LoaiPhong context)
        {
            context.startImport();
        }
        public void importToExcel(Phong context)
        {
            context.startImport();
        }
    }

    // Factory Method
    public interface IModelName
    {
        void exportExcel();
        void importExcel();
    }
    class LoaiDVModel : IModelName
    {
        public void exportExcel()
        {
            var context = new LoaiDichVu();
            var visitor = new StartExcelExport();
            visitor.exportToExcel(context);
        }
        public void importExcel()
        {
            var context = new LoaiDichVu();
            var visitor = new StartExcelExport();
            visitor.importToExcel(context);
        }
    }
    class DichVuModel : IModelName
    {
        public void exportExcel()
        {
            var context = new DichVu();
            var visitor = new StartExcelExport();
            visitor.exportToExcel(context);
        }
        public void importExcel()
        {
            var context = new DichVu();
            var visitor = new StartExcelExport();
            visitor.importToExcel(context);
        }
    }
    class NhanVienModel : IModelName
    {
        public void exportExcel()
        {
            var context = new NhanVien();
            var visitor = new StartExcelExport();
            visitor.exportToExcel(context);
        }
        public void importExcel()
        {
            var context = new NhanVien();
            var visitor = new StartExcelExport();
            visitor.importToExcel(context);
        }
    }

    class LoaiPhongModel : IModelName
    {
        public void exportExcel()
        {
            var context = new LoaiPhong();
            var visitor = new StartExcelExport();
            visitor.exportToExcel(context);
        }
        public void importExcel()
        {
            var context = new LoaiPhong();
            var visitor = new StartExcelExport();
            visitor.importToExcel(context);
        }
    }
    class PhongModel : IModelName
    {
        public void exportExcel()
        {
            var context = new Phong();
            var visitor = new StartExcelExport();
            visitor.exportToExcel(context);
        }
        public void importExcel()
        {
            var context = new Phong();
            var visitor = new StartExcelExport();
            visitor.importToExcel(context);
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
                case "NHANVIEN":
                    return new NhanVienModel();
                case "LOAIPHONG":
                    return new LoaiPhongModel();
                case "PHONG":
                    return new PhongModel();
                default:
                    return null;
            }
        }
    }

}