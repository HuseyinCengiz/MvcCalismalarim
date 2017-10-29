using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HaberPortali.Core.Infrastructure;
using HaberPortali.Data.Model;
using HaberPortali.Admin.Class;
using PagedList;
using HaberPortali.Admin.CustomFilter;

namespace HaberPortali.Admin.Controllers
{
    public class KategoriController : Controller
    {
        #region Kategori
        private readonly IKategoriRepository _kategorirepository;
        public KategoriController(IKategoriRepository kategorirepository)
        {
            _kategorirepository = kategorirepository;
        }
        #endregion
        #region Kategori Liste
        [HttpGet]
        [LoginFilter]
        public ActionResult Index(int Sayfa = 1)
        {
            return View(_kategorirepository.GetAll().OrderByDescending(x => x.ID).ToPagedList(Sayfa, 10));
        }
        #endregion
        #region Silme
        public JsonResult Sil(int ID)
        {
            Kategori kategori = _kategorirepository.GetById(ID);
            if (kategori == null)
            {
                return Json(new ResultJson { Success = false, Message = "Kategori Bulunamadı" });
            }
            _kategorirepository.Delete(ID);
            _kategorirepository.Save();
            return Json(new ResultJson { Success = true, Message = "Kategori Silme İşleminiz Başarılı" });
        }

        /* public ActionResult Sil(int ID)
         {
             Kategori kategori = _kategorirepository.GetById(ID);
             if (kategori == null)
             {
                 throw new Exception("Kategori Bulunamadı");
             }
             _kategorirepository.Delete(ID);
             _kategorirepository.Save();
             return RedirectToAction("Index", "Kategori");
         }
         */
        #endregion
        #region Kategori Düzenle
        [HttpGet]
        [LoginFilter]
        public ActionResult Duzenle(int ID)
        {
            Kategori kategori = _kategorirepository.GetById(ID);
            if (kategori == null)
            {
                throw new Exception("Kategori Bulunamadı");
            }
            SetKategoriListele();
            return View(kategori);
        }

        [HttpPost]
        public JsonResult Duzenle(Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                Kategori dbkategori = _kategorirepository.GetById(kategori.ID);
                dbkategori.AktifMi = kategori.AktifMi;
                dbkategori.KategoriAdi = kategori.KategoriAdi;
                dbkategori.ParentID = kategori.ParentID;
                dbkategori.URL = kategori.URL;
                _kategorirepository.Save();
                return Json(new ResultJson { Success = true, Message = "Düzenleme İşlemi Başarılı" });
            }
            return Json(new ResultJson {Success=false,Message="Düzenleme İşlemi Sırasında Bir Hata Oluştu" });
        }
        #endregion
        #region KategoriEkle
        [HttpGet]
        [LoginFilter]
        public ActionResult Ekle()
        {
            SetKategoriListele();
            return View();
        }

        [HttpPost]
     
        public JsonResult Ekle(Kategori kategori)
        {
            try
            {
                _kategorirepository.Insert(kategori);
                _kategorirepository.Save();
                return Json(new ResultJson { Success = true, Message = "Kategori Ekleme İşleminiz Başarılı" });
            }
            catch (Exception)
            {
                //Loglama Yapabiliriz.
                return Json(new ResultJson { Success = false, Message = "Kayıt Eklerken Hata Oluştu" });
            }
        }
        #endregion 
        #region SetKategori
        public void SetKategoriListele()
        {
            var KategoriList = _kategorirepository.GetMany(x => x.ParentID == 0).ToList();
            ViewBag.Kategori = KategoriList;
        }
        #endregion
    }
}