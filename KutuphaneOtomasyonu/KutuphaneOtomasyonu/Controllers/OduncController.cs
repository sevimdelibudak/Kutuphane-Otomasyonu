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
    public class OduncController : Controller
    {
        public IActionResult Index()
        {
            using (IDbConnection db = new SqlConnection(@"Server=DESKTOP-OT9BMVU\SQLEXPRESS;Initial Catalog=kütüphane;User ID=sa;Password=123456;"))
            {
                var data = db.Query<Odunc>("SELECT * FROM Odunc").ToList();
                return View(data);
            }
        }

        
      
    }
    
}
