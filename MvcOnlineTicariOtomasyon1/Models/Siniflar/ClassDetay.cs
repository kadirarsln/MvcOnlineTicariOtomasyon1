using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon1.Models.Siniflar
{
    public class ClassDetay
    {
        public IEnumerable<Urun> Urun1 { get; set; }
        public IEnumerable<Detay> Urun2 { get; set; }
    }
}