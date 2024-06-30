using Dapper;
using KutuphaneOtomasyonu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace KutuphaneOtomasyonu.Controllers
{
    public class YayinController : Controller
    {
        public IActionResult Index2()
        {
            using (IDbConnection db = new SqlConnection(@"Server=DESKTOP-OT9BMVU\SQLEXPRESS;Initial Catalog=kütüphane;User ID=sa;Password=123456;"))
            {
                var data = db.Query<Yayin>("SELECT * FROM Yayin").ToList();
                return View(data);
            }
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Yayin model)
        {
            using (IDbConnection db = new SqlConnection(@"Server=DESKTOP-OT9BMVU\SQLEXPRESS;Initial Catalog=kütüphane;User ID=sa;Password=123456;"))
            {

                string sqlQuery = "INSERT INTO Yayin (ISBN,Baslik,RafNo,Tur) VALUES(@ISBN,@Baslik,@RafNo,@Tur)";

                int rowsAffected = db.Execute(sqlQuery, model);

            }
            return RedirectToAction("Index2");
        }
        [HttpGet]
        public IActionResult Edit2(int RafNo)
        {
            using (IDbConnection db = new SqlConnection(@"Server=DESKTOP-OT9BMVU\SQLEXPRESS;Initial Catalog=kütüphane;User ID=sa;Password=123456;"))
            {
                var data = db.Query<Yayin>($"SELECT * FROM Yayin where RafNo = {RafNo}").FirstOrDefault();
                return View(data);
            }

        }

        [HttpPost]
        public IActionResult Edit(Yayin Model)
        {

            using (IDbConnection db = new SqlConnection(@"Server=DESKTOP-OT9BMVU\SQLEXPRESS;Initial Catalog=kütüphane;User ID=sa;Password=123456;"))
            {
                string sqlQuery = $"UPDATE Yayin set ISBN = '{Model.ISBN}' where RafNo = {Model.RafNo}";

                int rowsAffected = db.Execute(sqlQuery);
                return RedirectToAction("Index");
            }
        }

        public IActionResult OduncAl(int ISBN)
        {
            var uyeListesi = new List<Uye>();
            using (IDbConnection db = new SqlConnection(@"Server=DESKTOP-OT9BMVU\SQLEXPRESS;Initial Catalog=kütüphane;User ID=sa;Password=123456;"))
            {
                var data = db.Query<Uye>($"SELECT * FROM Uye").ToList();
                var selectedList = new List<SelectListItem>();
                foreach (var item in data)
                {
                    SelectListItem selectedItem = new SelectListItem();
                    selectedItem.Text = item.AdSoyad;
                    selectedItem.Value = item.KimlikNo;
                    selectedList.Add(selectedItem);

                    ViewBag.UyeListesi = selectedList;
                }
            }

            using (IDbConnection db = new SqlConnection(@"Server=DESKTOP-OT9BMVU\SQLEXPRESS;Initial Catalog=kütüphane;User ID=sa;Password=123456;"))
            {
                var data = db.Query<Yayin>($"SELECT * FROM Yayin where ISBN = {ISBN}").FirstOrDefault();
                ViewBag.ISBN = data.ISBN;
                ViewBag.YayinAdi = data.Baslik;
                

            }
            return View();    
        }

        [HttpPost]
        public IActionResult OduncAl(Odunc model)
        {
            model.AlimTarihi = DateTime.Now;
            using (IDbConnection db = new SqlConnection(@"Server=DESKTOP-OT9BMVU\SQLEXPRESS;Initial Catalog=kütüphane;User ID=sa;Password=123456;"))
            {

                string sqlQuery = "INSERT INTO Odunc (KimlikNo,ISBN,AlimTarihi,TeslimTarihi) VALUES(@KimlikNo,@ISBN,@AlimTarihi,@TeslimTarihi)";

                int rowsAffected = db.Execute(sqlQuery, model);
            }

            return RedirectToAction("Index2");
        }
        public IActionResult Delete(int ISBN)
        {
            using (IDbConnection db = new SqlConnection(@"Server=DESKTOP-OT9BMVU\SQLEXPRESS;Initial Catalog=kütüphane;User ID=sa;Password=123456;"))
            {
                string sqlQuery = $"Delete from Yayin Where ISBN={ISBN}";

                int rowsAffected = db.Execute(sqlQuery);
                return RedirectToAction("Index2");
            }
        }
    }
}
