$(document).ready(function () {
    //sayfa yüklendiðinde ilçe dropdown ý devre dýþý býrak
    $("#select_district").prop("disabled", true);
    $("#select_clinic").prop("disabled", true);
    $("#select_doctor").prop("disabled", true);

    //þehir dropdown ýnýn deðiþiklik olayýný ele al
    $("#select_province").change(function () {
        var selectedProvinceId = $(this).val();

        // Ýl seçilmiþse ilçe dropdown'ýný aktifleþtir
        if (selectedProvinceId !== "NOSELECTED") {
            $("#select_district").prop("disabled", false);
        } else {
            // Ýl seçilmemiþse ilçe dropdown'ýný devre dýþý býrak
            $("#select_district").prop("disabled", true);
        }

        // Seçilen ile göre ilçeleri getir
        $.get("/Apointment/GetDistricts", { id_province: selectedProvinceId }, function (data) {
            // Mevcut ilçe seçeneklerini temizle
            $("#select_district").empty();

            // Yeni ilçe seçeneklerini ekle
            $("#select_district").append('<option value="NOSELECTED">---Ýlçe Seçiniz---</option>');
            $.each(data, function (index, item) {
                $("#select_district").append('<option value="' + item.value + '">' + item.text + '</option>');
            });
        });
    });

    //ilçe seçilmeden majordepartment seçemez
    $("#select_district").change(function () {
        var selectedDistrictId = $(this).val();

        // Ýlçe seçilmiþse klinik dropdown'ýný aktifleþtir
        if (selectedDistrictId !== "NOSELECTED") {
            $("#select_major").prop("disabled", false);
        } else {
            // Ýl seçilmemiþse ilçe dropdown'ýný devre dýþý býrak
            $("#select_major").prop("disabled", true);
        }
    });

    //ilçe seçilmeden hastane dropdown'u aktif olmasýn
    $("#select_hospital").prop("disabled", true);

    $("#select_major").change(function () {
        var selectedDistrictId = $("#select_district").val();
        var selectedMajorId = $("#select_major").val();


        // ilçe ve klinik seçilmiþse hastane dropdown'ýný aktifleþtir
        if (selectedDistrictId !== "NOSELECTED" && selectedMajorId !== "NOSELECTED") {

            $("#select_hospital").prop("disabled", false);
        } else {
            // ilçe veya klinik seçilmemiþse hastane dropdown'ýný devre dýþý býrak
            $("#select_hospital").prop("disabled", true);
        }

        // Seçilen ilçe ve kliniklere göre hastaneleri getiren AJAX isteði yap
        $.get("/Apointment/GetHospitalsForMajorDepart", { id_district: selectedDistrictId, id_majorDepart: selectedMajorId }, function (data) {
            // Mevcut hastane seçeneklerini temizle
            $("#select_hospital").empty();

            // Yeni hastane seçeneklerini ekle
            $("#select_hospital").append('<option value="NOSELECTED">---Hastane Seçiniz---</option>');
            $.each(data, function (index, item) {
                $("#select_hospital").append('<option value="' + item.value + '">' + item.text + '</option>');
            });
        });
    });


    // Hastane veya major departman seçildiðinde poliklinikleri güncelle
    $("#select_hospital").change(function () {
        var selectedHospitalId = $("#select_hospital").val();
        var selectedMajorDepartId = $("#select_major").val();

        // Hastane ve major departman seçildiyse
        if (selectedHospitalId !== "NOSELECTED" && selectedMajorDepartId !== "NOSELECTED") {
            // Poliklinik dropdown'ýný aktifleþtir
            $("#select_clinic").prop("disabled", false);

        } else {
            // Hastane veya major departman seçilmemiþse poliklinik dropdown'ýný devre dýþý býrak
            $("#select_clinic").prop("disabled", true);
        }

        // Seçilen hastane ve major departmana göre poliklinikleri getir
        $.get("/Apointment/GetPoliclinicsForHospitalAndMajorDepart", { id_hospital: selectedHospitalId, id_majorDepart: selectedMajorDepartId }, function (data) {
            // Mevcut poliklinik seçeneklerini temizle
            $("#select_clinic").empty();

            // Yeni poliklinik seçeneklerini ekle
            $("#select_clinic").append('<option value="NOSELECTED">---Poliklinik Seçiniz---</option>');
            $.each(data, function (index, item) {
                $("#select_clinic").append('<option value="' + item.value + '">' + item.text + '</option>');
            });
        });
    });


    $("#select_clinic").change(function () {
        var selectedDoctorId = $(this).val();

        // Ýl seçilmiþse ilçe dropdown'ýný aktifleþtir
        if (selectedDoctorId !== "NOSELECTED") {
            $("#select_doctor").prop("disabled", false);
        } else {
            // Ýl seçilmemiþse ilçe dropdown'ýný devre dýþý býrak
            $("#select_doctor").prop("disabled", true);
        }

        // Seçilen ile göre ilçeleri getir
        $.get("/Apointment/GetDoctors", { id_clinic: selectedDoctorId }, function (data) {
            // Mevcut ilçe seçeneklerini temizle
            $("#select_doctor").empty();

            // Yeni ilçe seçeneklerini ekle
            $("#select_doctor").append('<option value="NOSELECTED">---Doktor Seçiniz---</option>');
            $.each(data, function (index, item) {
                $("#select_doctor").append('<option value="' + item.value + '">' + item.text + '</option>');
            });
        });
    });


    //reset butonu
    $("#resetButton").click(function () {
        // Form içindeki tüm input, select ve textarea'larý temizle
        $("form")[0].reset();

        //dropdown'larý devre dýþý býrak
        $("#select_district").prop("disabled", true);
        $("#select_clinic").prop("disabled", true);
        $("#select_major").prop("disabled", true);
        $("#select_hospital").prop("disabled", true);
        $("#select_doctor").prop("disabled", true);

    });
});