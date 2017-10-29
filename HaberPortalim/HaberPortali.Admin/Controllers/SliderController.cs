using HaberPortali.Admin.Class;
using HaberPortali.Admin.CustomFilter;
using HaberPortali.Core.Infrastructure;
using HaberPortali.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using HaberPortali.Admin.Helper;
using System.IO;

namespace HaberPortali.Admin.Controllers
{
    public class SliderController : Controller
    {
        private readonly ISliderRepository _sliderRepository;
        // GET: Slider
        public SliderController(ISliderRepository sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }
        [HttpGet]
        [LoginFilter]
        public ActionResult Index(int Sayfa = 1)
        {
            var slider = _sliderRepository.GetAll().OrderByDescending(x => x.ID).ToPagedList(Sayfa, 10);
            return View(slider);
        }
        #region Ekle
        [HttpGet]
        [LoginFilter]
        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        [LoginFilter]
        public JsonResult Ekle(Slider slider, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                if (ResimURL.ContentLength > 0)
                {
                    string Dosya = Guid.NewGuid().ToString().Replace(",", "");
                    string uzanti = System.IO.Path.GetExtension(ResimURL.FileName);
                    string ResimYol = "/External/Slider/" + Dosya + uzanti;
                    ResimURL.SaveAs(Server.MapPath(ResimYol));
                    slider.ResimURL = ResimYol;
                }
                _sliderRepository.Insert(slider);
                try
                {
                    _sliderRepository.Save();
                    return Json(new ResultJson { Success = true, Message = "Slider Ekleme İşleminiz Başarılı !" });
                }
                catch (Exception)
                {

                    return Json(new ResultJson { Success = false, Message = "Slider Ekleme İşleminiz Başarısız !" });
                }
            }
            return Json(new ResultJson { Success = false, Message = "Slider Ekleme İşleminiz Başarısız !" });
        }
        #endregion
        #region Duzenle
        [HttpGet]
        [LoginFilter]
        public ActionResult Duzenle(int ID)
        {
            var slidervarmi = _sliderRepository.GetById(ID);
            if (slidervarmi != null)
            {
                return View(slidervarmi);
            }
            return View();
        }

        [HttpPost]
        [LoginFilter]
        public JsonResult Duzenle(Slider slider, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                Slider dbslider = _sliderRepository.GetById(slider.ID);
                dbslider.Baslik = slider.Baslik;
                dbslider.Aciklama = slider.Aciklama;
                dbslider.AktifMi = slider.AktifMi;
                dbslider.URL = slider.URL;
                if (ResimURL != null && ResimURL.ContentLength > 0)
                {
                    if (dbslider.ResimURL != null)
                    {
                        string varolanURL = dbslider.ResimURL;
                        FileInfo file = new FileInfo(Server.MapPath(varolanURL));
                        if (file.Exists)
                        {
                            file.Delete();
                        }
                    }
                    dbslider.ResimURL = ResimYukle.Resim(ResimURL, slider);
                }
                try
                {
                    _sliderRepository.Save();
                    return Json(new ResultJson { Success = true, Message = "Slider Düzenleme İşlemi Başarılı" });
                }
                catch (Exception)
                {
                    return Json(new ResultJson { Success = false, Message = "Slider Düzenleme İşlemi Başarısız" });
                }
            }
            return Json(new ResultJson { Success = false, Message = "Bilinmeyen Hata Oluştu" });
        }
        #endregion
        #region Sil
        public JsonResult Sil(Slider slider)
        {
            Slider dbslider = _sliderRepository.GetById(slider.ID);
            if (dbslider == null)
            {
                return Json(new ResultJson { Success = false, Message = "Slider Bulunamadı." });
            }
            try
            {
                if (dbslider.ResimURL != null)
                {
                    string DosyaUrl = dbslider.ResimURL;
                    string TamYol = Server.MapPath(DosyaUrl);
                    FileInfo file = new FileInfo(TamYol);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }
                _sliderRepository.Delete(slider.ID);
                _sliderRepository.Save();
                return Json(new ResultJson { Success = true, Message = "Slider Silme İşleminiz Başarılı." });
            }
            catch (Exception)
            {
                return Json(new ResultJson { Success = false, Message = "Slider Silme İşleminiz Başarısız." });
            }
        }
        #endregion
    }
}