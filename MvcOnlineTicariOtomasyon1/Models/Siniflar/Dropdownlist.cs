using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon1.Models.Siniflar
{
    public class Dropdownlist
    {
        public IEnumerable<SelectListItem> Kategoriler { get; set; }
        public IEnumerable<SelectListItem> Urunler { get; set; }
    }
}