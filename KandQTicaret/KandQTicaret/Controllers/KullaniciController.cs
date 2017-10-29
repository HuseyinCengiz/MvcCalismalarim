using KandQTicaret.App_Classes;
using KandQTicaret.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace KandQTicaret.Controllers
{
    [Authorize]
    public class KullaniciController : Controller
    {
        // GET: Kullanici
        #region Kullanici Islemleri
       [Authorize(Roles ="Admin")]
        public ActionResult Kullanicilar()
        {
            MembershipUserCollection Users = Membership.GetAllUsers();
            return View(Users);
        }
        [AllowAnonymous]
        public ActionResult KayitOl()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public JsonResult KayitOl(KullaniciBilgi k)
        {
            jsonBilgi json = new jsonBilgi();
            MembershipCreateStatus durum;
            try
            {
                MembershipUser user = Membership.CreateUser(k.KullaniciAdi, k.Sifre, k.Email, k.GizliSoru, k.GizliCevap, true, out durum);
                switch (durum)
                {
                    case MembershipCreateStatus.Success:
                        Roles.AddUserToRole(user.UserName, "Musteri");
                        MusteriAdre adres = new MusteriAdre();
                        adres.UserID =Guid.Parse(user.ProviderUserKey.ToString());
                        adres.Adres = "";
                        Context.DB.MusteriAdres.Add(adres);
                        Context.DB.SaveChanges();
                        json.IsSuccess = true;
                        json.Message = "Kayıt İşleminiz Başarıyla Gerçekleştirdi.";
                        break;
                    case MembershipCreateStatus.InvalidUserName:
                        json.IsSuccess = false;
                        json.Message = "Geçersiz Kullanıcı Adı";
                        break;
                    case MembershipCreateStatus.InvalidPassword:
                        json.IsSuccess = false;
                        json.Message = "Geçersiz Parola";
                        break;
                    case MembershipCreateStatus.InvalidQuestion:
                        json.IsSuccess = false;
                        json.Message = "Geçersiz Güvenlik Sorusu";
                        break;
                    case MembershipCreateStatus.InvalidAnswer:
                        json.IsSuccess = false;
                        json.Message = "Geçersiz Güvenlik Cevabı";
                        break;
                    case MembershipCreateStatus.InvalidEmail:
                        json.IsSuccess = false;
                        json.Message = "Geçersiz Email";
                        break;
                    case MembershipCreateStatus.DuplicateUserName:
                        json.IsSuccess = false;
                        json.Message = "Varolan Kullanıcı Adı";
                        break;
                    case MembershipCreateStatus.DuplicateEmail:
                        json.IsSuccess = false;
                        json.Message = "Varolan Email";
                        break;
                    case MembershipCreateStatus.UserRejected:
                        break;
                    case MembershipCreateStatus.InvalidProviderUserKey:
                        break;
                    case MembershipCreateStatus.DuplicateProviderUserKey:
                        break;
                    case MembershipCreateStatus.ProviderError:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                json.IsSuccess = false;
                json.Message = "Beklenilmeyen Bir Hatayla Karşılaşıldı";
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public ActionResult GirisYap()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult GirisYap(string KullaniciAdi, string Sifre, bool Hatirla)
        {
            if (Membership.ValidateUser(KullaniciAdi, Sifre))
            {
                string[] rols = Roles.GetRolesForUser(KullaniciAdi);
                foreach (string rol in rols)
                {
                    if (rol == "Admin")
                    {
                        FormsAuthentication.RedirectFromLoginPage(KullaniciAdi, Hatirla);
                        return RedirectToAction("Index", "Admin");
                    }
                    if (rol == "Musteri")
                    {
                        FormsAuthentication.RedirectFromLoginPage(KullaniciAdi, Hatirla);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ViewBag.Mesaj = "Kullanıcı Adı ve ya Şifre Yanlış!";
            return View();
        }
        public ActionResult CikisYap()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("GirisYap");
        }
        [Authorize(Roles = "Admin")]
        public JsonResult KullaniciBanlama(string id)
        {
            jsonBilgi json = new jsonBilgi();
            try
            {
                MembershipUser user = Membership.GetUser(id);
                user.IsApproved = false;
                Membership.UpdateUser(user);
                json.IsSuccess = true;
                json.Message = "Kullanıcı Başarıyla Engellendi.";
            }
            catch (Exception)
            {
                json.IsSuccess = false;
                json.Message = "Kullanıcı Engellenirken Hata Oluştu!";
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AdresDuzenle()
        {
            MembershipUser user = Membership.GetUser(User.Identity.Name);
            Guid userkey = Guid.Parse(user.ProviderUserKey.ToString());
            MusteriAdre adres = Context.DB.MusteriAdres.FirstOrDefault(x => x.UserID == userkey);
            return View(adres);
        }
        [HttpPost]
        public ActionResult AdresDuzenle(string adres)
        {
            MembershipUser user = Membership.GetUser(User.Identity.Name);
            Guid userkey = Guid.Parse(user.ProviderUserKey.ToString());
            MusteriAdre adresim = Context.DB.MusteriAdres.FirstOrDefault(x => x.UserID == userkey);
            adresim.Adres = adres.Trim();
            Context.DB.SaveChanges();
            return RedirectToAction("BuyukSepet", "Home");
        }

        #endregion

        #region RolIslemleri

        [Authorize(Roles = "Admin")]
        public ActionResult Roller()
        {
            var Roller = Roles.GetAllRoles().ToList();
            return View(Roller);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult RolEkle()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult RolEkle(string RolAdi)
        {
            Roles.CreateRole(RolAdi);
            return RedirectToAction("Roller");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult RolAta()
        {
            ViewBag.Roller = Roles.GetAllRoles();
            ViewBag.Kullanicilar = Membership.GetAllUsers();
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult RolAta(string KullaniciAdi, string RolAdi)
        {
            Roles.AddUserToRole(KullaniciAdi, RolAdi);
            return RedirectToAction("Kullanicilar");
        }
        [Authorize(Roles = "Admin")]
        public JsonResult RolSil(string id)
        {
            jsonBilgi json = new jsonBilgi();
            try
            {
                string[] users = Roles.GetUsersInRole(id);
                if (users.Count() > 0)
                {
                    Roles.RemoveUsersFromRole(users, id);
                }
                Roles.DeleteRole(id);
                json.IsSuccess = true;
                json.Message = "Rol Başarıyla Silindi.";
            }
            catch (Exception)
            {
                json.IsSuccess = false;
                json.Message = "Rol Silinirken Hata Oluştu!";
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}