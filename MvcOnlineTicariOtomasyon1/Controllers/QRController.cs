using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QRCoder;

namespace MvcOnlineTicariOtomasyon1.Controllers
{
    public class QRController : Controller
    {
        // GET: QR
        public ActionResult QRIndex()
        {
            return View();
        }
        [HttpPost]
        public ActionResult QRIndex(string kod)                             //QRCODE İÇİN GEREKLİ KONTROLLER.
        {   
            using (MemoryStream memoryStream = new MemoryStream())
            {
                QRCodeGenerator codeGenerator = new QRCodeGenerator();      
                QRCodeGenerator.QRCode code = codeGenerator.CreateQrCode(kod, QRCodeGenerator.ECCLevel.Q);
                using (Bitmap bitmap=code.GetGraphic(10))
                {
                    bitmap.Save(memoryStream, ImageFormat.Png);
                    ViewBag.codeimage = "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray());
                }
            }
            return View();
        }
    }
}