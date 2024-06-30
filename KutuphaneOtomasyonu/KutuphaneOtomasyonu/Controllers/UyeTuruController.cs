using Dapper;
using KutuphaneOtomasyonu.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace KutuphaneOtomasyonu.Controllers
{
    public class UyeTuruController : Controller
    {
        public IActionResult Listele()
        {
            using (IDbConnection db = new SqlConnection(@"Server=DESKTOP-OT9BMVU\SQLEXPRESS;Initial Catalog=kütüphane;User ID=sa;Password=123456;"))
            {
                var data = db.Query<UyeTuru>("SELECT * FROM UyeTuru").ToList();
                return View(data);
            }
            
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(UyeTuru model)
        {
            using (IDbConnection db = new SqlConnection(@"Server=DESKTOP-OT9BMVU\SQLEXPRESS;Initial Catalog=kütüphane;User ID=sa;Password=123456;"))
            {

                string sqlQuery = "INSERT INTO UyeTuru (TurIsmı) VALUES(@TurIsmı)";

                int rowsAffected = db.Execute(sqlQuery, model);
                
            }
            return RedirectToAction("Listele");
        }

        [HttpGet]
        public IActionResult Edit(int ID)
        {
            using (IDbConnection db = new SqlConnection(@"Server=DESKTOP-OT9BMVU\SQLEXPRESS;Initial Catalog=kütüphane;User ID=sa;Password=123456;"))
            {
                var data = db.Query<UyeTuru>($"SELECT * FROM UyeTuru where ID = {ID}").FirstOrDefault();
                return View(data);
            }
            
        }

        [HttpPost]
        public IActionResult Edit(UyeTuru Model )
        {

            using (IDbConnection db = new SqlConnection(@"Server=DESKTOP-OT9BMVU\SQLEXPRESS;Initial Catalog=kütüphane;User ID=sa;Password=123456;"))
            {
                string sqlQuery = $"UPDATE UyeTuru set TurIsmı = '{Model.TurIsmı}' where ID = {Model.ID}";

                int rowsAffected = db.Execute(sqlQuery);
                return RedirectToAction("Listele");
            }
        }
        
        public IActionResult Delete(UyeTuru Model)
        {

            using (IDbConnection db = new SqlConnection(@"Server=DESKTOP-OT9BMVU\SQLEXPRESS;Initial Catalog=kütüphane;User ID=sa;Password=123456;"))
            {
               string sqlQuery = $"DELETE FROM UyeTuru  WHERE ID = {Model.ID}";

                int rowsAffected = db.Execute(sqlQuery);
                return RedirectToAction("Listele");
            }
        }
        
    }
}
