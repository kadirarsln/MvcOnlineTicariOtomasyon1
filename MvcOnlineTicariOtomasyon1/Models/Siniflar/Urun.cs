using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon1.Models.Siniflar
{
    public class Urun
    {
        [Key]
        public int UrunID { get; set; }

        [Display(Name = "Ürün Adı")]
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string UrunAd { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string Marka { get; set; }
        public short Stok { get; set; }
        public decimal AlısFiyat { get; set; }
        public decimal SatısFiyat { get; set; }
        public bool Durum { get; set; }          //Stok miktarı için kritik seviye 

        [Column(TypeName = "Varchar")]
        [StringLength(130)]
        public string UrunGorsel { get; set; }
        public int Kategoriid { get; set; }
        public virtual Kategori Kategori { get; set; }     // Virtual olarak kullanınca ulaşım sağladık kategori için.
        public ICollection<SatisHareket> SatisHarekets { get; set; }

}
}