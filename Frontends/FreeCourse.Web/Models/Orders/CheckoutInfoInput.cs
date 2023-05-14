using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Web.Models.Orders
{
    public class CheckoutInfoInput
    {
        [Display(Name ="İl")]
        public string Province { get; set; }
        [Display(Name = "İlçe")]
        public string District { get; set; }
        [Display(Name = "Cadde")]
        public string Street { get; set; }
        [Display(Name = "Posta Kodu")]
        public string ZipCode { get; set; }
        [Display(Name = "Adres")]
        public string Line { get; set; }
        [Display(Name = "Kart isim soyisim")]
        public string CardName { get; set; }
        [Display(Name = "Kart numarası")]
        public string CardNumber { get; set; }
        [Display(Name = "sonkullanma tarihi(ay/yıl)")]
        public string Expiration { get; set; }
        [Display(Name = "CVV/CVC2 numarası")]
        public string CVV { get; set; }
        [Display(Name = "Adres")]
        public decimal TotalPrice { get; set; }
    }
}
