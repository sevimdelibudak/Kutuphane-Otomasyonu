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
    public class UyeController : Controller
    {
        public IActionResult UyeListele()
        {
            var uyeViewModelList = new List<UyeViewModel>();
            {
                var siraNo = 0;
                using (IDbConnection db = new SqlConnection(@"Server=DESKTOP-OT9BMVU\SQLEXPRESS;Initial Catalog=kütüphane;User ID=sa;Password=123456;"))
                {
                    var data = db.Query<Uye>("SELECT * FROM Uye").ToList();

                    foreach (var item in data)
                    {
                        siraNo++;
                        UyeViewModel uye = new UyeViewModel();
                        uye.SiraNo = siraNo;
                        uye.AdSoyad = item.AdSoyad;
                        uye.KimlikNo = item.KimlikNo;
                        uye.TurIsmi = UyeTurIsmiGetir(item.TurID);

                        uyeViewModelList.Add(uye);
                    }

                    return View(uyeViewModelList);
                }

            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            var uyeTurleri = UyeTurleri(); 
            var selectedList = new List<SelectListItem>();
            foreach (var item in uyeTurleri)
            {
                SelectListItem selectedItem = new SelectListItem();
                selectedItem.Text = item.TurIsmı;
                selectedItem.Value = item.ID.ToString();

                selectedList.Add(selectedItem);
                //selectedList.Add(new SelectListItem
                //{
                //    Text = item.TurIsmı,
                //    Value = item.ID.ToString()
                //});
            }
            ViewBag.UyeTurleri = selectedList;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Uye model)
        {
            using (IDbConnection db = new SqlConnection(@"Server=DESKTOP-OT9BMVU\SQLEXPRESS;Initial Catalog=kütüphane;User ID=sa;Password=123456;"))
            {

                string sqlQuery = "INSERT INTO Uye (KimlikNo,AdSoyad,TurID) VALUES(@KimlikNo,@AdSoyad,@TurID)";

                int rowsAffected = db.Execute(sqlQuery, model);

            }
            return RedirectToAction("UyeListele");
        }
        [HttpGet]
        public IActionResult Edit(string KimlikNo)
        {
            var uyeTurleri = UyeTurleri();
            var selectedList = new List<SelectListItem>();
            foreach (var item in uyeTurleri)
            {
                SelectListItem selectedItem = new SelectListItem();
                selectedItem.Text = item.TurIsmı;
                selectedItem.Value = item.ID.ToString();

                selectedList.Add(selectedItem);
            }
            ViewBag.UyeTurleri = selectedList;

            using (IDbConnection db = new SqlConnection(@"Server=DESKTOP-OT9BMVU\SQLEXPRESS;Initial Catalog=kütüphane;User ID=sa;Password=123456;"))
            {
                var data = db.Query<Uye>($"SELECT * FROM Uye where KimlikNo = '{KimlikNo}'").FirstOrDefault();
             
                return View(data);
            }

        }

        [HttpPost]
        public IActionResult Edit(Uye Model)
        {

            using (IDbConnection db = new SqlConnection(@"Server=DESKTOP-OT9BMVU\SQLEXPRESS;Initial Catalog=kütüphane;User ID=sa;Password=123456;"))
            {
                string sqlQuery = $"UPDATE Uye set AdSoyad = '{Model.AdSoyad}', TurID={Model.TurID} where KimlikNo = {Model.KimlikNo}";

                int rowsAffected = db.Execute(sqlQuery);
                return RedirectToAction("UyeListele");
            }
   
        }
       

        public IActionResult Details(string KimlikNo)
        {
            
            var oduncViewModelList = new List<OduncViewModel>();
            var siraNo = 0;

            using (IDbConnection db = new SqlConnection(@"Server=DESKTOP-OT9BMVU\SQLEXPRESS;Initial Catalog=kütüphane;User ID=sa;Password=123456;"))
            {
                var uye = db.Query<Uye>($"SELECT * FROM Uye where KimlikNo = '{KimlikNo}'").FirstOrDefault();
                

                var odunc = db.Query<Odunc>($"SELECT * FROM Odunc where KimlikNo = '{KimlikNo}'").ToList();
                foreach (var item in odunc)
                {
                    var oduncViewModel = new OduncViewModel();
                    var yayin = db.Query<Yayin>($"SELECT * FROM Yayin where ISBN = {item.ISBN}").FirstOrDefault();
                    

                    siraNo++;
                    oduncViewModel.ISBN = item.ISBN;
                    oduncViewModel.OduncNo = item.OduncNo;
                    oduncViewModel.Baslik = yayin.Baslik;
                    oduncViewModel.AdSoyad = uye.AdSoyad;
                    oduncViewModel.SiraNo = siraNo;
                    oduncViewModel.KimlikNo = uye.KimlikNo;
                    oduncViewModel.AlimTarihi = item.AlimTarihi;
                    oduncViewModel.TeslimTarihi = item.TeslimTarihi;                  
                    oduncViewModel.Tur = yayin.Tur;
                    oduncViewModelList.Add(oduncViewModel);
                }                         
                return View(oduncViewModelList);
            }
        }
        public IActionResult Delete(string KimlikNo)
        {
            using (IDbConnection db = new SqlConnection(@"Server=DESKTOP-OT9BMVU\SQLEXPRESS;Initial Catalog=kütüphane;User ID=sa;Password=123456;"))
            {
                string sqlQuery = $"Delete from Uye Where KimlikNo={KimlikNo}";

                int rowsAffected = db.Execute(sqlQuery);
                return RedirectToAction("UyeListele");
            }
        }

        public List<UyeTuru> UyeTurleri()
        {
            {
                using (IDbConnection db = new SqlConnection(@"Server=DESKTOP-OT9BMVU\SQLEXPRESS;Initial Catalog=kütüphane;User ID=sa;Password=123456;"))
                {
                    var data = db.Query<UyeTuru>("SELECT * FROM UyeTuru").ToList();
                    return data;
                }

            }
        }

        public string UyeTurIsmiGetir(int ID)
        {
            using (IDbConnection db = new SqlConnection(@"Server=DESKTOP-OT9BMVU\SQLEXPRESS;Initial Catalog=kütüphane;User ID=sa;Password=123456;"))
            {
                var data = db.Query<string>($"SELECT TurIsmı FROM UyeTuru where ID={ID}").FirstOrDefault();
                return data;
            }
        }

    }
}
