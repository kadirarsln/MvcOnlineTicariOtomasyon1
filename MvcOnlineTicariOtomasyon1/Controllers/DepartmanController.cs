using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon1.Models.Siniflar;
namespace MvcOnlineTicariOtomasyon1.Controllers
{
    [Authorize]
    public class DepartmanController : Controller
    {
        // GET: Departman
        Context context = new Context();

        public ActionResult DepartmanIndex()
        {
            var departmanlar = context.Departmans.Where(d => d.Durum == true).ToList();
            return View(departmanlar);
        }

        //DEPARTMAN EKLE
        [HttpGet]
        [Authorize(Roles = "D")]
        public ActionResult DepartmanAdd()
        {
            return View();
        }

        [HttpPost]

        public ActionResult DepartmanAdd(Departman departman)
        {
            context.Departmans.Add(departman);
            context.SaveChanges();
            return RedirectToAction("DepartmanIndex");
        }

        //SİLME KISMI
        public ActionResult DepartmanDelete(int id)
        {
            var delete = context.Departmans.Find(id);
            delete.Durum = false;
            context.SaveChanges();
            return RedirectToAction("DepartmanIndex");
        }

        //GÜNCELLEME KISMI
        public ActionResult DepartmanGet(int id)
        {
            var departmanGet = context.Departmans.Find(id);
            return View("DepartmanGet", departmanGet);
        }

        public ActionResult DepartmanUpdate(Departman departman)
        {
            var update = context.Departmans.Find(departman.DepartmanID);
            update.DepartmanAd = departman.DepartmanAd;
            update.Durum = departman.Durum;
            context.SaveChanges();
            return RedirectToAction("DepartmanIndex");
        }

        //DETAYLAR
        public ActionResult DepartmanDetay(int id)
        {
            var departmanDetay = context.Personels.Where(d => d.Departmanid == id).ToList();
            var departmanDetay2 = context.Departmans.Where(d => d.DepartmanID == id).Select(d2 => d2.DepartmanAd)
                .FirstOrDefault();
            ViewBag.d = departmanDetay2;
            return View(departmanDetay);
        }

        public ActionResult DepartmanPersonelSatis(int id)
        {
            var departmanPersonelSatis = context.SatisHarekets.Where(d => d.Personelid == id).ToList();
            var departmanPersonelSatis2 = context.Personels.Where(d => d.PersonelID == id)
                .Select(d2 => d2.PersonelAd + " " + d2.PersonelSoyad).FirstOrDefault();
            ViewBag.dpersat = departmanPersonelSatis2;
            return View(departmanPersonelSatis);
        }
    }
}