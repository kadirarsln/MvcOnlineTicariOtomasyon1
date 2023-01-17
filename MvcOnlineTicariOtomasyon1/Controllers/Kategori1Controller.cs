using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon1.Models.Siniflar;
using PagedList;
using PagedList.Mvc;

namespace MvcOnlineTicariOtomasyon1.Controllers
{
    public class Kategori1Controller : Controller
    {
        // GET: Kategori1

        Context context = new Context();                //Context sınıfımıza erişim sağladık.
        public ActionResult Index(int sayfa = 1)
        {
            var kategoriler = context.Kategoris.ToList().ToPagedList(sayfa, 6);        //Listeleme işlemini burada gerçekleştirdik. Paged List ile yaptık.
            return View(kategoriler);
        }

        //Ekleme Kısmı
        [HttpGet]                                       // 2 taenm ekledik form yüklenince üst kısım boş şekilde. ancak butona tıklayınca post olarak çalışır
        public ActionResult KategoriAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KategoriAdd(Kategori kategori)
        {
            context.Kategoris.Add(kategori);            // Kategoris tabloya DB ye ekleme yapar.
            context.SaveChanges();                      //Veri tabanına kaydetme.
            return RedirectToAction("Index");           //Index aksiyonuna yönlendir.
        }

        //Silme Kısmı
        public ActionResult KategoriDelete(int id)
        {
            var delete = context.Kategoris.Find(id);
            context.Kategoris.Remove(delete);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        //Güncelleme Kısmı
        public ActionResult KategoriGet(int id)
        {
            var kategoriGet = context.Kategoris.Find(id);            // KategoriGet döndürüyor. kategoriden gelen değer ile .
            return View("KategoriGet", kategoriGet);
        }

        public ActionResult KategoriUpdate(Kategori kategori)
        {
            var update = context.Kategoris.Find(kategori.KategoriID);       //ID hafızaya aldık.ID ye göre işlem gerçekleştirdik.
            update.KategoriAd = kategori.KategoriAd;                        //Update değerini girdiğimizle değiştiriyoruz.sol taraf atancak değer sağ taraf yeni değer.
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult List()
        {
            Dropdownlist dropdownlist = new Dropdownlist();
            dropdownlist.Kategoriler = new SelectList(context.Kategoris, "KategoriID", "KategoriAd");
            dropdownlist.Urunler = new SelectList(context.Uruns, "UrunID", "UrunAd");
            return View(dropdownlist);

        }

        public JsonResult UrunList(int p)
        {
            var urunlistesi = (from _urun in context.Uruns
                               join _kategori in context.Kategoris
                                   on _urun.Kategori.KategoriID equals _kategori.KategoriID
                               where _urun.Kategori.KategoriID == p
                               select new
                               {
                                   Text = _urun.UrunAd,
                                   Value = _urun.UrunID.ToString()
                               }).ToList();
            return Json(urunlistesi, JsonRequestBehavior.AllowGet);
        }
    }
}