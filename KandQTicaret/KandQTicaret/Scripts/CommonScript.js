$(document).ready(function () {
    $(document).on("click", ".ustkategorisil", function () {
        var id = $(this).data("id");
        var silStr = $(this).closest("tr");
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
                    silStr.remove();
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
    $(document).on("click", ".kategorisil", function () {
        var id = $(this).data("id");
        var silStr = $(this).closest("tr");
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
                    silStr.remove();
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
    $(document).on("click", ".markasil", function () {
        var id = $(this).data("id");
        var silStr = $(this).closest("tr");
        swal({
            title: "Emin Misin?",
            text: "Bu Markayı Bir Daha Geri Getiremeyebilirsiniz!",
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
            url: "/Admin/MarkaSil/" + id,
            success: function (veri) {
                if (veri.IsSuccess) {
                    silStr.remove();
                    swal("Silindi!", veri.Message, "success");
                }
                else {
                    swal("Silinemedi!", veri.Message, "error");
                }
            }
        });
    } else {
        swal("İptal Edildi!", "Marka Silinmedi Güvende :)", "info");
    }
});
    });
    $(document).on("click", ".ozelliktipsil", function () {
        var id = $(this).data("id");
        var silStr = $(this).closest("tr");
        swal({
            title: "Emin Misin?",
            text: "Bu Özellik Tipi Bir Daha Geri Getiremeyebilirsiniz!",
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
            url: "/Admin/OzellikTipSil/" + id,
            success: function (veri) {
                if (veri.IsSuccess) {
                    silStr.remove();
                    swal("Silindi!", veri.Message, "success");
                }
                else {
                    swal("Silinemedi!", veri.Message, "error");
                }
            }
        });
    } else {
        swal("İptal Edildi!", "Özellik Tip Silinmedi Güvende :)", "info");
    }
});
    });
    $(document).on("click", ".ozellikdegersil", function () {
        var id = $(this).data("id");
        var silStr = $(this).closest("tr");
        swal({
            title: "Emin Misin?",
            text: "Bu Özellik Değeri Bir Daha Geri Getiremeyebilirsiniz!",
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
            url: "/Admin/OzellikDegerSil/" + id,
            success: function (veri) {
                if (veri.IsSuccess) {
                    silStr.remove();
                    swal("Silindi!", veri.Message, "success");
                }
                else {
                    swal("Silinemedi!", veri.Message, "error");
                }
            }
        });
    } else {
        swal("İptal Edildi!", "Özellik Değer Silinmedi Güvende :)", "info");
    }
});
    });
    $(document).on("click", ".tedarikcisil", function () {
        var id = $(this).data("id");
        var silStr = $(this).closest("tr");
        swal({
            title: "Emin Misin?",
            text: "Bu Tedarikçiyi Bir Daha Geri Getiremeyebilirsiniz!",
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
            url: "/Admin/TedarikciSil/" + id,
            success: function (veri) {
                if (veri.IsSuccess) {
                    silStr.remove();
                    swal("Silindi!", veri.Message, "success");
                }
                else {
                    swal("Silinemedi!", veri.Message, "error");
                }
            }
        });
    } else {
        swal("İptal Edildi!", "Tedarikçi Silinmedi Güvende :)", "info");
    }
});
    });
    $(document).on("click", ".urunsil", function () {
        var id = $(this).data("id");
        var silStr = $(this).closest("tr");
        swal({
            title: "Emin Misin?",
            text: "Bu Ürünü Bir Daha Geri Getiremeyebilirsiniz!",
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
            url: "/Admin/UrunSil/" + id,
            success: function (veri) {
                if (veri.IsSuccess) {
                    silStr.remove();
                    swal("Silindi!", veri.Message, "success");
                }
                else {
                    swal("Silinemedi!", veri.Message, "error");
                }
            }
        });
    } else {
        swal("İptal Edildi!", "Ürün Silinmedi Güvende :)", "info");
    }
});
    });
    $(document).on("click", ".kullanicibanlama", function () {
        var id = $(this).attr("data-id");
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
    $('#ustkategoriform').validate({
        rules: {
            "Adi": {
                required: true
            }
        },
        messages: {
            "Adi": {
                required: "Lütfen Üst Kategori Adını Boş Geçmeyiniz."
            }
        }
    });
    $.validator.addMethod("selectkontrol", function (value) {
        return (value != '0');
    }, "Lütfen Varsayılan Değerden Farklı Bir Değer Giriniz.");
    $('#kategoriform').validate({
        rules: {
            "Adi": {
                required: true
            },
            "UstKategoriID": {
                selectkontrol: true
            }
        },
        messages: {
            "Adi": {
                required: "Lütfen Kategori Adını Boş Geçmeyiniz."
            }
        }
    });
    $('#markaform').validate({
        rules: {
            "Adi": {
                required: true
            },
            "MarkaResim": {
                required: true
            }
        },
        messages: {
            "Adi": {
                required: "Lütfen Marka Adını Boş Geçmeyiniz."
            },
            "MarkaResim": {
                required: "Lütfen Resmi Boş Geçmeyiniz."
            }
        }
    });
    $('#ozelliktipform').validate({
        rules: {
            "Adi": {
                required: true
            },
            "KategoriID": {
                selectkontrol: true
            }
        },
        messages: {
            "Adi": {
                required: "Lütfen Özellik Tip Adını Boş Geçmeyiniz."
            }
        }
    });
    $('#ozellikdegerform').validate({
        rules: {
            "Adi": {
                required: true
            },
            "OzellikTipID": {
                selectkontrol: true
            }
        },
        messages: {
            "Adi": {
                required: "Lütfen Özellik Değer Adını Boş Geçmeyiniz."
            }
        }
    });
    $('#tedarikciform').validate({
        rules: {
            "SirketAdi": {
                required: true
            },
            "MüsteriAdi": {
                required: true
            },
            "Adres": {
                required: true
            },
            "Telefon": {
                required: true
            },
            "Faks": {
                required: true
            }
        },
        messages: {
            "SirketAdi": {
                required: "Lütfen Şirket Adını Boş Geçmeyiniz."
            },
            "MüsteriAdi": {
                required: "Lütfen Müşteri Adını Boş Geçmeyiniz."
            },
            "Adres": {
                required: "Lütfen Adresi Boş Geçmeyiniz."
            },
            "Telefon": {
                required: "Lütfen Telefonu Boş Geçmeyiniz."
            },
            "Faks": {
                required: "Lütfen Faksı Boş Geçmeyiniz."
            }
        }
    });
    $('#urunform').validate({
        rules: {
            "Adi": {
                required: true
            },
            "Fiyat": {
                required: true
            },
            "Stok": {
                required: true
            },
            "UrunResim": {
                required: true
            },
            "KategoriID": {
                selectkontrol: true
            },
            "MarkaID": {
                selectkontrol: true
            },
            "TedarikciID": {
                selectkontrol: true
            }
        },
        messages: {
            "Adi": {
                required: "Lütfen Ürün Adını Boş Geçmeyiniz."
            },
            "Fiyat": {
                required: "Lütfen Ürün Fiyatını Boş Geçmeyiniz."
            },
            "Stok": {
                required: "Lütfen Ürün Stok Adetini Boş Geçmeyiniz."
            },
            "UrunResim": {
                required: "Lütfen Ürün Resmini Boş Geçmeyiniz."
            }
        }
    });
    $('#urunduzenleform').validate({
        rules: {
            "Adi": {
                required: true
            },
            "Fiyat": {
                required: true
            },
            "Stok": {
                required: true
            },
            "KategoriID": {
                selectkontrol: true
            },
            "MarkaID": {
                selectkontrol: true
            },
            "TedarikciID": {
                selectkontrol: true
            }
        },
        messages: {
            "Adi": {
                required: "Lütfen Ürün Adını Boş Geçmeyiniz."
            },
            "Fiyat": {
                required: "Lütfen Ürün Fiyatını Boş Geçmeyiniz."
            },
            "Stok": {
                required: "Lütfen Ürün Stok Adetini Boş Geçmeyiniz."
            }
        }
    });
    $('#urunresimform').validate({
        rules: {
            "UrunResim": {
                required: true
            }
        },
        messages: {
            "UrunResim": {
                required: "Lütfen Ürün Resmini Boş Geçmeyiniz."
            }
        }
    });
    $('#rolekleform').validate({
        rules: {
            "RolAdi": {
                required: true
            }
        },
        messages: {
            "RolAdi": {
                required: "Rol Adını Boş Geçmeyiniz."
            }
        }
    });
    $('#rolataform').validate({
        rules: {
            "RolAdi": {
                selectkontrol: true
            },
            "KullaniciAdi": {
                selectkontrol: true
            }
        },
        messages: {

        }
    });
});