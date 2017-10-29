function KategoriEkle() {
    Kategori = new Object();
    Kategori.KategoriAdi = $('#kategoriAdi').val();
    Kategori.Url = $('#kategoriUrl').val();
    Kategori.AktifMi = $('#kategoriAktif').is(':checked');
    Kategori.ParentID = $('#ParentID').val();

    $.ajax({
        url: "/Kategori/Ekle",
        data: Kategori,
        type: "POST",
        dataType: 'json',
        success: function (response) {
            if (response.Success) {
                bootbox.alert(response.Message, function () {
                    location.reload();
                });
            }
            else {
                bootbox.alert(response.Message, function () {
                    //geri döndüğünde bir şey yapılması isteniyorsa buraya yazılır.
                })
            }
        }
    })
}
$(document).on("click", "#KategoriDelete", function () {
    var gelenid = $(this).attr('data-id');
    var silTR = $(this).closest("tr");//closest yakın olanı seçiyor.
    $.ajax({
        url: "/Kategori/Sil/" + gelenid,
        type: "POST",
        dataType: "json",
        success: function (response) {
            if (response.Success) {
                $.notify(response.Message, "success");
                silTR.fadeOut(300, function () {
                    silTR.remove();
                })
            }
            else {
                $.notify(response.Message, "danger");
            }
        }
    })
});

function KategoriDuzenle() {
    var gelenID = $("#ID").val();
    Kategori = new Object();
    Kategori.KategoriAdi = $('#kategoriAdi').val();
    Kategori.Url = $('#kategoriUrl').val();
    Kategori.AktifMi = $('#kategoriAktif').is(':checked');
    Kategori.ParentID = $('#ParentID').val();
    Kategori.ID = gelenID;
    $.ajax({
        url: "/Kategori/Duzenle",
        data: Kategori,
        type: "POST",
        dataType: 'json',
        success: function (response) {
            if (response.Success) {
                bootbox.alert(response.Message, function () {
                    location.reload();
                });
            }
            else {
                bootbox.alert(response.Message, function () {
                    //geri döndüğünde bir şey yapılması isteniyorsa buraya yazılır.
                })
            }
        }
    })
}