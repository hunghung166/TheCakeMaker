using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheCakeMaker.Models;


namespace TheCakeMaker.Controllers
{
    public class GioHangController : Controller
    {
        TheCakeMakerContext db = new TheCakeMakerContext();
        // GET: GioHang
        public List<GioHang> LayGioHang()
        {
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;
            if(listGioHang == null)
            {
                listGioHang = new List<GioHang>();
                Session["GioHang"] = listGioHang;
            }
            return listGioHang;
        }

        public ActionResult ThemGioHang(int iMaSP, string strUrl)
        {
            Cake cake = db.Cakes.SingleOrDefault(n => n.id == iMaSP);
            if(cake == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            List<GioHang> listGioHang = LayGioHang();
            GioHang sanpham = listGioHang.Find(n => n.iMaSP == iMaSP);
            if (sanpham == null)
            {
                sanpham = new GioHang(iMaSP);
                listGioHang.Add(sanpham);
                return Redirect(strUrl);
            }
            else
            {
                sanpham.iSoLuong++;
                return Redirect(strUrl);
            }
        }
        
        public ActionResult CapNhatGioHang(int iMaSP, FormCollection f)
        {
            Cake cake = db.Cakes.SingleOrDefault(n => n.TypeId == iMaSP);
            if(cake == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> listGioHang = LayGioHang();
            GioHang sanpham = listGioHang.SingleOrDefault(n=>n.iMaSP==iMaSP);
            if(sanpham != null)
            {
                sanpham.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return View("GioHang");
        }

        public ActionResult XoaGioHang(int iMaSP)
        {
            Cake cake = db.Cakes.SingleOrDefault(n=>n.TypeId == iMaSP);
            if (cake == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> listGioHang = LayGioHang();
            GioHang sanpham = listGioHang.SingleOrDefault(n=>n.iMaSP==iMaSP);
            if (sanpham != null)
            {
                listGioHang.RemoveAll(n => n.iMaSP == iMaSP);
            }
            if(listGioHang.Count == 0)
            {
                return RedirectToAction("TrangChu","Home");
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult Giohang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("TrangChu", "Home");
            }
            List<GioHang> listGioHang = LayGioHang();
            return View(listGioHang);
        }

        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;
            if (listGioHang != null)
            {
                iTongSoLuong = listGioHang.Sum(n=>n.iSoLuong);
            }
            return iTongSoLuong;
        }

        private double TongTien()
        {
            double dTongTien = 0;
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;
            if(listGioHang!= null)
            {
                dTongTien = listGioHang.Sum(n=>n.TongTien);
            }
            return dTongTien;
        }

        public ActionResult GiohangPartial()
        {
            if (TongSoLuong() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }

        [HttpPost]
        public ActionResult DatHang()
        {

            return View();
        }
    }
}