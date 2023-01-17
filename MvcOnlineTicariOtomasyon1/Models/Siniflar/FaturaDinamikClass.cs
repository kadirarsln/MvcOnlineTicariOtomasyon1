using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon1.Models.Siniflar
{
    public class FaturaDinamikClass
    {
        public IEnumerable<Faturalar> fatura1 { get; set; }
        public IEnumerable<FaturaKalem> fatura2 { get; set; }
    }
}