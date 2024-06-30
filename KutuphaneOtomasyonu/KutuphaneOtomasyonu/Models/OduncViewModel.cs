using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KutuphaneOtomasyonu.Models
{
    public class OduncViewModel
    {
        public int SiraNo { get; set; }
        public string KimlikNo { get; set; }
        public string TurIsmi { get; set; }
        public int OduncNo { get; set; }
        public int ISBN { get; set; }
        public string Tur { get; set; }
        public string Baslik { get; set; }
        public string AdSoyad { get; set; }
        public DateTime AlimTarihi { get; set; }
        public DateTime? TeslimTarihi { get; set; }
        
    }
}
