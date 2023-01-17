using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon1.Models.Siniflar
{
    public class Kategori
    {
        [Key]      //Primary Key
        public int  KategoriID { get; set; }

        [Display(Name = "Kategori Adı")]
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string KategoriAd { get; set; }
        public ICollection<Urun> Uruns { get; set; }      //Her bir kategori de birden fazla ürün yer alabilir
    }
}