$(document).ready(function () {
    $(document).on("click", "#ustkategorisil", function () {
        var id = $(this).attr("data-sid");
        var SilStr = $(this).closest("tr");
        swal({
            title: "Emin Misin?",
            text: "Bu Üst Kategoriyi Bir Daha Geri Getiremeyebilirsiniz!",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Evet, Sil Gitsin!",
            cancelButtonText: "Hayır, Silme Lütfen!",
            closeOnConfirm: true,
            closeOnCancel: false
        },
function (isConfirm) {
    if (isConfirm) {
        $.ajax({
            type: "Get",
            url: "/Admin/UstKategoriSil/" + id,
            success: function (veri) {
                if (veri.IsSuccess) {
                    SilStr.remove();
                    swal("Silindi!", veri.Message, "success");
                }
                else {
                    swal("Silinemedi!", veri.Message, "error");
                }

            }
        });
    } else {
        swal("İptal Edildi!", "Üst Kategori Silinmedi Güvende :)", "info");
    }
});
    });
    $(document).on("click", "#kategorisil", function () {
        var id = $(this).attr("data-sid");
        var SilStr = $(this).closest("tr");
        swal({
            title: "Emin Misin?",
            text: "Bu Kategoriyi Bir Daha Geri Getiremeyebilirsiniz!",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Evet, Sil Gitsin!",
            cancelButtonText: "Hayır, Silme Lütfen!",
            closeOnConfirm: true,
            closeOnCancel: false
        },
function (isConfirm) {
    if (isConfirm) {
        $.ajax({
            type: "Get",
            url: "/Admin/KategoriSil/" + id,
            success: function (veri) {
                if (veri.IsSuccess) {
                    SilStr.remove();
                    swal("Silindi!", veri.Message, "success");
                }
                else {
                    swal("Silinemedi!", veri.Message, "error");
                }
            }
        });
    } else {
        swal("İptal Edildi!", "Kategori Silinmedi Güvende :)", "info");
    }
});
    });
    $(document).on("click", "#makalesil", function () {
        var id = $(this).attr("data-msid");
        var SilStr = $(this).closest("tr");
        swal({
            title: "Emin Misin?",
            text: "Bu Makaleyi Bir Daha Geri Getiremeyebilirsiniz!",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Evet, Sil Gitsin!",
            cancelButtonText: "Hayır, Silme Lütfen!",
            closeOnConfirm: true,
            closeOnCancel: false
        },
 function (isConfirm) {
     if (isConfirm) {
         $.ajax({
             type: "Get",
             url: "/Admin/MakaleSil/" + id,
             success: function (veri) {
                 if (veri.IsSuccess) {
                     SilStr.remove();
                     swal("Silindi!", veri.Message, "success");
                 }
                 else {
                     swal("Silinemedi!", veri.Message, "error");
                 }
             }
         });
     } else {
         swal("İptal Edildi!", "Makale Silinmedi Güvende :)", "info");
     }
 });
    });
    $(document).on("click", "#etiketsil", function () {
        var id = $(this).attr("data-sid");
        var SilStr = $(this).closest("tr");
        swal({
            title: "Emin Misin?",
            text: "Bu Etiketi Bir Daha Geri Getiremeyebilirsiniz!",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Evet, Sil Gitsin!",
            cancelButtonText: "Hayır, Silme Lütfen!",
            closeOnConfirm: true,
            closeOnCancel: false
        },
 function (isConfirm) {
     if (isConfirm) {
         $.ajax({
             type: "Get",
             url: "/Admin/EtiketSil/" + id,
             success: function (veri) {
                 if (veri.IsSuccess) {
                     SilStr.remove();
                     swal("Silindi!", veri.Message, "success");
                 }
                 else {
                     swal("Silinemedi!", veri.Message, "error");
                 }
             }
         });
     } else {
         swal("İptal Edildi!", "Etiket Silinmedi Güvende :)", "info");
     }
 });
    });
    $(document).on("click", "#rolsil", function () {
        var id = $(this).attr("data-sad");
        var SilStr = $(this).closest("tr");
        swal({
            title: "Emin Misin?",
            text: "Bu Rolü Bir Daha Geri Getiremeyebilirsiniz!",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Evet, Sil Gitsin!",
            cancelButtonText: "Hayır, Silme Lütfen!",
            closeOnConfirm: true,
            closeOnCancel: false
        },
 function (isConfirm) {
     if (isConfirm) {
         $.ajax({
             type: "Get",
             url: "/Kullanici/RolSil/" + id,
             success: function (veri) {
                 if (veri.IsSuccess) {
                     SilStr.remove();
                     swal("Silindi!", veri.Message, "success");
                 }
                 else {
                     swal("Silinemedi!", veri.Message, "error");
                 }
             }
         });
     } else {
         swal("İptal Edildi!", "Rol Silinmedi Güvende :)", "info");
     }
 });
    });
    $(document).on("click", "#kullanicibanlama", function () {
        var id = $(this).attr("data-sid");
        var SilStr = $(this).closest("tr");
        swal({
            title: "Emin Misin?",
            text: "Bu Kullanıcıyı Engelleyecek Misin?",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Evet, Engelle Gitsin!",
            cancelButtonText: "Hayır, Engelleme Lütfen!",
            closeOnConfirm: true,
            closeOnCancel: false
        },
 function (isConfirm) {
     if (isConfirm) {
         $.ajax({
             type: "Get",
             url: "/Kullanici/KullaniciBanlama/" + id,
             success: function (veri) {
                 if (veri.IsSuccess) {
                     SilStr.remove();
                     swal("Engellendi!", veri.Message, "success");
                 }
                 else {
                     swal("Engellenemedi!", veri.Message, "error");
                 }
             }
         });
     } else {
         swal("İptal Edildi!", "Kullanıcı Engellenmedi Güvende :)", "info");
     }
 });
    });
    $(document).on("click", "#profilsil", function () {
        var id = $(this).attr("data-sid");
        var SilStr = $(this).closest("tr");
        swal({
            title: "Emin Misin?",
            text: "Bu Profili Bir Daha Geri Getiremeyebilirsiniz!",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Evet, Sil Gitsin!",
            cancelButtonText: "Hayır, Silme Lütfen!",
            closeOnConfirm: true,
            closeOnCancel: false
        },
  function (isConfirm) {
      if (isConfirm) {
          $.ajax({
              type: "Get",
              url: "/Admin/ProfilSil/" + id,
              success: function (veri) {
                  if (veri.IsSuccess) {
                      SilStr.remove();
                      swal("Silindi!", veri.Message, "success");
                  }
                  else {
                      swal("Silinemedi!", veri.Message, "error");
                  }
              }
          });
      } else {
          swal("İptal Edildi!", "Profil Silinmedi Güvende :)", "info");
      }
  });
    });
    $.validator.addMethod("selectkontrol", function (value) {
        return (value != '0');
    }, "Lütfen Varsayılan Değerden Farklı Bir Değer Giriniz.");
    $("#ustkategoriform").validate({
        rules: {
            "UstKategoriAdi": {
                required: true
            }
        },
        messages: {
            "UstKategoriAdi": {
                required: "Lütfen Üst Kategori Adı Giriniz!"
            }
        }
    });
    $("#kategoriform").validate({
        rules: {
            "KategoriAdi": {
                required: true
            },
            "UstKategoriID": {
                selectkontrol: true
            }
        },
        messages: {
            "KategoriAdi": {
                required: "Lütfen Kategori Adı Giriniz!"
            }
        }
    });
    $("#makaleform").validate({
        rules: {
            "Baslik": {
                required: true
            },
            "KisaAciklama": {
                required: true
            },
            "MakaleResim": {
                required: true
            },
            "KategoriID": {
                selectkontrol: true
            }
        },
        messages: {
            "Baslik": {
                required: "Lütfen Başlık Giriniz!"
            },
            "KisaAciklama": {
                required: "Lütfen Kısa Açıklama Giriniz!"
            },
            "MakaleResim": {
                required: "Lütfen Resim Seçiniz!"
            }
        }
    });
    $("#girisyapform").validate({
        rules: {
            "KullaniciAdi": {
                required: true
            },
            "Sifre": {
                required: true,
                minlength: 7
            }
        },
        messages: {
            "KullaniciAdi": {
                required: "Lütfen Kullanıcı Adını Boş Bırakmayınız!"
            },
            "Sifre": {
                required: "Lütfen Şifreyi Boş Bırakmayınız!",
                minlength: "En Az 7 Karakterli Şifre Giriniz!"
            }
        }
    });
    $("#kayitolform").validate({
        rules: {
            "KullaniciAdi": {
                required: true
            },
            "Sifre": {
                required: true,
                minlength: 7
            },
            "SifreTekrar": {
                equalTo: "#Sifre"
            },
            "Email": {
                required: true,
                email: true
            },
            "GizliSoru": {
                required: true
            },
            "GizliCevap": {
                required: true
            },
            "ProfilResim": {
                required: true
            }
        },
        messages: {
            "KullaniciAdi": {
                required: "Lütfen Kullanıcı Adını Boş Bırakmayınız!"
            },
            "Sifre": {
                required: "Lütfen Şifreyi Boş Bırakmayınız!",
                minlength: "En Az 7 Karakterli Şifre Giriniz!"
            },
            "SifreTekrar": {
                equalTo: "Lütfen Şifreyi Tekrar Yazınız!"
            },
            "Email": {
                required: "Email Boş Geçilemez!",
                email: "Lütfen Geçerli Bir Email Adresi Giriniz."
            },
            "GizliSoru": {
                required: "Gizli Soruyu Boş Bırakmayınız!"
            },
            "GizliCevap": {
                required: "Gizli Cevabı Boş Bırakmayınız!"
            },
            "ProfilResim": {
                required: "Profil Resmini Boş Bırakmayınız!"
            }
        }
    });
});

