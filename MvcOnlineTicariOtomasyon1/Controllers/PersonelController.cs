using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon1.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon1.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        Context context = new Context();
        public ActionResult PersonelIndex()
        {
            var personeller = context.Personels.ToList();
            return View(personeller);
        }

        //PERSONEL EKLEME
        [HttpGet]
        public ActionResult PersonelAdd()
        {
            List<SelectListItem> departmanList = (from d in context.Departmans.ToList()
                                                  select new SelectListItem
                                                  {
                                                      Text = d.DepartmanAd,                        //Dropdown yapısı kullandık listeleme şeklinde kategori seçimi yapabilşmek için.
                                                      Value = d.DepartmanID.ToString()
                                                  }).ToList();

            ViewBag.departmanList1 = departmanList;           //Controllerdan view tarafına değer ve veri taşırız.
            return View();
        }
        [HttpPost]
        public ActionResult PersonelAdd(Personel personel)
        {
            if (Request.Files.Count > 0)
            {
                string filename = Path.GetFileName(Request.Files[0].FileName);   //görsel ekleme için yazdık.
                string extension = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Images/" + filename + extension;
                Request.Files[0].SaveAs(Server.MapPath(path));
                personel.PersonelGorsel= "~/Images/" + filename + extension;
            }
            context.Personels.Add(personel);
            context.SaveChanges();
            return RedirectToAction("PersonelIndex");
        }

        //GÜNCELLEME KISMI
        public ActionResult PersonelGet(int id)
        {
            List<SelectListItem> departmanList = (from d in context.Departmans.ToList()
                                                  select new SelectListItem
                                                  {
                                                      Text = d.DepartmanAd,                        //Dropdown yapısı kullandık listeleme şeklinde kategori seçimi yapabilşmek için.
                                                      Value = d.DepartmanID.ToString()
                                                  }).ToList();

            ViewBag.departmanList1 = departmanList;           //Controllerdan view tarafına değer ve veri taşırız.
            var personelGet = context.Personels.Find(id);
            return View("PersonelGet", personelGet);
        }

        public ActionResult PersonelUpdate(Personel personel)
        {
            if (Request.Files.Count > 0)
            {
                string filename = Path.GetFileName(Request.Files[0].FileName);   //görsel ekleme için yazdık.
                string extension = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Images/" + filename + extension;
                Request.Files[0].SaveAs(Server.MapPath(path));
                personel.PersonelGorsel = "/Images/" + filename + extension;
            }
            var update = context.Personels.Find(personel.PersonelID);
            update.PersonelAd = personel.PersonelAd;
            update.PersonelSoyad = personel.PersonelSoyad;
            update.PersonelGorsel = personel.PersonelGorsel;
            update.Departmanid = personel.Departmanid;
            context.SaveChanges();
            return RedirectToAction("PersonelIndex");
        }

        //PERSONEL DETAY
        public ActionResult PersonelDetayList()
        {
            var sorgu = context.Personels.ToList();
            return View(sorgu);
        }
    }
}