//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLiKhachSan.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class LICHSUTHEMDICHVU
    {
        public System.DateTime ThoiGianThem { get; set; }
        public int SoLuong { get; set; }
        public int MaHoaDon { get; set; }
        public string DichVuID { get; set; }
    
        public virtual DICHVU DICHVU { get; set; }
        public virtual HOADONTHUEPHONG HOADONTHUEPHONG { get; set; }
    }
}
