using Blogum.App_Classes;
using Blogum.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Blogum.Controllers
{
    [Authorize]
    public class KullaniciController : Controller
    {
        // GET: Kullanici
        public ActionResult Index()
        {
            return View();
        }
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
            JsonResultModel json = new JsonResultModel();
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
        #region KullaniciIslemleri
        [Authorize(Roles = "Admin")]
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
        public ActionResult KayitOl(KullaniciBilgi k, HttpPostedFileBase ProfilResim)
        {
            MembershipCreateStatus durum;
            MembershipUser User = Membership.CreateUser(k.KullaniciAdi, k.Sifre, k.Email, k.GizliSoru, k.GizliCevap, true, out durum);
            string Mesaj = "";
            switch (durum)
            {
                case MembershipCreateStatus.Success:
                    KullaniciResimKayitOlustur(User.ProviderUserKey.ToString(), ProfilResim);
                    Roles.AddUserToRole(k.KullaniciAdi, "Uye");
                    return RedirectToAction("GirisYap", "Kullanici");
                case MembershipCreateStatus.InvalidUserName:
                    Mesaj = "Geçersiz Kullanıcı Adı!";
                    break;
                case MembershipCreateStatus.InvalidPassword:
                    Mesaj = "Geçersiz Parola!";
                    break;
                case MembershipCreateStatus.InvalidQuestion:
                    Mesaj = "Geçersiz Soru!";
                    break;
                case MembershipCreateStatus.InvalidAnswer:
                    Mesaj = "Geçersiz Cevap!";
                    break;
                case MembershipCreateStatus.InvalidEmail:
                    Mesaj = "Geçersiz Email!";
                    break;
                case MembershipCreateStatus.DuplicateUserName:
                    Mesaj = "Aynı Kullanıcı Adlı Üye Var!";
                    break;
                case MembershipCreateStatus.DuplicateEmail:
                    Mesaj = "Aynı Emaille Açılmış Bir Hesap Var";
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
                    Mesaj = "Bilinmeyen Hata";
                    break;
            }
            ViewBag.Mesaj = Mesaj;
            return View();
        }
        [AllowAnonymous]
        public void KullaniciResimKayitOlustur(string userid, HttpPostedFileBase presim)
        {
            aspnet_Resim resim = new aspnet_Resim();
            resim.UserId = Guid.Parse(userid);
            Image image = Image.FromStream(presim.InputStream);
            Bitmap bmp = new Bitmap(image, MyConfig.ProfilResimBoyut);
            FileInfo file = new FileInfo(presim.FileName);
            string resimyol = "/Content/KullaniciProfilResim/" + System.Guid.NewGuid() + file.Extension;
            resim.KucukResimYol = resimyol;
            bmp.Save(Server.MapPath(resimyol));
            Context.Baglanti.aspnet_Resim.Add(resim);
            Context.Baglanti.SaveChanges();
        }
        [AllowAnonymous]
        public ActionResult GirisYap()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult GirisYap(KullaniciBilgi k, bool Hatirla)
        {
            if (Membership.ValidateUser(k.KullaniciAdi, k.Sifre))
            {
                string[] rols = Roles.GetRolesForUser(k.KullaniciAdi);
                foreach (string rol in rols)
                {
                    if (rol == "Admin")
                    {
                        FormsAuthentication.RedirectFromLoginPage(k.KullaniciAdi, Hatirla);
                        return RedirectToAction("Index", "Admin");
                    }
                    if (rol == "Uye")
                    {
                        FormsAuthentication.RedirectFromLoginPage(k.KullaniciAdi, Hatirla);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ViewBag.Mesaj = "Kullanıcı Adı ve ya Parola Yanlış!";
            return View();
        }
        public ActionResult CikisYap()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("GirisYap");
        }
        [AllowAnonymous]
        public ActionResult SifremiUnuttum()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult SifremiUnuttum(KullaniciBilgi k)
        {
            MembershipUser Mu = Membership.GetUser(k.KullaniciAdi);
            if (Mu.PasswordQuestion == k.GizliSoru)
            {
                string eskisifre = Mu.ResetPassword(k.GizliCevap);
                Mu.ChangePassword(eskisifre, k.Sifre);
                return RedirectToAction("GirisYap");
            }
            else
            {
                ViewBag.Mesaj = "Girilen Bilgiler Yanlış!";
                return View();
            }
        }
        public JsonResult KullaniciBanlama(string id)
        {
            JsonResultModel json = new JsonResultModel();
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

        #endregion
    }
}