using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KutuphaneOtomasyonu.Models
{
    public class UyeViewModel
    {
        [Display(Name ="Sıra No")]
        public int SiraNo { get; set; }
        [Display(Name = "Ad Soyad")]
        public string AdSoyad { get; set; }
        [Display(Name = "Üye Türü")]
        public string TurIsmi { get; set; }
        [Display(Name = "Kimlik Numarası")]
        public string KimlikNo { get; set; }
    }
}
