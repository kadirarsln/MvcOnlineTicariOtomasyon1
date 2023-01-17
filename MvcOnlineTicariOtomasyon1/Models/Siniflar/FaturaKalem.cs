using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon1.Models.Siniflar
{
    public class FaturaKalem
    {
        [Key]
        public int FaturaKalemID { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(130)]
        public string Acıklama { get; set; }
        public int Miktar { get; set; }
        public decimal BrimFiyat { get; set; }
        public decimal Tutar { get; set; }
        public int Faturaid { get; set; }
        public virtual Faturalar Faturalars  { get; set; }

    }
}