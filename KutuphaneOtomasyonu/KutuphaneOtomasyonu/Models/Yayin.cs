using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KutuphaneOtomasyonu.Models
{
    public class Yayin
    {
        public int ISBN { get; set; }
        public string Baslik { get; set; }
        public int RafNo { get; set; }
        public string Tur { get; set; }
    }
}