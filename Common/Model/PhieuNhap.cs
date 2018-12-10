﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

[Table("PhieuNhap")]
public partial class PhieuNhap
{
    [Key]
    public int SoPhieuNhap { get; set; }


    [Column("Name", TypeName = "date")]
    public DateTime NgayNhap { get; set; }

    public int MaNhanVien { get; set; }
    public int MaNhaCungCap { get; set; }
    public decimal TongTien { get; set; }

    [StringLength(200)]
    public string Ghichu { get; set; }
    public bool TrangThai { get; set; }
    public DateTime NgayChinhSua { get; set; }






}