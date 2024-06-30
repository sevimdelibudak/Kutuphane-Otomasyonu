using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KutuphaneOtomasyonu.Models
{
    public class Odunc
    {
        public int OduncNo { get; set; }
        public string KimlikNo { get; set; }
        public int ISBN { get; set; }
        public DateTime AlimTarihi { get; set; }
        public DateTime? TeslimTarihi { get; set; }
    }
}
