using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon1.Models.Siniflar
{
    public class Mesajlar
    {
        [Key]      //Primary Key
        public int MesajID { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string Gönderici { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string Alici { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string Konu { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(100)]
        public string İcerik { get; set; }

        [Column(TypeName = "SmallDatetime")]
        
        public DateTime Tarih { get; set; }
    }
}