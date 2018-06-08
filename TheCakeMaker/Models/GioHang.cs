using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheCakeMaker.Models;
namespace TheCakeMaker.Models
{
    public class GioHang
    {
        TheCakeMakerContext db = new TheCakeMakerContext();
        public int iMaSP { get; set; }
        public string sTenSp { get; set; }
        public string sHinhAnh { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double TongTien
        {
            get { return iSoLuong * dDonGia; }
        }

        public GioHang (int MaSP)
        {
            iMaSP = MaSP;
            Cake cake = db.Cakes.Single(n=> n.id == iMaSP);
            sTenSp = cake.name;
            sHinhAnh = cake.image;
            dDonGia = double.Parse(cake.price.ToString());
            iSoLuong = 1;
        }
    } 
}