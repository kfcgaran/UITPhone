﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

[Table("ChiTietPhieuDatHang")]
public partial class ChiTietPhieuDatHang
{
    [Key]
    public int SoPhieuDatHang { get; set; }


    public int SoLuong { get; set; }

    [Key]
    public int MaHangHoa { get; set; }
    public decimal Gia { get; set; }
    public decimal ThanhTien { get; set; }
}