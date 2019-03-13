using BarkodSistem.Models;

using Qrcode.Models;
using QRCoder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;


namespace BarkodSistem.Controllers
{
    public class QrProductController : Controller
    {
        QrcodeContext db = new QrcodeContext();
        // GET: QrProduct
        public ActionResult Index()
        {
            //var result = (from p in db.Product
            //              join a in db.QrcodeInfo on p.ProductID equals a.QrCodeInfoID
                         
            //              select new QrProductViewModels
            //              {
            //                  ProductID = p.ProductID,
            //                  QrCodeInfoID = a.QrCodeInfoID,
            //                  ProductName = p.ProductName,
            //                  ProductionDate = a.ProductionDate,
            //                  Size = p.Size,
            //                  SKT = a.SKT,
            //                  TETT = a.TETT,
            //                  CategoryID = p.CategoryID,
            //                  UnitsInStock = p.UnitsInStock
            //              }).ToList();

       

            return View();
        }

        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Category, "CategoryID", "CategoryName");
            ViewBag.ProductID = new SelectList(db.QrcodeInfo, "QrcodeInfoID", "QrcodeInfoID");

            return View();
        }

        [HttpPost]
        public ActionResult Create(QrProductViewModels vm)
        {

            QrCodeInfo qr = new QrCodeInfo();
            qr.ProductionDate = vm.ProductionDate;
            qr.SKT = vm.SKT;
            qr.TETT = vm.TETT;

            db.QrcodeInfo.Add(qr);
            db.SaveChanges();

            int sonqr = db.QrcodeInfo.OrderByDescending(x => x.QrCodeInfoID).FirstOrDefault().QrCodeInfoID;
            ViewBag.CategoryID = new SelectList(db.Category, "CategoryID", "CategoryName",vm.CategoryID);
            Product p = new Product();
            p.ProductName = vm.ProductName;
            //p.QrCodeInfoID = vm.QrCodeInfoID;
            p.Size = vm.Size;
            p.UnitsInStock = vm.UnitsInStock;
            p.CategoryID = vm.CategoryID;
            p.ProductID = sonqr;

            db.Product.Add(p);
            db.SaveChanges();

            return View(vm);
        }

        [HttpGet]
        public JsonResult Listele()
        {

            var result = (from p in db.Product
                          join a in db.QrcodeInfo on p.ProductID equals a.QrCodeInfoID
                          select new QrProductViewModels
                          {
                              ProductID = p.ProductID,
                              QrCodeInfoID = a.QrCodeInfoID,
                              ProductName = p.ProductName,
                              //ProductionDate = a.ProductionDate,
                              //Size = p.Size,
                              SKT = a.SKT,
                              //TETT = a.TETT,
                              //CategoryID = p.CategoryID,
                              //UnitsInStock = p.UnitsInStock
                          }).ToList();

        


            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult QrOlustur()
        {
            var result = (from p in db.Product
                          join a in db.QrcodeInfo on p.ProductID equals a.QrCodeInfoID
                         
                          select new QrProductViewModels
                          {
                              ProductID = p.ProductID,
                              QrCodeInfoID = a.QrCodeInfoID,
                              ProductName = p.ProductName,
                              ProductionDate = a.ProductionDate,
                              Size = p.Size,
                              SKT = a.SKT,
                              TETT = a.TETT,
                              CategoryID = p.CategoryID,
                              UnitsInStock = p.UnitsInStock
                          }).ToList().FirstOrDefault();



            ViewBag.QrCode = result.SKT.ToString();

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(ViewBag.QrCode, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            //System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            //imgBarCode.Height = 150;
            //imgBarCode.Width = 150;
            Bitmap bitMap = qrCode.GetGraphic(20);
            
                MemoryStream ms = new MemoryStream();
                
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    ViewBag.imageBytes = ms.ToArray();
                   
                    //imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                
            

            return Json(ms.ToArray(),JsonRequestBehavior.AllowGet);
        }

    }
}