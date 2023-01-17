using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon1.Models.Siniflar
{
    public class Personel
    {
        [Key]
        public int PersonelID { get; set; }

        [Display(Name = "Personel Adı")]            // Labelların düzenlenmesi için kullanıyoruz.
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        [Required(ErrorMessage = "Bu alanı boş geçemezsiniz!")]
        public string PersonelAd { get; set; }

        [Display(Name = "Personel Soyadı")]
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        [Required(ErrorMessage = "Bu alanı boş geçemezsiniz!")]
        public string PersonelSoyad { get; set; }

        [Display(Name = "Personel Görsel")]
        [Column(TypeName = "Varchar")]
        [StringLength(250)]
        public string PersonelGorsel { get; set; }

        public int Departmanid { get; set; }
        public virtual Departman Departman { get; set; }
        public ICollection<SatisHareket> SatisHarekets { get; set; }
    }
}