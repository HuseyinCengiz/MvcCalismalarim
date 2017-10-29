$(document).ready(function () {
    $(document).on("click", ".sepeteekle", function () {
        var urunid = $(this).data("id");
        var adet = $("#quantity_wanted").val();
        if (isNaN(adet) && typeof adet == "undefined") {
            adet = 1;
        }
        $.ajax({
            url: "/Home/SepeteAt/",
            type: "Post",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ id: urunid, Adet: adet }),
            success: function () {
                $.ajax({
                    type: "GET",
                    url: "/Home/MiniSepet/",
                    success: function (gelenveri) {
                        $("#sepet").html(gelenveri);
                    }
                });
            },
            error: function () {
                alert("Ürün Sepete Eklenemedi");
            }
        });
    });
    $(document).on("click", ".sepettencikar", function () {
        var id = $(this).data("id");
        $.ajax({
            type: "GET",
            url: "/Home/SepettenCikar/" + id,
            success: function () {
                $.ajax({
                    type: "GET",
                    url: "/Home/MiniSepet/",
                    success: function (gelenveri) {
                        $("#sepet").html(gelenveri);
                    }
                });
            },
            error: function () {
                alert("Ürün Sepete Eklenemedi");
            }
        });
    });
    $(document).on("click", "#kayitol", function () {
        var kullaniciadi = $("#KullaniciAdi").val();
        var sifre = $("#Sifre").val();
        var email = $("#Email").val();
        var gizlisoru = $("#GizliSoru").val();
        var gizlicevap = $("#GizliCevap").val();
        $.ajax({
            url: "/Kullanici/KayitOl/",
            type: "Post",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ KullaniciAdi: kullaniciadi, Sifre: sifre, Email: email, GizliSoru: gizlisoru, GizliCevap: gizlicevap }),
            success: function (veri) {
                if (veri.IsSuccess) {
                    swal("Harika!", veri.Message, "success");
                    window.location = "/Home/GirisYap";
                }
                else {
                    swal("Malesef!", veri.Message, "error");
                }
            }
        });
    });
    $(document).on("click", ".rolsil", function () {
        var id = $(this).attr("data-id");
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
    $(document).on("change", "#selectProductSort", function () {
        var Id = $("#selectProductSort").data("katid");
        var siralamasekli = $(this).find("option:selected").val();
        $.ajax({
            type: "Get",
            url: "/Home/KategoriyeGoreUrunler/",
            data: { id: Id, SiralamaSekli: siralamasekli },
            success: function (gelenveri) {
                $("#ajax_urun").html(gelenveri);
            },
            error: function () {
                alert("Sıralanamadı");
            }
        });
    });
    $(document).on("click", ".Renk", function () {
        var renk = $(this).find("a").text();
        $("#renk").html('<a title="Cancel" data-rel="layered_id_attribute_group_1" href="#"><i class="icon icon-remove"></i></a>Renk: <span>' + renk + '</span>');
        $("#renk").css("display", "block");
    });
    $(document).on("click", ".Beden", function () {
        var beden = $(this).find("a").text();
        $("#beden").html('<a title="Cancel" data-rel="layered_id_attribute_group_1" href="#"><i class="icon icon-remove"></i></a>Beden: <span>' + beden + '</span>');
        $("#beden").css("display", "block");
    });
    $(document).on("click", ".filtrele", function () {
        var Id = $("#selectProductSort").data("katid");
        var siralamasekli = $("#selectProductSort").find("option:selected").val();
        var beden = $("#beden").find("span").text();
        var renk = $("#renk").find("span").text();
        var kucukfiyat = sliderRange.slider("values", 0);
        var buyukfiyat = sliderRange.slider("values", 1);
        $.ajax({
            type: "Get",
            url: "/Home/KategoriyeGoreUrunler/",
            data: { id: Id, SiralamaSekli: siralamasekli, Renk: renk, Beden: beden, KFiyat: kucukfiyat, BFiyat: buyukfiyat },
            success: function (gelenveri) {
                $("#ajax_urun").html(gelenveri);
            },
            error: function () {
                alert("Filtrelenirken Hata Oluştu ya da Urun Bulunamadı Tekrar Deneyiniz");
            }
        });
    });
    $(document).on("click", ".icon-remove", function () {
        var li = $(this).closest("li").empty();
        li.css("display", "none");
    });
    $(document).on("click", "#satis_tamamla", function () {
        var kargoID = $("#id_country").find("option:selected").val();
        var Siparis;
        var Siparisler = new Array();
        if (kargoID != "0") {  
            var toplamTutar = $("#total_price").data("toplamfiyat");
            $('.cart_item').each(function () {
                Siparis = new Object();
                Siparis.UrunID = $(this).data("urunid");
                Siparis.Adet = $(this).find("input[name=quantity]").val();
                Siparis.Fiyat = $(this).find(".fiyatim").data("fiyat");
                Siparisler.push(Siparis);
            });
            debugger;
            $.ajax({
                type: "post",
                url: "/Home/SatisTamamla",
                contentType: "application/json; charset=utf-8",
                traditional: true,
                data: JSON.stringify({ siparis: Siparisler, ToplamTutar: toplamTutar, KargoID: kargoID }),
                success: function (gelenveri) {
                    if (gelenveri.IsSuccess)
                    {
                        alert(gelenveri.Message);
                        window.location = "/Home/Index";
                    }
                    else
                    {
                        alert(gelenveri.Message);
                    }
                    
                },
                error:function()
                {
                    alert("Beklenilmeyen Hatayla Karşılaşıldı");
                }
            });

        }
        else {
            alert("Kargo Seçiniz");
        }
        debugger;
    });
});