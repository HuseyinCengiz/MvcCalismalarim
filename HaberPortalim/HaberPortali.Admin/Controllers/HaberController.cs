using HaberPortali.Admin.CustomFilter;
using HaberPortali.Core.Infrastructure;
using HaberPortali.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.IO;
using System.Text;

namespace HaberPortali.Admin.Controllers
{
    public class HaberController : Controller
    {
        #region Veritabanı
        private readonly IHaberRepository _haberRepository;
        private readonly IKullaniciRepository _kullaniciRepository;
        private readonly IKategoriRepository _kategoriRepository;
        private readonly IResimRepository _resimRepository;
        private readonly IEtiketRepository _etiketRepository;
        #endregion

        public HaberController(IHaberRepository haberRepository, IKullaniciRepository kullaniciRepository, IKategoriRepository kategoriRepository, IResimRepository resimRepository, IEtiketRepository etiketRepository)
        {
            _haberRepository = haberRepository;
            _kategoriRepository = kategoriRepository;
            _kullaniciRepository = kullaniciRepository;
            _resimRepository = resimRepository;
            _etiketRepository = etiketRepository;
        }
        // GET: Haber
        [LoginFilter]
        public ActionResult Index(int sayfa = 1)
        {
            var HaberList = _haberRepository.GetAll();
            return View(HaberList.OrderByDescending(x => x.ID).ToPagedList(sayfa, 15));
        }
        #region  Haber Ekle
        [HttpGet]
        [LoginFilter]
        public ActionResult Ekle()
        {
            SetKategoriListele();
            return View();
        }
        [HttpPost]
        [LoginFilter]
        public ActionResult Ekle(Haber haber, HttpPostedFileBase Resim, IEnumerable<HttpPostedFileBase> DetayResim, string etiket)
        {
            var SessionControl = HttpContext.Session["KullaniciEmail"];
            if (haber!=null)
            {
                Kullanici kullanici = _kullaniciRepository.GetById(Convert.ToInt32(SessionControl));
                haber.KullaniciID = kullanici.ID;
                if (Resim != null)
                {
                    //saveas fiziksel olarak dosyayı oraya kaydediyor server.mappath is oraya hangi uzanti ve isimle yolu belirtiyoruz
                    string DosyaAdi = Guid.NewGuid().ToString().Replace("-", "");//yeni dosya adi
                    string uzanti = System.IO.Path.GetExtension(Resim.FileName);//dosyanın uzantısı
                    string TamYol = "/External/Haber/" + DosyaAdi + uzanti;//nereye kaydedeceğimizi ve yenidosya adı uzantısıyla kaydediyoruzç
                    Resim.SaveAs(Server.MapPath(TamYol));//Resimi al kaydet ama nereye ve hangi adla uzantıyla kaydet.
                    haber.Resim = TamYol;
                }
                _haberRepository.Insert(haber);
                _haberRepository.Save();
                _etiketRepository.EtiketEkle(haber.ID, etiket);
                string detayresimvarmi = System.IO.Path.GetExtension(Request.Files[1].FileName);
                if (detayresimvarmi != "")
                {
                    foreach (var file in DetayResim)
                    {
                        if (file.ContentLength > 0)
                        {
                            string DosyaAdi = Guid.NewGuid().ToString().Replace("-", "");
                            string uzanti = System.IO.Path.GetExtension(file.FileName);
                            string TamYol = "/External/Haber/" + DosyaAdi + uzanti;
                            file.SaveAs(Server.MapPath(TamYol));
                            HaberPortali.Data.Model.Resim rsm = new Data.Model.Resim();
                            rsm.ResimUrl = TamYol;
                            rsm.HaberID = haber.ID;
                            _resimRepository.Insert(rsm);
                            _resimRepository.Save();
                        }

                    }
                }
            }
            TempData["Bilgi"] = "Haber Ekleme İşleminiz Başarıyla Gerçekleşti";
            return RedirectToAction("Index", "Haber");
        }
        #endregion
        #region Haber Sil
        public ActionResult Sil(int id)
        {
            Haber hbr = _haberRepository.GetById(id);
            var DetayResimler = _resimRepository.GetMany(x => x.HaberID == id);
            if (hbr == null)
            {
                throw new Exception("Haber Bulunamadı");
            }
            string file_name = hbr.Resim;
            string path = Server.MapPath(file_name);
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
            }
            if (DetayResimler != null)
            {
                foreach (var item in DetayResimler)
                {
                    string path2 = Server.MapPath(item.ResimUrl);
                    file = new FileInfo(path2);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }
            }
            _haberRepository.Delete(id);
            _haberRepository.Save();
            TempData["Bilgi"] = "Haber Başarılı Bir Şekilde Silindi";
            return RedirectToAction("Index", "Haber");
        }
        #endregion
        #region Set Kategori Listesi
        public void SetKategoriListele(object kategori = null)
        {
            var KategoriList = _kategoriRepository.GetMany(x => x.ParentID == 0).ToList();
            ViewBag.Kategori = KategoriList;
        }
        #endregion
        #region Duzenleme
        [HttpGet]
        [LoginFilter]
        public ActionResult Duzenle(int id)
        {
            Haber gelenhbr = _haberRepository.GetById(id);
            var gelenetiket = gelenhbr.Etiket.Select(x => x.EtiketAdi).ToArray();
            HaberEtiketModel model = new HaberEtiketModel
            {
                Haber = gelenhbr,
                Etiketler = _etiketRepository.GetAll(),
                GelenEtiketler = gelenetiket
            };
            StringBuilder birlestir = new StringBuilder();
            foreach (var etiket in gelenetiket)
            {
                birlestir.Append(etiket);
                birlestir.Append(",");
            }
            model.EtiketAdi = birlestir.ToString();
            if (gelenhbr == null)
            {
                throw new Exception("Haber Bulunamadı");
            }
            else
            {
                SetKategoriListele();
                return View(model);
            }
        }
        [HttpPost]
        [LoginFilter]
        public ActionResult Duzenle(Haber haber, HttpPostedFileBase Resim, IEnumerable<HttpPostedFileBase> DetayResim,string EtiketAdi)
        {
            Haber gelenhaber = _haberRepository.GetById(haber.ID);
            gelenhaber.Aciklama = haber.Aciklama;
            gelenhaber.AktifMi = haber.AktifMi;
            gelenhaber.Baslik = haber.Baslik;
            gelenhaber.KisaAciklama = haber.KisaAciklama;
            gelenhaber.KategoriID = haber.KategoriID;
            if (Resim != null)
            {
                string dosyaAdi = gelenhaber.Resim;
                string dosyaYolu = Server.MapPath(dosyaAdi);
                FileInfo dosya = new FileInfo(dosyaYolu);
                if (dosya.Exists)
                {
                    dosya.Delete();
                }
                string yeniDosyaAdi = Guid.NewGuid().ToString().Replace("-", "");
                string yeniDosyaUzanti = System.IO.Path.GetExtension(Resim.FileName);
                string yeniTamYol = "/External/Haber/" + yeniDosyaAdi + yeniDosyaUzanti;
                Resim.SaveAs(Server.MapPath(yeniTamYol));
                gelenhaber.Resim = yeniTamYol;
            }
            //BURDA uzantı alarak aşağıda kontrol ettiriyoruz uzantıya göre
            string CokluResim = System.IO.Path.GetExtension(Request.Files[1].FileName);
            if (CokluResim != "")
            {
                foreach (var item in DetayResim)
                {
                    string fileName = Guid.NewGuid().ToString().Replace("-", "");
                    string fileextension = System.IO.Path.GetExtension(Request.Files[2].FileName);
                    string filePath = "/External/Haber/" + fileName + fileextension;
                    item.SaveAs(Server.MapPath(filePath));
                    var img = new Resim
                    {
                        ResimUrl = filePath
                    };
                    img.HaberID = haber.ID;
                    img.EklenmeTarihi = DateTime.Now;
                    _resimRepository.Insert(img);
                    _resimRepository.Save();

                }
            }
            _etiketRepository.EtiketEkle(haber.ID, EtiketAdi);
            _haberRepository.Save();
            TempData["Bilgi"] = "Güncelleme İşleminiz Başarılı";
            return RedirectToAction("Index", "Haber");
        }
        #endregion
        #region Aktif/Pasif Yapma
        public ActionResult Onay(int id)
        {
            Haber hbr = _haberRepository.GetById(id);
            if (hbr.AktifMi == true)
            {
                hbr.AktifMi = false;
                _haberRepository.Save();
                TempData["Bilgi"] = "Pasif Yapma İşleminiz Başarıyla Gerçekleşti";
                return RedirectToAction("Index");
            }
            else if (hbr.AktifMi == false)
            {
                hbr.AktifMi = true;
                _haberRepository.Save();
                TempData["Bilgi"] = "Aktif Yapma İşleminiz Başarıyla Gerçekleşti";
                return RedirectToAction("Index");
            }
            return View();
        }
        #endregion
        #region ResimSil
        public ActionResult ResimSil(int id)
        {
            Resim gelenresim = _resimRepository.GetById(id);
            if (gelenresim == null)
            {
                throw new Exception("Resim Bulunamadı");
            }
            string file_name = gelenresim.ResimUrl;
            string file_fullpath = Server.MapPath(file_name);
            FileInfo file = new FileInfo(file_fullpath);
            if (file.Exists)
            {
                file.Delete();
            }
            _resimRepository.Delete(id);
            _resimRepository.Save();
            TempData["Bilgi"] = "Resim Silme İşlemi Başarılı";
            return RedirectToAction("Index", "Haber");
        }
        #endregion
    }
}